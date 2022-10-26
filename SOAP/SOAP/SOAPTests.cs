using CountryInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SOAP
{
    [TestClass]
    public class SOAPTests
    {
        private static CountryInfoServiceSoapTypeClient countryInfo = null;

        [TestInitialize]
        public void TestInit()
        {
            countryInfo = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        private List<tCountryCodeAndName> CountryList()
        {
            var countryList = countryInfo.ListOfCountryNamesByCode();
            return countryList;
        }

        private static tCountryCodeAndName RandomCountryCode(List<tCountryCodeAndName> countryList)
        {
            Random random = new Random();
            int countryListMaxCount = countryList.Count - 1;
            int randomNum = random.Next(0, countryListMaxCount);
            var randomCountryCode = countryList[randomNum];
            return randomCountryCode;
        }

        [TestMethod]
        public void ListCountryDetails()
        {
            var countryList = CountryList();
            var randomCountryRecord = RandomCountryCode(countryList);
            var randomCountryFullDetails = countryInfo.FullCountryInfo(randomCountryRecord.sISOCode);
            Assert.AreEqual(randomCountryRecord.sISOCode, randomCountryFullDetails.sISOCode, "Country code did not match.");
            Assert.AreEqual(randomCountryRecord.sName, randomCountryFullDetails.sName, "Country name did not match.");
        }

        [TestMethod]
        public void ListFiveCountryRecords()
        {
            var countryList = CountryList();
            List<tCountryCodeAndName> fiveRandomCountry = new List<tCountryCodeAndName>();

            for (int x = 0; x < 5; x++)
            {
                fiveRandomCountry.Add(RandomCountryCode(countryList));
            }

            foreach (var country in fiveRandomCountry)
            {
                var countryISOCode = countryInfo.CountryISOCode(country.sName);
                Assert.AreEqual(country.sISOCode, countryISOCode, "Country code did not match.");
            }

        }
    }
}
