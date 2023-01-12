using Domain.Aggregates.Companies.ValueObjects;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Aggregates.Companies.ValueObjects
{
    public class CompanyNameUnitTest:object
    {

        public CompanyNameUnitTest():base()
        {

        }

        [Fact]
        public void Add_Currect_Value_In_CompanyName()
        {
            var result = CompanyName.Create("Nike");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.Equal(expected: "Nike", actual: result.Value.Value);
        }

        [Fact]
        public void Add_Value_With_Space_In_CompanyName()
        {
            var result = CompanyName.Create(" Nike     ");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.Equal(expected: "Nike", actual: result.Value.Value);
        }

        [Fact]
        public void Add_Null_Value_In_CompanyName()
        {
            var result = CompanyName.Create(null);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.CompanyName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }


        [Fact]
        public void Add_Empty_String_Value_In_CompanyName()
        {
            var result = CompanyName.Create(String.Empty);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.CompanyName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }


        [Fact]
        public void Add_Space_Value_In_CompanyName()
        {
            var result = CompanyName.Create("        ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.CompanyName);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Value_More_Than_Maxlenght_In_CompanyName()
        {
            var result = CompanyName.Create("Nikeeeeeeeeeeeeeeee");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.MaxLength, DataDictionary.CompanyName, CompanyName.MaxLength);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }

    }
}
