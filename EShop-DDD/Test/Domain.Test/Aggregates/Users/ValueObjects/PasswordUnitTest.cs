using Domain.Aggregates.Users.ValueObjects;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Aggregates.Users.ValueObjects
{
    public class PasswordUnitTest : object
    {
        public PasswordUnitTest() : base()
        {

        }


        #region Create Test


        [Fact]
        public void Add_Currect_Value_In_Password()
        {
            var result = Password.Create("RezaAstaraki");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************


            Assert.Equal(expected: "RezaAstaraki", actual: result.Value.Value);


        }

        [Fact]
        public void Add_Null_Value_In_Password()
        {
            var result = Password.Create(null);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.Password);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Add_Empty_String_Value_In_Password()
        {
            var result = Password.Create(String.Empty);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.Password);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }

        [Fact]
        public void Add_Space_Value_In_Password()
        {
            var result = Password.Create("        ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.Password);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);

        }

      

      

        [Fact]
        public void Add_Value_Less_Than_Minlenght_In_Password()
        {
            var result = Password.Create("    AliAli   ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.MinLength, DataDictionary.Password, Password.MinLength);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);



        }


        [Fact]
        public void Add_Value_More_Than_Maxlenght_In_Password()
        {
            var result = Password.Create("    Reza Astarakiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii  ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.MaxLength, DataDictionary.Password, Password.MaxLength);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Is_Not_Match_Value_With_RegularExpression_In_Password()
        {
            var result = Password.Create("    Reza Astaraki");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Password);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }

       

        #endregion


    }
}
