using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleManagement.API.Entities
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Entry { get; set; }
        [Required]
        [MaxLength(200)]
        public string Motive { get; set; }
        [Required]
        public int ExpectedDuration { get; set; }
        public DateTime? Exit { get; set; }
        public Boolean IsActive { get; set; }
        [ForeignKey("PersonId")]
        public Person? Person { get; set; }
        public int PersonId { get; set; }

        public Registration(DateTime entry, string motive, int expectedDuration)
        {
            Entry = entry;
            Motive = motive;
            ExpectedDuration = expectedDuration;
            IsActive = true;
        }
    }
}
