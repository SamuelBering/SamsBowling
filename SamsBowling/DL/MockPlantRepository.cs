﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.DL
{
    public class MockPlantRepository : IPlantRepository
    {
        public PlantInMemoryStorage PlantStorage { get; set; }

        public MockPlantRepository()
        {
            PlantStorage = PlantInMemoryStorage.Instance;
        }

        public void AddContest(Contest contest)
        {
            PlantStorage.Contests.Add(contest.ContestNumber, contest);
        }

        public void AddMatch(Match match)
        {
            PlantStorage.Matches.Add(match.MatchNumber, match);
        }

        public void AddMember(Member member)
        {
            PlantStorage.Members.Add(member.MemberNumber, member);
        }

        public Contest GetContest(int contestNumber)
        {
            return PlantStorage.Contests[contestNumber];
        }

        public Match GetMatch(int matchNumber)
        {
            return PlantStorage.Matches[matchNumber];
        }

        public Member GetMember(int memberNumber)
        {
            return PlantStorage.Members[memberNumber];
        }
    }
}
