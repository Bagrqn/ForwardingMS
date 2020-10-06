using FMS.Services;
using FMS.Services.Models.City;
using FMS.Services.Models.Postcode;
using FMS.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FMS.WebClient.Controllers
{
    public class RequestController : Controller
    {
        private IRequestService requestService;
        private ICountryService countryService;
        private ICityService cityService;
        public RequestController(IRequestService requestService, ICountryService countryService, ICityService cityService)
        {
            this.requestService = requestService;
            this.countryService = countryService;
            this.cityService = cityService;
        }
        public IActionResult NewCustomerRequest(CustomerRequestViewModel model)
        {
            model.countryList = countryService.GetAll();
            return View(model);
        }
        public IActionResult Create(CustomerRequestViewModel model)
        {
            ;// логика за наливане 
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
    }
}