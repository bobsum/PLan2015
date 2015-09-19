using System;
using System.Web.Http;
using Plan2015.Data;
using Plan2015.Web.Filters;

namespace Plan2015.Web.Controllers.Api
{
    [InvalidModelStateFilter]
    public abstract class ApiControllerWithDB : ApiController
    {
        private readonly Lazy<DataContext> _db = new Lazy<DataContext>(
            () => new DataContext()
            );

        protected DataContext Db
        {
            get { return _db.Value; }
        }
    }
}