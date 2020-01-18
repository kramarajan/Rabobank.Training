using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;

namespace Rabobank.Training.ClassLibrary.Tests
{
    [TestClass]
    public class UnitTestClassLibrary
    {
        private IFundOfMandatesFile _fundOfMandatesFile;

        [TestInitialize]
         public void Initialize()
        {
            _fundOfMandatesFile = new FundOfMandatesFile();
        }

        [TestMethod]
        public void Test_ReadFundOfMandatesFile_Count()
        {
            string filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\TestData\FundsOfMandatesData.xml";
            List<FundOfMandates> fundsOfMandatesData = _fundOfMandatesFile.ReadFundOfMandatesFile(filename);

            //Assert.AreEqual(3, fundsOfMandatesData.Count);
            fundsOfMandatesData.Should().NotBeNull();
            fundsOfMandatesData.Should().HaveCount(3);
        }
    }
}
