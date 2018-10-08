using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.BL
{
    public interface IPlant
    {
        void RegisterMember(Member member);

        void RegisterMatch(Match match);

        void RegisterContest(Contest contest);

        Member GetMember(int memberNumber);

        Match GetMatch(int matchNumber);

        Contest GetContest(int contestNumber);

        MatchResult RunMatch(Match match);

        ContestResult RunContest(Contest contest);

        List<ChampionResult> GetChampionsOfTheYear(int year);
    }
}
