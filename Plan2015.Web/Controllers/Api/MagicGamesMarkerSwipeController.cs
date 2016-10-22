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
    public class MagicGamesMarkerSwipeController : ApiControllerWithDB
    {
        public async Task<IHttpActionResult> PostTurnoutSwipe(MagicGamesMarkerSwipeDto dto)
        {
            using (var reader = new StringReader(dto.Data))
            {
                var markerName = Path.GetFileNameWithoutExtension(dto.Name);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //var match = Regex.Match(line, @"^\x02\w{2}(\w{8})\w{2}\x03\x03$");
                    //if (!match.Success) continue;
                    
                    //var hexRfid = match.Groups[1].ToString();
                    //var rfid = Convert.ToInt64(hexRfid, 16);
                    var rfid = Convert.ToInt64(line);

                    var scout = Db.Scouts.FirstOrDefault(s => s.Rfid == rfid);

                    if (scout == null) continue;

                    if (await Db.MagicGamesMarkerPoints.AnyAsync(mp => mp.MarkerName == markerName && mp.HouseId == scout.HouseId)) continue;

                    var point = new MagicGamesMarkerPoint
                    {
                        House = scout.House,
                        MarkerName = markerName
                    };
                    //Console.WriteLine("{0} har fået point", scout.House.Name);
                    Db.MagicGamesMarkerPoints.Add(point);
                    await Db.SaveChangesAsync();
                    //Todo call hub
                }
                return Ok();
            }
        }
    }
}