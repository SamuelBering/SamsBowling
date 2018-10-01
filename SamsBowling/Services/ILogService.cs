using SamsBowling.BL;
using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Services
{
    public interface ILogService
    {
        void OutputMatchLog(MatchLog matchLog);
    }
}
