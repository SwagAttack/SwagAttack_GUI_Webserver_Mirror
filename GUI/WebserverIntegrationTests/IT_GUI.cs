using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace WebserverIntegrationTests
{

   

    [TestFixture]
    public class IT_GUI
    {

        private IUserProxy _fakeUserProxy = Substitute.For<IUserProxy>();
        private readonly IUser _person = Substitute.For<IUser>();
        private readonly User user = new User();
        [SetUp]
        public void Setup()
        {
        
        //TestUser
            user.Username = "apiuserh";
            user.Password = "Maxmaxmax";
            user.Email = "apiuser@123.dk";
            user.GivenName = "Apiuser";
            user.LastName = "Apiuser";


            //TestPersUI
            _person.Username = "apiuserh";
            _person.Password = "Maxmaxmax";
            _person.Email = "apiuser@123.dk";
            _person.GivenName = "Apiuser";
            _person.LastName = "Apiuser";
        }

        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(
                @"C:\Users\maxbj\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0" ,"geckodriver.exe");

            FirefoxProfile profile = new FirefoxProfile();

            var op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };

            

            service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

            

            System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\Users\maxbj\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0");

            IWebDriver driver = new FirefoxDriver(service, op, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://localhost:44321/");

            IWebElement typeUser = driver.FindElement(By.Name("Username"));
            IWebElement typePass = driver.FindElement(By.Name("Password"));
            
            typeUser.SendKeys("apiuserh");
            typePass.SendKeys("Maxmaxmax");

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.Quit();

            _fakeUserProxy.Received(1).RequestInstanceAsync(Arg.Any<string>(),Arg.Any<string>());

        }


    }
}