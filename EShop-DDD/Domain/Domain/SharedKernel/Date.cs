using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    public abstract class Date : ValueObject
    {


        #region Static Member(s)
        public static bool operator <(Date left, Date right)
        {
            return left.Value < right.Value;
        }

        public static bool operator <=(Date left, Date right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >(Date left, Date right)
        {
            return left.Value > right.Value;
        }

        public static bool operator >=(Date left, Date right)
        {
            return left.Value >= right.Value;
        }
        #endregion 

        #region Constractor

        public DateTime? Value { get; }

        protected Date(DateTime? value) : this()
        {
            if (value is not null)
            {
                Value = value.Value.Date;
            }

            Value = value;
        }

        protected Date() : base()
        {

        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
           yield return Value;
        }

        public override string ToString()
        {
            if (Value is null)
            {
                return "-------";
            }
            
             string result = Value.Value.ToString("yyyy/mm/dd");

            return result;
        }
    }
}
