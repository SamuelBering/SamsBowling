using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public interface ILane
    {
        int LaneNumber { get; set; }

        void SetCalculateWinnerStrategy(CalculateWinnerStrategy cWStrategy);

        void RunMatch(Match match);

    }
}
