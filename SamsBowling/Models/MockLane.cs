using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class MockLane : ILane
    {
        public int LaneNumber { get; set; }
        public bool Occupied { get; set; }
        public Set[] Player1Sets { get; set; } = new Set[3];
        public Set[] Player2Sets { get; set; } = new Set[3];

        public CalculateWinnerStrategy CalculateWinnerStrategy { get; set; }

        private void ShowPlayerTurnMessage(Player player)
        {

        }

        private Set GetSetResult(int setIndex, Set[] sets)
        {
            return sets[setIndex];
        }

        private MatchResult GetMatchResult(Match match)
        {
            var matchResult = CalculateWinnerStrategy.CalculateWinner(match);
            matchResult.Log = match.ToString();
            return matchResult;
        }
            
        public MatchResult RunMatch(Match match)
        {
            for (int i = 0; i < 3; i++)
            {
                ShowPlayerTurnMessage(match.Player1);
                var setResult = GetSetResult(i, Player1Sets);
                match.Player1Sets[i] = setResult;

                ShowPlayerTurnMessage(match.Player2);
                setResult = GetSetResult(i, Player2Sets);
                match.Player2Sets[i] = setResult;
            }

            match.Completed = true;
            var matchResult= GetMatchResult(match);
            match.Winner = matchResult.Winner;
            return matchResult;
        }


    }
}
