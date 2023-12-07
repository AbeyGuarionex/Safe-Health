/*
 * File: DataManagementController.cs
 * Author: Abey Velez Class 
 * UPR Bayamon
 * Date: 12/02/2023
 * Purpose: This class represents a controller in the mvc, that 
 * handles the log in.
 */

using SafeHealth.Domain;
using SafeHealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Data.Common;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;


namespace SafeHealth.Controllers
{
    [Route("DataManagement")]
    public class DataManagementController : Controller
    {

        /// <summary>The user that is signing in.</summary>
        private User user;

        private string userOfficeCode;

        private DataManagementService dataManagementService;


        public DataManagementController(DataManagementService _dataManagementService)
        {
            dataManagementService = _dataManagementService;
        }

        [Route("PatientUploadData")]
        public IActionResult PatientUploadData()
        {
            return View("PatientUploadData");
        }

        [Route("DoctorViewDocuments")]
        public IActionResult DoctorViewDocuments()
        {
            return View("DoctorViewDocuments");
        }

        [Route("DoctorViewPatients")]
        public IActionResult DoctorViewPatients()
        {
            return View("DoctorViewPatients");
        }



        [Route("PatientViewData")]
        public IActionResult PatientViewData()
        {
            return View("PatientViewData");
        }

        [HttpGet]
        [Route("GetPatients")]
        public IActionResult GetPatients()
        {
            userOfficeCode = HttpContext.Session.GetString("userOfficeCode");

            string result = dataManagementService.GetPatients(userOfficeCode);

            return Content(result);

        }


        [HttpGet]
        [Route("GetUserCode")]
        public IActionResult GetUserCode(string firstName, string paternalLastName, string userEmail)
        {
            // Call your service method to get documents based on the user name
            string userCode = dataManagementService.GetUserCode(firstName, paternalLastName, userEmail);


            return RedirectToAction("GetDocuments", new { userCode });
        }


        [HttpGet]
        [Route("GetDocuments")]
        public IActionResult GetDocuments(string userCode)
        {
            // Call your service method to get documents based on the user name
            string result = dataManagementService.GetDocuments(userCode);

            return View("PatientViewData");

        }

        [HttpPost]
        [Route("UploadDocument")]
        public IActionResult UploadDocument(IFormFile document, string documentTitle)
        {

            string userCode = HttpContext.Session.GetString("userCode");

            string userEmail = HttpContext.Session.GetString("userEmail");

            string result = dataManagementService.UploadFile(document, userCode, userEmail, documentTitle);

            return Content(result);
        }

        [HttpGet]
        [Route("GetPatientsDocuments")]
        public IActionResult GetPatientsDocuments()
        {
            string userCode = HttpContext.Session.GetString("userCode");

            string result = dataManagementService.GetPatientsDocuments(userCode);

            return Content(result);

        }

        [HttpGet]
        [Route("OpenDocument")]
        public IActionResult OpenDocument(string documentTitle, DateTime uploadedDocDate, string documentType)
        {
            string userCode = HttpContext.Session.GetString("userCode");

            byte[] documentContent = dataManagementService.GetDocumentContent(documentTitle, uploadedDocDate, documentType, userCode);

            if (documentContent != null)
            {
                // Determine content type based on the document type
                string contentType = GetContentType(documentType);

                // Set the Content-Disposition header to "inline"
                Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("inline")
                {
                    FileName = $"{documentTitle}.{documentType.ToLower()}"
                }.ToString();

                // Return the file with appropriate content type and file name
                return File(documentContent, contentType);
            }
            else
            {
                return Content("Document not found");
            }
        }



        private string GetContentType(string documentType)
        {
            switch (documentType.ToLower())
            {
                case "pdf":
                    return "application/pdf";
                case "png":
                    return "image/png";
                default:
                    return "application/octet-stream"; // Default content type if not recognized
            }
        }
    }
}

