namespace BusinessLayer.DTOs.StatisticsDtos
{
    public class UsersStatisticsResponseDto
    {
        public int UsersCount { get; set; }
        public int LastMonthAddedUsersCount { get; set; }
        public int ExpertUsersCount { get; set; }
        public int LastMonthAddedExpertsCount { get; set; }

    }
}
