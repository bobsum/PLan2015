using System.Collections.Generic;
using System.Linq;
using Plan2015.Data;
using Plan2015.Dtos;

namespace Plan2015.Web
{
    public class ScoreCalculator
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
                });
        }
    }
}