﻿namespace SimulationTask2.Areas.Manage.DTOS.Member
{
    public class CreateMemberDto
    {
        public string Name { get; set; }
        public string imageUrl { get; set; }

        public int PositionId { get; set; }
        public IFormFile formFile { get; set; }
    }
}