using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.ClassLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace Rabobank.Training.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {       
        private readonly ILogger<PortfolioController> _logger;
        private readonly IFundOfMandatesFile _fundOfMandatesFile;
        private IConfiguration _configuration;

        public PortfolioController(ILogger<PortfolioController> logger, IFundOfMandatesFile fundOfMandatesFile, IConfiguration configuration)
        {
            _logger = logger;
            _fundOfMandatesFile = fundOfMandatesFile;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<PositionVM> Get()
        {
            string path = string.Empty;
            List<FundOfMandates> fundOfMandatesList = null;
            PortfolioVM portfolioVM = null;
            List<PositionVM> positionVMList = null;
            try
            {
                path = _configuration["MyConfig:FundsOfMandatesFile"];                
                fundOfMandatesList = _fundOfMandatesFile.ReadFundOfMandatesFile(path);
                portfolioVM = _fundOfMandatesFile.GetPortfolio();
                positionVMList = _fundOfMandatesFile.GetCalculatedMandates(portfolioVM, fundOfMandatesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get portfolio method");
                throw ex;
            }
            return positionVMList;
        }
    }
}
