using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
  [TestFixture]
  class MathTests
  {
    private Math _math;

    [SetUp]
    public void SetUp()
    {
      //Arrange
      _math = new Math();
    }

    [Test]
    public void Add_WhenCalled_ReturnSumOfArguments()
    {
      
      //Act
      var result = _math.Add(1, 2);
      
      //Assert
      Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
    {
      var result = _math.Max(2,1);
      Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
    {
      var result = _math.Max(1, 2);
      Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
    {
      var result = _math.Max(1, 1);
      Assert.That(result, Is.EqualTo(1));
    }

  }
}
