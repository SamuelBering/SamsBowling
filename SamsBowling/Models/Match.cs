using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class Match
    {
        public int MatchNumber { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Set[] Player1Sets { get; set; } = new Set[3];
        public Set[] Player2Sets { get; set; } = new Set[3];
        public ILane Lane { get; set; }
        public DateTime StartDateTime { get; set; }
        public Player Winner { get; set; }

        public override string ToString()
        {
            return $"Match number: {MatchNumber}\r\n" +
                   $"Player1: {Player1.FirstName} {Player1.LastName} \r\n" +
                   $"Player2: {Player2.FirstName} {Player2.LastName} \r\n";
        }

    }

    
}
