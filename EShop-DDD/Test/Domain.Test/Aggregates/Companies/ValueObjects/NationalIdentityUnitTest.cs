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
    public class NationalIdentityUnitTest
    {
        public NationalIdentityUnitTest():base()
        {

        }

        [Fact]
        public void Add_Currect_Value_In_NationalIdentity()
        {
            var result = NationalIdentity.Create("4433312533");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.Equal(expected: "4433312533", actual: result.Value.Value);
        }

        [Fact]
        public void Add_Value_With_Space_In_NationalIdentity()
        {
            var result = CompanyName.Create(" 4433312533     ");

            //Assert

            Assert.True(condition: result.IsSuccess);

            //*********************************************************

            Assert.False(condition: result.IsFailed);

            //*********************************************************

            Assert.Equal(expected: "4433312533", actual: result.Value.Value);
        }

        [Fact]
        public void Add_Null_Value_In_NationalIdentity()
        {
            var result = NationalIdentity.Create(null);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.NationalIdentity);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Empty_String_Value_In_NationalIdentity()
        {
            var result = NationalIdentity.Create(String.Empty);

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.NationalIdentity);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);



        }


        [Fact]
        public void Add_Space_Value_In_NationalIdentity()
        {
            var result = NationalIdentity.Create("        ");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************


            string errorMessage = string.Format(Validations.Required, DataDictionary.NationalIdentity);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //*********************************************************


            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Value_Less_Than_Fixlenght_In_NationalIdentity()
        {
            var result = NationalIdentity.Create("443332222");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalIdentity, NationalIdentity.FixLength);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Value_More_Than_Fixlenght_In_NationalIdentity()
        {
            var result = NationalIdentity.Create("4433322222222223");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalIdentity, NationalIdentity.FixLength);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }


        [Fact]
        public void Add_Is_Not_Match_Value_With_RegularExpression_In_NationalIdentity()
        {
            var result = NationalIdentity.Create("A433B1255C");

            //Assert

            Assert.True(condition: result.IsFailed);

            //*********************************************************

            Assert.False(condition: result.IsSuccess);

            //*********************************************************

            string errorMessage = string.Format(Validations.RegularExpression, DataDictionary.NationalIdentity);

            Assert.Equal(expected: errorMessage, actual: result.Errors[0].Message);

            //***********************************************************

            Assert.Single(result.Errors);

        }



    }
}
