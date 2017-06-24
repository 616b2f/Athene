
using System;
using System.Security.Claims;
using Athene.Abstractions.Models;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Athene.Inventory.Web.Services
{
    public static class TestData
    {
        public static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            /* Board of education */
            var boardOfEducation = new BoardOfEducation("Landrat");

            /* School addresses */
            var schhol1Address = new Address("Musterstraße 1", "88451", "Dettingen a. d. Iller", "DE");
            var schhol2Address = new Address("Musterstraße 2", "88451", "Dettingen a. d. Iller", "DE");

            /* Schools */
            var school1 = new School("Schule 1", "SC1", schhol1Address, boardOfEducation);
            var school2 = new School("Schule 2", "SC2", schhol2Address, boardOfEducation);

            /* school classes */
            var schoolClass1 = new SchoolClass("BF1", school1);
            var schoolClass2 = new SchoolClass("BF2", school1);
            var schoolClass3 = new SchoolClass("BR2", school2);
            var schoolClass4 = new SchoolClass("BE2", school2);
            school1.SchoolClasses.Add(schoolClass1);
            school1.SchoolClasses.Add(schoolClass2);
            school2.SchoolClasses.Add(schoolClass3);
            school2.SchoolClasses.Add(schoolClass4);

            var adminUser = new ApplicationUser
            {
                UserName = "admin@athene.com",
                Email =  "admin@athene.com",
                Surname = "Max",
                Lastname = "Mustermann",
                Birthsday = new DateTime(1988, 1, 1),
            };
            var librarianUser = new ApplicationUser
            {
                UserName = "u.musterfrau@athene.com",
                Email =  "u.musterfrau@athene.com",
                Surname = "Uwe",
                Lastname = "Musterfrau",
                Birthsday = new DateTime(1989, 1, 1),
            };

            /* students */
            var student1 = new ApplicationUser
            {
                UserName = "teststudent1@athene.com",
                Email =  "teststudent1@athene.com",
                Surname = "Rudolf",
                Lastname = "Mustermann",
                Gender = Gender.Male,
                Birthsday = new DateTime(1988, 1, 1),
                Student = new Student {
                   StudentId = "43245BD",
                   SchoolClass = schoolClass1
                },
            };
            var student2 = new ApplicationUser
            {
                UserName = "teststudent2@athene.com",
                Email =  "teststudent2@athene.com",
                Surname = "Anna",
                Lastname = "Mustermann",
                Gender = Gender.Female,
                Birthsday = new DateTime(1978, 3, 1),
                Student = new Student {
                   StudentId = "43235BD",
                   SchoolClass = schoolClass3
                },
            };

            var librarianCLaim = new Claim(ClaimTypes.Role, "Librarian");
            var adminCLaim = new Claim(ClaimTypes.Role, "Administrator");
            var studentClaim = new Claim(ClaimTypes.Role, "Student");

            var res1 = userManager.CreateAsync(adminUser, "Admin123!").Result;
            var res2 = userManager.CreateAsync(librarianUser, "Test123!").Result;
            var res3 = userManager.CreateAsync(student1, "Test123!").Result;
            var res4 = userManager.CreateAsync(student2, "Test123!").Result;

            userManager.AddClaimAsync(adminUser, adminCLaim);
            userManager.AddClaimAsync(adminUser, librarianCLaim);
            userManager.AddClaimAsync(librarianUser, librarianCLaim);
            userManager.AddClaimAsync(student1, studentClaim);
            userManager.AddClaimAsync(student2, studentClaim);
        }
    }
}