using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Strategies
{
    public abstract class CalculateWinnerStrategy
    {
        public abstract MatchResult CalculateWinner(Match match);
    }
}
