using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.Services
{
    public class FileLogService : ILogService
    {
        string _fileName;

        public FileLogService()
        {
            _fileName = "PlantLog_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
        }

        public void Output(string message)
        {

            using (StreamWriter writer = new StreamWriter(_fileName, true))
            {
                writer.WriteLine(message);
            }
        }

        private void OutputChampionResults(List<ChampionResult> championResults)
        {
            foreach (var championResult in championResults)
                Output(championResult.ToString());
        }

        public void Output(List<ChampionResult> championResults)
        {
            if (championResults.Count > 1)
                Output($"-Champions of the year-\r\n");
            else
                Output($"-Champion of the year-\r\n");

            OutputChampionResults(championResults);
        }

        private void OutputChampionResults(List<ChampionResult> championResults, string title)
        {
            Output($"{title}\r\n");
            OutputChampionResults(championResults);
        }

        //public void Output(ContestResult contestResult)
        //{
        //    Output($"{contestResult.Contest.ToString()}\r\n");
        //    OutputChampionResults(contestResult.ChampionResults, "* Winner(s) of this contest: *");
        //}

        public void Output(ContestResult contestResult)
        {
            Output($"{contestResult.Contest.ToString()}\r\n");
            //Output(contestResult.ToString());
        }

    }
}
