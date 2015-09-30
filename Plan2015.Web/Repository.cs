using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Plan2015.Data;
using Plan2015.Dtos;
using Plan2015.Data.Entities;

namespace Plan2015.Web
{
    public class Repository
    {
        public IEnumerable<SchoolScoreDto> GetScore(DataContext db)
        {
            var ap = db.ActivityPoints
                .Select(p => new
                {
                    p.House,
                    p.Amount
                });
            var pp = db.PunctualityPoints
                .Select(p => new
                {
                    p.House,
                    Amount = 5
                });
            var tp = db.TurnoutPoints
                .Where(p => p.TeamMember != null)
                .Select(p => new
                {
                    p.House,
                    p.Amount
                });

            return ap
                .Concat(pp)
                .Concat(tp)
                .GroupBy(p => p.House, p => p.Amount, (h, a) => new
                {
                    h.School,
                    House = h,
                    Amount = a.Sum()
                })
                .ToList()
                .GroupBy(p => p.School, p => new HouseScoreDto
                {
                    Id = p.House.Id,
                    Name = p.House.Name,
                    Amount = p.Amount
                }, (s, h) => new SchoolScoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Houses = h
                }).ToList();
        }

        public IEnumerable<PunctualityStatusDto> GetPunctualityStatus(DataContext db, int id)
        {
            var scouts = db.Punctualities.Where(p => p.Id == id)
                .SelectMany(p => p.Swipes.Where(s => s.Time < p.Deadline))
                .Select(s => s.Scout)
                .Distinct();

            return db.Houses
                .Select(h => new PunctualityStatusDto
                {
                    HouseId = h.Id,
                    HouseName = h.Name,
                    Arrived = h.Scouts.Intersect(scouts).Select(s => new ScoutDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    }),
                    Missing = h.Scouts.Except(scouts).Select(s => new ScoutDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
                })
                .ToList();
        }
    }
}
