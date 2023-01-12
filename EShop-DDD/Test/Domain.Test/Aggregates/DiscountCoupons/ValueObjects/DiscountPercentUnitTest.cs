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
    public class DiscountPercentUnitTest : object
    {
        public DiscountPercentUnitTest() : base()
        {

        }

        [Fact]
        public void Set_Null_Value_In_DiscountPercent()
        {

            //Run
            var result = DiscountPercent.Create(value: null);

            //Assert
            Assert.True(condition: result.IsFailed);

            string erroeMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

            Assert.Equal(expected: erroeMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }



        [Fact]
        public void Set_Less_Than_Minimum_Value_In_DiscountPercent()
        {
                        
            //Run
            var result = DiscountPercent.Create(value: -1);

            //Assert
            Assert.True(condition: result.IsFailed);

            string erroeMessage = string.Format(Validations.Range,
                                                DataDictionary.DiscountPercent,
                                                DiscountPercent.Minimum,
                                                DiscountPercent.Maximum);

            Assert.Equal(expected: erroeMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }


        [Fact]
        public void Set_More_Than_Maximum_Value_In_DiscountPercent()
        {

            //Run
            var result = DiscountPercent.Create(value: 101);

            //Assert
            Assert.True(condition: result.IsFailed);

            string erroeMessage = string.Format(Validations.Range,
                                                DataDictionary.DiscountPercent,
                                                DiscountPercent.Minimum,
                                                DiscountPercent.Maximum);

            Assert.Equal(expected: erroeMessage, actual: result.Errors[0].Message);

            Assert.Single(result.Errors);

        }



        [Fact]
        public void Set_More_Than_Minimum_And_Lese_Than_Maximum_Value_In_DiscountPercent()
        {

            //Run
            var result = DiscountPercent.Create(value: 50);

            //Assert
            Assert.True(condition: result.IsSuccess);            

            Assert.Equal(expected: 50, actual: result.Value.Value);


        }
    }
}
