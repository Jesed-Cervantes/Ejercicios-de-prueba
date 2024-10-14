using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        [HttpGet("{n:int}")]
        public IActionResult GetFibonacci(int n)
        {
            if (n < 0)
                return BadRequest("El valor de n debe ser mayor o igual a 0.");

            var fibonacciSequence = CalculateFibonacci(n);
            return Ok(fibonacciSequence);
        }

        private List<int> CalculateFibonacci(int n)
        {
            var sequence = new List<int> { 0, 1 };

            for (int i = 2; i < n; i++)
            {
                int next = sequence[i - 1] + sequence[i - 2];
                sequence.Add(next);
            }

            return sequence.Take(n).ToList(); 
        }
    }
}
