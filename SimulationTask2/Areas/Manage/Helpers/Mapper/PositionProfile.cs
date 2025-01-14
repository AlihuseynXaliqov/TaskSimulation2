using AutoMapper;
using SimulationTask2.Areas.Manage.DTOS.Position;
using SimulationTask2.Models;

namespace SimulationTask2.Areas.Manage.Helpers.Mapper
{
    public class PositionProfile:Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionDto,Position>().ReverseMap();
        }
    }
}
