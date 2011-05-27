using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Sensors
{
    public class Program
    {
        private static LCD lcd;
        private static LightSensor light;
        private static TemperatureSensor temp;
        private static SDCard sd = new SDCard();
        private static PachubeSockets pachube = new PachubeSockets();
        
        public static void Main()
        {
            lcd = new LCD();
            light = new LightSensor(Pins.GPIO_PIN_A0);
            temp = new TemperatureSensor(Pins.GPIO_PIN_A1);
            StartSensing();
        }


        private static void StartSensing()
        {
            while (true)
            {
                TemperatureSensor.TemperatureResult tempResult = temp.GetTemperature(TemperatureSensor.TemperatureType.Cellsius);
                LightSensor.LightSensingResult lightResult = light.GetLightCondition();
                ShowResultsOnLCD(tempResult.TemperatureString, lightResult.LightConditionString);
                long secondsSinceReset = (Utility.GetMachineTime().Ticks / 10000000);
                if (secondsSinceReset % 20 == 0)   //write to disk every 20 seconds
                {
                    string line = secondsSinceReset.ToString() + ","
                        + tempResult.VoltageString + "," + tempResult.TemperatureString + ","
                        + lightResult.SensorReading.ToString() + "," + lightResult.LightConditionString;
                    sd.WriteToFile(line);

                    line = "light," + lightResult.SensorReading.ToString()
                        + "\r\n" 
                        + "temp," + tempResult.TemperatureString.TrimEnd('C');
                    pachube.WriteToPachube(line);
                }
                Thread.Sleep(1000);    //measure every 1 second
            }
        }

        private static void ShowResultsOnLCD(string tempResult, string lightResult)
        {
            lcd.Clear();
            lcd.Write(tempResult);
            lcd.SetCursorPosition(0, 1);
            lcd.Write(lightResult);
        }
    }
}
