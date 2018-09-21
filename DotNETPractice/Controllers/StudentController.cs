using EFApproaches.DAL.Entities;
using EFCodeFirstTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotNETPractice.Controllers
{
    /// <summary>
    /// Student class to check out how an API Controller works
    /// </summary>
    public class StudentController : ApiController
    {
        private List<Student> students;
            public List<Student> Students {
            get
            {
                if (students == null)
                {
                    students = DataHelper.GenerateStudentsList();
                }
                return students;
            }
        }

        //http://localhost:65037/api/student , it also works with HttpResponseMessage,return Request.CreateResponse(HttpStatusCode.OK,students);
        public IHttpActionResult Get()
        {
            //Creates an OKNegotiationAction Result, which implements IHttpActionResult
            return Ok(Students);
        }
        
        [ActionName("GetScotts")]
        [Route("api/student/Scotts")]
        public IHttpActionResult GetScotts()
        {
            IEnumerable<Student> scotts = from s in Students
                                          where s.FirstMidName == "Scott"
                                          select s;
            return Ok(scotts);
        }


        [Route("api/student/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            IEnumerable<Student> studentsByName = from s in Students
                                          where s.FirstMidName == name
                                          select s;
            return Ok(studentsByName);
        }

        //http://localhost:65037/api/student?id=3
        public IHttpActionResult Get(int id)
        {
            var student = Students.FirstOrDefault(s => id == s.ID);
            try
            {
                if (null == student)
                {
                    return Content(HttpStatusCode.NotFound, "Student not found");//NotFound(); does not give a message
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest();
                
            }
        }
    }
}
