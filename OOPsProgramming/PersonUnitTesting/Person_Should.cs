using System;
using System.Collections.Generic;
using System.Text;

//include the using statement to Person to indentify the namespace
#region Additional Namespaces
using LearningObjects;
#endregion

namespace PersonUnitTesting
{
    public class Person_Should
    {
        //the type [Fact] says to run the test once
        //[Fact] is called an annotation
        //ANY unit test MUST have an annotation in front
        //  of the unit test method to be recognized as an unit test
        [Fact]
        public void Successfully_Create_Default_Instance()
        {
            //Arrange
            //optionally
            //this area of your unit test is used to
            //  create test data needed to complete the test


            //Act
            //optionally
            //this area of your unit test would represent the
            //  line of code in any program that will be executed
            Person sut = new Person();

            //Assert
            //this area of your unit test checks the expected result
            //  of your unit test

            Assert.Equal("Unknown", sut.Name);
            Assert.Equal(0, sut.Age);
            Assert.Equal(0.00m, sut.Wage);
        }
        [Fact]
        public void Successfully_Create_Greedy_Instance_With_Wage()
        {
            //Arrange
            //optionally
            //this area of your unit test is used to
            //  create test data needed to complete the test


            //Act
            //optionally
            //this area of your unit test would represent the
            //  line of code in any program that will be executed
            Person sut = new Person("Don",70,55990.00m);

            //Assert
            //this area of your unit test checks the expected result
            //  of your unit test

            Assert.Equal("Don", sut.Name);
            Assert.Equal(70, sut.Age);
            Assert.Equal(55990.00m, sut.Wage);
        }
        [Fact]
        public void Successfully_Create_Greedy_Instance_Without_Wage()
        {
            //Arrange
            //optionally
            //this area of your unit test is used to
            //  create test data needed to complete the test


            //Act
            //optionally
            //this area of your unit test would represent the
            //  line of code in any program that will be executed
            Person sut = new Person("Don", 70);

            //Assert
            //this area of your unit test checks the expected result
            //  of your unit test

            Assert.Equal("Don", sut.Name);
            Assert.Equal(70, sut.Age);
            Assert.Equal(0.00m, sut.Wage);
        }
    }
}
