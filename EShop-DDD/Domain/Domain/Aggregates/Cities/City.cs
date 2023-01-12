using Domain.Aggregates.Provinces;
using Domain.SharedKernel;
using FluentResults;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Cities
{
	public class City : SeedWork.AggregateRoot
	{
		#region Static Member(s)
		public static Result<City> Create(Province province, string name)
		{
			var result =
				new Result<City>();

			// **************************************************
			if (province is null)
			{
				string errorMessage = string.Format
					(Validations.Required, DataDictionary.Province);

				result.WithError(errorMessage: errorMessage);
			}
			// **************************************************

			// **************************************************
			var nameResult =
				Name.Create(value: name);

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
				new City(province: province, name: nameResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
        #endregion

       
        #region Consractors

        public Name Name { get; private set; }

		public Province Province { get; private set; }

		private City(Province province, Name name) : this()
		{
			Name = name;
			Province = province;
		}


		private City() : base()
		{
		}

		#endregion

		public Result Update(string name)
        {
			var result = Create(province:Province,name: name);

            if (result.IsFailed)
            {
				return result.ToResult();
            }

			Name = result.Value.Name;

			return result.ToResult();
        }
	}
}
