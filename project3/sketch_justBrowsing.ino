#include "Ultrasonic.h"

#include <FastLED.h>

FASTLED_USING_NAMESPACE

#define DATA_PIN    5
#define LED_TYPE    WS2812B
#define COLOR_ORDER GRB
#define NUM_LEDS    25
CRGB leds[NUM_LEDS];

#define BRIGHTNESS          96
#define FRAMES_PER_SECOND  120

Ultrasonic ultrasonic(3);
boolean lightsOn = false;
const int buttonPin = 6;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(buttonPin, INPUT);
  FastLED.addLeds<LED_TYPE,DATA_PIN,COLOR_ORDER>(leds, NUM_LEDS).setCorrection(TypicalLEDStrip);
  FastLED.setBrightness(BRIGHTNESS);
}


void loop() {
  if (someoneInRange()) {
    if (lightsOn) {
      turnLightsOff();
      lightsOn = false;
      delay(250);
    }
    if (buttonDown()) {
      turnLightsOn();
      lightsOn = true;
      delay(250);
    }
  } else {
    turnLightsOff();
  }
}

void turnLightsOff() {
  fill_solid(leds, NUM_LEDS, CRGB(0, 0, 0));
  FastLED.show();
}

void turnLightsOn() {
  fill_solid(leds, NUM_LEDS, CRGB(0, 255, 255));
  FastLED.show();
}

void turnOnPocketLight() {
  
}

void turnOffPocketLight() {
  
}

boolean someoneInRange() {
  Serial.print(ultrasonic.MeasureInCentimeters());//0~400cm
  return ultrasonic.MeasureInCentimeters() < 390;
}

boolean buttonDown() {
  return digitalRead(buttonPin) == HIGH;
}
