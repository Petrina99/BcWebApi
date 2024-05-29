using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        static Car car1 = new Car(1, "BMW", "M5", 500, 2022, 120000);
        static Car car2 = new Car(2, "Mercedes", "C class", 130, 2016, 200000);
        static Car car3 = new Car(3, "Suzuki", "Swift", 90, 2023, 12000);
        static Car car4 = new Car(4, "Audi", "A6", 270, 2019, 189400);

        public static List<Car> cars = new List<Car> { car1, car2, car3, car4 };

        [HttpGet]
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

        [HttpGet("id")]
        public IActionResult GetCar(int id)
        {
            Car foundCar = cars.Find(x => x.Id == id);
            if (cars == null)
            {
                return NotFound($"Car with id {id} not found");
            }
            else
            {
                return Ok(foundCar);
            }
        }

        [HttpGet("make")]
        public IActionResult GetAllWithMake(string make)
        {
            List<Car> filteredCars = new List<Car>();

            foreach(Car car in cars)
            {
                if (car.CarMake == make)
                {
                    filteredCars.Add(car); 
                }
            }

            if (filteredCars == null)
            {
                return NotFound();
            } else
            {
                return Ok(filteredCars);
            }
        }

        [HttpPost]
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

        [HttpPut]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
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

        [HttpDelete]
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
