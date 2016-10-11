using System;
using System.Collections.Generic;
using System.Linq;
using Plan2015.Data;
using Plan2015.Dtos;

namespace Plan2015.Web
{
    public class Repository
    {
        public IEnumerable<SchoolScoreDto> GetScore(DataContext db)
        {
            var ap = db.ActivityPoints
                .Where(p => p.Visible)
                .Select(p => new
                {
                    p.House,
                    p.Amount
                });
            var aph = db.ActivityPoints
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
            var qp = db.QuizPoints
                .Select(p => new
                {
                    p.House,
                    Amount = 1
                });
            var tp = db.TurnoutPoints
                .Where(p => !p.Discarded)
                .Select(p => new
                {
                    p.House,
                    p.Amount
                });

            return db.Schools
                .Select(s => new SchoolScoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Houses = s.Houses
                        .Select(h => new HouseScoreDto
                        {
                            Id = h.Id,
                            Name = h.Name,
                            Amount = ap.Concat(pp).Concat(qp).Concat(tp).Where(p => p.House == h).Sum(p => (int?) p.Amount) ?? 0,
                            HiddenAmount = aph.Concat(pp).Concat(qp).Concat(tp).Where(p => p.House == h).Sum(p => (int?)p.Amount) ?? 0
                        })
                })
                .ToList();
        }

        public IList<PunctualityStatusHouseDto> GetStatus(DataContext db, int id)
        {
            return db.Punctualities
                .Where(p => p.Id == id)
                .Select(p => p.Swipes.Where(s => p.Start < s.Time && s.Time < p.Stop).Select(s => s.Scout).Distinct())
                .SelectMany(p => db.Houses
                    .Select(h => new PunctualityStatusHouseDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Scouts = h.Scouts.Where(s => !s.Home).Select(s => new PunctualityStatusScoutDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Arrived = p.Contains(s)
                        })
                    })
                )
                .ToList();
        }

        public PunctualityStatusDto GetPunctualityStatus(DataContext db, int id)
        {
            return db.Punctualities
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    Punctuality = p,
                    Scouts = p.Swipes.Where(s => p.Start < s.Time && s.Time < p.Stop).Select(s => s.Scout).Distinct()
                })
                .Select(p => new PunctualityStatusDto
                {
                    Punctuality = new PunctualityDto
                    {
                        Id = p.Punctuality.Id,
                        Name = p.Punctuality.Name,
                        Start = p.Punctuality.Start,
                        Stop = p.Punctuality.Stop,
                        StationId = p.Punctuality.StationId,
                        All = p.Punctuality.All
                    },
                    Houses = db.Houses
                        .Select(h => new PunctualityStatusHouseDto
                        {
                            Id = h.Id,
                            Name = h.Name,
                            Scouts = h.Scouts.Where(s => !s.Home).Select(s => new PunctualityStatusScoutDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Arrived = p.Scouts.Contains(s)
                            })
                        })
                })
                .FirstOrDefault();
        }
    }
}
