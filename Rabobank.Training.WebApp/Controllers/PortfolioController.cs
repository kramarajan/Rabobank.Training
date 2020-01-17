using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary;
using System.IO;
using Rabobank.Training.ClassLibrary.Models;

namespace Rabobank.Training.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public PortfolioController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PositionVM> Get()
        {
            string path = string.Empty;
            FundOfMandatesFile fundOfMandatesFile = new FundOfMandatesFile();
            path = Directory.GetCurrentDirectory();
            path = @"G:\Source\Core\Rabobank.Training\Rabobank.Training.ClassLibrary\Data\FundsOfMandatesData.xml";

            List<FundOfMandates> fundOfMandatesList = fundOfMandatesFile.ReadFundOfMandatesFile(path);
            PortfolioVM portfolioVM = fundOfMandatesFile.GetPortfolio();
            List<PositionVM> positionVMList = fundOfMandatesFile.GetCalculatedMandates(portfolioVM, fundOfMandatesList);
            return positionVMList;
        }
    }
}
