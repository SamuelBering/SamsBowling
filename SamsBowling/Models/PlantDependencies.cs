using SamsBowling.DL;
using SamsBowling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling.Models
{
    public class PlantDependencies
    {
        public IPlantRepository PlantRepository { get; set; }
        public ILogService LogService { get; set; }
        public ILaneService LaneService { get; set; }
        public IExportMemberService ExportMemberService { get; set; }
        public CalculateChampionStrategy CalculateChampionStrategy { get; set; }
    }
}
