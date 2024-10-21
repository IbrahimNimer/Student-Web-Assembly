using Microsoft.AspNetCore.Mvc;
using Student.Server.Repository;
using Student.Shared;

namespace Student.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentContext _enrollmentContext;

        public EnrollmentsController(EnrollmentContext enrollmentContext)
        {
            _enrollmentContext = enrollmentContext;
        }

        // GET: api/enrollments
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var enrollments = _enrollmentContext.Select().Execute();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    
        // POST: api/enrollments
        [HttpPost]
        public IActionResult Add([FromBody] EnrollmentModel enrollment)
        {
            if (enrollment == null)
            {
                return BadRequest("Enrollment data is null.");
            }

            try
            {
                // Insert logic here
                var result = _enrollmentContext
                    .Insert()
                    .WithFields(m => m.Exclude(f => f.Id))
                    .Execute(enrollment, returnNewRecord: true);

                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/enrollments/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _enrollmentContext.Delete().Where(m => m.Eq(f => f.Id, id)).Execute();

                if (result == 0)
                {
                    return NotFound($"Enrollment with id {id} not found.");
                }

                return Ok($"Enrollment with id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
