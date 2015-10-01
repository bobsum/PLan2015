using System.Collections.Generic;
using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IPunctualityStatusHubClient
    {
        void Updated(PunctualityStatusDto status);
    }
}