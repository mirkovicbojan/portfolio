using TimeSheet.Contexts;
using TimeSheet.Models;

namespace IntegrationTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(TimeSheetContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category
                    {
                        categoryID = 1,
                        categoryName = "Back-End",
                        categoryDescription = "Description test back"
                    },
                    new Category
                    {
                        categoryID = 2,
                        categoryName = "Front-End",
                        categoryDescription = "Description test front"
                    }
                );
            }

            if (!db.Client.Any())
            {
                db.Client.AddRange(
                    new Client
                    {
                        clientID = 1,
                        clientName = "TestName",
                        address = "TestAddress",
                        city = "Test City",
                        country = "Test Country",
                        zip = 1111
                    },
                    new Client
                    {
                        clientID = 2,
                        clientName = "TestName2",
                        address = "TestAddress2",
                        city = "Test City2",
                        country = "Test Country2",
                        zip = 2222
                    }
                );
            }
            if (!db.Member.Any())
            {
                db.Member.AddRange(
                    new Member
                    {
                        memberID = 1,
                        memberName = "Test Member 1",
                        hoursPerWeek = 40,
                        username = "user1",
                        email = "email1@gmail.com",
                        status = 1,
                        role = 1
                    },
                    new Member
                    {
                        memberID = 2,
                        memberName = "Test Member 2",
                        hoursPerWeek = 40,
                        username = "user2",
                        email = "email2@gmail.com",
                        status = 1,
                        role = 1
                    }
                );
            }
            if (!db.Project.Any())
            {
                db.Project.AddRange
                (
                    new Project
                    {
                        projectID = 1,
                        projectDescription = "Project Test Desc 1",
                        projectName = "Test name 1",
                        currentclientID = 1,
                        memberID = 1
                    },
                    new Project
                    {
                        projectID = 2,
                        projectDescription = "Project Test Desc 2",
                        projectName = "Test name 2",
                        currentclientID = 2,
                        memberID = 2
                    }
                );
            }

            if (!db.TimeSheets.Any())
            {
                db.TimeSheets.Add
                (
                    new TimeSheetClass
                    {
                        sheetID = 1,
                        description = "TSheet Test 1",
                        time = 8,
                        overtime = 0,
                        date = Convert.ToDateTime("02/05/2019"),
                        clientID = 1,
                        projectID = 1,
                        categoryID = 1,
                        memberID = 1,
                    }
                );
                db.TimeSheets.Add
                (
                    new TimeSheetClass
                    {
                        sheetID = 2,
                        description = "TSheet Test 2",
                        time = 8,
                        overtime = 0,
                        date = Convert.ToDateTime("02/05/2019"),
                        clientID = 2,
                        projectID = 2,
                        categoryID = 2,
                        memberID = 2,
                    }
                );
            }
            db.SaveChangesAsync();
        }
    }
}