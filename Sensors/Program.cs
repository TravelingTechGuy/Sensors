using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Sensors
{
    public class Program
    {
        private static string fileName = "sensors-log.csv";

        public static void Main()
        {
            LCD lcd = new LCD();
            LightSensor light = new LightSensor(Pins.GPIO_PIN_A0);
            TemperatureSensor temp = new TemperatureSensor(Pins.GPIO_PIN_A1);

           while (true)
            {
                TemperatureSensor.TemperatureResult tempResult = temp.GetTemperature(TemperatureSensor.TemperatureType.Cellsius);
                LightSensor.LightSensingResult lightResult = light.GetLightCondition();
                lcd.Clear();
                lcd.Write(tempResult.TemperatureString);
                lcd.SetCursorPosition(0, 1);
                lcd.Write(lightResult.LightConditionString);
                long secondsSinceReset = (Utility.GetMachineTime().Ticks / 10000000);
                if (secondsSinceReset % 10 == 0)   //write to disk every 10 seconds
                {
                    string line = secondsSinceReset.ToString() + "," 
                        + tempResult.VoltageString + "," + tempResult.TemperatureString 
                        + lightResult.SensorReading.ToString() + "," + lightResult.LightConditionString;
                    WriteToFile(line);
                }
                Thread.Sleep(1000);    //measure every 1 second
            }
        }

        public static bool WriteToFile(string line)
        {
            string path = @"\SD\" + fileName;
            TextWriter file;
            try
            {
                if (!File.Exists(path))
                {
                    file = new StreamWriter(path, false);
                    file.WriteLine("Seconds,Voltage,Temperature,CDS,Light");
                }
                else
                {
                    file = new StreamWriter(path, true);
                }
                file.WriteLine(line);
                file.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
