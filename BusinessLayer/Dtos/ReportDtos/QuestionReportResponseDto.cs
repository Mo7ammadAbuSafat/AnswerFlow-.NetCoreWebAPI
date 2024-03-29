﻿using BusinessLayer.DTOs.UserDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.ReportDtos
{
    public class QuestionReportResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int QuestionId { get; set; }
        public UserOverviewResponseDto User { get; set; }
        public ReportStatus Status { get; set; }
    }
}
