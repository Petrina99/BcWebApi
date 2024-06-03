using System.ComponentModel.DataAnnotations;

namespace WebApi.Project
{
    public class CarMake
    {
        [Required]
        public int Id { get; set; }
        public string? MakeName { get; set; }
        public string? MakeCountry { get; set; }

        public bool IsActive { get; set; }
        public List<Car>? Cars { get; } = new List<Car>();
        public CarMake() { }
        public CarMake(int id, string? makeName, string? makeCountry)
        {
            Id = id;
            MakeName = makeName;
            MakeCountry = makeCountry;
        }
    }
}
