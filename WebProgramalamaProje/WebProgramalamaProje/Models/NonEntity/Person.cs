using System.ComponentModel.DataAnnotations;

namespace WebProgramalamaProje.Models.NonEntity
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TCNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
