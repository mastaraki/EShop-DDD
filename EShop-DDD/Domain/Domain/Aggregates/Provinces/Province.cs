using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Provinces
{
	public class Province : SeedWork.AggregateRoot
	{
		#region Static Member(s)
		public static FluentResults.Result<Province> Create(string name)
		{
			var result =
				new FluentResults.Result<Province>();

			// **************************************************
			var nameResult =
				SharedKernel.Name.Create(value: name);

			if (nameResult.IsFailed)
			{
				result.WithErrors(errors: nameResult.Errors);
			}
			// **************************************************

			if (result.IsFailed)
			{
				return result;
			}

			var returnValue =
				new Province(name: nameResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private Province() : base()
		{
			_cities =
				new System.Collections.Generic.List<Cities.City>();
		}

		private Province(SharedKernel.Name name) : this()
		{
			Name = name;
		}

		public SharedKernel.Name Name { get; private set; }

		// **********
		private readonly System.Collections.Generic.List<Cities.City> _cities;

		public System.Collections.Generic.IReadOnlyList<Cities.City> Cities
		{
			get
			{
				return _cities;
			}
		}
		// **********

		public FluentResults.Result<Cities.City> AddCity(string name)
		{
			var result =
				new FluentResults.Result<Cities.City>();

			// **************************************************
			var cityResult =
				Aggregates.Cities.City.Create(province: this, name: name);

			if (cityResult.IsFailed)
			{
				result.WithErrors(errors: cityResult.Errors);

				return result;
			}
			// **************************************************

			// **************************************************
			var hasAny =
				_cities
				.Where(current => current.Name == cityResult.Value.Name)
				.Any();

			if (hasAny)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.CityName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}
			// **************************************************

			_cities.Add(cityResult.Value);

			result.WithValue(cityResult.Value);

			return result;
		}

		public FluentResults.Result RemoveCity(string cityName)
		{
			var result =
				new FluentResults.Result();

			// **************************************************
			if (string.IsNullOrWhiteSpace(cityName))
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.City);

				result.WithError(errorMessage: errorMessage);

				return result;
			}
			// **************************************************

		

			// **************************************************
			var foundedCity =
				_cities
				.Where(current => current.Name.Value.ToLower() == cityName.ToLower())
				.FirstOrDefault();

			if (foundedCity == null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.NotFound, Resources.DataDictionary.City);

				result.WithError(errorMessage: errorMessage);

				return result;
			}
			// **************************************************

			_cities.Remove(foundedCity);

			return result;
		}
	}
}
