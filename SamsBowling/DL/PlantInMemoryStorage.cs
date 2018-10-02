using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.DL
{

    public sealed class PlantInMemoryStorage
    {
        private static PlantInMemoryStorage instance = null;
        private static readonly object padlock = new object();

        public Dictionary<int, Member> Members { get; set; }
        public Dictionary<int, Match> Matches { get; set; }
        public Dictionary<int, Contest> Contests { get; set; }

        private PlantInMemoryStorage()
        {
            Members = new Dictionary<int, Member>();
            Matches = new Dictionary<int, Match>();
            Contests = new Dictionary<int, Contest>();
        }

        public static PlantInMemoryStorage Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PlantInMemoryStorage();
                    }
                    return instance;
                }
            }
        }
    }
}
