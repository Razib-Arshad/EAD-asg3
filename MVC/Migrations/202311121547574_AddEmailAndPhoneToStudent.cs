namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddEmailAndPhoneToStudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                {
                    CourseID = c.Int(nullable: false, identity: true),
                    title = c.String(),
                    Credits = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CourseID);

            CreateTable(
                "dbo.Enrollments",
                c => new
                {
                    EnrollmentID = c.Int(nullable: false, identity: true),
                    CourseID = c.Int(nullable: false),
                    StudentID = c.Int(nullable: false),
                    Grade = c.Int(),
                })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    lastName = c.String(),
                    firstMidName = c.String(),
                    email = c.String(),
                    phoneNumber = c.String(),
                    EnrollmentDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }
        public void InsertStudent(string lastName, string firstMidName, string email, string phoneNumber, DateTime enrollmentDate)
        {
            Sql($"INSERT INTO dbo.Students (lastName, firstMidName, email, phoneNumber, EnrollmentDate) VALUES ('{lastName}', '{firstMidName}', '{email}', '{phoneNumber}', '{enrollmentDate.ToString("yyyy-MM-dd HH:mm:ss")}')");
        }

        public void UpdateStudent(int studentId, string lastName, string firstMidName, string email, string phoneNumber)
        {
            Sql($"UPDATE dbo.Students SET lastName = '{lastName}', firstMidName = '{firstMidName}', email = '{email}', phoneNumber = '{phoneNumber}' WHERE ID = {studentId}");
        }

        public void DeleteStudent(int studentId)
        {
            Sql($"DELETE FROM dbo.Students WHERE ID = {studentId}");
        }
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropTable("dbo.Students");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Courses");
        }
    }
}
