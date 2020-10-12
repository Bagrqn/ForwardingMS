﻿using FMS.Services;
using FMS.Services.Models.City;
using FMS.Services.Models.Company;
using FMS.Services.Models.Postcode;
using FMS.Services.Models.Request;
using FMS.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.WebClient.Controllers
{
    public class RequestController : Controller
    {
        private IRequestService requestService;
        private ICountryService countryService;
        private ICityService cityService;
        private ILoadService loadService;
        private IRequestTypeService requestTypeService;
        private IRequestStatusService requestStatusService;
        private ICompanyService companyService;

        public RequestController(IRequestService requestService,
            ICountryService countryService,
            ICityService cityService,
            ILoadService loadService,
            IRequestTypeService requestTypeService,
            IRequestStatusService requestStatusService,
            ICompanyService companyService)
        {
            this.requestService = requestService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.loadService = loadService;
            this.requestTypeService = requestTypeService;
            this.requestStatusService = requestStatusService;
            this.companyService = companyService;
        }
        public IActionResult NewCustomerRequest(CurtomerRequestModel model)
        {
            model.countryList = countryService.GetAll();
            return View(model);
        }
        public IActionResult Create(CurtomerRequestModel model)
        {
            requestService.NewCustomerRequest(model);
            return Redirect("/");
        }
        public IActionResult NewRequestsList(int page = 1)
        {
            int defaultStatusID = requestStatusService.GetDefaultStatusID(); //When customer create request, request is created with default status. Thats why here we get default status. 
            var newRequests = requestService.GetAllByStatus(defaultStatusID, page);

            var model = new RequestListingViewModel
            {
                list = newRequests,
                CurrentPage = page,
                CountByStatus = requestService.CountByStatus(defaultStatusID)
            };
            ;
            return View(model); //return View(newRequests);  
        }

        public IActionResult ProcessCurtomerRequest(int requestID)
        {
            //Open form to process request
            var fullRequestInfo = requestService.GetRequest(requestID);
            ;
            return View(fullRequestInfo);
        }

        public IActionResult SaveChanges(FullInfoRequestServiceModel model)
        {
            requestService.SaveChanges(model);
            ;
            return Redirect("/");
        }
        public IActionResult Accept(FullInfoRequestServiceModel model)
        {
            ;
            return Redirect("/");
        }
        public IActionResult Delete(FullInfoRequestServiceModel model)
        {
            ;
            return Redirect("/");
        }
        //Web API
        [AllowAnonymous]
        [HttpGet("api/GetCities/{countryID}")]
        public IEnumerable<CityListingServiceModel> GetCities(int countryID)
        {
            return cityService.GetAllByCountry(countryID);
        }

        //Web API
        [AllowAnonymous]
        [HttpGet("api/GetPostcode/{cityID}")]
        public IEnumerable<PostcodeListingServiceModel> GetPostcode(int cityID)
        {
            return cityService.GetPostcodes(cityID);
        }

        [AllowAnonymous]
        [HttpGet("api/GetPackageTypes")]
        public IEnumerable<PackageTypeListingServiceModel> GetPackageTypes()
        {
            return loadService.GetAllTypes();
        }


        [HttpGet("api/GetCarrierCompany")]
        public IEnumerable<CompanyListingServiceModel> GetCarrierCompanies()
        {
            return companyService.GetCarrierCompanies();
        }
        [HttpGet("api/GetPayerCompany")]
        public IEnumerable<CompanyListingServiceModel> GetPayerCompany()
        {
            return companyService.GetPayerCompanies();
        }
    }
}