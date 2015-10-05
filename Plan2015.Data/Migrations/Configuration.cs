using System.Data.Entity.Migrations;
using Plan2015.Data.Entities;

namespace Plan2015.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            var schools = new[]
            {
                new School {Name = "Agernholdt"},
                new School {Name = "Ravnsborg"},
                new School {Name = "Hardenberg"}
            };
            context.Schools.AddOrUpdate(s => s.Name, schools);
            var houses = new[]
            {
                new House {Name = "Guldberg", School = schools[0]},
                new House {Name = "Basse", School = schools[0]},
                new House {Name = "Adamsen", School = schools[0]},
                new House {Name = "Lauridsen", School = schools[0]},
                new House {Name = "Jensen", School = schools[1]},
                new House {Name = "Witt", School = schools[1]},
                new House {Name = "Blumensaat", School = schools[1]},
                new House {Name = "Glarbo", School = schools[1]},
                new House {Name = "Kolze", School = schools[2]},
                new House {Name = "Fjord", School = schools[2]},
                new House {Name = "Bruun", School = schools[2]},
                new House {Name = "Malling", School = schools[2]}
            };
            context.Houses.AddOrUpdate(h => h.Name, houses);

            for (int i = 0; i < 84; i++)
            {
               context.Scouts.AddOrUpdate(s => s.Name, new Scout
                {
                    Name = "Spejder " + (i + 1),
                    Rfid = (i + 1).ToString("D10"),
                    House = houses[i/7]
                });
            }

            /*context.Scouts.AddOrUpdate(s => s.Name,
                new Scout { Name = "Spejder 1", Rfid = "0000000001", House = houses[0]},
                new Scout { Name = "Spejder 2", Rfid = "0000000002", House = houses[0]},
                new Scout { Name = "Spejder 3", Rfid = "0000000003", House = houses[0]},
                new Scout { Name = "Spejder 4", Rfid = "0000000004", House = houses[4]},
                new Scout { Name = "Spejder 5", Rfid = "0000000005", House = houses[4]},
                new Scout { Name = "Spejder 6", Rfid = "0000000006", House = houses[4]},
                new Scout { Name = "Spejder 7", Rfid = "0000000007", House = houses[8]},
                new Scout { Name = "Spejder 8", Rfid = "0000000008", House = houses[8]},
                new Scout { Name = "Spejder 9", Rfid = "0000000009", House = houses[8]}
                );*/

            context.TeamMembers.AddOrUpdate(t => t.Name,
                new TeamMember { Name = "TeamMember 1", Rfid = "0003375431" },
                new TeamMember { Name = "TeamMember 2", Rfid = "0003375432" }
                );
        }
    }
}
