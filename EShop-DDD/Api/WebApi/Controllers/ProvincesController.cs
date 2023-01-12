using Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace WebApi.Controllers
{
	public class ProvincesController : Utilities.ControllerBaseTemp
	{
		public ProvincesController(DatabaseContext dbContext) : base(dbContext)
		{
		}


		#region PostAsync
		/// <summary>
		/// Create
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpPost]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Result
				<ViewModels.Provinces.ProvinceViewModel>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task
			<Result<ViewModels.Provinces.ProvinceViewModel>>
			PostAsync
			([Microsoft.AspNetCore.Mvc.FromBody]
			ViewModels.Provinces.ProvinceRequestViewModel viewModel)
		{
			var result =
				new FluentResults.Result
				<ViewModels.Provinces.ProvinceViewModel>();

			try
			{
				// **************************************************
				var provinceResult =
					Domain.Aggregates.Provinces.Province.Create(name: viewModel.Name);

				if (provinceResult.IsFailed)
				{
					result.WithErrors(errors: provinceResult.Errors);

					return result.ConvertToDtxResult();
				}
				// **************************************************

				// **************************************************
				// نکته مهم: دستور ذیل کار نمی‌کند
				//bool hasAny =
				//	DatabaseContext
				//	.Provinces
				//	.Where(current => current.Name.Value.ToLower()
				//		== provinceResult.Value.Name.Value.ToLower())
				//	.Any();

				bool hasAny =
					DatabaseContext
					.Provinces
					.Where(current => current.Name == provinceResult.Value.Name)
					.Any();

				if (hasAny)
				{
					string errorMessage = string.Format
						(Resources.Messages.Validations.Repetitive,
						Resources.DataDictionary.ProvinceName);

					result.WithError(errorMessage: errorMessage);

					return result.ConvertToDtxResult();
				}
				// **************************************************

				// **************************************************
				// دستور ذیل کار نمی‌کند DDD با نگاه
				//DatabaseContext.Provinces.Add(provinceResult.Value);

				var entity =
					DatabaseContext.Attach(provinceResult.Value);

				entity.State =
					Microsoft.EntityFrameworkCore.EntityState.Added;

				await DatabaseContext.SaveChangesAsync();
				// **************************************************

				// **************************************************
				var value =
					new ViewModels.Provinces.ProvinceViewModel
					{
						Id = provinceResult.Value.Id,
						Name = provinceResult.Value.Name.Value,
					};

				result.WithValue(value: value);
				// **************************************************

				// **************************************************
				string successMessage = string.Format
					(Resources.Messages.Successes.SuccessCreate,
					Resources.DataDictionary.Province);

				result.WithSuccess(successMessage: successMessage);
				// **************************************************
			}
			catch //System.Exception ex)
			{
				// Log Error!

				result.WithError
					(errorMessage: Resources.Messages.Errors.UnexpectedError);
			}

			return result.ConvertToDtxResult();
		}
		#endregion /PostAsync

		#region GetAsync
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpGet]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Result
				<System.Collections.Generic.IList
				<ViewModels.Provinces.ProvinceViewModel>>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task
			<Result
				<System.Collections.Generic.IList
				<ViewModels.Provinces.ProvinceViewModel>>>
			GetAsync()
		{
			var result =
				new FluentResults.Result
				<System.Collections.Generic.IList
				<ViewModels.Provinces.ProvinceViewModel>>();

			try
			{
				var value =
					await
					DatabaseContext.Provinces
					.Select(current => new ViewModels.Provinces.ProvinceViewModel
					{
						Id = current.Id,
						Name = current.Name.Value,
					})
					.ToListAsync()
					;

				result.WithValue(value: value);
			}
			catch //(System.Exception ex)
			{
				// Log Error!

				result.WithError
					(errorMessage: Resources.Messages.Errors.UnexpectedError);
			}

			return result.ConvertToDtxResult();
		}
		#endregion /GetAsync

		#region GetByIdAsync
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Result
				<ViewModels.Provinces.ProvinceViewModel>),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task
			<Result<ViewModels.Provinces.ProvinceViewModel>>
			GetByIdAsync([Microsoft.AspNetCore.Mvc.FromRoute] System.Guid id)
		{
			var result =
				new FluentResults.Result
				<ViewModels.Provinces.ProvinceViewModel>();

			try
			{
				var value =
					await
					DatabaseContext.Provinces
					.Where(current => current.Id == id)
					.Select(current => new ViewModels.Provinces.ProvinceViewModel
					{
						Id = current.Id,
						Name = current.Name.Value,
					})
					.FirstOrDefaultAsync();

				if (value is null)
				{
					string errorMessage = string.Format
						(Resources.Messages.Validations.NotFound,
						Resources.DataDictionary.Province);

					result.WithError(errorMessage: errorMessage);

					return result.ConvertToDtxResult();
				}

				result.WithValue(value: value);
			}
			catch //(System.Exception ex)
			{
				// Log Error!

				result.WithError
					(errorMessage: Resources.Messages.Errors.UnexpectedError);
			}

			return result.ConvertToDtxResult();
		}
		#endregion /GetByIdAsync

		
	

		#region DeleteByIdAsync
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Microsoft.AspNetCore.Mvc.HttpDelete(template: "{id}")]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(type: typeof(Result),
			statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public
			async
			System.Threading.Tasks.Task<Result>
			DeleteByIdAsync([Microsoft.AspNetCore.Mvc.FromRoute] System.Guid id)
		{
			var result =
				new FluentResults.Result();

			try
			{
				var foundedObject =
					await
					DatabaseContext.Provinces
					.Where(current => current.Id == id)
					.FirstOrDefaultAsync();

				if (foundedObject is null)
				{
					string errorMessage = string.Format
						(Resources.Messages.Validations.NotFound, Resources.DataDictionary.Province);

					result.WithError(errorMessage: errorMessage);

					return result.ConvertToDtxResult();
				}

				DatabaseContext.Remove(foundedObject);

				//DatabaseContext.Provinces.Remove(foundedObject);

				await DatabaseContext.SaveChangesAsync();

				// **************************************************
				string successMessage = string.Format
					(Resources.Messages.Successes.SuccessDelete, Resources.DataDictionary.Province);

				result.WithSuccess(successMessage: successMessage);
				// **************************************************
			}
			catch (Microsoft.Data.SqlClient.SqlException)
			{
				string errorMessage = string.Format
					(Resources.Messages.Errors.CanNotDelete, Resources.DataDictionary.Province);

				result.WithError
					(errorMessage: errorMessage);
			}
			catch //(System.Exception ex)
			{
				// Log Error!

				string errorMessage =
					Resources.Messages.Errors.UnexpectedError;

				result.WithError
					(errorMessage: errorMessage);
			}

			return result.ConvertToDtxResult();
		}
		#endregion /DeleteByIdAsync
	}

}
