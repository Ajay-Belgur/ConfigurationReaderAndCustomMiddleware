using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigurationReader.Controllers
{
    [Route("/api/[Controller]")]
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly SectionOneOptions sectionOneOptions;
        private readonly SectionTwoOptions sectionTwoOptions;

        public enum Sections 
        {
           SectionOne,
           SectionTwo
        }
        public ConfigurationController(IConfiguration configuration, IOptions<SectionOneOptions> secOneOptions, IOptions<SectionTwoOptions> SecTwoOptions)
        {
            this.configuration = configuration;
            sectionOneOptions = secOneOptions.Value;
            sectionTwoOptions = SecTwoOptions.Value;

        }

        [HttpGet]
        [Route("GetConfigurationUsingDependencyInjection")]
        public IActionResult GetConfigurationUsingDependencyInjection(string sectionName)
        {
            return Content($"{configuration[sectionName]}");
        }

       

        //Reads config section using IConfiguration dependency injection 
        //and options pattern
        [HttpGet]
        [Route("GetConfigurationUsingOptionsPattern")]
        public IActionResult GetConfigurationUsingOptionsPattern(string sectionName)
        {
            if (Sections.SectionOne.Equals(sectionName))
                return Ok(configuration.GetSection(SectionOneOptions.SectionOne).Get<SectionOneOptions>());
            else if (Sections.SectionOne.Equals(sectionName))
                return Ok(configuration.GetSection(SectionTwoOptions.SectionTwo).Get<SectionTwoOptions>());

            return BadRequest("Invalid configuration section provided as input");
        }

        [HttpGet]
        [Route("GetConfigurationUsingOptionsInjection")]
        public IActionResult GetConfigurationUsingOptionsInjection(string sectionName)
        {
            if (Sections.SectionOne.Equals(sectionName))
                return Ok(sectionOneOptions);
            else if (Sections.SectionOne.Equals(sectionName))
                return Ok(sectionTwoOptions);

            return BadRequest("Invalid configuration section provided as input");
        }
    }


}
