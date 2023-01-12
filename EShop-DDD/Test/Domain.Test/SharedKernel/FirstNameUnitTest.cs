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
    public class FirstNameUnitTest : object
    {

        #region Create Test

        [Fact]
        public void Create_Null_Value_In_FirstName()
        {
            var result = FirstName.Create(null);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Create_Null_String_Value_In_FirstName()
        {
            var result = FirstName.Create("");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Create_Space_Value_In_FirstName()
        {
            var result = FirstName.Create("        ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Create_Currect_Value_In_FirstName()
        {
            var result = FirstName.Create("Reza");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************


            Assert.Equal(expected: "Reza", actual: result.Value.Value);

           
        }

        [Fact]
        public void Create_Currect_Value_With_Space_In_FirstName()
        {
            var result = FirstName.Create("    Reza   ");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************


            Assert.Equal(expected: "Reza", actual: result.Value.Value);


        }

        #endregion
    }
}
