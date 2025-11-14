using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using var writer = new StreamWriter("students.txt");
            foreach (var student in students)
            {
                writer.WriteLine($"{student.Name},{student.RollNumber},{student.Grade}");
            }
        }
        public void LoadStudentsFromFile()
        {
            if (!File.Exists("students.txt")) return;
            foreach (var line in File.ReadAllLines("students.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length != 3) continue;
                string name = parts[0];
                int roll = int.Parse(parts[1]);
                char grade = char.Parse(parts[2]);
                students.Add(new Student(name, roll, grade));
            }
        }
    }
}
