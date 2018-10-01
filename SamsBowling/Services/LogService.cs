using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.Services
{
    public class LogService : ILogService
    {
        public void OutputMatchLog(MatchLog matchLog)
        {
            Console.WriteLine(matchLog.Log);
        }
    }
}
