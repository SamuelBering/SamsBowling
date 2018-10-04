using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class Contest
    {
        public int ContestNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<Match> Matches { get; set; }

        public override string ToString()
        {
            var matchInfo = CreateMatchInfo();

            return $"Contest number: {ContestNumber}\r\n" +
                   $"Title: {Title}\r\n" +
                   $"Description: {Description}\r\n" +
                   $"Start date and time: {StartDateTime.ToString()}\r\n" +
                   $"End date and time {EndDateTime.ToString()}\r\n\r\n" +
                   $"Match information:\r\n" +
                   $"{matchInfo}";
        }

        private string CreateMatchInfo()
        {
            var matchInfo = "";

            if (Matches != null && Matches.Count > 0)
            {
                matchInfo = $"Number of matches: {Matches.Count}\r\n---------------------------\r\n";

                foreach (var match in Matches)
                    matchInfo += $"{match.ToString()}\r\n";
            }
            else
                matchInfo = "No matches registered";

            return matchInfo;
        }
    }
}
