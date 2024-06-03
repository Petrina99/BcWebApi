using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;

namespace WebApi.Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private IConfiguration Configuration;
        
        public CarController(IConfiguration _configuration) 
        { 
            this.Configuration = _configuration;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            string connString = this.Configuration.GetConnectionString("db");
            var conn = new NpgsqlConnection(connString);

            conn.Open();

            using var cmd1 = new NpgsqlCommand(connString, conn);

            cmd1.CommandText = $"SELECT * FROM \"CarMake\"";
            using var reader1 = cmd1.ExecuteReader();

            var makeResult = new List<CarMake>();

            while (reader1.Read())
            {
                try
                {
                    makeResult.Add(
                       new CarMake(
                           id: (int)reader1[0],
                           makeName: reader1[1].ToString(),
                           makeCountry: reader1[2].ToString()
                       )
                    );
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            conn.Close();

            conn.Open();

            using var cmd2 = new NpgsqlCommand(connString, conn);
            cmd2.CommandText = $"SELECT * FROM \"Car\"";
            
            using var reader2 = cmd2.ExecuteReader();

            var carResult = new List<Car>();

            while(reader2.Read())
            {
                try
                {
                    int carMakeId = (int)reader2["CarMakeId"];
                    CarMake make = makeResult.Find(x => x.Id == carMakeId);

                    carResult.Add(
                        new Car(
                            id: (int)reader2[0],
                            carMake: make,
                            carModel: reader2[1].ToString(),
                            yearOfMake: (int)reader2[2],
                            mileage: (int)reader2[3],
                            horsepower: (int)reader2[4]
                        )
                    );
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            conn.Close();
            if (carResult == null)
            {
                return NotFound("Cars not found");
            } else
            {
                return Ok(carResult);
            }
        }
        
        [HttpPost]
        public IActionResult CreateCar([FromBody] Car newCar)
        {
            string connString = Configuration.GetConnectionString("db");
            var conn = new NpgsqlConnection(connString);

            conn.Open();
            using var cmd = new NpgsqlCommand(connString, conn);
            cmd.CommandText = "INSERT INTO \"Car\" (\"CarModel\", \"YearOfMake\"," +
                " \"Mileage\", \"Horsepower\", \"CarMakeId\") values (@model, @yearOfMake, " +
                "@mileage, @horsepower, @makeId)";

            cmd.Parameters.AddWithValue("model", NpgsqlTypes.NpgsqlDbType.Text ,newCar.CarModel);
            cmd.Parameters.AddWithValue("yearOfMake", NpgsqlTypes.NpgsqlDbType.Integer, newCar.YearOfMake);
            cmd.Parameters.AddWithValue("mileage", NpgsqlTypes.NpgsqlDbType.Integer,newCar.Mileage);
            cmd.Parameters.AddWithValue("horsepower", NpgsqlTypes.NpgsqlDbType.Integer, newCar.Horsepower);
            cmd.Parameters.AddWithValue("makeId", NpgsqlTypes.NpgsqlDbType.Integer, newCar.CarMakeId);

            int commits = cmd.ExecuteNonQuery();

            conn.Close();
            if (commits == 0)
            {
                return BadRequest("Error adding a car");
            }
            else
            {
               
                return Ok(newCar);
            }
        }

        [HttpDelete]
        public IActionResult DeleteCar(int id)
        {
            string connString = Configuration.GetConnectionString("db");
            var conn = new NpgsqlConnection(connString);

            conn.Open();
            using var cmd = new NpgsqlCommand(connString, conn);

            cmd.CommandText = $"DELETE FROM \"Car\" WHERE \"Id\" = {id}";
            int commits = cmd.ExecuteNonQuery();

            if (commits == 0)
            {
                return BadRequest();
            } 
            else
            {
                return Ok();
            }
        }
        /*
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
                return NotFound($"No cars found with {make} make");
            } else
            {
                return Ok(filteredCars);
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
                return Ok(carToUpdate);
            }
        }

        
        */
    }

}
