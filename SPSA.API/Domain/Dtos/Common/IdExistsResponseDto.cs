﻿namespace SPSA.API.Domain.Dtos.Common
{
    public class IdExistsResponseDto
    {
        public bool DoesAllIdExists { get; set; }
        public List<long>? NotExistsList { get; set; }
    }
}
