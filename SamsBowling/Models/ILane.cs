using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public interface ILane
    {
        int LaneNumber { get; set; }

        bool Occupied { get; set; }

        CalculateWinnerStrategy CalculateWinnerStrategy { get; set; }

        MatchResult RunMatch(Match match);

    }
}
