using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsBowling.Models;

namespace SamsBowling.Services
{
    public class MockExportMemberService : IExportMemberService
    {
        public bool LastExportWasSuccessful { get; set; }
        public bool ExportMember(Member member)
        {
            LastExportWasSuccessful = true;
            return true;
        }
    }
}
