﻿using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class QuestionReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ReportStatus Status { get; set; }
    }
}
