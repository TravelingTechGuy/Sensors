using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Sensors
{
    class TemperatureSensor
    {
        private AnalogInput sensor;

        public enum TemperatureType
        {
            Cellsius,
            Farrenheit
        }

        public TemperatureSensor(Cpu.Pin pin)
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

        private double GetVoltage(int reading)
        {
            // converting that reading to voltage
            // for 5v use 5.0, for 3.3v use 3.3
            double voltage = (reading * 3.3);
            voltage /= 1024.0;
            return voltage;
        }

        /// <summary>
        /// Returns tempeature based on sensor
        /// See http://www.ladyada.net/learn/sensors/tmp36.html
        /// </summary>
        /// <param name="ttype">Should temperature be in Cellsius degrees or Farrenheit</param>
        /// <returns>string containing interpretation of value from CS</returns>
        public string GetTemperature(TemperatureType ttype = TemperatureType.Cellsius, bool includeVoltage = false)
        {
            //getting the voltage reading from the temperature sensor
            int reading = GetRaw();
            double voltage = GetVoltage(reading);
            
            // now calculate the temperature
            //converting from 10 mv per degree with 500 mV offset
            //to degrees ((volatge - 500mV) times 100)
            double temperatureC = (voltage - 0.5) * 100;

            // now convert to Farrenheit
            double temperatureF = (temperatureC * 9.0 / 5.0) + 32.0;

            string result = includeVoltage ? (voltage.ToString("F") + "v,") : string.Empty;
            result += (ttype == TemperatureType.Cellsius) ? (temperatureC.ToString("F") + "C") : (temperatureF.ToString("F") + "F");
            return result;
        }
    }
}
