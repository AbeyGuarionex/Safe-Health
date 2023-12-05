/*
 * File: AccountServiceImpl.cs
 * Author: Abey Velez Class 
 * Argentis Consulting
 * Date: 12/22/2022
 * Purpose: This class implements the created methods in AccountService.cs
 */

using SafeHealth.DataAccess;
using SafeHealth.Domain;

namespace SafeHealth.Models
{
    public class AccountServiceImpl : AccountService
    {
        /// <summary>The database context.</summary>
        private SafeHealthMdfContext context;

        /// <summary>The user that is signing in.</summary>
        private User user;



        /// <summary>
        /// Method that check if the given credentials are correct.
        /// </summary>
        /// <param name="userEmail">The control that raised the event.</param>
        /// <param name="userPasswrod">The event data.</param>

        public User Login(string userEmail, string userPasswrod)
        {
            context = new SafeHealthMdfContext();

            var q = from p in context.Users

                    where p.UserEmailPk2 == userEmail
                    && p.UserPassword == userPasswrod

                    select p;
            if (q.Any())
            {
                return user = q.First();
            }

            else

            {
                return user = null;
            }

        }


    }
}
