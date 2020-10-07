using FMS.Services;
using FMS.Services.Models.City;
using FMS.Services.Models.Postcode;
using FMS.Services.Models.Request;
using FMS.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FMS.WebClient.Controllers
{
    public class RequestController : Controller
    {
        private IRequestService requestService;
        private ICountryService countryService;
        private ICityService cityService;
        private ILoadService loadService;
        private IRequestTypeService requestTypeService;

        public RequestController(IRequestService requestService, ICountryService countryService, ICityService cityService, ILoadService loadService, IRequestTypeService requestTypeService)
        {
            this.requestService = requestService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.loadService = loadService;
            this.requestTypeService = requestTypeService;
        }
        public IActionResult NewCustomerRequest(CurtomerRequestModel model)
        {
            model.countryList = countryService.GetAll();
            return View(model);
        }
        public IActionResult Create(CurtomerRequestModel model)
        {
            requestService.NewCustomerRequest(model);
            return View();
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
    }
}