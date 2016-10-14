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
                new School {Name = "Agernholt"},
                new School {Name = "Ravnsborg"},
                new School {Name = "Hardenberg"}
            };
            context.Schools.AddOrUpdate(s => s.Name, schools);
            var houses = new[]
            {
                new House {Name = "Adamsen", School = schools[0]},
                new House {Name = "Fjord", School = schools[0]},
                new House {Name = "Lassen", School = schools[0]},
                new House {Name = "Glarbo", School = schools[0]},
                new House {Name = "Blumensaat", School = schools[1]},
                new House {Name = "Warming", School = schools[1]},
                new House {Name = "Deleuran", School = schools[1]},
                new House {Name = "Seerup", School = schools[1]},
                new House {Name = "Basse", School = schools[2]},
                new House {Name = "Due", School = schools[2]},
                new House {Name = "Malling", School = schools[2]},
                new House {Name = "Furholt", School = schools[2]}
            };
            context.Houses.AddOrUpdate(h => h.Name, houses);
            var punctuality = new[]
            {
                new Entities.PunctualityStation {Name = "Mad", DefaultAll = false},
                new Entities.PunctualityStation {Name = "Storsal", DefaultAll = true}
            };
            context.PunctualityStations.AddOrUpdate(s => s.Name, punctuality);
            /*
            for (int i = 0; i < 84; i++)
            {
               context.Scouts.AddOrUpdate(s => s.Name, new Scout
                {
                    Name = "Spejder " + (i + 1),
                    Rfid = (i + 1).ToString("D10"),
                    House = houses[i/7]
                });
            }

            for (int i = 0; i < 21; i++)
            {
                context.TeamMembers.AddOrUpdate(t => t.Name, new TeamMember
                {
                    Name = "TeamMember " + (i + 1),
                    Rfid = (i + 101).ToString("D10")
                });
            }*/
        }
    }
}
