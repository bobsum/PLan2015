using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class PunctualitySwipeController : ApiControllerWithDB
    {
        public async Task<IHttpActionResult> PostPunctualitySwipe(PunctualitySwipeDto dto)
        {
            var scout = Db.Scouts.FirstOrDefault(s => s.Rfid == dto.Rfid);
            if (scout == null) return StatusCode(HttpStatusCode.BadRequest);
            var punctuality = Db.Punctualities.FirstOrDefault(p => p.Id == dto.PunctualityId);
            if (punctuality == null) return StatusCode(HttpStatusCode.BadRequest);

            var entity = new PunctualitySwipe
            {
                Scout = scout,
                Punctuality = punctuality,
                Time = dto.Time
            };

            Db.PunctualitySwipes.Add(entity);
            await Db.SaveChangesAsync();
            return Ok();
        }
    }
}