using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
  [TestFixture]
  public class ReservationTests
  {
    [Test]
    public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
    {
      //Arange
      var reservation = new Reservation();

      //Act
      var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

      //Assert
      Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
    {
      //Arange
      var reservation = new Reservation();
      var testUser = new User();
      reservation.MadeBy = testUser;

      //Act
      var result = reservation.CanBeCancelledBy(testUser);

      //Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void CanBeCancelledBy_UserIsNotAdmin_ReturnsFalse()
    {
      //Arange
      var reservation = new Reservation();

      //Act 
      var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

      //Asser
      Assert.IsFalse(result);
    }

    [Test]
    public void CanBeCancelledBy_AnotherUserCancelling_ReturnFalse()
    {
      //Arrange
      var reservation = new Reservation() { MadeBy = new User() };

      //Act
      var result = reservation.CanBeCancelledBy(new User());

      //Assert
      Assert.IsFalse(result);
    }
  }
}