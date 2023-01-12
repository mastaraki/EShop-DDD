using Domain.Aggregates.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer
{
    public class Relation:SeedWork.Entity
    {

        public Relation():base()
        {

        }


        public Relation(Customer customer,Company company):this()
        {
            Customer = customer;
            Company = company;
        }

        public Customer Customer { get; private set; }
        public Company Company { get; private set; }
    }
}
