// THIS IS NAVIGATION CIRCUIT CONTROLLER PROGRAM.

//Needed for I2C THERMOPILE SENSOR
#include <Wire.h>

//SENSOR DEFINITIONS
const int trigPin = 16;  //Ultrasonic sensor
const int echoPin = 10; 


#define D6T_ADDR 0x0A  // for I2C 7bit address
#define D6T_CMD 0x4C 

#define PARA_0100MS_1	((uint8_t)0x90)
#define PARA_0100MS_2	((uint8_t)0xDA)
#define PARA_0100MS_3	((uint8_t)0x16)

#define N_ROW 1
#define N_PIXEL 1
#define N_READ ((N_PIXEL + 1) * 2 + 1)

uint8_t rbuf[N_READ];
int16_t pix_data = 0;
int16_t seqData[40] = {0};

float duration, distance; 
uint16_t  totalCount = 0;

//TERMOPILE SENSOR SETTINGS (1)
#define comparingNumInc 16 // x samplingTime ms   (range: 1 to 39)  (example) 16 x 100 ms -> 1.6 sec
#define comparingNumDec 16  // x samplingTime ms  (range: 1 to 39)  (example) 16 x 100 ms -> 1.6 sec
#define threshHoldInc 10 //  /10 degC   (example) 10 -> 1.0 degC (temperature change > 1.0 degC -> Enable)  
#define threshHoldDec 10 //  /10 degC   (example) 10 -> 1.0 degC (temperature change > 1.0 degC -> Disable)

//TERMOPILE SENSOR SETTINGS (2)
#define samplingTime 100

// CONTROL MESSAGE VARIABLE
String MSG;

// SCANNING HEAD CONTROL PINS
const short unsigned int SW_Rot_Lim_1=8;//Rotation Limit Switch Right
const short unsigned int SW_Rot_Lim_2=7;//Rotation Limit Switch Left
const short unsigned int SW_Pit_Lim_1=6;//Pitch Limit Switch Closer
const short unsigned int SW_Pit_Lim_2=9;//Pitch Limit Switch Further
const short unsigned int DriverPitch_Dir=A3;//(Analog pin 3)Pitch motor driver direction
const short unsigned int DriverPitch_Step=A2;//(Analog pin 2)Pitch motor driver step command
const short unsigned int DriverRotation_Dir=4;//Rotation motor driver direction
const short unsigned int DriverRotation_Step=5;//Rotation motor driver step command

const int ButtonCheckCount =100;//How many times to check the switches if they've been triggered during the homing process

// MOVEMENT CONTROL PINS

const short unsigned int T_bridge_Left_Forward = A1;
const short unsigned int T_bridge_Left_Backwards = A0;
const short unsigned int T_bridge_Right_Forward = 15;
const short unsigned int T_bridge_Right_Backwards = 14;   

// MOVEMENT CONTROL VARIABLES
int moveCounter=0;
char dir='F';
// PITCH CONTROL
bool pitchA = false;
bool pitchB = false;
bool yawA = false;
bool yawB = false;


/** <!-- D6T_checkPEC {{{ 1--> D6T PEC(Packet Error Check) calculation.
 * calculate the data sequence,
 * from an I2C Read client address (8bit) to thermal data end.
 */
uint8_t calc_crc(uint8_t data) {
    int index;
    uint8_t temp;
    for (index = 0; index < 8; index++) {
        temp = data;
        data <<= 1;
        if (temp & 0x80) {data ^= 0x07;}
    }
    return data;
}

bool D6T_checkPEC(uint8_t buf[], int n) {
    int i;
    uint8_t crc = calc_crc((D6T_ADDR << 1) | 1);  // I2C Read address (8bit)
    for (i = 0; i < n; i++) {
        crc = calc_crc(buf[i] ^ crc);
    }
    bool ret = crc != buf[n];
    if (ret) {
        Serial.print("PEC check failed:");
        Serial.print(crc, HEX);
        Serial.print("(cal) vs ");
        Serial.print(buf[n], HEX);
        Serial.println("(get)");
    }
    return ret;
}
//PEC END

/** <!-- conv8us_s16_le {{{1 --> convert a 16bit data from the byte stream.
 */
int16_t conv8us_s16_le(uint8_t* buf, int n) {
    int ret;
    ret = buf[n];
    ret += buf[n + 1] << 8;
    return (int16_t)ret;   // and convert negative.
}


// Compare two strings
bool cmpNstr(String a, String b,int n){
  for(int i=0;i<n;i++){
    if(a[i]!=b[i])
    return 0;
  }
  return 1;
}
// Convert message on Serial to task
void MessageInterpreter(char comm){
  int opVal=comm-48;
  if(opVal>0){
    delay(500);
  Serial1.print("RCV");
  Serial.print("Message Awknowledged: ");
  Serial.print(comm);
  }
  switch(opVal){
    case 1:
    Serial1.println("RX OK!");
    break;
    case 2:
    break;
    case 3:
    break;
    case ('F'-48):
    dir='F';
    moveCounter=MSG.substring(5).toInt();
    Serial.println(moveCounter);
    break;
    case ('B'-48):
    dir='B';
    moveCounter=MSG.substring(5).toInt();
    Serial.println(moveCounter);
    break;
    case ('R'-48):
    dir='R';
    moveCounter=MSG.substring(5).toInt();
    Serial.println(moveCounter);
    break;
    case ('L'-48):
    dir='L';
    moveCounter=MSG.substring(5).toInt();
    Serial.println(moveCounter);
    break;
    case ('U'-48):
    digitalWrite(DriverPitch_Dir, 1);
    dir='U'; 
    moveCounter=MSG.substring(5).toInt();
    break;
    case ('D'-48):
    digitalWrite(DriverPitch_Dir, 0);
    dir='D';
    moveCounter=MSG.substring(5).toInt();
    break;
    case ('X'-48):
    digitalWrite(DriverRotation_Dir, 1);
    dir='X';
    moveCounter=MSG.substring(5).toInt();
    break;
    case ('Y'-48):
    digitalWrite(DriverRotation_Dir, 0);
    dir='Y';
    moveCounter=MSG.substring(5).toInt();
    break;
    case ('N'-48):
    moveCounter=1;
    break;
    case ('T'-48):
    TempTarget(0);
    break;
    case('S'-48):
    TempTarget(1);
    break;
    case('P'-48):
    DistTarget();
    break;
  }
  opVal=0;
}

bool HomeYawMotor(){
  yawA = false;
  yawB = false;
  bool SwitchAState = !digitalRead(SW_Rot_Lim_1);
  bool SwitchBState = !digitalRead(SW_Rot_Lim_2);
  if(SwitchAState && SwitchBState){
    return true;
  }
  digitalWrite(DriverRotation_Dir, 1);
  for (int i=0;i<380 && !(yawA && yawB);i++){
    digitalWrite(DriverRotation_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverRotation_Step,LOW);
    delayMicroseconds(2000);
    if(SwitchAState != !digitalRead(SW_Rot_Lim_1)){
      yawA = true;
    }
    if(SwitchBState != !digitalRead(SW_Rot_Lim_2)){
      yawB = true;
    }
  }
  digitalWrite(DriverRotation_Dir, 0);
  for (int i=0;i<380*2 && !(yawA && yawB);i++){
    digitalWrite(DriverRotation_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverRotation_Step,LOW);
    delayMicroseconds(2000);
    if(SwitchAState != !digitalRead(SW_Rot_Lim_1)){
      yawA = true;
    }
    if(SwitchBState != !digitalRead(SW_Rot_Lim_2)){
      yawB = true;
    }
  }
  if(yawA&&yawB){ // Go just three more steps due to how the magnetic field is aligned to the sensor, it triggers earlier
    for(int i=0;i<3;i++){
    digitalWrite(DriverRotation_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverRotation_Step,LOW);
    delayMicroseconds(2000);
    }
  }
    return(yawA&&yawB);
}

bool HomePitchMotor(){
  pitchA = false;
  pitchB = false;
  bool SwitchAState = !digitalRead(SW_Pit_Lim_1);
  bool SwitchBState = !digitalRead(SW_Pit_Lim_2);
  if(SwitchAState && SwitchBState){
    return true;
  }
  digitalWrite(DriverPitch_Dir, 1);
  for (int i=0;i<246 && !(pitchA && pitchB);i++){
    digitalWrite(DriverPitch_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverPitch_Step,LOW);
    delayMicroseconds(2000);
    if(SwitchAState != !digitalRead(SW_Pit_Lim_1)){
      pitchA = true;
    }
    if(SwitchBState != !digitalRead(SW_Pit_Lim_2)){
      pitchB = true;
    }
  }
  digitalWrite(DriverPitch_Dir, 0);
  for (int i=0;i<256*2 && !(pitchA && pitchB);i++){
    digitalWrite(DriverPitch_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverPitch_Step,LOW);
    delayMicroseconds(2000);
    if(SwitchAState != !digitalRead(SW_Pit_Lim_1)){
      pitchA = true;
    }
    if(SwitchBState != !digitalRead(SW_Pit_Lim_2)){
      pitchB = true;
    }
  }
  if(pitchA&&pitchB){ // Go just three more steps due to how the magnetic field is aligned to the sensor, it triggers
    for(int i=0;i<3;i++){
    digitalWrite(DriverPitch_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverPitch_Step,LOW);
    delayMicroseconds(2000);
    }
  }
    return(pitchA&&pitchB);
}

void TempTarget (bool RQST){
    int i, j;

    memset(rbuf, 0, N_READ);
    // Wire buffers are enough to read D6T-16L data (33bytes) with
    // MKR-WiFi1010 and Feather ESP32,
    // these have 256 and 128 buffers in their libraries.
    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(D6T_CMD);               // D6T register
    Wire.endTransmission();            // I2C repeated start for read
    Wire.requestFrom(D6T_ADDR, N_READ);
    i = 0;
    while (Wire.available()) {
        rbuf[i++] = Wire.read();
    }

    if (D6T_checkPEC(rbuf, N_READ - 1)) {
        return;
    }

    // 1st data is PTAT measurement (: Proportional To Absolute Temperature)
    int16_t itemp = conv8us_s16_le(rbuf, 0);
    if(RQST){
    Serial1.print(itemp / 10.0, 1);
    }

    // loop temperature pixels of each thrmopiles measurements
    for (i = 0, j = 2; i < N_PIXEL; i++, j += 2) {
        itemp = conv8us_s16_le(rbuf, j);
        pix_data = itemp;
        if(!RQST){
          Serial1.print("TMP");
          Serial1.print(itemp / 10.0, 1);  // print PTAT & Temperature
            Serial1.print('\n');   // print delimiter
        }
        }
    }

void DistTarget (){
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  duration = pulseIn(echoPin, HIGH);
  distance = (duration*.0343)/2;
  distance = round(distance*100);
  Serial1.print("DST");
  Serial1.print((int)distance);
}

void setup() {
  //Pindef ultrasonics
  pinMode(trigPin, OUTPUT);  
	pinMode(echoPin, INPUT); 
  //Serial1 I/O (UART)
  Serial1.begin(9600);
  //Serial I/O (USB, Debug)
  Serial.begin(9600);

  //Sensor Init Config Block START
  uint8_t para[3] = {0};
    para[0] = PARA_0100MS_1;
		para[1] = PARA_0100MS_2;
		para[2] = PARA_0100MS_3;
 
 
    Serial.begin(115200);  // Serial baudrate = 115200bps
    Wire.begin();  // i2c master

    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(0x02);                  // D6T register
    Wire.write(0x00);                  // D6T register
    Wire.write(0x01);                  // D6T register
    Wire.write(0xEE);                  // D6T register
    Wire.endTransmission();            // I2C repeated start for read
    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(0x05);                  // D6T register
    Wire.write(para[0]);                  // D6T register
    Wire.write(para[1]);                  // D6T register
    Wire.write(para[2]);                  // D6T register
    Wire.endTransmission();            // I2C repeated start for read
    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(0x03);                  // D6T register
    Wire.write(0x00);                  // D6T register
    Wire.write(0x03);                  // D6T register
    Wire.write(0x8B);                  // D6T register
    Wire.endTransmission();            // I2C repeated start for read
    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(0x03);                  // D6T register
    Wire.write(0x00);                  // D6T register
    Wire.write(0x07);                  // D6T register
    Wire.write(0x97);                  // D6T register
    Wire.endTransmission();            // I2C repeated start for read
    Wire.beginTransmission(D6T_ADDR);  // I2C client address
    Wire.write(0x02);                  // D6T register
    Wire.write(0x00);                  // D6T register
    Wire.write(0x00);                  // D6T register
    Wire.write(0xE9);                  // D6T register
    Wire.endTransmission();            // I2C repeated start for read	
  //Sensor Init Config Block END
  

  //Limit switches
  pinMode(SW_Rot_Lim_1,INPUT_PULLUP);
  pinMode(SW_Rot_Lim_2,INPUT_PULLUP);
  pinMode(SW_Pit_Lim_1,INPUT_PULLUP);
  pinMode(SW_Pit_Lim_2,INPUT_PULLUP);
  //Head control
  pinMode(DriverPitch_Dir,OUTPUT);
  digitalWrite(DriverPitch_Dir,LOW);
  pinMode(DriverPitch_Step,OUTPUT);
  digitalWrite(DriverPitch_Step,LOW);
  pinMode(DriverRotation_Dir,OUTPUT);
  digitalWrite(DriverRotation_Dir,LOW);
  pinMode(DriverRotation_Step,OUTPUT);
  digitalWrite(DriverRotation_Step,LOW);
  //Movement control
  pinMode(T_bridge_Left_Backwards, OUTPUT);
  digitalWrite(T_bridge_Left_Backwards, LOW);
  pinMode(T_bridge_Left_Forward,OUTPUT);
  digitalWrite(T_bridge_Left_Forward,LOW);
  pinMode(T_bridge_Right_Backwards,OUTPUT);
  digitalWrite(T_bridge_Right_Backwards,LOW);
  pinMode(T_bridge_Right_Forward,OUTPUT);
  digitalWrite(T_bridge_Right_Forward,LOW);

  //Homing
  if(!HomeYawMotor()){
    Serial.println("Motor failure! Check machine.");
    for(;;);//You don't want the machine to tangle its wires.
  }
  else{
    Serial.println("Yaw motor homing OK!");
  }
  if(!HomePitchMotor()){
    Serial.println("Motor failure! Check machine.");
    for(;;);//Same here.
  }
  else{
    Serial.println("Pitch motor homing OK!");
  }
  delay(100);
  Serial.println("Motor homing success!");
}

void loop() {
  if(Serial1.available() > 0){
  MSG=Serial1.readString();
  if (cmpNstr(MSG,"NAV",3))
  
  MessageInterpreter(MSG[4]);
  }
  //Movement logic (must be in loop so the MCU can switch between driving the motors, running the counter for the motors and using navigation sensors)
  if(moveCounter){
    switch (dir) {
    case 'F':
    digitalWrite(T_bridge_Left_Backwards,LOW);
    digitalWrite(T_bridge_Right_Backwards,LOW);
    digitalWrite(T_bridge_Left_Forward,HIGH);
    digitalWrite(T_bridge_Right_Forward,HIGH);
    break;
    case 'B':
    digitalWrite(T_bridge_Left_Forward,LOW);
    digitalWrite(T_bridge_Right_Forward,LOW);
    digitalWrite(T_bridge_Left_Backwards,HIGH);
    digitalWrite(T_bridge_Right_Backwards,HIGH);
    break;
    case 'R':
    digitalWrite(T_bridge_Left_Forward,HIGH);
    digitalWrite(T_bridge_Right_Forward,LOW);
    digitalWrite(T_bridge_Left_Backwards,LOW);
    digitalWrite(T_bridge_Right_Backwards,HIGH);
    break;
    case 'L':
    digitalWrite(T_bridge_Left_Forward,LOW);
    digitalWrite(T_bridge_Right_Forward,HIGH);
    digitalWrite(T_bridge_Left_Backwards,HIGH);
    digitalWrite(T_bridge_Right_Backwards,LOW);
    break;
    case 'U':
    digitalWrite(DriverPitch_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverPitch_Step,LOW);
    delayMicroseconds(2000);
    break;
    case 'D':
    digitalWrite(DriverPitch_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverPitch_Step,LOW);
    delayMicroseconds(2000);
    break;
    case 'X':
    digitalWrite(DriverRotation_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverRotation_Step,LOW);
    delayMicroseconds(2000);
    break;
    case 'Y':
    digitalWrite(DriverRotation_Step,HIGH);
    delayMicroseconds(2000);
    digitalWrite(DriverRotation_Step,LOW);
    delayMicroseconds(2000);
    break;
    }
    moveCounter--;
    Serial.println(moveCounter);
    Serial.print(dir);
    if(moveCounter == 1){
      DistTarget();
    }
  }
  else{
    digitalWrite(T_bridge_Left_Backwards,LOW);
    digitalWrite(T_bridge_Left_Forward,LOW);
    digitalWrite(T_bridge_Right_Backwards,LOW);
    digitalWrite(T_bridge_Right_Forward,LOW);
  }

}
