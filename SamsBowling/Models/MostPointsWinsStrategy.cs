using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class MostPointsWinsStrategy : CalculateWinnerStrategy
    {
        public override MatchResult CalculateWinner(Match match)
        {
            var matchResult = new MatchResult
            {
                Player1TotalPoints = match.Player1TotalPoints,
                Player2TotalPoints = match.Player2TotalPoints
            };

            if (matchResult.Player1TotalPoints > matchResult.Player2TotalPoints)
                matchResult.Winner = match.Player1;
            else if (matchResult.Player1TotalPoints < matchResult.Player2TotalPoints)
                matchResult.Winner = match.Player2;
            else
                matchResult.Winner = null;

            return matchResult;
        }
    }
}
