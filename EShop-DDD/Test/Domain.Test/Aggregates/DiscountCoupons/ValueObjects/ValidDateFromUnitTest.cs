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
    public class ValidDateFromUnitTest : object
    {
        public ValidDateFromUnitTest() : base()
        {

        }

        [Fact]
        public void When_Value_Is_Null()
        {
            //Run
            var result = ValidDateFrom.Create(value: null);


            //Assert
            Assert.True(condition: result.IsFailed);

            string errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }

        [Fact]
        public void Set_DateTime_Befor_In_ValidDateFrom()
        {

            //Run
            var result = ValidDateFrom.Create(value: DateTime.Now.AddDays(-1));

            //Assert
            Assert.True(condition: result.IsFailed);

            string errorMessage = string.Format(Validations.GreaterThanOrEqualTo_FieldValue,
                DataDictionary.ValidDateFrom,DataDictionary.CurrentDate);

            Assert.Equal(expected: errorMessage, actual:result.Errors[0].Message);

            Assert.Single(result.Errors);

        }
      

        [Fact]
        public void Set_DateTime_Now_In_ValidDateFrom()
        {

            //Run
            var result = ValidDateFrom.Create(value: DateTime.Now);

            //Assert
            Assert.True(condition: result.IsSuccess);           


        }

        [Fact]
        public void Set_DateTime_After_In_ValidDateFrom()
        {

            //Run
            var result = ValidDateFrom.Create(value: DateTime.Now.AddDays(1));

            //Assert
            Assert.True(condition: result.IsSuccess);

        }

    }
}

