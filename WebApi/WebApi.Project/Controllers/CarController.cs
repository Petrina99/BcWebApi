using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public List<Car> cars = new List<Car>();

        Car car1 = new Car(1, "BMW", "M5", 500, 2022, 120000);
        Car car2 = new Car(2, "Mercedes", "C class", 130, 2016, 200000);
        Car car3 = new Car(3, "Suzuki", "Swift", 90, 2023, 12000);
        Car car4 = new Car(4, "Audi", "A6", 270, 2019, 189400);

        public CarController()
        {
            cars.Add(car1);
            cars.Add(car2);
            cars.Add(car3);
            cars.Add(car4);
        }

        [HttpGet(Name = "GetCars")]
        public IActionResult GetCars()
        {
            if (cars == null)
            {
                return NotFound("Cars not found");
            } else
            {
                return Ok(cars);
            }
        }
        
        [HttpPost(Name="CreateCar")]
        public IActionResult CreateCar(Car newCar)
        {
            if (newCar == null)
            {
                return BadRequest();
            } else
            {
                cars.Add(newCar);
                return Ok(cars);
            }
        }

        [HttpPut(Name = "UpdateCar")]
        public IActionResult UpdateCar([FromQuery] int id, [FromBody] Car updatedCar)
        {
            Car carToUpdate = cars.Find(x => x.Id == id);
            if (carToUpdate == null)
            {
                return BadRequest();
            } else
            {
                
                carToUpdate.Id = id;
                carToUpdate.CarMake = updatedCar.CarMake;
                carToUpdate.CarModel = updatedCar.CarModel;
                carToUpdate.Horsepower = updatedCar.Horsepower;
                carToUpdate.YearOfMake = updatedCar.YearOfMake;
                carToUpdate.Mileage = updatedCar.Mileage;
                return Ok(cars);
            }
        }

        [HttpDelete(Name = "DeleteCar")]
        public IActionResult DeleteCar(int id) 
        { 
            Car carToRemove = cars.Find(x  => x.Id == id);
            if (carToRemove == null) { return BadRequest(); }
            else
            {
                cars.Remove(carToRemove);
                return Ok(cars);
            }
        }

    }
}
