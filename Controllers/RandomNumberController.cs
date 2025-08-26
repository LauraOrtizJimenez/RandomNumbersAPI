
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using RandomNumbersAPI.Models;
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
            int result = _random.Next();
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
            int result = _random.Next(min, max + 1);
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
        // POST /api/randomnumber/custom
        [HttpPost("custom")]
        public IActionResult GetCustomRandom([FromBody] RandomRequest request)
        {
            if (request.Type == null)
                return BadRequest(new { error = "Debe especificar el parámetro 'type'." });

            switch (request.Type.ToLower())
            {
                case "number":
                    int min = request.Min ?? 0;
                    int max = request.Max ?? 100;
                    if (min > max)
                        return BadRequest(new { error = "'min' no puede ser mayor que 'max'." });

                    int numberResult = _random.Next(min, max + 1);
                    return Ok(new { result = numberResult });

                case "decimal":
                    double decimalResult = _random.NextDouble();
                    // Redondeamos según request.Decimals
                    decimalResult = Math.Round(decimalResult, request.Decimals);
                    return Ok(new { result = decimalResult });

                case "string":
                    int length = request.Length;
                    if (length < 1 || length > 1024)
                        return BadRequest(new { error = "'length' debe estar entre 1 y 1024." });

                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var sb = new StringBuilder(length);
                    for (int i = 0; i < length; i++)
                        sb.Append(chars[_random.Next(chars.Length)]);

                    string stringResult = sb.ToString();
                    return Ok(new { result = stringResult });

                default:
                    return BadRequest(new { error = "El tipo debe ser 'number', 'decimal' o 'string'." });
            }
        }
    }
}

