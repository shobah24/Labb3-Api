using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webb_API.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PhoneNumber { get; set; }

        // navigation properties
      
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //[JsonIgnore]
        //public ICollection<Link> Links { get; set; } = new List<Link>();

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonIgnore]
        public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();

    }
}
