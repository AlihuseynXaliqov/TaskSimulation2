using AutoMapper;
using SimulationTask2.Areas.Manage.DTOS.Member;
using SimulationTask2.Models;

namespace SimulationTask2.Areas.Manage.Helpers.Mapper
{
    public class MemberProfile:Profile
    {
        public MemberProfile()
        {
            CreateMap<CreateMemberDto,Member>().ReverseMap();
            CreateMap<UpdateMemberDto,Member>().ReverseMap();
        }
    }
}
