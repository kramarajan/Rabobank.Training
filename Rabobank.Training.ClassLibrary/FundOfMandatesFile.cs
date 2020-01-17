using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Rabobank.Training.ClassLibrary.Models;
using System.Linq;

namespace Rabobank.Training.ClassLibrary
{
    public class FundOfMandatesFile : IFundOfMandatesFile
    {
        public List<FundOfMandates> ReadFundOfMandatesFile(string filename)
        {
            FundsOfMandatesData fundsOfMandatesData = null;
            Serializer serialize = new Serializer();
            string xmlInputData = string.Empty;            

            //XML to Object  //TODO Newtonsoft
            xmlInputData = File.ReadAllText(filename);            
            fundsOfMandatesData = serialize.Deserialize<FundsOfMandatesData>(xmlInputData);
            return fundsOfMandatesData.FundsOfMandates.ToList();
        }

        public PortfolioVM GetPortfolio()
        {
            PortfolioVM portfolioVM = new PortfolioVM();
            portfolioVM.Positions = new List<PositionVM>()
            {
                new PositionVM() { Code = "NL0000009165", Name = "Heineken", Value = 12345 },
                new PositionVM() { Code = "NL0000287100", Name = "Optimix Mix Fund", Value = 23456 },
                new PositionVM() { Code = "LU0035601805", Name = "DP Global Strategy L High", Value = 34567 },
                new PositionVM() { Code = "NL0000292332", Name = "Rabobank Core Aandelen Fonds T2", Value = 45678 },
                new PositionVM() { Code = "LU0042381250", Name = "Morgan Stanley Invest US Gr Fnd", Value = 56789 }
            };         
            return portfolioVM;
        }

        public List<PositionVM> GetCalculatedMandates(PortfolioVM portfolioVM, List<FundOfMandates> fundsOfMandates)
        {
            portfolioVM.Positions.ForEach(position => {
                FundOfMandates fund = fundsOfMandates.Where(funds => funds.InstrumentCode == position.Code).FirstOrDefault();
                if (fund != null)
                {
                    position.Mandates = new List<MandateVM>();
                    fund.Mandates.ToList().ForEach(mandate => {
                        MandateVM mandateVM = new MandateVM();
                        mandateVM.Name = mandate.MandateName;
                        mandateVM.Allocation = (mandate.Allocation / 100);
                        mandateVM.Value = (mandate.Allocation / 100) * position.Value;
                        position.Mandates.Add(mandateVM);
                    });
                    if (fund.LiquidityAllocation > 0)
                        position.Mandates.Add(new MandateVM
                        {
                            Name = "Liquidity",
                            Allocation = (fund.LiquidityAllocation / 100),
                            Value = (fund.LiquidityAllocation / 100) * position.Value
                        });
                }                
            });
            return portfolioVM.Positions;
        }
    }

    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
    }

    //public class XMLHelper
    //{
    //    public static ObjectType ReadXml<ObjectType>(string fileName)
    //    {
    //        using (var sw = new StreamReader(ENV.PathDecoder.DecodePath(fileName)))
    //        {
    //            return (ObjectType)new XmlSerializer(typeof(ObjectType)).Deserialize(sw);
    //        }
    //    }

    //    public static void SaveXml<ObjectType>(ObjectType o, string fileName)
    //    {
    //        using (var sw = new StreamWriter(ENV.PathDecoder.DecodePath(fileName)))
    //        {
    //            new XmlSerializer(typeof(ObjectType)).Serialize(sw, o);
    //        }
    //    }
    //    public static ObjectType ReadXmlString<ObjectType>(string xmlString)
    //    {
    //        using (var sw = new StringReader(xmlString))
    //        {
    //            return (ObjectType)new XmlSerializer(typeof(ObjectType)).Deserialize(sw);
    //        }
    //    }

    //    public static string SaveXmlString<ObjectType>(ObjectType o)
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            new XmlSerializer(typeof(ObjectType)).Serialize(sw, o);
    //            return sw.ToString();
    //        }
    //    }
    //}
}
