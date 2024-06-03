using System.ComponentModel.DataAnnotations;

namespace WebApi.Project
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        public int CarMakeId { get; set; }
        public string? CarModel { get; set; }
        public int Horsepower { get; set; }
        public int YearOfMake { get; set; }
        public int Mileage { get; set; }
        public Boolean isActive { get; } = true;

        public Car() { }
        public Car(int id, int carMakeId, string? carModel, int horsepower, int yearOfMake, int mileage)
        {
            Id = id;
            CarMakeId = carMakeId;
            CarModel = carModel;
            Horsepower = horsepower;
            YearOfMake = yearOfMake;
            Mileage = mileage;
        }
    }
}
