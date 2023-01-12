using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace WebApi.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ApiController]

	[Microsoft.AspNetCore.Mvc.Route
		(template: Constants.Routing.Controller)]
	public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
        protected DatabaseContext DatabaseContext { get; }

        public ControllerBase(DatabaseContext dbContext)
        {
            DatabaseContext = dbContext;
        }



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="unitOfWork"></param>
        //public ControllerBase(Persistence.IUnitOfWork unitOfWork)
        //{
        //	UnitOfWork = unitOfWork;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //protected Persistence.IUnitOfWork UnitOfWork { get; }
    }
}
