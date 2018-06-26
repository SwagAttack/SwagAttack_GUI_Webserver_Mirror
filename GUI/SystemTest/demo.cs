using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
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

namespace SystemTest
{



    [TestFixture]
    public class Demo
    {
        //Services setup

        private readonly User user = new User();


        //host

        public static string host = "https://projectswagattack.azurewebsites.net";



        //download driveren fra: https://chromedriver.storage.googleapis.com/2.40/chromedriver_win32.zip

        //set path til chromedriver

        private IWebDriver driver = new ChromeDriver(@"C:\Users\Max\Documents\chromedriver");



        public static string setrandoms()
        {


            Random rnd = new Random();
            int usernum = rnd.Next(1, 1000);
            string random_user = "DemoUser" + usernum;

            return random_user;

        }



        public string ChromeUser = setrandoms();

        [SetUp]
        public void Setup()
        {




            //TestUser

            user.Username = setrandoms();
            user.Password = "Maxmaxmax";
            user.Email = "demo@123.dk";
            user.GivenName = "DemoName";
            user.LastName = "DemoLastName";


        }


        //test opret konto OBS; husk at ændre brugernavne!

        [Test]
        public async Task demo()
        {
           
            driver.Navigate().GoToUrl(host);

            driver.FindElement(By.XPath("//button[2]")).Click();

            var typeUser = driver.FindElement(By.Name("Username"));
            var typePass = driver.FindElement(By.Name("Password"));
            var typePass2 = driver.FindElement(By.Name("ConfirmPassword"));
            var GivenName = driver.FindElement(By.Name("GivenName"));
            var LastName = driver.FindElement(By.Name("LastName"));
            var Email = driver.FindElement(By.Name("Email"));


            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.LastName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();

            

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));


            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();


            driver.FindElement(By.XPath("//button[2]")).Click();

            driver.FindElement(By.XPath("//div/div/div/form/div/div/input[1]")).SendKeys("DemoLobby");

            driver.FindElement(By.XPath("//div/div/div/form/div/div[2]/button[1]")).Click();

            driver.FindElement(By.Id("messageInput")).SendKeys("I am a lonely Demo!");
            driver.FindElement(By.Id("sendButton")).Click();

            Thread.Sleep(30000);

            driver.FindElement(By.Id("ForLadLobby")).Click();

            driver.FindElement(By.XPath("//div/div/button[1]")).Click();

            driver.FindElement(By.XPath("//div/div/button")).Click();

            typeUser = driver.FindElement(By.Name("Username"));
            typeUser.SendKeys("The End");

            driver.Quit();


        }
    }
}