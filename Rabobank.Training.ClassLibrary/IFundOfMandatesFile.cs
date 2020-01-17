using Rabobank.Training.ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary
{
    interface IFundOfMandatesFile
    {
        List<FundOfMandates> ReadFundOfMandatesFile(string filename);

        PortfolioVM GetPortfolio();

        List<PositionVM> GetCalculatedMandates(PortfolioVM portfolioVM, List<FundOfMandates> fundOfMandates);
    }
}
