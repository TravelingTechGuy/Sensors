using System;
using System.IO;
using Microsoft.SPOT;

namespace Sensors
{
    class SDCard
    {
        private string fileName = "sensors-log.csv";

        public bool WriteToFile(string line)
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
