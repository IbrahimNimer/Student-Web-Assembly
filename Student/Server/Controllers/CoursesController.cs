using Microsoft.AspNetCore.Mvc;
using Student.Server.Repository;
using Student.Shared;

namespace Student.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseContext _courseContext;

        public CoursesController(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        // GET: api/courses
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var courses = _courseContext.Select().Execute();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/courses
        [HttpPost]
        public IActionResult Add([FromBody] CourseModel course)
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }

            try
            {
                var result = _courseContext
                    .Insert()
                    .WithFields(m => m.Exclude(f => f.Id))
                    .Execute(course, returnNewRecord: true);

                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CourseModel course)
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }

            try
            {
                var updateResult = _courseContext
                    .Update()
                    .Where(m => m.Eq(f => f.Id, id))
                    .WithFields(m => m.ExcludeAll().FromField(f => f.Name).FromField(f => f.Description))
                    .Execute(course);

                if (updateResult == null)
                {
                    return NotFound($"Course with id {id} not found.");
                }

                var updatedCourse = _courseContext.Select().Where(m => m.Eq(f => f.Id, id)).Execute().FirstOrDefault();
                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _courseContext.Delete().Where(m => m.Eq(f => f.Id, id)).Execute();

                if (result == 0)
                {
                    return NotFound($"Course with id {id} not found.");
                }

                return NoContent(); // Return 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
