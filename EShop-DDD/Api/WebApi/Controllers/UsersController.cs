using Domain.Aggregates.DiscountCoupons;
using Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Users.ViewModels;
using Resources.Messages;
using ViewModels.DiscountCoupons;
using ViewModels.Users;

namespace WebApi.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public class UsersController : Utilities.ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="databaseContext"></param>
		public UsersController
			(DatabaseContext databaseContext) : base( databaseContext)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpGet]
		public string Index()
		{
			return "Hello, World!";
		}

		#region HttpPost
		/// <summary>
		/// Create
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpPost]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Result
				<Persistence.Users.ViewModels.UserViewModel>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task
			<Result<Persistence.Users.ViewModels.UserViewModel>>
			PostAsync
			([Microsoft.AspNetCore.Mvc.FromBody]
			Persistence.Users.ViewModels.UserRequestViewModel viewModel)
		{
			var result =
				new FluentResults.Result
				<Persistence.Users.ViewModels.UserViewModel>();

			try
			{
				// **************************************************
				var userResult =
					Domain.Aggregates.Users.User.Create
					(username: viewModel.Username,
					password: viewModel.Password,
					emailAddress: viewModel.EmailAddress,
					role: viewModel.Role,
					gender: viewModel.Gender,
					firstName: viewModel.FirstName,
					lastName: viewModel.LastName);

				if (userResult.IsFailed)
				{
					result.WithErrors(errors: userResult.Errors);

					return result.ConvertToDtxResult();
				}
				// **************************************************

				// **************************************************
				DatabaseContext.Attach(userResult.Value);

				//DatabaseContext.Users.Add(userResult.Value);

				await DatabaseContext.SaveChangesAsync();
				// **************************************************

				// **************************************************
				var value =
					new Persistence.Users.ViewModels.UserViewModel
					{
						Id = userResult.Value.Id,
						Username = userResult.Value.Username.Value,
						Password = userResult.Value.Password.Value,
						EmailAddress = userResult.Value.EmailAddress.Value,
						EmailAddressIsVerified = userResult.Value.EmailAddress.IsVerified,
						EmailAddressVerificationKey = userResult.Value.EmailAddress.VerificationKey,
						Role = userResult.Value.Role.Value,
						Gender = userResult.Value.FullName.Gender.Value,
						FirstName = userResult.Value.FullName.FirstName.Value,
						LastName = userResult.Value.FullName.LastName.Value,
					};

				result.WithValue(value: value);
				// **************************************************

				// **************************************************
				string successMessage = string.Format
					(Resources.Messages.Successes.SuccessCreate, Resources.DataDictionary.User);

				result.WithSuccess(successMessage: successMessage);
				// **************************************************
			}
			catch //(System.Exception ex)
			{
				// Log Error!

				result.WithError
					(errorMessage: Resources.Messages.Errors.UnexpectedError);
			}

			return result.ConvertToDtxResult();
		}
		#endregion /HttpPost
	}
}

