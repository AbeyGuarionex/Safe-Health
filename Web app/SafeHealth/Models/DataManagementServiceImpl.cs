/*
 * File: DataManagementServiceImpl.cs
 * Author: Abey Velez Class 
 * Argentis Consulting
 * Date: 12/22/2022
 * Purpose: This class implements the created methods in AccountService.cs
 */

using Microsoft.Data.SqlClient;
using SafeHealth.DataAccess;
using SafeHealth.Domain;
using System;
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

        public string UploadFile(IFormFile document, string userCode, string userEmail, string documentTitle)
        {
            if (document == null || document.Length <= 0)
            {
                return "Please select a document to upload.";
            }

            try
            {

                // Take the first 25 letters
                string first25Letters = documentTitle.Length > 25 ? documentTitle.Substring(0, 25) : documentTitle;

                // Get the last 3 letters
                string last3Letters = documentTitle.Length > 3 ? documentTitle.Substring(documentTitle.Length - 3) : documentTitle;

                // Convert the IFormFile to a byte array
                byte[] documentBytes;
                using (var memoryStream = new MemoryStream())
                {
                    document.CopyTo(memoryStream);
                    documentBytes = memoryStream.ToArray();
                }

                using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SafeHealthDB.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();

                    using (var command = new SqlCommand("INSERT INTO DOCUMENTS (userCodeFK1, userEmailFK2, documentTitle, uploadedDocDate, document, documentType, authorizedStatus) VALUES (@userCode, @userEmail, @documentTitle, @uploadedDocDate, @document, @documentType, @authorizedStatus)", connection))
                    {
                        command.Parameters.AddWithValue("@userCode", userCode); // Replace with actual user code
                        command.Parameters.AddWithValue("@userEmail", userEmail); // Replace with actual user email
                        command.Parameters.AddWithValue("@documentTitle", first25Letters); // Replace with actual document title
                        command.Parameters.AddWithValue("@uploadedDocDate", DateTime.Now.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@document", documentBytes);
                        command.Parameters.AddWithValue("@documentType", last3Letters.ToUpper()); // Replace with actual document type
                        command.Parameters.AddWithValue("@authorizedStatus", "Y"); // Replace with actual authorized status

                        command.ExecuteNonQuery();
                    }
                }

                return "Document uploaded successfully!";
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                return "An error occurred while uploading the document .";
            }
        }

        public string GetPatientsDocuments(string userCode)
        {
            using (var context = new SafeHealthMdfContext())
            {
                var docuements = from p in context.Documents
                               where p.UserCodeFk1 == userCode 
                               select p;

                // Define a StringBuilder to hold the HTML table data
                StringBuilder htmlTable = new StringBuilder();

                // Add the table header row to the StringBuilder
                htmlTable.Append("<table style='margin: 0 auto;'><tr><th colspan='2' style='text-align: center;'>Patient's Documents</th></tr><tr><th>Document Title</th><th>Uploaded Document Date</th><th>Document Type</th><th>Show Document</th></tr>");

                // Loop through the patients and add their details to the HTML table
                foreach (var document in docuements)
                {
                    htmlTable.Append("<tr><td>" + document.DocumentTitle + "</td>");
                    htmlTable.Append("<td>" + document.UploadedDocDate.ToShortDateString() + "</td>");
                    htmlTable.Append("<td>" + document.DocumentType + "</td>");
                    htmlTable.Append("<td><button onclick='showDocument(this)'>Show Document</button></td></tr>");



                }

                // Add the table closing tag to the StringBuilder
                htmlTable.Append("</table>");

                // Return the HTML table data as a string
                return htmlTable.ToString();
            }
        }

        public byte[] GetDocumentContent(string documentTitle, DateTime uploadedDocDate, string documentType, string userCode)
        {


            using (var context = new SafeHealthMdfContext())
            {
                var document = context.Documents
                    .FirstOrDefault(p => p.UserCodeFk1 == userCode && p.DocumentTitle == documentTitle && p.UploadedDocDate == uploadedDocDate && p.DocumentType == documentType);

                return document?.Document1; // Assuming the 'Document' property contains the binary content
            }
        }

    }
}

