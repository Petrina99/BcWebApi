using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public static List<Car> cars = new List<Car>();

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
            List<int> ids = new List<int>();

            foreach (Car car in cars) 
            {
                ids.Add(car.Id);
            }

            int newId = 0;

            if (ids.Count > 0) {
                newId = ids.Max() + 1;
            }

            if (newCar == null)
            {
                return BadRequest();
            } else
            {
                newCar.Id = newId;
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
