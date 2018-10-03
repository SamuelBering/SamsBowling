using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.Services
{
    public class FileLogService : ILogService
    {
        string _fileName;

        public FileLogService()
        {
            _fileName = "PlantLog_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
        }

        public void Output(string message)
        {

            using (StreamWriter writer = new StreamWriter(_fileName, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
