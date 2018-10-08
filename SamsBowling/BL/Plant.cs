using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.DL;
using SamsBowling.Models;
using SamsBowling.Services;

namespace SamsBowling.BL
{
    public class Plant : IPlant
    {

        PlantDependencies _plantDependencies;

        public Plant(PlantDependencies plantDependencies)
        {
            _plantDependencies = plantDependencies;
        }

        public List<ChampionResult> GetChampionsOfTheYear(int year)
        {
            var matches = _plantDependencies.PlantRepository.GetCompletedMatches(new DateTime(year, 1, 1), new DateTime(year, 12, 31));
            var championResults = _plantDependencies.CalculateChampionStrategy.CalculateChampion(matches);
            _plantDependencies.LogService.Output($"***Getting champion(s) of the year: {year}***\r\n");
            _plantDependencies.LogService.Output(championResults);
            return championResults;
        }

        public Contest GetContest(int contestNumber)
        {
            var contest = _plantDependencies.PlantRepository.GetContest(contestNumber);
            _plantDependencies.LogService.Output($"***Getting contest***\r\n{contest.ToString()}");
            return contest;
        }

        public Match GetMatch(int matchNumber)
        {
            var match = _plantDependencies.PlantRepository.GetMatch(matchNumber);
            _plantDependencies.LogService.Output($"***Getting match***\r\n{match.ToString()}");
            return match;
        }

        public Member GetMember(int memberNumber)
        {
            var member = _plantDependencies.PlantRepository.GetMember(memberNumber);

            _plantDependencies.LogService.Output($"***Getting member***\r\n{member.ToString()}");

            return member;
        }

        public void RegisterContest(Contest contest)
        {
            if (!contest.MatchesIsValid())
            {
                var errorMessage = "Two matches can't contain the same players";
                _plantDependencies.LogService.Output($"***Can´t register contest!!***\r\nError message: {errorMessage}\r\n{contest.ToString()}");
                throw new InvalidOperationException(errorMessage);
            }
             
            _plantDependencies.PlantRepository.AddContest(contest);
            _plantDependencies.LogService.Output($"***Registering contest***\r\n{contest.ToString()}");

        }

        public void RegisterMatch(Match match)
        {
            _plantDependencies.PlantRepository.AddMatch(match);
            _plantDependencies.LogService.Output($"***Registering match***\r\n{match.ToString()}");

        }

        public void RegisterMember(Member member)
        {
            _plantDependencies.PlantRepository.AddMember(member);
            _plantDependencies.LogService.Output($"***Registering member***\r\n{member.ToString()}");
        }

        public MatchResult RunMatch(Match match)
        {
            var matchResult = _plantDependencies.LaneService.RunMatch(match);

            _plantDependencies.LogService.Output($"***Running match***\r\n{match.ToString()}");

            return matchResult;
        }
    }
}
