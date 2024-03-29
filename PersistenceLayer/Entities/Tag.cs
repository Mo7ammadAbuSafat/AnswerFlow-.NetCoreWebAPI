﻿namespace PersistenceLayer.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceLink { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}