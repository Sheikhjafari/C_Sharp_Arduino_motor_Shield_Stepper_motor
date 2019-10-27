/*
Interface an Android smartphone with an Arduino via Bluetooth 
to control an LED from your phone
modified on 22 Oct 2019
by Alireza Sheikhjafari @ Rabbit Channel
https://www.aparat.com/v/35zIX
*/
#include <AFMotor.h>
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";

int EndOfCommend;
int EndOfdata;

// two stepper motors one on each port
AF_Stepper myStepperX(200, 1);
AF_Stepper myStepperY(200, 2);

void setup() {
 Serial.begin(9600); 

  myStepperX.setSpeed(100);
  myStepperY.setSpeed(100);
}
void loop() {
   if(Serial.available()) serialEvent();
  if(stringComplete)
  {
      stringComplete = false;
      getCommand();
      
      if(commandString.equals("S1F"))
      {
        String text = getData();
        int steps= text.toInt();
        Serial.println(steps);
        myStepperY.step(steps, FORWARD, MICROSTEP);  
      }      
     
    inputString = "";
  }
  delay(100);
  
}






void getCommand()
{
  if(inputString.length()>0)
  {  
     EndOfCommend=inputString.indexOf(',');
     commandString = inputString.substring(1,EndOfCommend);
     
     Serial.print("Command:");
     Serial.println(commandString);
  }
}




String getData()
{
  String value = inputString.substring(EndOfCommend+1,inputString.length()-2);
  Serial.print("Data:");
  Serial.println(value);
  Serial.println("---------------------------");
  return value;
}



void serialEvent() {
  while (Serial.available() && stringComplete == false) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    Serial.print(inChar);
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}

