using Domain.Aggregates.DiscountCoupons.ValueObjects;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateToUnitTest : object
    {

        public ValidDateToUnitTest() : base()
        {

        }


        [Fact]
        public void Set_Null_Value_In_ValidDateTo()
        {
            //Run
            var result = ValidDateTo.Create(value: null);


            //Assert
            Assert.True(condition: result.IsFailed);

            string errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }

        [Fact]
        public void Set_DateTime_Before_In_ValidDateTo()
        {

            var result =
                ValidDateTo.Create(value: DateTime.Now.AddDays(-1));

            Xunit.Assert.True(condition: result.IsFailed);

            string errorMessage = string.Format
                (Validations.GreaterThanOrEqualTo_FieldValue,
                DataDictionary.ValidDateTo, DataDictionary.CurrentDate);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }


        [Fact]
        public void Set_DateTime_Now_In_ValidDateTo()
        {

            //Run
            var result = ValidDateTo.Create(value: DateTime.Now);

            //Assert
            Assert.True(condition: result.IsSuccess);


        }

        [Fact]
        public void Set_DateTime_After_In_ValidDateTo()
        {
            var result = ValidDateTo.Create(value: System.DateTime.Now.AddDays(1));

            Xunit.Assert.True(condition: result.IsSuccess);

        }


    }
}
