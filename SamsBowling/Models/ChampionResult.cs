using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class ChampionResult
    {
        public int TotalMatches { get; set; }
        public int WonMatches { get; set; }
        public double WonMatchesPercent { get; set; }
        public List<Match> Matches { get; set; }
        public Member Member { get; set; }

        public override string ToString()
        {
            return $"Champion name: {Member.FirstName} {Member.LastName}\r\n" +
                   $"Total matches: {TotalMatches}\r\n" +
                   $"Won matches: {WonMatches}\r\n" +
                   $"Won matches percent: {WonMatchesPercent * 100}%\r\n";                   
        }

    }
}
