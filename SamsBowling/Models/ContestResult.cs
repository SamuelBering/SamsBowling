using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class ContestResult
    {
        public Contest Contest { get; set; }
        public List<MatchResult> MatchResults { get; set; } = new List<MatchResult>();
        public List<ChampionResult> ChampionResults { get; set; } = new List<ChampionResult>();

        public override string ToString()
        {
            var championResultsStr = "";

            foreach (var championResult in ChampionResults)
                championResultsStr += championResult.ToString() + "\r\n";

            return $"{championResultsStr}";

        }
    }
}
