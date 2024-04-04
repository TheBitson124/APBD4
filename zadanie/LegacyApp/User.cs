using System;

namespace LegacyApp
{
    public class User
    {
        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public bool HasValidCreditLimit()
        {
            if((HasCreditLimit && CreditLimit < 500) || (Client.GetType().Equals("VeryImportantClient")))
            {
                return false;
            }
            return true;
        }

        public bool setClientCreditLimit()
        {
                using (var userCreditService = new UserCreditService())
                {
                    CreditLimit = userCreditService.GetCreditLimit(LastName,DateOfBirth) * 2;
                }
                if (Client.GetType().Equals("ImportantClient"))
                {
                    CreditLimit *= 2;
                }
                return HasValidCreditLimit();
        }
    }
}