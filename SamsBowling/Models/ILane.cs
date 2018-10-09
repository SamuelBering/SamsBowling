using SamsBowling.Strategies;

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
