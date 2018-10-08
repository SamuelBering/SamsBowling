using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public abstract class CalculateChampionStrategy
    {
        public abstract List<ChampionResult> CalculateChampion(List<Match> matches);
    }
}
