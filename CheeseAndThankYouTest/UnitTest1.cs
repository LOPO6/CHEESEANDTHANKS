using CheeseAndThankYou.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CheeseAndThankYouTest
{
    // suite of unit tests for methods in HomeController of cheeseandthankyou web project
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexReturnsView()
        {
            //1. arrange - set up any vars/objects needed
            var controller = new HomeController();

            //2. act - execute the method we want to evaluate, must cast iactionresult to viewresult
            var result = (ViewResult)controller.Index();


            //3. assert - did we get the correct result
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]
        public void PrivacyReturnsView()
        {
            //arrange
            var controller = new HomeController();

            //act
            var result = (ViewResult)controller.Privacy();

            //assert 
            Assert.AreEqual("Privacy", result.ViewName);

        }
    }
}