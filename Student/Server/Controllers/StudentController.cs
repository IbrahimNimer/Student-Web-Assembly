using Microsoft.AspNetCore.Mvc;
using Student.Server.Repository;
using Student.Shared;

namespace Student.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _studentContext;

        public StudentsController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        // GET: api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var students = _studentContext.Select().Execute();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/students
        [HttpPost]
        public IActionResult Add([FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            try
            {
                var result = _studentContext
                    .Insert()
                    .WithFields(m => m.Exclude(f => f.Id)) // Exclude Id from being inserted
                    .Execute(student, returnNewRecord: true);

                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            try
            {
                var updateResult = _studentContext
                    .Update()
                    .Where(m => m.Eq(f => f.Id, id))
                    .WithFields(m => m.ExcludeAll().FromField(f => f.Name).FromField(f => f.Address))
                    .Execute(student);

                if (updateResult == null)
                {
                    return NotFound($"Student with id {id} not found.");
                }

                // Query the database again to confirm the update
                var updatedStudent = _studentContext.Select().Where(m => m.Eq(f => f.Id, id)).Execute().FirstOrDefault();

                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _studentContext
                    .Delete()
                    .Where(m => m.Eq(f => f.Id, id))
                    .Execute();

                if (result == 0)
                {
                    return NotFound($"Student with id {id} not found.");
                }

                return Ok($"Student with id {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
