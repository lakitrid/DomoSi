DomoSi
======

Home Appliance project in c#.

The system will have the following elements :
- a data collecting core on a BeagleBone Black (Debian - Mono)
- a control system on Windows Server
- a frontend using AngularJs and REST services in C#
- a MongoDB for storing DataSamples

The target is to have the following hardware elements :
- TeleInfo reading (French power meter information)
- Temperature reading from Oregon Scientific sensor, using an arduino as RF receiver.
- Temperature reading over 1Wire bus
- Control of Home heaters using relays to produce control signal
- Wireless control of a power outlet using NRF24L01 as communication device between the BeagleBone and an Arduino
 

Third Party software used :
- Oregon decoding on Arduino : http://connectingstuff.net/blog/decodage-protocole-oregon-arduino-1/
