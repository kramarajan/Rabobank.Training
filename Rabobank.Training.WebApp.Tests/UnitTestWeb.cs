using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.ClassLibrary.Models;
using Rabobank.Training.WebApp.Controllers;
using System.Collections.Generic;
using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Rabobank.Training.WebApp.Tests
{
    [TestClass]
    public class UnitTestWeb
    {
        [TestMethod]
        public void Test_Get_ShouldReturnPortfolios()
        {
            //Arrange
            var logger = Substitute.For<ILogger<PortfolioController>>();
            var fundOfMandatesFile = Substitute.For<IFundOfMandatesFile>();
            var configuration = Substitute.For<IConfiguration>();
                        
            string path = configuration["MyConfig:FundsOfMandatesFile"] = "Data\\FundsOfMandatesData.xml";
            var fundList = new List<FundOfMandates>()
            {
              new FundOfMandates(){InstrumentCode="", InstrumentName="", LiquidityAllocation=0, Mandates = new List<Mandate>().ToArray()},
              new FundOfMandates(){InstrumentCode="", InstrumentName="", LiquidityAllocation=0, Mandates = new List<Mandate>().ToArray()},
              new FundOfMandates(){InstrumentCode="", InstrumentName="", LiquidityAllocation=0, Mandates = new List<Mandate>().ToArray()},
              new FundOfMandates(){InstrumentCode="", InstrumentName="", LiquidityAllocation=0, Mandates = new List<Mandate>().ToArray()},
              new FundOfMandates(){InstrumentCode="", InstrumentName="", LiquidityAllocation=0, Mandates = new List<Mandate>().ToArray()}
            };

            var fortFolio = new PortfolioVM()
            {
                Positions = new List<PositionVM>()
                 {
                    new PositionVM() { Code = "NL0000009165", Name = "Heineken", Value = 12345 },
                    new PositionVM() { Code = "NL0000287100", Name = "Optimix Mix Fund", Value = 23456 },
                    new PositionVM() { Code = "LU0035601805", Name = "DP Global Strategy L High", Value = 34567 },
                    new PositionVM() { Code = "NL0000292332", Name = "Rabobank Core Aandelen Fonds T2", Value = 45678 },
                    new PositionVM() { Code = "LU0042381250", Name = "Morgan Stanley Invest US Gr Fnd", Value = 56789 }
                }
            };

            fundOfMandatesFile.ReadFundOfMandatesFile(path).Returns(fundList);
            fundOfMandatesFile.GetPortfolio().Returns(fortFolio);
            fundOfMandatesFile.GetCalculatedMandates(fortFolio, fundList).Returns(new List<PositionVM>()
            {
                new PositionVM() { Code = "NL0000009165", Name = "Heineken", Value = 12345 },
                new PositionVM() { Code = "NL0000287100", Name = "Optimix Mix Fund", Value = 23456 },
                new PositionVM() { Code = "LU0035601805", Name = "DP Global Strategy L High", Value = 34567 },
                new PositionVM() { Code = "NL0000292332", Name = "Rabobank Core Aandelen Fonds T2", Value = 45678 },
                new PositionVM() { Code = "LU0042381250", Name = "Morgan Stanley Invest US Gr Fnd", Value = 56789 }
            });

            //Act
            PortfolioController portfolioController = new PortfolioController(logger, fundOfMandatesFile,configuration);
            IEnumerable<PositionVM> positionVMList = portfolioController.Get();
            List<PositionVM> positionList = (List<PositionVM>)positionVMList;

            //Assert
            Assert.AreEqual(positionList.Count, 5);
            positionList.Should().HaveCount(5);
        }
    }
}
