using EFApproaches.DAL.Entities;
using EFCodeFirstTest.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Xml.Linq;

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
        #region APIMethods
        //http://localhost:65037/api/student , it also works with HttpResponseMessage,return Request.CreateResponse(HttpStatusCode.OK,students);
        public IHttpActionResult Get()
        {
            //Creates an OKNegotiationAction Result, which implements IHttpActionResult
            return Ok(Students);
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
        [ActionName("GetScotts")]
        [Route("api/student/Scotts")]
        public IHttpActionResult GetScotts()
        {
            IEnumerable<string> studentNames = from s in Students
                                          where s.FirstMidName == "Scott"
                                          select s.FullName;
            return Ok(studentNames);
        }


        [Route("api/student/name/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            IEnumerable<Student> studentsByName = from s in Students
                                          where s.FirstMidName == name
                                          select s;
            return Ok(studentsByName);
        }

        /// <summary>
        /// Return the public types that are being executed
        /// </summary>
        /// <returns></returns>
        [Route("api/student/types")]
        public IHttpActionResult GetAssemblyPublicTypes()
        {
            return Ok(QueryTypes());
        }
        /// <summary>
        /// LINQ deferred execution test
        /// </summary>
        /// <returns></returns>
        [Route("api/student/deferEx")]
        public IHttpActionResult GetElementsUsingReferredExecution()
        {

            //The LINQ expression is just the declaration, not execution
            IEnumerable<string> studentNames = from s in Students
                                               let fullName = s.FirstMidName + " " + s.LastName
                                               where fullName == "Scott"
                                               select fullName;
            //we can add data to the collection after the LINQ
            Students.Add(new Student() {  FirstMidName = "Scott", LastName = "James", EmailAddress= "sjames@school.com"});
            //And LINQ should include it when is executed
            return Ok(studentNames);
        }
        [Route("api/student/linqxml")]
        public IHttpActionResult GetLINQAndXML()
        {
            //create XML document
            XDocument xmlDocument = new XDocument(
                /*we can embed a LINQ query inside of the element*/
                new XElement("Processes",
                    from p in Process.GetProcesses()
                    
                    orderby p.ProcessName ascending
                    /*what we are going to do with the select is an actual projection*/
                    select new XElement("Process",
                        new XAttribute("Name", p.ProcessName),
                        new XAttribute("PID", p.Id)
                        )
                    )
                );
            return Ok(xmlDocument.ToString());
        }
        [Route("api/student/LinqXmlExtMethod")]
        public IHttpActionResult GetLINQAndXMLUsingExtensionMethod()
        {
            //create XML document
            XDocument xmlDocument = new XDocument(
                /*using extension methods to build the XML file from the processed list*/
                new XElement("Processes", 
                Process.GetProcesses().Where(p => p.Id > 16000).OrderBy(p => p.ProcessName).OrderByDescending(p => p.Id).Select(p =>
                        new XElement("Process",
                        new XAttribute("Name", p.ProcessName),
                        new XAttribute("PID", p.Id))
                    )
                ));
            return Ok(xmlDocument.ToString());
        }
        #endregion
        #region private methods
        private static IEnumerable<string> QueryTypes()
        {
            IEnumerable<string> publicTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsPublic
                select t.FullName;
            return publicTypes;
        }
        #endregion
    }
}
