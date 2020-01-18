using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.ClassLibrary.Models;

namespace Rabobank.Training.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {       
        private readonly ILogger<PortfolioController> _logger;
        private readonly IFundOfMandatesFile _fundOfMandatesFile;

        public PortfolioController(ILogger<PortfolioController> logger, IFundOfMandatesFile fundOfMandatesFile)
        {
            _logger = logger;
            _fundOfMandatesFile = fundOfMandatesFile;
        }

        [HttpGet]
        public IEnumerable<PositionVM> Get()
        {
            string path = string.Empty;           
            path = Directory.GetCurrentDirectory();
            //string filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace(@"\bin\Debug", "") + @"\Data\FundsOfMandatesData.xml";
            path = @"G:\Source\Core\Rabobank.Training\Rabobank.Training.ClassLibrary\Data\FundsOfMandatesData.xml";

            List<FundOfMandates> fundOfMandatesList = _fundOfMandatesFile.ReadFundOfMandatesFile(path);
            PortfolioVM portfolioVM = _fundOfMandatesFile.GetPortfolio();
            List<PositionVM> positionVMList = _fundOfMandatesFile.GetCalculatedMandates(portfolioVM, fundOfMandatesList);

            return positionVMList;
        }
    }
}
