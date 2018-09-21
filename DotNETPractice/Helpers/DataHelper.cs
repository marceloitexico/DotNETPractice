using EFApproaches.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFCodeFirstTest.Helpers
{
    public static class DataHelper
    {
        /// <summary>
        ///generate list of data in memory
        /// </summary>
        /// <returns></returns>
        public static List<Student> GenerateStudentsList()
        {
            var data = new List<Student>
            {
                new Student {ID = 1, FirstMidName = "Scott", LastName = "Eldridge", EmailAddress  = "neldridge@domain.com",  EnrollmentDate = new DateTime(2018,2,20)},
                new Student {ID = 2, FirstMidName = "Scott", LastName = "Lakhani", EmailAddress  = "slakhani@domain.com" ,  EnrollmentDate = new DateTime(2018,3,21)},
                new Student {ID = 3, FirstMidName = "Camille", LastName = "Lozerone", EmailAddress  = "clozerone@domain.com",  EnrollmentDate = new DateTime(2018,4,22) },
                new Student {ID = 4, FirstMidName = "John", LastName = "Papa", EmailAddress  = "jpapa@domain.com",  EnrollmentDate = new DateTime(2018,5,23) },
            };
            return data;
        }

        public static List<Course> GenerateCoursesList()
        {
            var data = new List<Course>
            {
                new Course {CourseID = 1, Title = "Computers Architechture II", Credits = 8 },
                new Course {CourseID = 2, Title = "Artificial Intelligence", Credits = 7 },
                new Course {CourseID = 3, Title = "Compilers Theory", Credits = 10 },
                new Course {CourseID = 4, Title = "Operating Systems", Credits = 12 },
                new Course {CourseID = 5, Title = "Graphs Theory II", Credits = 12 },
            };
            return data;
        }

        public static XDocument GenerateXmlData()
        {
            //Convert to XML :
            XDocument xDocument = new XDocument(
            new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("MESSAGE_CONTENT"),
            new XElement("TEXT_MESSAGE"),
            new XElement("MESSAGE_INDEX", 0),
            new XElement("USER_TO"),
            new XElement("CIML")
            );
            //This Works:
            xDocument.Add(new XComment("This is a comment."));
            return xDocument;
        }
    }
}
