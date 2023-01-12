using Domain.Aggregates.Companies.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Companies
{
    public class Company : SeedWork.AggregateRoot
    {

        #region Constractors

        public Company() : base()
        {

        }
        public string Address { get;private set; }
        public string Description { get; private set; }
        public CompanyName CompanyName { get; private set; }
        public NationalIdentity NationalIdentity { get; private set; }

        public Company(CompanyName companyName,
                       NationalIdentity nationalIdentity,
                       string address,
                       string description)
        {
            var result =
                new FluentResults.Result<Company>();

            //****************************************************************

            var companyNameResult =
                CompanyName.Create(value: companyName.Value);

            result.WithErrors(errors: companyNameResult.Errors);


            //*******************************************************************

            var nationalIdentityResult =
                NationalIdentity.Create(value: nationalIdentity.Value);

            result.WithErrors(errors: nationalIdentityResult.Errors);

            Address = address.Fix();
            Description = description.Fix();
            CompanyName = companyName;
            NationalIdentity = nationalIdentity;           

        }
        #endregion
    }
}
