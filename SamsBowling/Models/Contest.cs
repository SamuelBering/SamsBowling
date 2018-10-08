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
        public bool Completed { get; set; }
        public ContestResult ContestResult { get; set; }
        private List<Match> _matches;


        public List<Match> Matches
        {
            get
            {
                return _matches;
            }
            set
            {
                if (value != null)
                {
                    foreach (var match in value)
                    {
                        match.StartDateTime = StartDateTime;
                    }
                }
                _matches = value;
            }
        }

        public override string ToString()
        {
            var matchInfo = CreateMatchInfo();
            var contestResultInfo = CreateContestResultInfo();

            return $"Contest number: {ContestNumber}\r\n" +
                   $"Title: {Title}\r\n" +
                   $"Description: {Description}\r\n" +
                   $"Start date and time: {StartDateTime.ToString()}\r\n" +
                   $"End date and time {EndDateTime.ToString()}\r\n\r\n" +
                   $"Match information:\r\n" +
                   $"{matchInfo}\r\n" +
                   $"*Contest Result*:\r\n" +
                   $"{contestResultInfo}\r\n";
        }

        public bool MatchesIsValid()
        {
            if (Matches == null)
                return true;

            List<Match> validMatches = new List<Match>();

            foreach (var match in Matches)
            {
                foreach (var validMatch in validMatches)
                {
                    if (Match.HasSamePlayers(match, validMatch))
                        return false;
                }
                validMatches.Add(match);
            }

            return true;
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

        private string CreateContestResultInfo()
        {
            if (Completed)
                return ContestResult.ToString();
            else
                return "Not completed";
        }
    }
}
