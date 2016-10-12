using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class TurnoutSwipeController : ApiControllerWithDB
    {
        public async Task<IHttpActionResult> PostTurnoutSwipe(TurnoutSwipeDto dto)
        {
            using (var reader = new StringReader(dto.Data))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var match = Regex.Match(line, @"^([+-])\x02\w{2}(\w{8})\w{2}\x03\x03$");
                    if (!match.Success) continue;

                    var sign = match.Groups[1].ToString();
                    var hexRfid = match.Groups[2].ToString();
                    var rfid = Convert.ToInt32(hexRfid, 16).ToString("D10");

                    var scout = await Db.Scouts.FirstOrDefaultAsync(s => s.Rfid == rfid);

                    if (scout == null) continue;

                    var point = new TurnoutPoint
                    {
                        Amount = sign == "+" ? 1 : -1,
                        HouseId = scout.HouseId,
                        Time = DateTime.Now
                    };
                    Db.TurnoutPoints.Add(point);
                    await Db.SaveChangesAsync();
                    ScoreUpdated();
                }
                return Ok();
            }
        }
    }
}