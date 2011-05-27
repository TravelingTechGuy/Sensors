using System;
using Microsoft.SPOT;
using Gsiot.PachubeClient;

namespace Sensors
{
    class Pachube
    {
        private const string apiKey = "Add your key here";
        private const string feedId = "25788";

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
