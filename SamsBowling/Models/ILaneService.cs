using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public interface ILaneService
    {
        CalculateWinnerStrategy CalculateWinnerStrategy { get; set; }
        MatchResult RunMatch(Match match);
        List<MatchResult> RunContest(Contest contest);
    }
}
