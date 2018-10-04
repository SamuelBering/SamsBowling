using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.DL
{
    public interface IPlantRepository
    {
        void AddMember(Member member);
        void AddMatch(Match match);
        void AddContest(Contest contest);
        Member GetMember(int memberNumber);
        Match GetMatch(int matchNumber);
        Contest GetContest(int contestNumber);
        List<Match> GetCompletedMatches(DateTime startDateTime, DateTime endDateTime);
    }
}
