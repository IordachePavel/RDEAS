#include "MAX30100_PulseOximeter.h"
#define REPORTING_PERIOD_MS     1000
PulseOximeter pox;
uint32_t tsLastReport = 0;
//Check If The Sensor Is Connected
bool OxInstalled=0;
//Sensor Measurements


///Display
#include <Nokia_LCD.h>
Nokia_LCD lcd(6 /* CLK */, 5 /* DIN */, 4 /* DC */, 3 /* CE */, 9 /* RST */);
// God bless my past self for naming the wires in the code I needed to replace the MCU lmaon

///Atmospherics Sensor Library And Global Variables
#include <Wire.h>
#include "forcedBMX280.h"
ForcedBMP280 climateSensor = ForcedBMP280();
 int32_t temp_val;  // temperature
 uint32_t pres_val;    // pressure

///Information Display Page Manager Variable
unsigned short int val=3;
bool reInit=0; /// If user asks to reinitiate a specific page sensor

/// Serial Monitor Debug Variables
bool inUse = false;

///Button Interface Used Pins 
const int ButtonA=10;
const int ButtonB=11;
const int ButtonC=12;
const int BCKLGHT=7;
unsigned short int buttonTimer=100;
bool backlight=true;

///Message Interpreter
String MSG;

void initOxSens(){
  if (!pox.begin()) {
        OxInstalled=0;
    } else {
        OxInstalled=1;
    }
         pox.setIRLedCurrent(MAX30100_LED_CURR_4_4MA);
}

bool cmpNstr(String a, String b,int n){
  for(int i=0;i<n;i++){
    if(a[i]!=b[i])
    return 0;
  }
  return 1;
}
/// Pulse-Oxymeter and Atmospherics Sensor Display Page
void envNuser(){
  ///ask on page to reinit pulse oxymeter via global variable
  if(reInit=1){
    initOxSens();
    reInit=0;
  }
        lcd.setCursor(0,0);
        lcd.setInverted(true);
        lcd.println("Env:");
        lcd.setInverted(false);
        climateSensor.takeForcedMeasurement();
        temp_val=climateSensor.getTemperatureCelsius();
        pres_val=climateSensor.getPressure();
        lcd.print(temp_val/100);
        lcd.print(".");
        lcd.print(temp_val%100);
        lcd.println(" degC");
        lcd.print(pres_val/100);
        lcd.print(".");
        lcd.print(pres_val%100);
        lcd.println(" hPa");
        lcd.setInverted(true);
        lcd.println("Usr:");
        lcd.setInverted(false);
    if(OxInstalled){
      float HeartRate;
      uint8_t Oxy;

      pox.update();
      if (millis() - tsLastReport > REPORTING_PERIOD_MS) {
      HeartRate=pox.getHeartRate();
      Oxy=pox.getSpO2();
        tsLastReport = millis();
    }
      if(HeartRate==0){
      lcd.setInverted(true);
      lcd.println("Sensor is not");
      lcd.println("on finger!");
      lcd.setInverted(false);
      }
      else{
        lcd.print(HeartRate);
        lcd.println(" Bpm   ");
        lcd.print(Oxy);
        lcd.println(" SpO2%   ");
      }
    }
    else{
      lcd.setInverted(true);
      lcd.println("No sensor");
      lcd.println("installed!");
      lcd.setInverted(false);
    }
}

///Display on the screen incoming serial data
void SerialMonitor(){
  lcd.setCursor(0,0);
  String recievedString;
  if(Serial.available() > 0){
  recievedString=Serial.readString();
  inUse=1;
  lcd.clear();
  }
  if(inUse==0){
  lcd.setInverted(true);
  lcd.println("Serial Monitor");
  lcd.setInverted(false);
  lcd.println("Debugging Tool");
  lcd.println("==============");
  lcd.println("waiting...");
  lcd.setInverted(true);
  lcd.println("Press any key");
  lcd.println("to leave mode!");
  lcd.setInverted(false);
  }
  else{
    lcd.setCursor(0,0);
    for(int i=0;recievedString[i];i++)
    lcd.print(recievedString[i]);
  }
}

/// 1=test,2=done,3=request atm measurements;
void MessageInterpreter(char comm){
  int valMem=val;
  int opVal=comm-48;
  switch(opVal){
    case 1:
    Serial.println("RX OK!");
    break;
    case 3:
    val=1;
    /// Encode 2 values. VAL1,VAL2,LENGTH1,LENGTH2. Should be its own subprogram.
    int64_t MeasurementToSend=0;
    int TempLength=0;
    int PressureLength=0;
    int auxTemp=temp_val;
    int auxPres=pres_val;
    while(auxTemp){
      auxTemp=auxTemp/10;
      TempLength++;
    }
    while(auxPres){
      auxPres=auxPres/10;
      PressureLength++;
    }
    Serial.print(pres_val);
    Serial.print(temp_val);
    Serial.print(TempLength);
    Serial.println(PressureLength);
    break;
    case 2:
    val=3;
    break;
  }
  /// Refresh if value changed from last time. Helps reduce time by not clearing the whole display, as the already existing memory in the display is getting overwritten anyway.
  if(val!=valMem){
    lcd.clear();
  }
}

///Page Logic;
void pageManager(bool a,bool b, bool c){
  /// Many combinations. Just three buttons. Can also alter global variables.
  unsigned short int memVal=val;
    if(a+b+c){
  val= 1*a+2*b+4*c;
  lcd.clear();
  inUse=0;
    }
  if(val==7){
    val=memVal;
    reInit=1;
  }
  if(val==5){
    val=memVal;
    if(backlight){
      backlight=!backlight;
      digitalWrite(BCKLGHT,LOW);
    }
    else{
      backlight=!backlight;
      digitalWrite(BCKLGHT,HIGH);
    }
  }
  switch (val){
    case 1:
    envNuser();
    break;
    case 2:
    SerialMonitor();
    break;
    case 4:
    /// find something else to display here, graphs?
    break;
    case 3:
    /// Make a home page and return it here
    lcd.setCursor(0,0);
    lcd.println("Very manly welcome message placeholder here")
    break;
    
    

  }
  delay(100);
}

void setup(){
  pinMode(ButtonA,INPUT_PULLUP);
  pinMode(ButtonB,INPUT_PULLUP);
  pinMode(ButtonC,INPUT_PULLUP);
  pinMode(BCKLGHT, OUTPUT);
  Serial.begin(9600);
  Serial.setTimeout(10);
  lcd.begin();
  lcd.setContrast(50);
  lcd.clear(true);
  lcd.clear();
  lcd.setCursor(0, 0);
  //Start Sensors
  climateSensor.begin();
  initOxSens();

  
}

void loop() {
  ///button logic with adjustable time allocated to input
  if(inUse!=1&&val!=2){
   if(Serial.available() > 0){
  MSG=Serial.readString();
  if (cmpNstr(MSG,"MCU",3))
  MessageInterpreter(MSG[4]);
  else{
    Serial.print(MSG);
  }
  }   
  }
  int btA=0,btB=0,btC=0;
  for(int i=0;i<buttonTimer;i++){
    btA=!digitalRead(ButtonA);
    btB=!digitalRead(ButtonB);
    btC=!digitalRead(ButtonC);
  }
  /// Page Manager Logic
  pageManager(btA,btB,btC);

}
