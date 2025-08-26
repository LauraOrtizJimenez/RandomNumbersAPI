
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace RandomNumbersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController: ControllerBase
    {
        private readonly System.Random _random;

        public RandomNumberController()
        {
            _random = new System.Random();
        }
        //GET /randomnumber/number/default
        [HttpGet("number/default")]
        public IActionResult GetRandomNumberDefault()
        {
            int result = new Random().Next();
            return Ok(new { type = "integer", result });
        }
        //GET /randomnumber/number/range?min=10&max=50
        [HttpGet("number/range")]
        public IActionResult GetRandomNumberRange([FromQuery] int min=10, [FromQuery] int max=50)
        {
            if (min>max)
            {
                return BadRequest( new{error = "El parámetro 'min' no puede ser mayor que 'max'." });
            }
            int result = new Random().Next(min, max + 1);
            return Ok(new { type = "integer", min, max, result });
        }
        //GET /randomnumber/number/decimal
        [HttpGet("number/decimal")]
        public IActionResult GetRandomNumberDecimal()
        {
            double result = new Random().NextDouble();
            return Ok(new { type = "decimal", result });
        }
        //GET /randomnumber/string?lenght=8
        [HttpGet("string")]
        public IActionResult GetRandomString([FromQuery] int length = 8)
        {
            if (length < 1 || length > 1024)
            {
                return BadRequest(new { error = "El parámetro 'length' debe estar entre 1 y 1024." });
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[_random.Next(chars.Length)]);
            }
            string result = sb.ToString();
            return Ok(new { type = "string", length, result });
        }
    }
}

