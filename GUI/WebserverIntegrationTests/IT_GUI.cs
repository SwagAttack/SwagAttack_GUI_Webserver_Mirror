﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
        //Services setup
        IWebDriver driver;
        IWebElement typeUser, typePass,typePass2, GivenName, LastName, Email;
        FirefoxDriverService service;
        FirefoxOptions op;

        //Fakes Setup
        private IUserProxy _fakeUserProxy = Substitute.For<IUserProxy>();
        private readonly IUser _person = Substitute.For<IUser>();
        private readonly User user = new User();

        //Paths for FirefoxSetup  - ændre den til egen sti for at køre testen
        private string pathForGecko = @"C:\Users\maxbj\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0";
        private string Gecko = "geckodriver.exe";
        private string pathToFireFox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        public static string localhost = "https://localhost:44321/";


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
        //test opret konto OBS; husk at ændre brugernavne!

        [Test]
        public async Task IT_WS_2_Test_Opret()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            driver.FindElement(By.XPath("//button[2]")).Click();

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));
            typePass2 = driver.FindElement(By.Name("ConfirmPassword"));
            GivenName = driver.FindElement(By.Name("GivenName"));
            LastName = driver.FindElement(By.Name("LastName"));
            Email = driver.FindElement(By.Name("Email"));


            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.Username);
            LastName.SendKeys(user.GivenName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();


            //  driver.Url.CompareTo("https://localhost:44321/Home/PostLogInd?Username=apiuserh&Password=Maxmaxmax&Email=apiuser@123.dk&GivenName=Apiuser&LastName=Apiuser");

            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo("https://localhost:44321/"));



        }

        //Test Opret Fail
        [Test]
        public async Task IT_WS_2_Test_OpretUsernameFail()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            driver.FindElement(By.XPath("//button[2]")).Click();

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));
            typePass2 = driver.FindElement(By.Name("ConfirmPassword"));
            GivenName = driver.FindElement(By.Name("GivenName"));
            LastName = driver.FindElement(By.Name("LastName"));
            Email = driver.FindElement(By.Name("Email"));


            typeUser.SendKeys("0!!=)#");
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.Username);
            LastName.SendKeys(user.GivenName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();

            string usernameFail = driver.FindElement(By.XPath("//div/div/div/div/form/div/ul/li[1]")).Text;

            //  driver.Url.CompareTo("https://localhost:44321/Home/PostLogInd?Username=apiuserh&Password=Maxmaxmax&Email=apiuser@123.dk&GivenName=Apiuser&LastName=Apiuser");

            string testUrl = driver.Url;

          //  driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("value cannot be less than 8 characters"));
        }


        //Test Opret Fail
        [Test]
        public async Task IT_WS_2_Test_OpretNameFail()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            driver.FindElement(By.XPath("//button[2]")).Click();

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));
            typePass2 = driver.FindElement(By.Name("ConfirmPassword"));
            GivenName = driver.FindElement(By.Name("GivenName"));
            LastName = driver.FindElement(By.Name("LastName"));
            Email = driver.FindElement(By.Name("Email"));


            typeUser.SendKeys("NybrugerOK");
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys("0!!=)#");
            LastName.SendKeys(user.LastName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();

            string usernameFail = driver.FindElement(By.XPath("//div/div/div/div/form/div/ul/li[1]")).Text;

            //  driver.Url.CompareTo("https://localhost:44321/Home/PostLogInd?Username=apiuserh&Password=Maxmaxmax&Email=apiuser@123.dk&GivenName=Apiuser&LastName=Apiuser");

            string testUrl = driver.Url;

            //  driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("Use letters only please"));
        }

        //IT_WS_1_Test_Login
        [Test]
        public async Task IT_WS_1_Test_Login()
        {
            
            FireFoxSetup(out service, out op);
           
            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();

          //  await _fakeUserProxy.Received().RequestInstanceAsync(Arg.Any<string>(), Arg.Any<string>());

            string loginName = driver.FindElement(By.XPath("/html/body/div/div/div/div[1]")).ToString();

            string testUrl = driver.Url;

            driver.Quit();

           Assert.That(testUrl, Is.EqualTo("https://localhost:44321/Home/PostLogInd?Username=apiuserh&Password=Maxmaxmax&Email=apiuser@123.dk&GivenName=Apiuser&LastName=Apiuser"));

           

        }

        //IT_WS_1_Test_LoginFail
        [Test]
        public async Task IT_WS_1_Test_LoginFail()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));
            typeUser.SendKeys("fwqf2q3fafwf");
            typePass.SendKeys("fw3fwsefewf");



            driver.FindElement(By.XPath("//button[1]")).Click();

            string testUrl = driver.Url;

            driver.Quit();
           

           Assert.That(testUrl,Is.EqualTo("https://localhost:44321/"));



        }


        //IT_WS_1_Test_Login_OpretLobby
        [Test]
        public async Task IT_WS_1_Test_Login_OpretLobby()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.FindElement(By.XPath("//button[2]")).Click();

            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo("https://localhost:44321/Lobby/OpretLobby"));



        }


        //IT_WS_1_Test_Login_OpretLobby
        [Test]
        public async Task IT_WS_1_Test_Login_OpretLobby_Typed()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.FindElement(By.XPath("//button[2]")).Click();

            IWebElement lobbyId = driver.FindElement(By.Name("Id"));

            lobbyId.SendKeys("TestLobby");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();


            string testUrl = driver.Url;

            driver.Quit();

            // Assert.That(testUrl, Is.EqualTo("https://localhost:44321/Lobby/TilslutLobby"));



        }


        //IT_WS_1_Test_Login_TilslutLobby
        [Test]
        public async Task IT_WS_1_Test_Login_Tilslutlobby()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.FindElement(By.XPath("//div/div/form/div/div/a")).Click();


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo("https://localhost:44321/Lobby/TilslutLobby?password=Maxmaxmax&username=apiuserh"));



        }

        //IT_WS_1_Test_Login_LogUd
        [Test]
        public async Task IT_WS_1_Test_Login_LogUd()
        {

            FireFoxSetup(out service, out op);

            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.FindElement(By.XPath("//div/div/button")).Click();


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo("https://localhost:44321/"));



        }



        //****************************************************************//////



        //CleanupMethods for firefox

        private void FireFoxSetup(out FirefoxDriverService service, out FirefoxOptions op)
        {
            service = FirefoxDriverService.CreateDefaultService(pathForGecko, Gecko);
            FirefoxProfile profile = new FirefoxProfile();

            op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            service.FirefoxBinaryPath = pathToFireFox;
        }

        //CleanupMethods for driver

        private static void setUp(FirefoxDriverService service, FirefoxOptions op, out IWebDriver driver)
        {
            driver = new FirefoxDriver(service, op, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(localhost);
        }
    }
}