using Domain.Aggregates.DiscountCoupons;
using Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Resources.Messages;
using ViewModels.DiscountCoupons;

namespace WebApi.Controllers
{
    public class DiscountCouponsController : Utilities.ControllerBase
    {
        public DiscountCouponsController(DatabaseContext dbContext) : base(dbContext)
        {
        }

        #region HttpGet
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType
            (type: typeof(Result
                <IList
                <DiscountCouponViewModel>>),
            statusCode: StatusCodes.Status200OK)]
        public async Task<Result<IList<DiscountCouponViewModel>>> GetAsync()
        {
            var result =
                new FluentResults.Result
                <IList<DiscountCouponViewModel>>();

            try
            {
                var value =
                    await
                    DatabaseContext.DiscountCoupons
                    .Select(current => new DiscountCouponViewModel
                    {
                        Id = current.Id,
                        ValidDateTo = current.ValidDateTo.Value,
                        ValidDateFrom = current.ValidDateFrom.Value,
                        DiscountPercent = current.DiscountPercent.Value,
                    }).ToListAsync();

                result.WithValue(value: value);
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Errors.UnexpectedError);
            }

            return result.ConvertToDtxResult();
        }
        #endregion


        #region HttpPost

        [HttpPost]

        [ProducesResponseType
            (type: typeof(Result
                <DiscountCouponViewModel>),
            statusCode: StatusCodes.Status200OK)]
        public
            async
            Task
            <Result<DiscountCouponViewModel>>
            PostAsync
            ([FromBody]
            DiscountCouponRequestViewModel viewModel)
        {
            var result =
                new FluentResults.Result
                <DiscountCouponViewModel>();

            try
            {
                // **************************************************
                var discountCouponResult =
                    DiscountCoupon.Create
                    (validDateTo: viewModel.ValidDateTo,
                    validDateFrom: viewModel.ValidDateFrom,
                    discountPercent: viewModel.DiscountPercent);

                if (discountCouponResult.IsFailed)
                {
                    result.WithErrors(errors: discountCouponResult.Errors);

                    return result.ConvertToDtxResult();
                }
                // **************************************************

                // **************************************************
                DatabaseContext.Attach(discountCouponResult.Value);

                await DatabaseContext.SaveChangesAsync();
                // **************************************************

                // **************************************************
                var value =
                    new DiscountCouponViewModel
                    {
                        Id = discountCouponResult.Value.Id,
                        ValidDateTo = discountCouponResult.Value.ValidDateTo.Value,
                        ValidDateFrom = discountCouponResult.Value.ValidDateFrom.Value,
                        DiscountPercent = discountCouponResult.Value.DiscountPercent.Value,
                    };

                result.WithValue(value: value);
                // **************************************************

                // **************************************************
                string successMessage = string.Format
                    (Successes.SuccessCreate, Resources.DataDictionary.DiscountCoupon);

                result.WithSuccess(successMessage: successMessage);
                // **************************************************
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Errors.UnexpectedError);
            }

            return result.ConvertToDtxResult();
        }
        #endregion /HttpPost
    }
}
