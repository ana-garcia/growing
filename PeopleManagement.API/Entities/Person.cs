using System.ComponentModel.DataAnnotations;

namespace PeopleManagement.API.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        public ICollection<Registration> Registrations { get; set; }
            = new List<Registration>();

        public Person(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }   
    }
}
