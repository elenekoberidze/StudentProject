using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentProject.Services
{
    public class StudentManager
    {
        private readonly List<Student> students = [];
        public event Action<Student>? StudentAdded;
        public void AddStudent(Student student)
        {
            if (students.Any(s => s.RollNumber == student.RollNumber))
            {
                throw new ArgumentException($"Student with roll number {student.RollNumber} already exists.");
            }
            students.Add(student);
            StudentAdded?.Invoke(student);
        }

        public IEnumerable<Student> GetAllStudents() => students;

        public Student? FindStudent(int rollNumber)
        {
            return students.FirstOrDefault(s => s.RollNumber == rollNumber);
        }

        public void UpdateStudentGrade(int rollNumber, char newGrade)
        {
            var student = FindStudent(rollNumber) ?? throw new ArgumentException($"Student with roll number {rollNumber} not found.");
            student.UpdateGrade(newGrade);
        }


        public void SaveStudentsToFile()
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "students.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using(FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, students);
            }

        }

        public void LoadStudentsFromFile()
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "students.xml");

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("The students.xml file does not exist.",filePath);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using(FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                students = (List<Student>)serializer.Deserialize(fs);
            }

        }
    }
}
