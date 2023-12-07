/*
 * File: DataManagementService.cs
 * Author: Abey Velez Class 
 * UPR Bayamon
 * Date: 12/02/2023
 * Purpose: This class is to create the methods that will be
 * used in the controllers for sign in purposes. 
 */

using SafeHealth.DataAccess;
using SafeHealth.Domain;
using System.Xml.Linq;

namespace SafeHealth.Models
{

    public interface DataManagementService
    {


        public string GetPatients(string officeCode);

        public string GetDocuments(string userCode);

        public string GetUserCode(string userName, string userLastNamePaternal,string userEmail);

        public string UploadFile(IFormFile document, string userCode, string userEmail, string documentTitle);

        public string GetPatientsDocuments(string userCode);

        public byte[] GetDocumentContent(string documentTitle, DateTime uploadedDocDate, string documentType, string userCode);
    }
}
