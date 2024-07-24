/*
 * Geiger counter Kit could get on:  https://www.aliexpress.com            search: geiger counter kit
* --------------------------------------------------------------------------------------
* WHAT IS CPM?
* CPM (or counts per minute) is events quantity from Geiger Tube you get during one minute. Usually it used to 
* calculate a radiation level. Different GM Tubes has different quantity of CPM for background. Some tubes can produce
* about 10-50 CPM for normal background, other GM Tube models produce 50-100 CPM or 0-5 CPM for same radiation level.
* Please refer your GM Tube datasheet for more information. Just for reference here, J305 and SBM-20 can generate 
* about 10-50 CPM for normal background. 
* --------------------------------------------------------------------------------------
* HOW TO CONNECT GEIGER KIT?
* The kit 3 wires that should be connected to Arduino UNO board: 5V, GND and INT. PullUp resistor is included on
* kit PCB. Connect INT wire to Digital Pin#2 (INT0), 5V to 5V, GND to GND. Then connect the Arduino with
* USB cable to the computer and upload this sketch. 
* 
 * Author:JiangJie Zhang * If you have any questions, please connect cajoetech@qq.com
 * 
 * License: MIT License
 * 
 * Please use freely with attribution. Thank you!
*/

/// Program modified to suit Iordache Pavel Sebastian's purposes for a simple serial-readable radiation sensor.




#include <SPI.h>
#define LOG_PERIOD 15000  //Logging period in milliseconds, recommended value 15000-60000.
#define MAX_PERIOD 60000  //Maximum logging period without modifying this sketch

unsigned long counts;     //variable for GM Tube events
unsigned long cpm;        //variable for CPM
unsigned int multiplier;  //variable for calculation CPM in this sketch
unsigned long previousMillis;  //variable for time measurement
int SensorID;


bool cmpNstr(String a, String b,int n){
  for(int i=0;i<n;i++){
    if(a[i]!=b[i])
    return 0;
  }
  return 1;
}

bool IsSensorInit(){
  bool SensorInitOK =false;
  delay(250);
  digitalWrite(7, LOW);
  if(Serial.available()>0){
    String MSG=Serial.readString();
    if(cmpNstr(MSG, "INIT"+String(SensorID) ,5)){
    Serial.println("Radition Sensor");
    SensorInitOK=true;
    }
  }
  delay(250);
  digitalWrite(7, HIGH);
  return SensorInitOK;
}

bool IsGeigerPluggedIn(){
  bool IsPluggedIn = false;
  delay(250);
  digitalWrite(6, LOW);
  if(digitalRead(9)){
    IsPluggedIn=true;
    counts=0;
  }
  delay(250);
  digitalWrite(6, HIGH);
  return IsPluggedIn;
}

void tube_impulse(){       //subprocedure for capturing events from Geiger Kit
  counts++;
}

void setup(){             //setup subprocedure
  counts = 0;
  cpm = 0;
  multiplier = MAX_PERIOD / LOG_PERIOD;      //calculating multiplier, depend on your log period
  Serial.begin(9600);
  attachInterrupt(0, tube_impulse, FALLING); //define external interrupts 
  pinMode(8, OUTPUT);
  pinMode(9, INPUT);
  pinMode(7, OUTPUT);
  pinMode(6, OUTPUT);
  SensorID = 0;
  while (!IsGeigerPluggedIn());
  digitalWrite(8, HIGH); //State pin, set high if ready to function
  while (!IsSensorInit());
}

void loop(){                                 //main cycle
  unsigned long currentMillis = millis();
  if(currentMillis - previousMillis > LOG_PERIOD){
    previousMillis = currentMillis;
    cpm = counts * multiplier;
    counts = 0;
  }
  if(Serial.available()>0){
      digitalWrite(6, LOW);
    String MSG=Serial.readString();
    if(cmpNstr(MSG, "MSR"+String(SensorID) ,4)){   
    Serial.print(cpm);
    Serial.print(" CPM, ");
    Serial.print((float) cpm/151);
    Serial.println(" uSv/h");    
    }
    if(cmpNstr(MSG, "KAF"+String(SensorID) ,4)){
      PrintOutKafkaByByte();
    }
    digitalWrite(6,HIGH);
  }
  
}
