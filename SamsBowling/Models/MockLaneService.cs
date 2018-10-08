using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class MockLaneService : ILaneService
    {
        Dictionary<int, ILane> Lanes { get; set; }
        public CalculateWinnerStrategy CalculateWinnerStrategy { get; set; }

        public List<Match> MockMatches { get; set; }

        public Set[] Player1Sets { get; set; } = new Set[3];
        public Set[] Player2Sets { get; set; } = new Set[3];

        public MockLaneService(int numberOfLanes, CalculateWinnerStrategy cWinnerStrategy)
        {
            CalculateWinnerStrategy = cWinnerStrategy;
            CreateLanes(numberOfLanes);
        }

        public MatchResult RunMatch(Match match)
        {
            var lane = GetFreeLane() as MockLane;

            if (lane == null)
                throw new InvalidOperationException("All lanes are busy!");

            lane.Occupied = true;
            match.Lane = lane;
            match.StartDateTime = match.StartDateTime ?? DateTime.Now;
            lane.Player1Sets = this.Player1Sets;
            lane.Player2Sets = this.Player2Sets;
            var matchResult = lane.RunMatch(match);
            lane.Occupied = false;
            return matchResult;
        }


        public List<MatchResult> RunContest(Contest contest)
        {
            if (contest.Matches.Count != MockMatches.Count)
                throw new InvalidOperationException("Number of matches must be equal with number of mock matches");

            var matchResults = new List<MatchResult>();

            for (int i = 0; i < contest.Matches.Count; i++)
            {
                var match = contest.Matches[i];
                Player1Sets = MockMatches[i].Player1Sets;
                Player2Sets = MockMatches[i].Player2Sets;
                matchResults.Add(RunMatch(match));
            }

            contest.Completed = true;

            return matchResults;
        }


        private ILane GetFreeLane()
        {
            foreach (var laneKVP in Lanes)
            {
                if (!laneKVP.Value.Occupied)
                    return laneKVP.Value;
            }
            return null;
        }

        void CreateLanes(int numberOfLanes)
        {
            Lanes = new Dictionary<int, ILane>();

            for (var i = 0; i < numberOfLanes; i++)
            {
                Lanes.Add(i + 1, new MockLane
                {
                    LaneNumber = i + 1,
                    CalculateWinnerStrategy = this.CalculateWinnerStrategy
                });
            }
        }


    }
}
