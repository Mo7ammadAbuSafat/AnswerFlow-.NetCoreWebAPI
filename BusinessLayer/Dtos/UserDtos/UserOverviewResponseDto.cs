﻿using BusinessLayer.DTOs.ImageDtos;

namespace BusinessLayer.DTOs.UserDtos
{
    public class UserOverviewResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ImageResponseDto Image { get; set; }
    }
}
