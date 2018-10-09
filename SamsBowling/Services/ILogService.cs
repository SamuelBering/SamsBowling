using SamsBowling.Models;
using System.Collections.Generic;

namespace SamsBowling.Services
{
    public interface ILogService
    {
        void Output(string message);
        void Output(List<ChampionResult> championResults);
        void Output(ContestResult contestResult);
    }
}
