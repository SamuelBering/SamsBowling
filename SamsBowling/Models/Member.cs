using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class Member
    {
        public int MemberNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string PostTown { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Member number: {MemberNumber}\r\n" +
                   $"First name: {FirstName}\r\n" +
                   $"Last name: {LastName}\r\n" +
                   $"Street address: {StreetAddress}\r\n" +
                   $"Post code: {PostCode}\r\n" +
                   $"Post town: {PostTown}\r\n" +
                   $"Email: {Email}";
        }

        public void InitMember(Member member)
        {
            this.MemberNumber = member.MemberNumber;
            this.FirstName = member.FirstName;
            this.LastName = member.LastName;
            this.StreetAddress = member.StreetAddress;
            this.PostCode = member.PostCode;
            this.PostTown = member.PostTown;
            this.Email = member.Email; 
        }
    }
}
