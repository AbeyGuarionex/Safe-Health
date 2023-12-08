/*
 * File: AccountService.cs
 * Author: Abey Velez Class 
 * UPR Bayamon
 * Date: 12/02/2023
 * Purpose: This class is to create the methods that will be
 * used in the controllers for sign in purposes. 
 */

using SafeHealth.DataAccess;
using SafeHealth.Domain;

namespace SafeHealth.Models
{

    public interface AccountService
    {

        /// <summary>
        /// Method that check if the given credentials are correct.
        /// </summary>
        /// <param name="userEmail">The user email entered by the user.</param>
        /// <param name="userPasswrod">The password entered by the user.</param>
        public User Login(string userEmail, string userPasswrod);


    }
}
