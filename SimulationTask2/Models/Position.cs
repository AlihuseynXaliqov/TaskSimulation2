using SimulationTask2.Models.Base;

namespace SimulationTask2.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }

        public List<Member> Members { get; set; }
    }
}
