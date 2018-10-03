using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class Player : Member
    {

        public Player(Member member)
        {
            InitMember(member);
        }

        public List<Contest> Contests { get; set; }
        public List<Match> Matches { get; set; }
    }
}
