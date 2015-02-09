namespace XavSpace.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using XavSpace.DataAccess.DbContexts;
    using XavSpace.Entities.Data;
    using XavSpace.Entities.Users;

    internal sealed class Configuration : DbMigrationsConfiguration<XavSpace.DataAccess.DbContexts.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XavSpace.DataAccess.DbContexts.ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
        }


        #region Identity
        private static void InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //Create an Admin role if it does not already exist
            const string roleName = "Admin";

            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var trole = roleManager.FindByName("Tester");
            if (trole == null)
            {
                trole = new IdentityRole("Tester");
                var roleresult = roleManager.Create(trole);
            }


            // add a user ...
            const string name = "admin@xavspace.com";
            const string password = "adminpassword";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }
            // ... and make him the admin
            var rolesForUser = userManager.GetRoles(user.Id);
                        // var role = roleManager.FindByName(roleName);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }


            // add more default users for testing
            string uname = "moderator@xavspace.com", pwd = "moderatorpassword";
            var newUser = userManager.FindByName(uname);
            if (newUser == null)
            {
                newUser = new Moderator { UserName = uname, Email = uname, Post = "Test Mod" };
                var result = userManager.Create(newUser, pwd);
            }
            uname = "staff@xavspace.com";
            pwd = "staffpassword";
            newUser = userManager.FindByName(uname);
            if (newUser == null)
            {
                newUser = new Staff { UserName = uname, Email = uname, Post = "Test Staff" };
                var result = userManager.Create(newUser, pwd);
            }
            uname = "user@xavspace.com";
            pwd = "userpassword";
            newUser = userManager.FindByName(uname);
            if (newUser == null)
            {
                newUser = new ApplicationUser { UserName = uname, Email = uname };
                var result = userManager.Create(newUser, pwd);
            }


            uname = "tester@xavspace.com";
            pwd = "testerpassword";
            newUser = userManager.FindByName(uname);
            if (newUser == null)
            {
                newUser = new ApplicationUser { UserName = uname, Email = uname };
                var result = userManager.Create(newUser, pwd);
            }
            // add to tester role
            var rolesForTester = userManager.GetRoles(newUser.Id);
            if (!rolesForTester.Contains(trole.Name))
            {
                var result = userManager.AddToRole(user.Id, trole.Name);
            }
        }
        #endregion
    }
}
