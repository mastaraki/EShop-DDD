using Domain.Aggregates.Companies;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer
{
    public class Customer : SeedWork.AggregateRoot
    {

        #region Constractors       
        public Customer() : base()
        {

        }

        public Customer(SharedKernel.FullName fullName,
                        SharedKernel.EmailAddress emailAddress,
                        SharedKernel.NationalCode nationalCode) : this()
        {

            FullName = fullName;
            EmailAddress = emailAddress;
            NationalCode = nationalCode;
        }

        public FullName FullName { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public NationalCode NationalCode { get; private set; }

        private readonly System.Collections.Generic.List<Relation> _relations;
        public virtual System.Collections.Generic.IReadOnlyList<Relation> Relations
        {
            get
            {
                return _relations;
            }
        }


        #endregion


        public void AssignToCompany(Company company)
        {
            if (company is null)
            {
                throw new System.ArgumentNullException(paramName: nameof(company));
            }

            var hasAny =
                _relations
                .Where(current => current.Company == company)
                .Any();

            if (hasAny)
            {
                return;
            }

            var relation =
                new Relation(customer: this, company: company);

            _relations.Add(relation);
        }
    }
}
