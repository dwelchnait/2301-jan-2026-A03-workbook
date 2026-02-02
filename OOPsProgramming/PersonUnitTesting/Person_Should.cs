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
        #region Valid data testing
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

        [Fact]
        public void Successfully_Alter_Name_Via_Propetry()
        {
            //arrange
            Person sut = new Person("Don", 70, 25.34m);

            //act
            sut.Name = "    Terry   ";

            //assert
            Assert.Equal("Terry", sut.Name);
        }
        [Fact]
        public void Successfully_Alter_Age_Via_Propetry()
        {
            //arrange
            Person sut = new Person("Don", 70, 25.34m);

            //act
            sut.Age = 65;

            //assert
            Assert.Equal(65, sut.Age);
        }
        [Fact]
        public void Successfully_Alter_Wage_Via_ChangeWage()
        {
            //arrange
            Person sut = new Person("Don", 70, 25.34m);

            //act
            sut.ChangeWage(32.45m);

            //assert
            Assert.Equal(32.45m, sut.Wage);
        }
        [Fact]
        public void Successfully_Create_A_ToString_Of_The_Instance()
        {
            //arrange
            Person sut = new Person("Don", 70, 25.34m);

            //act
            string expectedToString = sut.ToString();

            //assert
            Assert.Equal("Don;70;25.34", expectedToString);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void Successfully_Calculate_The_Bonus(decimal years)
        {
            //arrange
            Person sut = new Person("Don", 70, 25.34m);
            decimal expectedBonus = 25.34m * years;
            //act
            

            //assert
            Assert.Equal(expectedBonus, sut.CalculateBonus(years));
        }
        #endregion

        #region Invalid data testing
        //how many invalid values could one have for the string Name
        // null, empty string, just blanks in the string

        //Is there a way to have one unit test, execute n possible different
        //  actions?
        //YES: use the [Theory] annotation
        //the Theory annotation executes as if it were a loop
        //after the annotation include the value(s) for each iteration of the theory
        //      using the annotation [InlineData(....)] where .... is your value(s)
        //[Theory]
        //[InlineData(null,70,14.00)]
        //[InlineData("", 70, 14.00)]
        //[InlineData("     ", 70, 14.00)]
        //public void Fail_To_Create_Instance_For_Invalid_Names(string name,int age, decimal wage)
        //{
        //    //IF you method is part of a Theory test which contains iteration value(s)
        //    //  you MUST include as part of the header, as a parameter list,
        //    //  a parameter for each value in your InlineData

        //    //Arrange

        //    //Act

        //    //Assert
        //    //when test throw asserts, the preferred method is to include
        //    //  the Act as part of the Assert
        //    Assert.Throws<ArgumentNullException>(() => new Person(name, age, wage));
        //}

        //version 2
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Fail_To_Create_Instance_For_Invalid_Names(string name)
        {
            //Arrange

            //Act

            //Assert
         
            Assert.Throws<ArgumentNullException>(() => new Person(name, 70, 14.00m));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-34)]
        public void Fail_To_Create_Instance_For_Invalid_Age(int age)
        {
            //Arrange

            //Act

            //Assert

            Assert.Throws<ArgumentException>(() => new Person("don", age, 14.00m));
        }
        [Theory]
        [InlineData(-0.01)]
        [InlineData(-34.23)]
        public void Fail_To_Create_Instance_For_Invalid_Wage(decimal wage)
        {
            //Arrange

            //Act

            //Assert

            Assert.Throws<ArgumentException>(() => new Person("don", 70, wage));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Fail_To_Alter_Invalid_Name_VIA_Property(string name)
        {
            //Arrange
            Person sut = new Person("Don", 70, 14.00m);
            //Act

            //Assert

            Assert.Throws<ArgumentNullException>(() => sut.Name = name);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-34)]
        public void Fail_To_Alter_Invalid_Age_VIA_Property(int age)
        {
            //Arrange
            Person sut = new Person("Don", 70, 14.00m);
            //Act

            //Assert

            Assert.Throws<ArgumentException>(() => sut.Age = age);
        }
        [Theory]
        [InlineData(-0.01)]
        [InlineData(-34.23)]
        public void Fail_To_Alter_Invalid_Wage_VIA_ChangeWage(decimal wage)
        {
            //Arrange
            Person sut = new Person("Don", 70, 14.00m);
            //Act

            //Assert
            //checks for the correct exception type
            //AND
            //checks the messaage content of the exception
            //to capture an exception so that the message can be check
            // the Assert will assign the throw exception to a variable
            var ex = Assert.Throws<ArgumentException>(() => sut.ChangeWage(wage));
            //check the message
            Assert.Contains($"{wage} is invalid", ex.Message);
        }
        #endregion
    }
}
