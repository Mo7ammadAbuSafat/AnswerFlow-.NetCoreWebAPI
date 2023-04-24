namespace BusinessLayer.DTOs.StatisticsDtos
{
    public class QuestionsStatisticsResponseDto
    {
        public int Count { get; set; }
        public int LastMonthQuestionsCount { get; set; }
        public int OpenQuestionsCount { get; set; }
        public int ClosedQuestionsCount { get; set; }
    }
}
