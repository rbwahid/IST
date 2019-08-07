//var seedCompany = new List<Company>()
//            {
//                new Company{ShortName="EIL",Name="Encoders Infotech Limited",Address = "H-121, R-21, Mohakhali DOHS, Dhaka-1206.",Phone="+343 545163",Email="info@encodersbd.com",MobileNo="01751 000000"}
//            };
//seedCompany.ForEach(s => context.Companies.AddOrUpdate(c => c.Name, s));
//            context.SaveChanges();

//            var seedRoles = new List<UserRole>
//            {
//                new UserRole {RoleName = "Administrator", Status = 2},
//                new UserRole {RoleName = "Admin", Status = 1},
//                new UserRole {RoleName = "Manager", Status = 1},
//                new UserRole {RoleName = "Developer", Status = 1 },
//                new UserRole {RoleName = "Customer", Status = 1 },
//            };
//seedRoles.ForEach(s => context.UserRoles.AddOrUpdate(r => r.RoleName, s));
//            context.SaveChanges();
//            var seedRolePermissionList = new List<UserRolePermission>
//            {
//                new UserRolePermission{RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id, Permission = "Global_SupAdmin"},
//            };
//seedRolePermissionList.ForEach(s => context.UserRolePermissions.AddOrUpdate(u => u.Permission, s));
//            context.SaveChanges();
//            #region users seed
//            var users = new List<User>
//            {
//                new User { FullName="Administrator",UserName = "admin",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true, Status=2,LastPassChangeDate = DateTime.Now,PasswordChangedCount=1},//12345
//                new User { FullName="Development",UserName = "dev",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true, Status=2,LastPassChangeDate = DateTime.Now,PasswordChangedCount=1}, //12345

//            };
//users.ForEach(s => context.Users.AddOrUpdate(u => u.UserName, s));
//            context.SaveChanges();
//            #endregion