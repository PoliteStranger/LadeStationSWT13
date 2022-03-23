using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface ILogger
    {
        void Log(string logdata);

    }
    public class Logger:ILogger
    {
        // Navnet på systemets log-fil
        private string logFile = "logfile.txt"; 

        public bool LogWritten { get; set; }

        public Logger()
        {
            // Kun til test:
            LogWritten = false;
        }

        public void Log(string logdata)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + logdata);
                
                // Kun til test
                LogWritten = true;
            }
        }
    }
}
