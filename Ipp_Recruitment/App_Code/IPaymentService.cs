using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Ipp_Recruitment
{

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]


    /// <summary>
    /// The contract required to be implemented by a payment service
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Returns the unique ID allocated to a candidate
        /// </summary>
        string WhatsYourId();

        /// <summary>
        /// Performs a Mod-10/LUHN check on the passed number and returns true if the check passed
        /// </summary>
        /// <param name="cardNumber">A 16 digit card number</param>
        /// <returns>true if the card number is valid, otherwise false</returns>
        /// <remarks>
        /// Refer here for MOD10 algorithm: https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </remarks>
        bool IsCardNumberValid(string cardNumber);

        /// <summary>
        /// Checks if the amount represents a valid payment amount 
        /// </summary>
        /// <param name="amount">An amount value in cents (1 Dollar = 100 cents)</param>
        /// <remarks>
        /// Validation:
        /// The amount must be between 99 cents and 99999999 cents
        /// </remarks>
        bool IsValidPaymentAmount(long amount);

        /// <summary>
        /// Validates the card number, expiry motnh and year to ensure the details can be used to make a payment
        /// </summary>
        /// <param name="cardNumber">A 16 digit card number</param>
        /// <param name="expiryMonth">Month part of the expiry date</param>
        /// <param name="expiryYear">Year part of the expiry date</param>
        /// <returns>true if the details represent a valid card, otherwise false</returns>
        /// <remarks>
        /// Validations:
        /// cardNumber: Ensure the passed string is 16 in length and passes the MOD10/LUHN check
        /// expiryMonth: should represent a month number between 1 and 12
        /// expiryYear: Should represent a year value, 4 characters in lenght and either the current or a future year
        /// The expiry month + year should represent a date in the future
        /// </remarks>
        bool CanMakePaymentWithCard(string cardNumber, int expiryMonth, int expiryYear);
    }

    [WebService(Namespace = "http://tempuri.org/", Description = "Payment Gateway", Name = "PaymentGateway")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class Service : System.Web.Services.WebService, IPaymentService
    {
        [WebMethod]
        public String WhatsYourId()
        {
            try
            {
                //static uuid
                //return "2a8dec5a-5f70-45a2-9e0b-b14064850de0";

                //dynamic uuid
                return Guid.NewGuid().ToString();
            }
            catch (SoapException e)
            {
                throw new SoapException("Soap Exception occured", SoapException.ServerFaultCode, "Soap Exception occured", e);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured", ex);
            }

        }

        [WebMethod]
        public bool IsCardNumberValid(string cardNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
                {
                    return false;
                }
                else
                {
                    int sumOfDigits = cardNumber.Where((digit) => digit >= '0' && digit <= '9')
                    .Reverse()
                    .Select((digit, index) => ((int)digit - 48) * (index % 2 == 0 ? 1 : 2))
                    .Sum((digit) => digit / 10 + digit % 10);
                    return sumOfDigits % 10 == 0 ? true : false;
                }
            }
            catch (SoapException e)
            {
                throw new SoapException("Soap Exception occured", SoapException.ServerFaultCode, "Soap Exception occured", e);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured", ex);
            }

        }

        [WebMethod]
        public bool IsValidPaymentAmount(long amount)
        {
            try
            {
                if (amount >= 99 && amount <= 99999999)
                    return true;
                else
                    return false;
            }
            catch (SoapException e)
            {
                throw new SoapException("Soap Exception occured", SoapException.ServerFaultCode, "Soap Exception occured", e);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured", ex);
            }

        }

        [WebMethod]
        public bool CanMakePaymentWithCard(string cardNumber, int expiryMonth, int expiryYear)
        {
            try
            {
                int month = DateTime.UtcNow.Month;
                int year = DateTime.UtcNow.Year;

                Boolean bIsCardValid = IsCardNumberValid(cardNumber);
                bool validmonth = expiryMonth >= 1 ? (expiryMonth <= 12 ? (expiryMonth >= month ? true : false) : false) : false;
                bool validyear = expiryYear >= year ? true : false;

                if (bIsCardValid && validmonth && validyear)
                    return true;
                else
                    return false;
            }
            catch (SoapException e)
            {
                throw new SoapException("Soap Exception occured", SoapException.ServerFaultCode, "Soap Exception occured", e);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured", ex);
            }
        }
    }
}
