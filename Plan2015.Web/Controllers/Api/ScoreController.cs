using System.Linq;

namespace Plan2015.Web.Controllers.Api
{
    public class ScoreController : ApiControllerWithDB
    {
        public void GetScore()
        {
            var points = Db.PunctualityPoints.Select(p => new
            {
                House = p.House,
                Amount = 5
            }).Concat(
                Db.ActivityPoints.Select(p => new
                {
                    House = p.House,
                    Amount = p.Amount
                })).Concat(
                    Db.TurnoutPoints.Where(p => p.TeamMember != null).Select(p => new
                    {
                        House = p.House,
                        Amount = p.Amount
                    })).GroupBy(p => p.House, p => p.Amount, (h, a) => new
                    {
                        HouseId = h.Id,
                        HouseName = h.Name,
                        Amount = a.Sum()
                    });
        }
    }
}