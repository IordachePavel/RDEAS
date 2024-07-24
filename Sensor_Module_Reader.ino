/// Sensor module MCU



//Uart modular sensor settings + variables
#define ModuleReadyDetect 2
#define ModuleDetect 3
#define ConnectionAttempts 20
#define ConnectionTimeout 2000 //ms
String SensorType = "";
bool SensorConnected = false;
bool LastSensorState = false;

    
bool InitSensor(int ConAttmpt){
  SensorType="";
  for(int i=0; i<ConAttmpt&&SensorType=="";i++){
  delay(200);
  Serial1.println("INIT0");
  delay(1000);
  if(Serial1.available()>0){
    SensorType=Serial1.readString();
    if(!IsValueNotJunk(SensorType))
    SensorType="";
  }
  if(SensorType!=""){
      return true;
  }
  }
  return false;
}

bool IsValueNotJunk(String MSG){
  for(int i=0;MSG[i];i++){
    if(!isAscii(MSG[i]))
    return false;
  }
  return true;
}

bool RequestMeasurements(String &Measure){
  Measure = "";
  for(int i=0;i<ConnectionAttempts&&Measure=="";i++){
  delay(200);
  Serial1.println("MSR0");
  //Serial.println("Attempt: "+String(i));
  int TimeoutCounter=0;
  while(!Serial1.available()>0&&i<ConnectionTimeout){
   delay(1);
   TimeoutCounter++;
  }
    Measure = "";
    Measure=Serial1.readString();
    //Serial.println(Measure);
    if(IsValueNotJunk(Measure)){
    return 1;
    }
    else{
    Measure="";
    } 
  }
  return 0;
}

void setup() {

  Serial.begin(9600); //USB
  Serial1.begin(9600); //UART

  pinMode(ModuleReadyDetect,INPUT_PULLDOWN);
  pinMode(ModuleDetect,INPUT_PULLDOWN);
}

void loop() {
  
  SensorConnected = digitalRead(ModuleReadyDetect);
  if(SensorConnected!=LastSensorState){
    LastSensorState=SensorConnected;
    if(SensorConnected){
      while(!digitalRead(ModuleDetect));
      if(InitSensor(ConnectionAttempts)){
        Serial.println("Sensor installed: " + SensorType);
      }
      else{
        Serial.println("Failed to initialize the sensor.");
      }
    }
    else{
      SensorType="";
      Serial.println("Sensor disconnected");
    }
  }
  if(Serial.available()>0){
    String Message = Serial.readString();
    if(Message=="MSR"){
      if(SensorType!=""){
      String Measurements = "";
      RequestMeasurements(Measurements);
      if(Measurements!="")
      Serial.println(Measurements);
      else{
       Serial.println("Error while retrieving measurements");
      }
      }
      else{
        Serial.println("No sensor installed!");
      }      
    }
    if(Message == "LST"){
      if(SensorType==""){
        Serial.println("No sensor attached.");
      }
      else{
        Serial.println("Sensors installed: "+SensorType);
      }
    }
    
  }
}
