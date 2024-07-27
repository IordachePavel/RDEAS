// THIS IS NAVIGATION CIRCUIT CONTROLLER PROGRAM.

//Needed for I2C THERMOPILE SENSOR
#include <Wire.h>

//SENSOR DEFINITIONS
const int trigPin = 16;  //Ultrasonic sensor
const int echoPin = 10; 


#define D6T_ADDR 0x0A  // for I2C 7bit address
#define D6T_CMD 0x4C 

#define PARA_0100MS_1  ((uint8_t)0x90)
#define PARA_0100MS_2 ((uint8_t)0xDA)
#define PARA_0100MS_3 ((uint8_t)0x16)

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
    Serial.print(" STMP:");
    Serial.print(itemp / 10.0, 1);
    }

    // loop temperature pixels of each thrmopiles measurements
    for (i = 0, j = 2; i < N_PIXEL; i++, j += 2) {
        itemp = conv8us_s16_le(rbuf, j);
        pix_data = itemp;
        if(!RQST){
          Serial.print(" TMP:");
          Serial.println(itemp / 10.0, 1);  // print PTAT & Temperature
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
  Serial.print("DST:");
  Serial.print((int)distance/10);
}

void setup() {
  //Pindef ultrasonics
  pinMode(trigPin, OUTPUT);  
  pinMode(echoPin, INPUT); 
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
}

void loop() {
  if(Serial.available() > 0){
  MSG=Serial.readString();
  if (cmpNstr(MSG,"MSR",3)){
    DistTarget();
    TempTarget(1);
    TempTarget(0);
  }
  }
}
