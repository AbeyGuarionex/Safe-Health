/*
 * File: DataManagementServiceImpl.cs
 * Author: Abey Velez Class 
 * Argentis Consulting
 * Date: 12/22/2022
 * Purpose: This class implements the created methods in AccountService.cs
 */

using SafeHealth.DataAccess;
using SafeHealth.Domain;
using System.Text;

namespace SafeHealth.Models
{
    public class DataManagementServiceImpl : DataManagementService
    {
        /// <summary>The database context.</summary>
        private SafeHealthMdfContext context;

        /// <summary>The user that is signing in.</summary>
        private User user;

        private string userTypePatient = "P";

        public string GetDocuments(string userCode)
        {
            using (var context = new SafeHealthMdfContext())
            {
                var documents = from p in context.Documents
                                where p.UserCodeFk1 == userCode && p.AuthorizedStatus == "Y"
                                select p;

                // Define a StringBuilder to hold the HTML table data
                StringBuilder htmlTable = new StringBuilder();

                // Add the table header row to the StringBuilder
                htmlTable.Append("<table style='margin: 0 auto;'><tr><th colspan='3' style='text-align: center;'>Patient Information</th></tr><tr><th>Uploaded Date</th><th>Document Type</th><th>Action</th></tr>");

                // Loop through the documents and add their details to the HTML table
                foreach (var document in documents)
                {
                    // Build the table row with the document details and a button
                    htmlTable.Append("<tr><td>" + document.UploadedDocDate + "</td>");
                    htmlTable.Append("<td>" + document.DocumentType + "</td>");

                    // Add a button to open the document
                    htmlTable.Append("<td><button onclick='openDocument()'>Open Document</button></td></tr>");
                }

                // Add the table closing tag to the StringBuilder
                htmlTable.Append("</table>");

                // Return the HTML table data as a string
                return htmlTable.ToString();
            }
        }

        public string GetPatients(string officeCode)
        {
            using (var context = new SafeHealthMdfContext())
            {
                var patients = from p in context.Users
                               where p.UserOfficeCodeFk == officeCode && p.UserType == userTypePatient
                               select p;

                // Define a StringBuilder to hold the HTML table data
                StringBuilder htmlTable = new StringBuilder();

                // Add the table header row to the StringBuilder
                htmlTable.Append("<table style='margin: 0 auto;'><tr><th colspan='2' style='text-align: center;'>Patient Information</th></tr><tr><th>Name</th><th>Last Name</th><th>Patient's Email</th></tr>");

                // Loop through the patients and add their details to the HTML table
                foreach (var patient in patients)
                {
                    // Build the table row with the patient details
                    htmlTable.Append("<tr><td>" + patient.FirstName + "</td>");
                    htmlTable.Append("<td>" + patient.PaternalLastName + "</td></tr>");
                    htmlTable.Append("<td>" + patient.UserEmailPk2 + "</td></tr>");
                }

                // Add the table closing tag to the StringBuilder
                htmlTable.Append("</table>");

                // Return the HTML table data as a string
                return htmlTable.ToString();
            }
        }

        public string GetUserCode(string userName, string userLastNamePaternal,string userEmail)
        {
            using (var context = new SafeHealthMdfContext())
            {
                var user = context.Users
                    .Where(u => u.FirstName == userName && u.PaternalLastName == userLastNamePaternal && u.UserEmailPk2 == userEmail)
                    .Select(u => new { u.UserCodePk1, u.UserEmailPk2 })
                    .FirstOrDefault();

                if (user != null)
                {
                    return $"{user.UserCodePk1}-{user.UserEmailPk2}";
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
