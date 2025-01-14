using SimulationTask2.Models.Base;

namespace SimulationTask2.Models
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string imageUrl { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

    }
}
