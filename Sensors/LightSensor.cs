using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Sensors
{
    class LightSensor
    {
        private AnalogInput sensor;

        public LightSensor(Cpu.Pin pin)
        {
            sensor = new AnalogInput(pin);
        }

        /// <summary>
        /// Get CDS sensor value
        /// </summary>
        /// <returns>Raw value from sensor</returns>
        public int GetRaw()
        {
            return sensor.Read();
        }

        /// <summary>
        /// Returns light conditions based on sensor
        /// Values may change depending on the resistor connected to the CDS
        /// See http://www.ladyada.net/learn/sensors/cds.html
        /// </summary>
        /// <param name="withValue">Should raw value be returned as part of string (default no)</param>
        /// <returns>string containing interpretation of value from CS</returns>
        public string GetLightCondition(bool withValue = false)
        {
            int reading = GetRaw();
            string result = withValue ? (reading.ToString() + ",") : string.Empty;
            if (reading < 10)
            {
                result += "Dark";
            }
            else if (reading < 200)
            {
                result += "Dim";
            }
            else if (reading < 500)
            {
                result += "Light";
            }
            else if (reading < 800)
            {
                result += "Bright";
            }
            else
            {
                result += "Very bright";
            }
            return result;
        }
    }
}
