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
        public bool Completed { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Set[] Player1Sets { get; set; } = new Set[3];
        public Set[] Player2Sets { get; set; } = new Set[3];
        public int Player1TotalPoints
        {
            get
            {
                return Player1Sets.Sum(s => s.Points);
            }
        }
        public int Player2TotalPoints
        {
            get
            {
                return Player2Sets.Sum(s => s.Points);
            }
        }
        public ILane Lane { get; set; }
        public DateTime StartDateTime { get; set; }
        public Player Winner { get; set; }

        public override string ToString()
        {
           
            string str;

            if (Completed)
            {
                var resultMessage = Winner != null ? $"winner is {Winner.FirstName} {Winner.LastName}" : "oavgjort";

                str = $"Match number: {MatchNumber}\r\nStart date and time: {StartDateTime.ToString()}\r\nLane: {(Lane.LaneNumber)}\r\n" +
                       $"Player1: {Player1.FirstName} {Player1.LastName}\t\tPlayer2: {Player2.FirstName} {Player2.LastName}\r\n" +
                       $"Set 1: {Player1Sets[0].Points}\t\t\tSet 1: {Player2Sets[0].Points}\r\n" +
                       $"Set 2: {Player1Sets[1].Points}\t\t\tSet 2: {Player2Sets[1].Points}\r\n" +
                       $"Set 3: {Player1Sets[2].Points}\t\t\tSet 3: {Player2Sets[2].Points}\r\n\r\n" +
                       $"Total points\r\nplayer1: {Player1TotalPoints}\tplayer2: {Player2TotalPoints}\r\n" +
                       $"\r\nResult: {resultMessage}";
            }
            else
                str = $"Match number: {MatchNumber} - Not completed -\r\n" +
                       $"Player1: {Player1.FirstName} {Player1.LastName}\tPlayer2: {Player2.FirstName} {Player2.LastName}\r\n";

            return str;
        }

    }

    
}
