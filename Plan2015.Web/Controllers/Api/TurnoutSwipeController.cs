using System;
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
        private const string LINE_FORMAT = @"^([+-])\x02(\w{12})\x03\x03$";
        private const int MAX_POINTS = 10 * 6;

        public async Task<IHttpActionResult> PostTurnoutSwipe(TurnoutSwipeDto dto)
        {
            var reader = new StringReader(dto.Data);

            TeamMember teamMember = null;
            
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var match = Regex.Match(line, LINE_FORMAT);
                if (!match.Success) continue;

                var sign = match.Groups[1].ToString();
                var rfid = match.Groups[2].ToString();

                var member = Db.TeamMembers.FirstOrDefault(t => t.Rfid == rfid);
                if (member != null)
                {
                    teamMember = member;
                    continue;
                }

                if (teamMember != null)
                {
                    var totalPoints = Db.TurnoutPoints
                        .Where(tp => tp.TeamMemberId == teamMember.Id)
                        .Select(tp => tp.Amount)
                        .ToList()
                        .Sum(a => Math.Abs(a));

                    if (totalPoints >= MAX_POINTS) teamMember = null;
                }

                var scout = Db.Scouts.FirstOrDefault(s => s.Rfid == rfid);

                if (scout == null) continue;

                var point = new TurnoutPoint
                {
                    Amount = sign == "+" ? 1 : -1,
                    House = scout.House,
                    TeamMember = teamMember
                };
                //Todo call hub
                //builder.AppendLine(string.Format("{0}/{1} har fået {2} points", scout.House.Name, scout.Name, point.Amount));
                Db.TurnoutPoints.Add(point);
                await Db.SaveChangesAsync();
            }
            //Todo Call Hub
            return Ok();
        }
    }
}