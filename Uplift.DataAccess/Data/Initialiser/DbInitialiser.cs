using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.Models;
using Uplift.Utility;
using UpliftUdemy.DataAccess.Data;

namespace Uplift.DataAccess.Data.Initialiser
{
    public class DbInitialiser : IDbInitialiser
    {
        //Dependancy Injection
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitialiser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public void Initialise()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)    //any pending migrations?
                {
                    _db.Database.Migrate();                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (_db.Roles.Any(r => r.Name == SD.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();//makes sure this executes before proceceding with anything else
            _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser        //can also use IDENTITYUSER or applicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name = "TaiWai"
                //can set other properties, this is for the initial setup
            }, "Abc123!").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult(); //single user obj so make sure it is role not roles
       

        }
    }
}
