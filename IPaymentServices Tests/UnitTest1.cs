using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IPaymentServices_Tests
{
    [TestClass]
    public class UnitTest1
    {

        //Test Case 1 - Card Number validation
        //Input - Valid card number
        //Expected Result - true
        [TestMethod]
        public void IsCardNumberValid_test1()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = true;
            bool actual;
            actual = target.IsCardNumberValid("5546232901773076");
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 2 - Card Number validation
        //Input - InValid card number
        //Expected Result - false
        [TestMethod]
        public void IsCardNumberValid_test2()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = false;
            bool actual;
            actual = target.IsCardNumberValid("5546232901773075");
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 3 - Amount validation
        //Input - InValid Amount
        //Expected Result - false
        [TestMethod]
        public void IsValidPaymentAmount_test1()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = false;
            bool actual;
            actual = target.IsValidPaymentAmount(9);
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 4 - Amount validation
        //Input - Valid Amount
        //Expected Result - true
        [TestMethod]
        public void IsValidPaymentAmount_test2()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = true;
            bool actual;
            actual = target.IsValidPaymentAmount(999);
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 5 - can make payment
        //Input - Valid card,valid month, valid year
        //Expected Result - true
        [TestMethod]
        public void CanMakePaymentWithCard_test1()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = true;
            bool actual;
            actual = target.CanMakePaymentWithCard("5546232901773076", 12, 2016);
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 6 - can make payment
        //Input - InValid card,valid month, valid year
        //Expected Result - false
        [TestMethod]
        public void CanMakePaymentWithCard_test2()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = false;
            bool actual;
            actual = target.CanMakePaymentWithCard("5546232901773075", 12, 2016);
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 7 - can make payment
        //Input - Valid card,Invalid month, valid year
        //Expected Result - false
        [TestMethod]
        public void CanMakePaymentWithCard_test3()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = false;
            bool actual;
            actual = target.CanMakePaymentWithCard("5546232901773076", 13, 2016);
            Assert.AreEqual(expected, actual, "Not Equal");
        }

        //Test Case 8 - can make payment
        //Input - Valid card,valid month, Invalid year (past)
        //Expected Result - false
        [TestMethod]
        public void CanMakePaymentWithCard_test4()
        {
            ServiceReference1.PaymentGatewaySoapClient target = new ServiceReference1.PaymentGatewaySoapClient();
            bool expected = false;
            bool actual;
            actual = target.CanMakePaymentWithCard("5546232901773076", 12, 2014);
            Assert.AreEqual(expected, actual, "Not Equal");
        }
    }
}
