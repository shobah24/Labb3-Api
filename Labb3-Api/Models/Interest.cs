using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webb_API.Models
{
    public class Interest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;


        // navigation properties

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //[JsonIgnore]
        //public ICollection<Link> Links { get; set; } = new List<Link>();

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonIgnore]
        public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();


    }
}
