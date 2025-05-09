namespace webb_API.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int InterestId { get; set; }
        public int PersonId { get; set; }
        public PersonInterest PersonInterest { get; set; }


        
    }
}
