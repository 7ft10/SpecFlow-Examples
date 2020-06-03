﻿using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecRun;

namespace BookShop.AcceptanceTests.Drivers.Selenium
{
    public class BrowserSeleniumDriverFactory
    {
        private readonly TestRunContext _testRunContext;
        private readonly WebServerDriver _webServerDriver;

        public BrowserSeleniumDriverFactory(TestRunContext testRunContext, WebServerDriver webServerDriver)
        {
            _testRunContext = testRunContext;
            _webServerDriver = webServerDriver;
        }

        public IWebDriver GetForBrowser(string browserId)
        {
            string lowerBrowserId = browserId.ToUpper();
            switch (lowerBrowserId)
            {
                case "IE": return GetInternetExplorerDriver();
                case "CHROME": return GetChromeDriver();
                case "FIREFOX": return GetFirefoxDriver();
                case "EDGE": return GetEdgeDriver();
                case string browser: throw new NotSupportedException($"{browser} is not a supported browser");
                default: throw new NotSupportedException("not supported browser: <null>");
            }
        }

        private IWebDriver GetEdgeDriver()
        {
            return new EdgeDriver(EdgeDriverService.CreateDefaultService())
            {
                Url = _webServerDriver.Hostname
            };
        }

        private IWebDriver GetFirefoxDriver()
        {
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(_testRunContext.TestDirectory);
            return new FirefoxDriver(firefoxDriverService)
            {
                Url = _webServerDriver.Hostname,
            };
        }

        private IWebDriver GetChromeDriver()
        {
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(_testRunContext.TestDirectory))
            {
                Url = _webServerDriver.Hostname
            };
        }

        private IWebDriver GetInternetExplorerDriver()
        {
            var internetExplorerOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,


            };
            return new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_testRunContext.TestDirectory), internetExplorerOptions)
            {
                Url = _webServerDriver.Hostname,


            };
        }
    }
}