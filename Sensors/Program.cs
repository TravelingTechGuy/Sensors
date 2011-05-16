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
        private static string fileName = "log.csv";

        public static void Main()
        {
            LCD lcd = new LCD();
            LightSensor light = new LightSensor(Pins.GPIO_PIN_A0);
            TemperatureSensor temp = new TemperatureSensor(Pins.GPIO_PIN_A1);
            
            while (true)
            {
                long loop = 0;
                string tempResult = temp.GetTemperature(TemperatureSensor.TemperatureType.Cellsius, true);
                string lightResult = light.GetLightCondition(true);
                lcd.Clear();
                lcd.Write(tempResult);
                lcd.SetCursorPosition(0, 1);
                lcd.Write(lightResult);
                Thread.Sleep(10000);    //measure every 10 seconds
                loop++;
                if (loop % (6 * 60) == 0)   //write to disk once an hour
                {
                    string id = (loop / (6 * 60)).ToString();
                    WriteToFile(id, tempResult, lightResult);
                }
            }
        }

        public static bool WriteToFile(string id, string temperature, string light)
        {
            string path = @"\SD\" + fileName;
            TextWriter file;
            try
            {
                if (!File.Exists(path))
                {
                    file = new StreamWriter(path, false);
                    file.WriteLine("ID,Voltage,Temperature,Sense,Light");
                    file.WriteLine(id + "," + temperature + "," + light);
                }
                else
                {
                    file = new StreamWriter(path, true);
                    file.WriteLine(id + "," + temperature + "," + light);
                }
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
