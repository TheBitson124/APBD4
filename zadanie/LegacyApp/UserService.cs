using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if ((string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))||(!email.Contains("@") && !email.Contains(".")))
            {
                return false;
            }
            if (!IsAgeOver21(DateTime.Now, dateOfBirth)) return false;
            var client = new ClientRepository().GetById(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            bool return_value = user.setClientCreditLimit();
            if (!return_value) return false;
            UserDataAccess.AddUser(user);
            return true;
        }

        public bool IsAgeOver21(DateTime now, DateTime dateOfBirth)
        {
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age > 21;
        }
    }
}
