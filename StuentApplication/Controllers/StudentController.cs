// StudentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuentApplication.Data;
using StuentApplication.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using StuentApplication.Repository;

namespace StuentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository studentRepository;
        public StudentController(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Student> students = studentRepository.GetStudents();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Student student = studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            studentRepository.CreateStudent(student);
            return Ok(student);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student updatedStudent)
        {
            studentRepository.UpdateStudent(id, updatedStudent);
            return Ok(updatedStudent);
        }

        [HttpPatch("{id}")]

        public IActionResult Patch(int id, [FromBody] Student student)
        {

            studentRepository.PatchStudent(id, student);
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            studentRepository.DeleteStudent(id);
            return Ok();
        }

        [HttpGet("search")]
        public IActionResult Search(string searchItem)
        {
            if (searchItem == null)
            {
                return NotFound();
            }

            List<Student> matchingStudents = studentRepository.searchStudents(searchItem);
            return Ok(matchingStudents);
        }


        [HttpGet("GroupByDepartment")]
        public IActionResult GetStudentGroupByDepartment()
        {
            var studentsList = studentRepository.GroupByDepartment();
            return Ok(studentsList);
        }
    }
}

