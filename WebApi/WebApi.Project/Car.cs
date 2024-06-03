using System.ComponentModel.DataAnnotations;

namespace WebApi.Project
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        public int CarMakeId { get; set; }
        public CarMake? Make { get; set; }
        public string? CarModel { get; set; }
        public int Horsepower { get; set; }
        public int YearOfMake { get; set; }
        public int Mileage { get; set; }

        public Car() { }
        public Car(int id, CarMake? carMake, string? carModel, int horsepower, int yearOfMake, int mileage)
        {
            Id = id;
            Make = carMake;
            CarModel = carModel;
            Horsepower = horsepower;
            YearOfMake = yearOfMake;
            Mileage = mileage;
        }

        public Car(int id, string? carModel, int horsepower, int yearOfMake, int mileage)
        {
            Id = id;
            CarModel = carModel;
            Horsepower = horsepower;
            YearOfMake = yearOfMake;
            Mileage = mileage;
        }
    }
}
