# 2016windowsiot-iothub-PWM-PCA9685-L298-motor-driver
WORK IN PROGRESS, as for 2016-07-16

2016windowsiot-iothub-PWM-PCA9685-L298-motor-driver

## Components
* PCA9685 
* [L298, GHI, .NET Gadgeteer](https://www.ghielectronics.com/catalog/product/315) (or similar)
    * [P Socket, .NET Gadgeteer](http://gadgeteer.codeplex.com/wikipage?title=Socket%20Type%20P) 
* 2 servos
* 2 DC motors


## Connections
### I2C
Connect PCA9685 to I2C

### P Socket
* P1 - +3.3V (not used ?)
* P2 - +5V (connect to +5 on RPI)
* P3, 


### PWM from PCA9685
1,2 -> Servo

4 -> P8

5 -> P7

### GPIO
-> P9 (direction)

-> P6 (direction)

### 5V
On P socket pin 2 (marked as 5V). 3.3V - not needed

### Others
GND!

DC Motor - 6V, PCA9685 + L298 - need 9V! for 0.99 = 1
## Remarks
Do not mix GNP and + in load - will not start
