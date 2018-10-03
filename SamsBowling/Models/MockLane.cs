using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class MockLane : ILane
    {
        public int LaneNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void RunMatch(Match match)
        {
            throw new NotImplementedException();
        }

        public void SetCalculateWinnerStrategy(CalculateWinnerStrategy cWStrategy)
        {
            throw new NotImplementedException();
        }
    }
}
