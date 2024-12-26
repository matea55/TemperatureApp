using Microsoft.AspNetCore.Mvc;
using Temperature.Model;

namespace Temperature.Controllers
{
    [ApiController]
    public class CityTemperatureController : ControllerBase
    {
        private readonly ILogger<CityTemperatureController> _logger;

        public CityTemperatureController(ILogger<CityTemperatureController> logger)
        {
            _logger = logger;
        }
        [HttpGet("allcities")]
        public List<CityViewModel> GetAllThreeTemp()
        {
            if (Helper.CheckIfChanged())
            {
                Helper.ParseCsv();
                
            }
            return Helper.CitiesOutput();

        }

        [HttpGet("singlecity/{city}")]
        public CityViewModel GetSignleCity(string city)
        {
            if (Helper.CheckIfChanged())
            {
                Helper.ParseCsv();
                
            }
            return Helper.CityOutput(city);

        }

        [HttpGet("filtercitiesgreaterthan/{temp}")]
        public List<CityViewModel> FilterCitiesGreaterThan(double temp)
        {
            if (Helper.CheckIfChanged())
            {
                Helper.ParseCsv();                
            }
            return Helper.FilterCitiesGreaterThan(temp);


        }

        [HttpGet("filtercitiessmallerthan/{temp}")]
        public List<CityViewModel> FilterCitiesSmallerThan(double temp)
        {
            if (Helper.CheckIfChanged())
            {
                Helper.ParseCsv();
            }
            return Helper.FilterCitiesSmallerThan(temp);


        }
    }
}
