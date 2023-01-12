using Domain.SharedKernel;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.SharedKernel
{
    public class GenderUnitTest:object
    {
        public GenderUnitTest():base()
        {

        }


        #region Create Test

        [Fact]
        public void Add_Null_Value_In_Gender()
        {
            var result = Gender.GetByValue(null);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.Gender);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Add_Out_Of_Range_Value_In_Gender()
        {
            var result = Gender.GetByValue(value:10);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);

        }

        [Fact]
        public void Add_Male_Value_In_Gender()
        {
            var result = Gender.GetByValue(0);

            //Assert

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.Equal(expected: Gender.Male, actual: result.Value);


        }


        [Fact]
        public void Add_Famale_Value_In_Gender()
        {
            var result = Gender.GetByValue(1);

            //Assert

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.Equal(expected: Gender.Famale, actual: result.Value);

        }       

        #endregion
    }
}
