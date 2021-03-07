using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using web_scraper;

namespace web_scraper_tests
{
    [TestClass]
    public class UnitTests
    {
        #region Process Date

        [TestMethod]
        public void ProcessDate_DateHTMLEmpty_ReturnsEmptyString()
        {
            //Arrange

            string dateHTML = string.Empty;

            //Act

            var result = Helper.ProcessDate(dateHTML);

            //Assert

            StringAssert.Equals(result, string.Empty);
        }

        [TestMethod]
        public void ProcessDate_Success_ReturnsFormattedDate()
        {
            //Arrange

            string dateHTML = "<div class=\"col-lg-2 col-md-2 col-sm-12 col-sx-12\" style=\"font-size: 10pt;\">" +
                                "16:48 07/03" +
                              "</div>";

            string expectedResult = "16:48 07/03";

            //Act

            var result = Helper.ProcessDate(dateHTML);

            //Assert

            StringAssert.Equals(result, expectedResult);
        }

        #endregion

        #region Process Species

        [TestMethod]
        public void ProcessSpecies_SpeciesHTMLEmpty_ReturnsEmptyString()
        {
            //Arrange

            string speciesHTML = string.Empty;

            //Act

            var result = Helper.ProcessSpecies(speciesHTML);

            //Assert

            StringAssert.Equals(result, string.Empty);
        }

        [TestMethod]
        public void ProcessSpecies_Success_ReturnsFormattedSpecies()
        {
            //Arrange

            string speciesHTML = "<div class=\"col-lg-3 col-md-3 col-sm-12 col-sx-12\" style=\"font-size: 10pt;\">" +
                                    "<a class=\"\" href=\"/species-guide/ioc/asio-flammeus/\">Short-eared Owl</a>" +
                                 "</div>";

            string expectedResult = "Short-eared Owl";

            //Act

            var result = Helper.ProcessSpecies(speciesHTML);

            //Assert

            StringAssert.Equals(result, expectedResult);
        }

        #endregion

        #region Capitalize

        [TestMethod]
        public void Capitalize_WordEmpty_ReturnsEmptyString()
        {
            //Arrange

            string word = string.Empty;

            //Act

            var result = Helper.Capitalize(word);

            //Assert

            StringAssert.Equals(result, string.Empty);
        }

        [TestMethod]
        public void Capitalize_Success_ReturnsCapitalizedWord()
        {
            //Arrange

            Dictionary<string, string> words = new Dictionary<string, string>()
            {
                { "test", "Test"},
                { "tEst", "TEst" },
                { "Test", "Test" },
                { "TeSt", "TeSt" },
                { "TEST", "TEST" }
            };

            //Act

            bool result = true;

            foreach(var item in words)
            {
                if(item.Value != Helper.Capitalize(item.Key))
                {
                    result = false;
                }
            }

            //Assert

            Assert.IsTrue(result);
        }

        #endregion

        #region Process HTML

        [TestMethod]
        public void ProcessHTML_HTMLEmpty_ReturnsEmptyStringArray()
        {
            //Arrange

            string html = string.Empty;

            string county = "Warwickshire";

            //Act

            var response = Helper.ProcessHTML(html, county);

            bool result = response.GetType() == typeof(string[]) && response.Length == 0;

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessHTML_CountyEmpty_ReturnsEmptyStringArray()
        {
            //Arrange

            string html = "test";

            string county = string.Empty;

            //Act

            var response = Helper.ProcessHTML(html, county);

            bool result = response.GetType() == typeof(string[]) && response.Length == 0;

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessHTML_Success_ReturnsProcessedHTML()
        {
            //Arrange

            string currentDir = Environment.CurrentDirectory,

                   path = currentDir.Split("\\bin")[0] + "\\Assets\\TestHTML.html",

                   testHTML = File.ReadAllText(path),

                   county = "Norfolk";

            string[] expectedResult = new string[]
            {
                "\">\r\n            <div class=\"row\">\r\n\r\n                <div class=\"col-lg-1 col-md-1 col-sm-12 col-sx-12\">\r\n\r\n                </div>\r\n\r\n                <div class=\"col-lg-2 col-md-2 col-sm-12 col-sx-12\" style=\"font-size: 10pt;\">\r\n                    15:05 07/03\r\n                </div>\r\n\r\n                <div class=\"col-lg-3 col-md-3 col-sm-12 col-sx-12\" style=\"font-size: 10pt;\">\r\n                    <a class=\"\" href=\"/species-guide/ioc/branta-bernicla-hrota/\">Pale-bellied Brent Goose</a>\r\n                </div>\r\n\r\n                <div class=\"col-lg-2 col-md-2 col-sm-12 col-sx-12\">\r\n                    <a href=\"/sites/location/2313/\">\r\n                        Norfolk\r\n                    </a>\r\n                </div>\r\n\r\n\r\n                <div class=\"col-lg-3 col-md-3 col-sm-12 col-xs-12\">\r\n\r\n                </div>\r\n                <div class=\"col-lg-1 col-md-1 col-sm-12 col-sx-12\" style=\"font-size: 10pt;\">\r\n                    05/03\r\n                </div>\r\n\r\n\r\n            </div>\r\n        </div>\r\n        <div class=\"sighting-body\">\r\n            <div class=\"row\">\r\n                <div class=\"col-lg-1 col-md-12 col-sm-12\"></div>\r\n                <div class=\"col-lg-9 col-md-12 col-sm-12\">\r\n\r\n                    <p>\r\n                        Some details of this Pale-bellied Brent Goose sighting are only available to our <a href=\"/store/birdguides-subscriptions/\">subscribers</a>. Please <a href=\"/account/login/?ref=%2Fsightings\">login</a> or <a href=\"/store/birdguides-subscriptions/\">subscribe</a> to view this information.\r\n                    </p>\r\n                </div>\r\n                <div class=\"col-lg-2 col-md-12 col-sm-12 text-right\">\r\n\r\n                    <a class=\"btn btn-blue btn-sm\" href=\"/sightings/branta-bernicla-hrota/3563094/\"><i class=\"fa fa-binoculars\"></i> View Sighting</a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n            <div class=\"col-md-12 col-sm-12 col-xs-12\" style=\"border-bottom: 1px solid #243267;\"></div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n    <div class=\"sighting\">\r\n        <div class=\""
            };

            //Act

            var result = Helper.ProcessHTML(testHTML, county);

            //Assert

            StringAssert.Equals(result, expectedResult);
        }

        #endregion
    }
}
