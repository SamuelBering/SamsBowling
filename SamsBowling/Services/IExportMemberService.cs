using SamsBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Services
{
    public interface IExportMemberService
    {
        bool ExportMember(Member member);
    }
}
