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
        private IPlantRepository _plantRepository;
        private ILogService _logService;

        public Plant(IPlantRepository plantRepository, ILogService logService)
        {
            _plantRepository = plantRepository;
            _logService = logService;
        }

        public Player GetChampionOfTheYear(int year)
        {
            throw new NotImplementedException();
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
            var member = _plantRepository.GetMember(memberNumber);

            _logService.Output($"***Getting member***\r\n{member.ToString()}");

            return member;
        }

        public void RegisterContest(Contest contest)
        {
            throw new NotImplementedException();
        }

        public void RegisterMatch(Match match)
        {
            _plantRepository.AddMatch(match);
            _logService.Output($"***Registering match***\r\n{match.ToString()}");

        }

        public void RegisterMember(Member member)
        {
            _plantRepository.AddMember(member);
            _logService.Output($"***Registering member***\r\n{member.ToString()}");
        }

        public MatchLog RunMatch(Match match)
        {
            throw new NotImplementedException();
        }
    }
}
