using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Services
{
    public class AppController
    {
        private readonly StudentManager studentManager = new();
        public AppController()
        {
            studentManager.StudentAdded += (student) =>
            {
                Console.WriteLine($"Student added: {student.Name}, Roll: {student.RollNumber}");
            };

        }
        public void Run()
        {
            studentManager.LoadStudentsFromFile();
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";
                try
                {
                    switch(choice)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                           ViewAll();
                            break;
                       
                        case "3":
                            studentManager.SaveStudentsToFile();
                            Console.WriteLine("Students saved. Exiting...");
                            return;
                        
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static void ShowMenu()
        {
            Console.WriteLine("\n======= STUDENT MANAGEMENT SYSTEM =======");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student by Roll Number");
            Console.WriteLine("4. Update Student Grade");
            Console.WriteLine("5. Save and Exit");
            Console.WriteLine("6. Exit without Saving");
            Console.Write("Enter choice: ");
        }
        private void AddStudent()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter roll number: ");
            if (!int.TryParse(Console.ReadLine(), out int roll))
                throw new Exception("Invalid roll number.");
            Console.Write("Enter grade (A-F): ");
            char g = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            var stu = new Student(name, roll, g);
            stu.GradeChanged += (s, oldG) =>
            Console.WriteLine($"Event: Grade changed for {s.Name} from {oldG} to {s.Grade}");

            studentManager.AddStudent(stu);

            Console.WriteLine("Student added!");
        }
        private void ViewAll()
        {
            Console.WriteLine("\n--- ALL STUDENTS ---");
            foreach (var s in studentManager.GetAllStudents()) ;
               
        }
    }
}
