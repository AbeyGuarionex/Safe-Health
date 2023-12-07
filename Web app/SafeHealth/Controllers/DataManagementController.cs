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


    }
}

