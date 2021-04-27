using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using address.Data;

namespace address.Models
{
    public static class StartData
    {
        public static void Initialize(AddressSystemContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new Users
                    {
                        Name = "TestUser",
                        Password = "testuser",
                        Group = 0,
                        RegistrationDate = DateTime.Today,
                        Email = "TestUser@mail.ru"
                    },
                    new Users
                    {
                        Name = "TestAdmin",
                        Password = "testadmin",
                        Group = 1,
                        RegistrationDate = DateTime.Today,
                        Email = "TestAdmin@mail.ru"
                    },
                    new Users
                    {
                        Name = "TestOwner",
                        Password = "testowner",
                        Group = 2,
                        RegistrationDate = DateTime.Today,
                        Email = "TestOwner@mail.ru"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Regions.Any())
            {
                context.Regions.AddRange(
                    new Regions
                    {
                        RegionName = "Республика Татарстан"
                    },
                    new Regions
                    {
                        RegionName = "Волгоградская область"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Areas.Any())
            {
                context.Areas.AddRange(
                    new Areas
                    {
                        AreaName = "Казанский городской округ",
                        RegionId = 1
                    },
                    new Areas
                    {
                        AreaName = "Балтасинский район",
                        RegionId = 1
                    },
                    new Areas
                    {
                        AreaName = "Волгоградский городской округ",
                        RegionId = 1
                    },
                    new Areas
                    {
                        AreaName = "Алексеевский район",
                        RegionId = 1
                    }
                );
                context.SaveChanges();
            }
            if (!context.Localities.Any())
            {
                context.Localities.AddRange(
                    new Localities
                    {
                        LocalityName = "Казань",
                        AreaId = 1
                    },
                    new Localities
                    {
                        LocalityName = "Карадуванское поселение",
                        AreaId = 2
                    },
                    new Localities
                    {
                        LocalityName = "пгт Балтаси",
                        AreaId = 2
                    },
                    new Localities
                    {
                        LocalityName = "Волгоград",
                        AreaId = 3
                    }
                );
                context.SaveChanges();
            }
            if (!context.Districts.Any())
            {
                context.Districts.AddRange(
                    new Districts
                    {
                        DistrictName = "Ново-Савиновский район",
                        LocalityId = 1
                    },
                    new Districts
                    {
                        DistrictName = "деревня Карадуван",
                        LocalityId = 2
                    },
                    new Districts
                    {
                        DistrictName = "Советский район",
                        LocalityId = 1
                    }
                );
                context.SaveChanges();
            }
            if (!context.Streets.Any())
            {
                context.Streets.AddRange(
                    new Streets
                    {
                        StreetName = "ул. Чистопольская",
                        DistrictId = 1
                    },
                    new Streets
                    {
                        StreetName = "ул. Родник",
                        DistrictId = 2
                    }, 
                    new Streets
                    {
                        StreetName = "ул. Чуйкова",
                        DistrictId = 1
                    }
                );
                context.SaveChanges();
            }
            if (!context.Houses.Any())
            {
                context.Houses.AddRange(
                    new Houses
                    {
                        HouseNum = "11",
                        Index = 420066,
                        Floors = 9,
                        Entrances = 2,
                        Flats = 0,
                        StreetId = 1
                    },
                    new Houses
                    {
                        HouseNum = "1",
                        Index = 422261,
                        Floors = 1,
                        Entrances = 1,
                        Flats = 1,
                        StreetId = 2
                    },
                    new Houses
                    {
                        HouseNum = "9",
                        Index = 420066,
                        Floors = 9,
                        Entrances = 6,
                        Flats = 216,
                        StreetId = 3
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
