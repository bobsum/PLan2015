using System.Web.Http;

namespace Plan2015.Web.Controllers.Api
{
    public class TurnoutController : ApiControllerWithDB
    {
        public IHttpActionResult PostTurnout()
        {
            ScoreUpdated();
            return Ok();
        }
    }
}