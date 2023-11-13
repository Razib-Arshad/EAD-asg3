// See https://aka.ms/new-console-template for more information
using System.Data.Entity;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        firstMidName = "Atyia",
        lastName = "Alam",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.students.Add(student);
    
    var student1 = new Student
    {
        firstMidName = "Ali",
        lastName = "Ahmed",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.students
                    orderby s.firstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.firstMidName + " " + stdnt.lastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string? lastName { get; set; }
    public string? firstMidName { get; set; }
    public string? email { get; set; }
    public string? phoneNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string? title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment>? enrollments { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course>? courses { get; set; }
    public virtual DbSet<Enrollment>? enrollments { get; set; }
    public virtual DbSet<Student>? students { get; set; }
}


