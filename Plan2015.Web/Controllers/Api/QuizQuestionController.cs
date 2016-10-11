using System.Threading.Tasks;
using System.Web.Http;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class QuizQuestionController : ApiControllerWithDB
    {
        public async Task<IHttpActionResult> PostQuestion()
        {
            var entity = new QuizQuestion();
            Db.QuizQuestions.Add(entity);
            await Db.SaveChangesAsync();

            var dto = new QuizQuestionDto
            {
                Id = entity.Id
            };
            
            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }
    }
}