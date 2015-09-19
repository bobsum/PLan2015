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

        /*protected override void Seed(DataContext context)
        {
            var schools = new[]
            {
                new School {Name = "Agernholdt"},
                new School {Name = "Hardenberg"},
                new School {Name = "Ravnsborg"}
            };
            context.Schools.AddOrUpdate(s => s.Name, schools);
            var houses = new[]
            {
                new House {Name = "A1", School = schools[0]},
                new House {Name = "A2", School = schools[0]},
                new House {Name = "A3", School = schools[0]},
                new House {Name = "A4", School = schools[0]},
                new House {Name = "H1", School = schools[1]},
                new House {Name = "H2", School = schools[1]},
                new House {Name = "H3", School = schools[1]},
                new House {Name = "H4", School = schools[1]},
                new House {Name = "R1", School = schools[2]},
                new House {Name = "R2", School = schools[2]},
                new House {Name = "R3", School = schools[2]},
                new House {Name = "R4", School = schools[2]}
            };
            context.Houses.AddOrUpdate(h => h.Name, houses);

            context.Scouts.AddOrUpdate(s => s.Name,
                new Scout { Name = "Spejder 1", Rfid = "5B00338152BA", House = houses[0]},
                new Scout { Name = "Spejder 2", Rfid = "5B00338152BB", House = houses[0]},
                new Scout { Name = "Spejder 3", Rfid = "5B00338152BC", House = houses[0]},
                new Scout { Name = "Spejder 4", Rfid = "5B00338152BD", House = houses[0]},
                new Scout { Name = "Spejder 5", Rfid = "5B00338152BE", House = houses[0]},
                new Scout { Name = "Spejder 6", Rfid = "5B00338152BF", House = houses[0]},
                new Scout { Name = "Spejder 7", Rfid = "5B00338152CA", House = houses[0]},
                new Scout { Name = "Spejder 8", Rfid = "5B00338152CB", House = houses[1]},
                new Scout { Name = "Spejder 9", Rfid = "5B00338152CC", House = houses[1]},
                );

            context.TeamMembers.AddOrUpdate(t => t.Name,
                new TeamMember { Name = "TeamMember 1", Rfid = "3B00338152BB" },
                new TeamMember { Name = "TeamMember 2", Rfid = "4B00338152BB" }
                );
        }*/
    }
}
