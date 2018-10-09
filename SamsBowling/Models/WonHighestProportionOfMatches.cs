using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class WonHighestProportionOfMatches : CalculateChampionStrategy
    {
        int _minPlayedMatches;

        public WonHighestProportionOfMatches(int minPlayedMatches)
        {
            _minPlayedMatches = minPlayedMatches;
        }

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

        private List<PlayerWithStatistic> GetPlayersWithHighestProportionOfWonMatches(List<PlayerWithStatistic> players)
        {
            var player = players.OrderByDescending(p => p.wonMatchesPercent).First();
            return players.Where(p => p.wonMatchesPercent == player.wonMatchesPercent && p.totalMatches>=_minPlayedMatches).ToList();
        }

        public override List<ChampionResult> CalculateChampion(List<Match> matches)
        {
            var playersWithStatistic = GetUniquePlayers(matches);
            CalculateStatistic(playersWithStatistic, matches);
            var playersWithHighestProportionOfWonMatches = GetPlayersWithHighestProportionOfWonMatches(playersWithStatistic);
            var championResults = playersWithHighestProportionOfWonMatches.Select(p =>
              new ChampionResult
              {
                  Member = p.Member,
                  Matches = p.Matches,
                  TotalMatches = p.totalMatches,
                  WonMatches = p.wonMatches,
                  WonMatchesPercent = p.wonMatchesPercent
              }).ToList();

            return championResults;
        }
    }
}
