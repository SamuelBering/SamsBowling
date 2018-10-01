using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.DL
{
    public class PlantRepository : IPlantRepository
    {
        PlantStorage _plantStorage;

        public PlantRepository()
        {
            _plantStorage = PlantStorage.Instance;
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
            _plantStorage.Members.Add(member.MemberNumber, member);
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
            return _plantStorage.Members[memberNumber];
        }
    }
}
