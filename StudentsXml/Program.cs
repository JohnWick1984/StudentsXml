using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace StudentDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
           
            List<Student> students = GenerateRandomStudents(20);

            
            WriteStudentsToXml(students, "all_students.xml");

           
            WriteStudentsToXml(GetStudentsByGroup(students, "Group1"), "group1_students.xml");

           
            WriteStudentsToXml(GetStudentsWithMaxScholarship(students), "max_scholarship_students.xml");

           
            WriteAverageScholarshipToXml(GetAverageScholarship(students), "average_scholarship.xml");

           
            WriteStudentsToXml(GetStudentsWithLowGrades(students), "low_grades_students.xml");

            Console.WriteLine("XML files generated successfully.");
        }

        
        static List<Student> GenerateRandomStudents(int count)
        {
            Random random = new Random();
            string[] firstNames = { "John", "Alice", "Michael", "Emma", "Daniel" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Taylor", "Wilson" };
            string[] groups = { "Group1", "Group2", "Group3", "Group4" };

            List<Student> students = new List<Student>();

            for (int i = 0; i < count; i++)
            {
                Student student = new Student();
                student.FirstName = firstNames[random.Next(firstNames.Length)];
                student.LastName = lastNames[random.Next(lastNames.Length)];
                student.Group = groups[random.Next(groups.Length)];
                student.Grade = random.Next(1, 6);
                student.Scholarship = random.Next(1500, 20001);
                students.Add(student);
            }

            return students;
        }

      
        static List<Student> GetStudentsByGroup(List<Student> students, string group)
        {
            return students.Where(s => s.Group == group).ToList();
        }

        
        static List<Student> GetStudentsWithMaxScholarship(List<Student> students)
        {
            int maxScholarship = students.Max(s => s.Scholarship);
            return students.Where(s => s.Scholarship == maxScholarship).ToList();
        }

       
        static double GetAverageScholarship(List<Student> students)
        {
            return students.Average(s => s.Scholarship);
        }

       
        static List<Student> GetStudentsWithLowGrades(List<Student> students)
        {
            return students.Where(s => s.Grade <= 3).ToList();
        }

        
        static void WriteStudentsToXml(List<Student> students, string fileName)
        {
            XElement studentsXml = new XElement("Students",
                from student in students
                select new XElement("Student",
                    new XElement("FirstName", student.FirstName),
                    new XElement("LastName", student.LastName),
                    new XElement("Group", student.Group),
                    new XElement("Grade", student.Grade),
                    new XElement("Scholarship", student.Scholarship)
                )
            );

            studentsXml.Save(fileName);
        }

        
        static void WriteAverageScholarshipToXml(double averageScholarship, string fileName)
        {
            XElement averageScholarshipXml = new XElement("AverageScholarship",
                new XElement("Value", averageScholarship)
            );

            averageScholarshipXml.Save(fileName);
        }
    }

  
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public int Grade { get; set; }
        public int Scholarship { get; set; }
    }
}
