using System.ComponentModel.DataAnnotations;

namespace WebApi.Project
{
    public class Car
    {
        [Required]
        public int Id { get; set; }

        public string? CarMake { get; set; }

        public string? CarModel { get; set; }

        public int Horsepower { get; set; }
        public int YearOfMake { get; set; }
        public int Mileage { get; set; }

        public Car() { }
        public Car(int id, string? carMake, string? carModel, int horsepower, int yearOfMake, int mileage)
        {
            Id = id;
            CarMake = carMake;
            CarModel = carModel;
            Horsepower = horsepower;
            YearOfMake = yearOfMake;
            Mileage = mileage;
        }
    }
}
