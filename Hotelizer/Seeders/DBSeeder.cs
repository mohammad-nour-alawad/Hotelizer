using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Seeders
{
    public static class DBSeeder
    {
        public static void SeedUser(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminRole = roleManager.FindByNameAsync("SuperAdmin").Result;

            if (adminRole == null)
            {
                var role = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                }).Result;
                if (role.Succeeded) unitOfWork.context.SaveChanges();

                role = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }).Result;
                if (role.Succeeded) unitOfWork.context.SaveChanges();

                role = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }).Result;
                if (role.Succeeded) unitOfWork.context.SaveChanges();
            }

            var user = userManager.FindByEmailAsync("SuperAdmin@gmail.com").Result;

            if (user == null)
            {
                ApplicationUser result = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "SuperAdmin@gmail.com",
                    FirstName = "SuperAdmin",
                    LastName = "SuperAdmin",
                    EmailConfirmed = true,
                    NormalizedEmail = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin@gmail.com",
                    AccessFailedCount = 100000,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword(null, "Nour@123")
                };

                unitOfWork.UserRepo.Insert(result);
                unitOfWork.Save();

                var ok = userManager.AddToRoleAsync(result, "SuperAdmin").Result;

                if (ok.Succeeded)
                    unitOfWork.Save();
            }

            user = userManager.FindByEmailAsync("SuperAdmin@gmail.com").Result;

            if (!userManager.IsInRoleAsync(user, "SuperAdmin").Result)
            {
                var ok = userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                if (ok.Succeeded)
                {
                    unitOfWork.Save();
                }
            }
        }


        public static void SeedCategories(IUnitOfWork unitOfWork)
        {
            List<string> categories = new List<string>()
            {
                "Single Room", "VIP Room", "Double Room", "Suite"
            };
            List<int> prices = new List<int>()
            {
                1000, 2500, 2000, 5000
            };
            int i = 0;
            foreach (string catName in categories)
            {
                List<Catergory> cats = unitOfWork.CategoryRepo.GetAll(e => e.Name == catName).ToList();
                if (cats.Count == 0)
                {
                    Catergory cat = new Catergory()
                    {
                        Name = catName,
                        ImageUrl = Path.Combine("/images/categories/", catName + ".jpg"),
                        BasePrice = prices[i]
                    };
                    i++;
                    unitOfWork.CategoryRepo.Insert(cat);
                    unitOfWork.Save();
                }
            }
        }
        public static void SeedServices(IUnitOfWork unitOfWork)
        {
            var ok = unitOfWork.HotelRepo.GetAll(e => e.Name == "Hotelizer").ToList().Count;
            if (ok == 0)
                unitOfWork.HotelRepo.Insert(new Hotel()
                {
                    Name = "Hotelizer",
                    Location = "Syria-Damascus"
                });

            List<string> services = new List<string>()
            {
                "Breakfast", "Full Meals", "Light Laundry", "Medium Laundry", "Heavy Laundry", "Transport", "Pool"
            };

            foreach (string serviceName in services)
            {
                List<Service> servs = unitOfWork.ServiceRepo.GetAll(e => e.Name == serviceName).ToList();
                if (servs.Count == 0)
                {
                    Service cat = new Service()
                    {
                        Name = serviceName,
                        ImageUrl = Path.Combine("/images/services/", serviceName + ".jpg")
                    };
                    unitOfWork.ServiceRepo.Insert(cat);
                    unitOfWork.Save();
                }
            }
        }



        public static void SeedServiceCategories(IUnitOfWork unitOfWork)
        {
            List<string> services = new List<string>() {
                "Breakfast", "Full Meals", "Light Laundry", "Medium Laundry", "Heavy Laundry", "Transport", "Pool"
            };
            List<string> categories = new List<string>()
            {
                "Single Room", "VIP Room", "Double Room", "Suite"
            };
            int[,] prices = {
                                { 150000, 175000, 10000, 15000, 20000, 0, 50000 },
                                { 200000, 250000, 25000, 50000, 75000, 50000, 75000},
                                { 250000,300000,20000,30000,40000,0,100000 },
                                { 350000,400000,40000,60000,800000,0,20000 }
                            };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (prices[i, j] != 0)
                    {
                        var service = unitOfWork.ServiceRepo.GetAll(e => e.Name == services[j]).ToList();
                        var category = unitOfWork.CategoryRepo.GetAll(e => e.Name == categories[i]).ToList();
                        var servCat = unitOfWork.ServiceCategoryRepo.GetAll(e => e.ServiceId == service[0].Id && e.CategoryId == category[0].Id).ToList();
                        if (servCat.Count == 0)
                        {
                            unitOfWork.ServiceCategoryRepo.Insert(new ServiceCategory()
                            {
                                CategoryId = category[0].Id,
                                ServiceId = service[0].Id,
                                Cost = prices[i, j]
                            });
                            unitOfWork.Save();
                        }
                    }
                }
            }

        }


        public static void SeedSpecifications(IUnitOfWork unitOfWork)
        {
            List<string> specifications = new List<string>()
            {
                "Sea View", "Mountain View", "Higher", "Lower floor", "Balcony", "Jacuzzi"
            };

            foreach (string specificationName in specifications)
            {
                List<Specification> servs = unitOfWork.SpecificationRepo.GetAll(e => e.Type == specificationName).ToList();
                if (servs.Count == 0)
                {
                    Specification spec = new Specification()
                    {
                        Type = specificationName
                    };
                    unitOfWork.SpecificationRepo.Insert(spec);
                    unitOfWork.Save();
                }
            }
        }

        public static void SeedRooms(IUnitOfWork unitOfWork)
        {
            var categories = unitOfWork.CategoryRepo.GetAll().ToList();
            var hotel = unitOfWork.HotelRepo.GetAll(e => e.Name == "Hotelizer").ToList()[0];
            var specs = unitOfWork.SpecificationRepo.GetAll().ToList();
            var rs = unitOfWork.RoomRepo.GetTotalCounts();
            if (rs == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    Room r = new Room()
                    {
                        FloorNumber = i,
                        CategoryId = categories[i % 4].Id,
                        HotelId = hotel.Id,
                        Status = "Available",
                        ImageUrl = Path.Combine("/images/rooms/", "default.jpg")
                    };
                    unitOfWork.RoomRepo.Insert(r);
                    unitOfWork.Save();

                    var room = unitOfWork.RoomRepo.GetAll(e => e.FloorNumber == i).ToList()[0];

                    unitOfWork.RoomSpecificationRepo.Insert(new RoomSpecification()
                    {
                        RoomId = room.Id,
                        SpecId = specs[i % 6].Id
                    });
                    unitOfWork.Save();

                    unitOfWork.RoomSpecificationRepo.Insert(new RoomSpecification()
                    {
                        RoomId = room.Id,
                        SpecId = specs[(i + 2) % 6].Id
                    });
                    unitOfWork.Save();
                }
            }
        }




        public static void SeedFood(IUnitOfWork unitOfWork)
        {
            List<string> FoodItemType = new List<string>()
            {
                "Cold Drinks", "Hot Drinks", "Desserts", "Main Course"
            };

            foreach (string type in FoodItemType)
            {
                List<FoodItemType> foodTypes = unitOfWork.FoodItemTypeRepo.GetAll(e => e.Type == type).ToList();
                if (foodTypes.Count == 0)
                {
                    FoodItemType foodType = new FoodItemType()
                    {
                        Type = type
                    };
                    unitOfWork.FoodItemTypeRepo.Insert(foodType);
                    unitOfWork.Save();
                }
            }

            var typess = unitOfWork.FoodItemTypeRepo.GetAll().ToList();

            List<List<string>> foods = new List<List<string>>()
            {
                new List<string>() {"Ice Tea", "CocaCola", "Pepsi" , "Ice Coffee"},
                new List<string>() {"Tea", "Coffee", "Mocka", "Latte"},
                new List<string>() {"Chocolate-Mint Bars", "Lemon-Scented Blueberry", "Peach Melba pie", "Chocolate Cupcakes"},
                new List<string>() {"Shawrma", "Crispy", "Escalop", "Cordon Bleu"},
            };

            var allFood = unitOfWork.FoodItemRepo.GetTotalCounts();
            if (allFood == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        FoodItem foodItem = new FoodItem()
                        {
                            FoodItemTypeId = typess[i].Id,
                            Name = foods[i][j],
                            Cost = (i + 1) * (j + 1) * 100,
                            ImageUrl = Path.Combine("/images/foodItems/", "default.jpg")
                        };
                        unitOfWork.FoodItemRepo.Insert(foodItem);
                    }
                }
                unitOfWork.Save();
            }
        }
    }
}