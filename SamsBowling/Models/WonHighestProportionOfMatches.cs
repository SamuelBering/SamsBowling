using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class WonHighestProportionOfMatches : CalculateChampionStrategy
    {
        class PlayerWithStatistic
        {
            public int memberNumber;
            public int totalMatches;
            public int wonMatches;
            public double wonMatchesPercent;
            public List<Match> Matches;
            public Member Member;
        }

        private List<PlayerWithStatistic> GetUniquePlayers(List<Match> matches)
        {
            var players = matches.Select(m => m.Player1).ToList();
            players.AddRange(matches.Select(m => m.Player2).ToList());

            var playersWithStatistic = players.GroupBy(p => p.MemberNumber).Select(kvp => new PlayerWithStatistic
            {
                memberNumber = kvp.Key,
                Member = (Member)kvp.First()
            }).ToList();

            return playersWithStatistic;
        }

        private void CalculateStatistic(List<PlayerWithStatistic> players, List<Match> matches)
        {
            foreach (var playerWithStatistic in players)
            {
                var associatedMatches = matches
                    .Where(m => m.Player1.MemberNumber == playerWithStatistic.memberNumber
                          || m.Player2.MemberNumber == playerWithStatistic.memberNumber).ToList();

                playerWithStatistic.Matches = associatedMatches;
                playerWithStatistic.totalMatches = associatedMatches.Count;
                playerWithStatistic.wonMatches = associatedMatches.Count(m => (m.Winner?.MemberNumber ?? -1) == playerWithStatistic.memberNumber);
                playerWithStatistic.wonMatchesPercent = (double)playerWithStatistic.wonMatches / (double)playerWithStatistic.totalMatches;
            }
        }

        private PlayerWithStatistic GetPlayerWithHighestProportionOfWonMatches(List<PlayerWithStatistic> players)
        {
            return players.OrderByDescending(p => p.wonMatchesPercent).First();
        }

        public override ChampionResult CalculateChampion(List<Match> matches)
        {
            var playersWithStatistic = GetUniquePlayers(matches);

            CalculateStatistic(playersWithStatistic, matches);

            var playerWithHighestProportionOfWonMatches = GetPlayerWithHighestProportionOfWonMatches(playersWithStatistic);

            var championResult = new ChampionResult
            {
                Member = playerWithHighestProportionOfWonMatches.Member,
                Matches = playerWithHighestProportionOfWonMatches.Matches,
                TotalMatches = playerWithHighestProportionOfWonMatches.totalMatches,
                WonMatches = playerWithHighestProportionOfWonMatches.wonMatches,
                WonMatchesPercent = playerWithHighestProportionOfWonMatches.wonMatchesPercent
            };

            return championResult;

            /*
             * gör en lista av unika spelareMedStatistik från alla matcher
             * loopa igenom listan av spelare
             *     hämta alla matcher där akutell spelaren är med 
             *     lägg till totalt matchantal i spelaren
             *     lägg till vunna matcher i spelare
             *     beräkna andel vunna matcher dvs vunna matcher/totalt antal matcher
             *     lägg till andel vunna matcher i spelaren
             *     
             *                  
             * hämta den spelareMedstatistik som har störst värde på wonMatchesPercent 
             * skapa en spelare med denna info och returnera
             */

        }
    }
}
