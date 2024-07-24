/**
 * DHT11 Sensor Reader
 * This sketch reads temperature and humidity data from the DHT11 sensor and prints the values to the serial port.
 * It also handles potential error states that might occur during reading.
 *
 * Author: Dhruba Saha
 * Version: 2.1.0
 * License: MIT
 */
 /*
  MQUnifiedsensor Library - reading an MQ4

  Demonstrates the use a MQ4 sensor.
  Library originally added 01 may 2019
  by Miguel A Califa, Yersson Carrillo, Ghiordy Contreras, Mario Rodriguez
 
  Added example
  modified 23 May 2019
  by Miguel Califa 

  Updated library usage
  modified 26 March 2020
  by Miguel Califa 

  Wiring:
  https://github.com/miguel5612/MQSensorsLib_Docs/blob/master/static/img/MQ_Arduino.PNG
  Please make sure arduino A0 pin represents the analog input configured on #define pin

 This example code is in the public domain.

*/

bool cmpNstr(String a, String b,int n){
  for(int i=0;i<n;i++){
    if(a[i]!=b[i])
    return 0;
  }
  return 1;
}

int SensorID;

bool IsSensorInit(){
  bool SensorInitOK =false;
  delay(250);
  digitalWrite(7, LOW);
  if(Serial.available()>0){
    String MSG=Serial.readString();
    if(cmpNstr(MSG, "INIT"+String(SensorID) ,5)){
    Serial.println("Temperature, humidity, gas");
    SensorInitOK=true;
    }
  }
  delay(250);
  digitalWrite(7, HIGH);
  return SensorInitOK;
}

//Include the library
#include <MQUnifiedsensor.h>
/************************Hardware Related Macros************************************/
#define         Board                   ("Arduino UNO")
#define         Pin                     (A3)  //Analog input 4 of your arduino
/***********************Software Related Macros************************************/
#define         Type                    ("MQ-4") //MQ4
#define         Voltage_Resolution      (5)
#define         ADC_Bit_Resolution      (10) // For arduino UNO/MEGA/NANO
#define         RatioMQ4CleanAir        (4.4) //RS / R0 = 60 ppm 
/*****************************Globals***********************************************/
//Declare Sensor
MQUnifiedsensor MQ4(Board, Voltage_Resolution, ADC_Bit_Resolution, Pin, Type);



// Include the DHT11 library for interfacing with the sensor.
#include <DHT11.h>

// Create an instance of the DHT11 class.
// - For Arduino: Connect the sensor to Digital I/O Pin 2.
// - For ESP32: Connect the sensor to pin GPIO2 or P2.
// - For ESP8266: Connect the sensor to GPIO2 or D4.
DHT11 dht11(2);

void setup() {
  pinMode(8, OUTPUT); //Set high if ready to use
  pinMode(7, OUTPUT); //Status LED1
  pinMode(6, OUTPUT); //Status LED2
  SensorID = 0;

  //Init the serial port communication - to debug the library
  Serial.begin(9600); //Init serial port

  //Set math model to calculate the PPM concentration and the value of constants
  MQ4.setRegressionMethod(0); //_PPM =  pow(10, (log10(ratio)-b)/a)

  /*****************************  MQ Init ********************************************/ 
  //Remarks: Configure the pin of arduino as input.
  /************************************************************************************/ 
  MQ4.init(); 
  /* 
    //If the RL value is different from 10K please assign your RL value with the following method:
    MQ4.setRL(10);
  */
  /*****************************  MQ CAlibration ********************************************/ 
  // Explanation: 
   // In this routine the sensor will measure the resistance of the sensor supposedly before being pre-heated
  // and on clean air (Calibration conditions), setting up R0 value.
  // We recomend executing this routine only on setup in laboratory conditions.
  // This routine does not need to be executed on each restart, you can load your R0 value from eeprom.
  // Acknowledgements: https://jayconsystems.com/blog/understanding-a-gas-sensor
  
  float calcR0 = 0;
  for(int i = 1; i<=10; i ++)
  {
    digitalWrite(6, LOW);
    delay(250);
    MQ4.update(); // Update data, the arduino will read the voltage from the analog pin
    calcR0 += MQ4.calibrate(RatioMQ4CleanAir);
    digitalWrite(6,HIGH);
    delay(250);
  }
  MQ4.setR0(calcR0/10);
  
  if(isinf(calcR0)) {; //Open circuit. Sensor is burnt.
  while(1){
    digitalWrite(6, LOW);
    digitalWrite(7, HIGH);
    delay(250);
    digitalWrite(6,HIGH);
    digitalWrite(7,LOW);
    delay(250);
  } 
  }
  if(calcR0 == 0){;  //Short circuit. Sensor shorted to ground.
  while(1){
    digitalWrite(6, LOW);
    digitalWrite(7, HIGH);
    delay(250);
    digitalWrite(6,HIGH);
    digitalWrite(7,LOW);
    delay(250);
  } 
  }
  /*****************************  MQ CAlibration ********************************************/ 

  
    // Initialize serial communication to allow debugging and data readout.
    // Using a baud rate of 9600 bps.
    Serial.begin(9600);
    
    // Uncomment the line below to set a custom delay between sensor readings (in milliseconds).
    // dht11.setDelay(500); // Set this to the desired delay. Default is 500ms.
    digitalWrite(8,HIGH);
  while (!IsSensorInit()) ;


}

void loop() {
  if(Serial.available()){
    digitalWrite(6, LOW);
    String MSG = Serial.readString();
    if(cmpNstr(MSG, "MSR"+String(SensorID) ,4)){
    int temperature = 0;
    int humidity = 0;


    MQ4.update(); // Update data, the arduino will read the voltage from the analog pin
  
  //https://jayconsystems.com/blog/understanding-a-gas-sensor 
  MQ4.setA(-0.318); MQ4.setB(1.133); // A -> Slope, B -> Intersect with X - Axis
  float LPG = MQ4.readSensor(); // Sensor will read PPM concentration using the model, a and b values set previously or from the setup



    // Attempt to read the temperature and humidity values from the DHT11 sensor.
    int result = dht11.readTemperatureHumidity(temperature, humidity);

    // Check the results of the readings.
    // If the reading is successful, print the temperature and humidity values.
    // If there are errors, print the appropriate error messages.
    if (result == 0) {
        Serial.print("Temperature: ");
        Serial.print(temperature);
        Serial.print(" C Humidity: ");
        Serial.print(humidity);
        Serial.print(" % ");
    } else {
        // Print error message based on the error code.
        Serial.print(DHT11::getErrorString(result));
    }
    Serial.print(",Explosive Gas Mixture(ppm): ");
    Serial.println(LPG);
   }
   digitalWrite(6, HIGH);
  }
}
