using Domain.Aggregates.DiscountCoupons;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Aggregates.DiscountCoupons
{
    public class DiscountCouponUnitTest : object
    {
        public DiscountCouponUnitTest() : base()
        {

        }


        #region Create Test

       

        [Fact]
        public void Set_Currect_Value_In_DiscountCoupon()
        {
            //Run
            var result = DiscountCoupon.Create(discountPercent: 50,
                                               validDateFrom: DateTime.Now.AddDays(1),
                                               validDateTo: DateTime.Now.AddDays(2));

            //Assert

            Assert.True(condition: result.IsSuccess);





        }


        [Fact]
        public void Set_Null_Value_In_DiscountCoupon()
        {
            //Run
            var result = DiscountCoupon.Create(discountPercent: null,
                                               validDateFrom: null,
                                               validDateTo: null);

            //Assert

            Assert.True(result.IsFailed);

            //**********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

            Assert.Equal(expected: errorMessage, result.Errors[0].Message);

            //**********************************************************


            errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

            Assert.Equal(expected: errorMessage, result.Errors[1].Message);

            //**********************************************************


            errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

            Assert.Equal(expected: errorMessage, result.Errors[2].Message);

            //**********************************************************

            Assert.Equal(expected: 3, actual: result.Errors.Count());


        }

        [Fact]
        public void Set_Grater_Value_In_ValidDateFrom_Than_ValidDateTo()
        {
            //Run
            var result = DiscountCoupon.Create(discountPercent: 50,
                                               validDateFrom: DateTime.Now.AddDays(2),
                                               validDateTo: DateTime.Now.AddDays(1));

            //Assert

            Assert.True(result.IsFailed);

            //**********************************************************


            string errorMessage = string.Format(Validations.GreaterThanOrEqualTo_TwoFields,
                                                DataDictionary.ValidDateTo,
                                                DataDictionary.ValidDateFrom);

            Assert.Equal(expected: errorMessage, result.Errors[0].Message);

            //**********************************************************

            Assert.Single(result.Errors);


        }

               

        [Fact]
        public void Set_Equal_Value_In_ValidDateTo_Than_ValidDateFrom()
        {
            //Run
            var result = DiscountCoupon.Create(discountPercent: 50,
                                               validDateFrom: DateTime.Now.AddDays(1),
                                               validDateTo: DateTime.Now.AddDays(1));

            //Assert

            Assert.True(condition: result.IsSuccess);


        }

        #endregion

        #region Update Test       

        [Fact]
        public void Update_Null_Value_In_DiscountCoupon()
        {

            #region Create

            //Run
            var mainResult = DiscountCoupon.Create(discountPercent: 50,
                                                   validDateFrom: DateTime.Now.AddDays(1),
                                                   validDateTo: DateTime.Now.AddDays(2));

            //Assert

            Assert.True(condition: mainResult.IsSuccess);

            #endregion

            #region Update

            //Run

            var discountCoupon = mainResult.Value;

            var result = discountCoupon.Update(discountPercent: null,
                                                    validDateFrom: null,
                                                    validDateTo: null);
            //Assert

            Assert.True(condition: result.IsFailed);

            //************************************************************

            string errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //************************************************************

            errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

            Assert.Equal(expected: errorMessage, actual: result.Errors[1].Message);

            //************************************************************

            errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

            Assert.Equal(expected: errorMessage, actual: result.Errors[2].Message);

            //************************************************************

            Assert.Equal(expected: 3, actual: result.Errors.Count);

            #endregion

        }

        [Fact]
        public void Update__Grater_Value_In_ValidDateFrom_Than_ValidDateTo()
        {

            #region Create

            //Run
            var mainResult = DiscountCoupon.Create(discountPercent: 50,
                                                   validDateFrom: DateTime.Now.AddDays(1),
                                                   validDateTo: DateTime.Now.AddDays(2));

            //Assert

            Assert.True(condition: mainResult.IsSuccess);

            #endregion



            #region Update

            //Run

            var discountCoupon = mainResult.Value;

            var result = discountCoupon.Update(discountPercent: 70,
                                                    validDateFrom: DateTime.Now.AddDays(2),
                                                    validDateTo: DateTime.Now.AddDays(1));

            //Assert

            Assert.True(condition: result.IsFailed);

            //************************************************************

            string errorMessage = string.Format(Validations.GreaterThanOrEqualTo_TwoFields,
                                                           DataDictionary.ValidDateTo,
                                                           DataDictionary.ValidDateFrom);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //************************************************************


            Assert.Single(result.Errors);


            #endregion

        }

        [Fact]
        public void Update_Currect_Value_In_DiscountCoupon()
        {

            #region Create

            //Run
            var mainResult = DiscountCoupon.Create(discountPercent: 50,
                                                   validDateFrom: DateTime.Now.AddDays(1),
                                                   validDateTo: DateTime.Now.AddDays(2));

            //Assert

            Assert.True(condition: mainResult.IsSuccess);

            #endregion



            #region Update

            //Run

            var discountCoupon = mainResult.Value;

            var result = discountCoupon.Update(discountPercent: 70,
                                                    validDateFrom: DateTime.Now.AddDays(2),
                                                    validDateTo: DateTime.Now.AddDays(3));

            //Assert

            Assert.True(condition: result.IsSuccess);          

            #endregion

        }

        [Fact]
        public void Update__Equal_Value_In_ValidDateTo_Than_ValidDateFrom()
        {

            #region Create

            //Run
            var mainResult = DiscountCoupon.Create(discountPercent: 50,
                                                   validDateFrom: DateTime.Now.AddDays(1),
                                                   validDateTo: DateTime.Now.AddDays(2));

            //Assert

            Assert.True(condition: mainResult.IsSuccess);

            #endregion



            #region Update

            //Run

            var discountCoupon = mainResult.Value;

            var result = discountCoupon.Update(discountPercent: 70,
                                                    validDateFrom: DateTime.Now.AddDays(3),
                                                    validDateTo: DateTime.Now.AddDays(3));

            //Assert

            Assert.True(condition: result.IsSuccess);

            #endregion

        }

        #endregion
    }
}
