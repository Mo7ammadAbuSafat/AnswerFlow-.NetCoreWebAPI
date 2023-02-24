namespace BusinessLayer.DTOs.outgoing
{
    public class UserOverviewDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ImageDto? Image { get; set; }
    }
}
