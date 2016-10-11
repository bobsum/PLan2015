using System.Collections.Generic;
using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IPunctualityHubClient
    {
        void Add(PunctualityDto punctuality);
        void Remove(int id);
        void UpdatedStatus(IList<PunctualityStatusHouseDto> status);
    }
}