using System;
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
            throw new NotImplementedException();
        }

        public void AddMatch(Match match)
        {
            throw new NotImplementedException();
        }

        public void AddMember(Member member)
        {
            PlantStorage.Members.Add(member.MemberNumber, member);
        }

        public Contest GetContest(int contestNumber)
        {
            throw new NotImplementedException();
        }

        public Match GetMatch(int matchNumber)
        {
            throw new NotImplementedException();
        }

        public Member GetMember(int memberNumber)
        {
            return PlantStorage.Members[memberNumber];
        }
    }
}
