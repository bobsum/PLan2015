using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class TeamMemberController : ApiControllerWithDB
    {
        public async Task<IEnumerable<TeamMemberDto>> GetTeamMembers()
        {
            return await Db.TeamMembers.Select(ToDto()).ToListAsync();
        }

        private static Expression<Func<TeamMember, TeamMemberDto>> ToDto()
        {
            return s => new TeamMemberDto
            {
                Id = s.Id,
                Rfid = s.Rfid,
                Name = s.Name
            };
        }
    }
}