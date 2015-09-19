using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public interface IPunctualityClient
    {
        void Add(PunctualityDto punctuality);
    }
}