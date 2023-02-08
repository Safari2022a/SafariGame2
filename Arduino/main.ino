#include <M5StickCPlus.h>
#include "BluetoothSerial.h"
using namespace std;

BluetoothSerial SerialBT;

void setup() {
  M5.begin();
  M5.Lcd.setTextColor(WHITE); //文字を白にする
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setTextSize(2);
  M5.Lcd.setCursor(3, 5);
  Serial.begin(9600);
  SerialBT.begin("M5StickSafari");

  M5.Lcd.print("Good Morning");
}

int loopCnt = 0;
String state[] = {"Idle", "Happy", "Hate"};
int i = 0;
void loop() {
  if(Serial.available()) {
    String msg = removeN(Serial.readString());
    handleState(msg);
    Serial.print("recieved: " + msg + "\n");
  }

  if (SerialBT.available()) {
    String msg = removeN(SerialBT.readString());
    handleState(msg);
    SerialBT.print("recieved: " + msg + "\n");
  }

  if (loopCnt++ % 30 == 0) {
    Serial.print("I am alive\n");
    SerialBT.print("I am alive\n");
    Serial.flush();
    SerialBT.flush();
  }
  
  delay(33); //fps30程度
}

void clearLcd(uint16_t color = BLACK) {
  M5.Lcd.fillScreen(color);
  M5.Lcd.setCursor(3, 5);
}

void handleState(String state) {
  uint16_t color;
  if (state == "Idle") {
    color = BLACK;
  } else if (state == "Happy") {
    color = MAROON;
  } else if (state == "Hate") {
    color = NAVY;
  } else {
    return;
  }
  clearLcd(color);
  M5.Lcd.print(state);
}

//末尾の改行を削除した文字列を返す
String removeN(String s) {
  string res = s.c_str();
  if (res.back() == '\n') {
    res.pop_back();
  }
  return res.c_str();
}
