using SamsBowling.Services;
using SamsBowling.Strategies;

namespace SamsBowling.Plant
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
