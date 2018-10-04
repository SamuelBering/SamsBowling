using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class MatchResult
    {
        public string Log { get; set; }
        public int Player1TotalPoints { get; set; }
        public int Player2TotalPoints { get; set; }
        public Player Winner { get; set; }
    }
}
