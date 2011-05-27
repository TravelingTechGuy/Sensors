using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;

namespace Sensors
{
    class LightSensor
    {
        private AnalogInput sensor;

        public struct LightSensingResult
        {
            public int SensorReading;
            public string LightConditionString;
        }

        public LightSensor(Cpu.Pin pin)
        {
            sensor = new AnalogInput(pin);
        }

        /// <summary>
        /// Get CDS sensor value
        /// </summary>
        /// <returns>Raw value from sensor</returns>
        public int GetRawReading()
        {
            return sensor.Read();
        }

        /// <summary>
        /// Returns light conditions based on sensor
        /// Values may change depending on the resistor connected to the CDS
        /// See http://www.ladyada.net/learn/sensors/cds.html
        /// </summary>
        /// <returns>Sensing result struct, cointaining both raw data and interpretaion</returns>
        public LightSensingResult GetLightCondition()
        {
            LightSensingResult result = new LightSensingResult();
            result.SensorReading = GetRawReading();

            if (result.SensorReading < 10)
            {
                result.LightConditionString = "Dark";
            }
            else if (result.SensorReading < 200)
            {
                result.LightConditionString = "Dim";
            }
            else if (result.SensorReading < 500)
            {
                result.LightConditionString = "Light";
            }
            else if (result.SensorReading < 800)
            {
                result.LightConditionString = "Bright";
            }
            else
            {
                result.LightConditionString = "Very bright";
            }
            return result;
        }
    }
}
