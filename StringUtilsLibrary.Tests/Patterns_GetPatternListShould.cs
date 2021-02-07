using System;
using System.Collections.Generic;
using Xunit;

namespace StringUtilsLibrary.Tests
{
    /* Based on:
     * https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
     */

    public class Patterns_GetPatternListShould
    {
        [Fact]
        public void GetPatternList_InputString_Empty_ShouldReturn_ArgumentException()
        {
            //Arrange
            string inputString = "";
            int patternLength = 0;

            //Act
            var ex = Assert.Throws<ArgumentException>(() => Patterns.GetPatternList(inputString, patternLength));

            //Assert
            Assert.Equal("Could not be null or empty (Parameter 'inputString')", ex.Message);
        }

        [Fact]
        public void GetPatternList_patternLength_EqualsZero_ShouldReturn_ArgumentException()
        {
            //Arrange
            string inputString = "123456676";
            int patternLength = 0;
            
            //Act
            var ex = Assert.Throws<ArgumentException>(() => Patterns.GetPatternList(inputString, patternLength));

            //Assert
            Assert.Equal("Should be greater than 0 (Parameter 'patternLength')", ex.Message);
        }

        [Fact]
        public void GetPatternList_patternLength_GreaterThanInputStringLength_ShouldReturn_ArgumentException()
        {
            //Arrange
            string inputString = "123456676";
            int patternLength = 10;

            //Act
            var ex = Assert.Throws<ArgumentException>(() => Patterns.GetPatternList(inputString, patternLength));

            //Assert
            Assert.Equal("Should be less or equal than inputString.Length (Parameter 'patternLength')", ex.Message);
        }

        [Fact]
        public void GetPatternList_ValidInputString_ShouldReturn_ThreeItemsOnList()
        {
            //Arrange
            string inputString = "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=";
            int patternLength = 3;
            //Act
            List<KeyValuePair<string, int>> patternList = Patterns.GetPatternList(inputString, patternLength);

            //Assert
            Assert.Equal(3, patternList.Count);
            
            Assert.Equal("zf3", patternList[0].Key);
            Assert.Equal(3, patternList[0].Value);

            Assert.Equal("abx", patternList[1].Key);
            Assert.Equal(2, patternList[1].Value);

            Assert.Equal("bxc", patternList[2].Key);
            Assert.Equal(2, patternList[2].Value);
        }

        [Fact]
        public void GetPatternList_ValidInputStringButNoMatches_ShouldReturn_EmptyList()
        {
            //Arrange
            string inputString = "1234567890";
            int patternLength = 3;
            //Act
            List<KeyValuePair<string, int>> patternList = Patterns.GetPatternList(inputString, patternLength);

            //Assert
            Assert.Empty(patternList);
        }
        
        [Fact]
        public void GetPatternList_UnicodeInputString_ShouldReturnOneItemsOnList()
        {
            //Arrange
            string inputString = "😱😱A😱😱2";
            int patternLength = 2;

            //Act
            List<KeyValuePair<string, int>> patternList = Patterns.GetPatternList(inputString, patternLength);

            //Assert
            Assert.Single(patternList);
            Assert.Equal("😱😱", patternList[0].Key);
            Assert.Equal(2, patternList[0].Value);
        }
    }
}
