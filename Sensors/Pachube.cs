using System;
using Microsoft.SPOT;
using Gsiot.PachubeClient;

namespace Sensors
{
    class Pachube
    {
        private const string apiKey = "ADD YOUR KEY";
        private const string feedId = "ADD YOUR FEED NUMBER";

        public bool WriteToPachube(string light, string temperature)
        {
            try
            {
                PachubeClient.Send(apiKey, feedId, light + "," + temperature);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
