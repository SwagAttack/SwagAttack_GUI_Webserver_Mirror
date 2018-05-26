using System;
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

namespace SystemTest
{

   

    [TestFixture]
    public class SystemTest
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
        private readonly User user2 = new User();

        //Paths for FirefoxSetup  - ændre den til egen sti for at køre testen
        private string pathForGecko = @"C:\Users\Max\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0";
        private string Gecko = "geckodriver.exe";
        private string pathToFireFox = @"C:\Users\Max\AppData\Local\Mozilla Firefox\firefox.exe";
        public static string host = "https://swagattack.azurewebsites.net/";
        private IWebDriver ChromeDriver = new ChromeDriver(@"C:\Users\Max\Documents\chromedriver");



        public static string setrandoms()
        {


        Random rnd = new Random();
        int usernum = rnd.Next(1, 1000);
        string random_user = "SystemTest" + usernum;

            return random_user;

        }

        public string FirefoxUser = setrandoms();


        public string ChromeUser = setrandoms();

        [SetUp]
        public void Setup()
        {

     


        //TestUser

            user.Username = "SystemTest920";
            user.Password = "Maxmaxmax";
            user.Email = "SystemTest@123.dk";
            user.GivenName = "SystemTest";
            user.LastName = "SystemTest";

            //TestUser2

            user2.Username = "SystemTest310";
            user2.Password = "Maxmaxmax";
            user2.Email = "SystemTest@123.dk";
            user2.GivenName = "SystemTest";
            user.LastName = "SystemTest";



            //TestPersUI
            _person.Username = "SystemTest";
            _person.Password = "Maxmaxmax";
            _person.Email = "SystemTest@123.dk";
            _person.GivenName = "SystemTest";
            _person.LastName = "SystemTest";

            //The New User created in Create 1:
            
        }


        //test opret konto OBS; husk at ændre brugernavne!

        [Test]
        public async Task a1System_Test_Opret()
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


            typeUser.SendKeys(FirefoxUser);
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.GivenName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();


           

            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo(host));

        }


        [Test]
        public async Task a2CreateTheSecondUserForOurTest()
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




            typeUser.SendKeys(ChromeUser);
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.GivenName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();




            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo(host));

        }


        //Test Opret Fail
        [Test]
        public async Task System_Test_OpretUsernameFail()
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


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("value cannot be less than 8 characters"));
        }


        //Test Opret Fail
        [Test]
        public async Task System_Test_OpretGivenNameFail()
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


            string testUrl = driver.Url;

              driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("Use letters only please"));
        }

        //Test Opret Fail
        [Test]
        public async Task System_Test_OpretLastNameFail()
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
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys("0!!=)#");
            Email.SendKeys(user.Email);
            
            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();
            
            string lastnameFail = driver.FindElement(By.XPath("//div/div/div/div/form/div/ul/li[1]")).Text;


            string testUrl = driver.Url;

           driver.Quit();

            Assert.That(lastnameFail, Is.EqualTo("Use letters only please"));
        }

        //Test Opret Fail
        [Test]
        public async Task System_Test_EmailFail()
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
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.LastName);
            Email.SendKeys("0!!=)#");

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.Not.EqualTo(host));
        }


        [Test]
        public async Task System_Test_PasswFail()
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

            string password2 = "forkertkode";

            typeUser.SendKeys("NybrugerOK");
            typePass.SendKeys(user.Password);
            typePass2.SendKeys(password2);
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.LastName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();

            string usernameFail = driver.FindElement(By.XPath("//div/div/div/div/form/div/ul/li[1]")).Text;


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("Password not the same"));
        }


        [Test]
        public async Task System_Test_Passw2Fail()
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

            string password2 = "forkertkode";

            typeUser.SendKeys("NybrugerOK");
            typePass.SendKeys(password2);
            typePass2.SendKeys(user.Password);
            GivenName.SendKeys(user.GivenName);
            LastName.SendKeys(user.LastName);
            Email.SendKeys(user.Email);

            driver.FindElement(By.XPath("//div/div/div/div/form/div/div/div/input[1]")).Click();

            string usernameFail = driver.FindElement(By.XPath("//div/div/div/div/form/div/ul/li[1]")).Text;


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(usernameFail, Is.EqualTo("Password not the same"));
        }


        //System_Test_Login
        [Test]
        public async Task System_Test_Login()
        {
            
            FireFoxSetup(out service, out op);
           
            setUp(service, op, out driver);

            typeUser = driver.FindElement(By.Name("Username"));
            typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user.Username);
            typePass.SendKeys(user.Password);

            driver.FindElement(By.XPath("//button[1]")).Click();



            string loginName = driver.FindElement(By.XPath("/html/body/div/div/div/div[1]")).ToString();

            string testUrl = driver.Url;

           driver.Quit();

           Assert.That(testUrl, Is.EqualTo(host+ "Home/PostLogInd?username="+user.Username+"&Password=Maxmaxmax&Email=SystemTest@123.dk&GivenName=SystemTest&LastName=SystemTest"));

           

        }
        //Test_Login_LogUd
        [Test]
        public async Task System_Test_Login_LogUd()
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

            Assert.That(testUrl, Is.EqualTo(host));



        }

        //System_test_LoginFail
        [Test]
        public async Task System_Test_LoginFail()
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
           

           Assert.That(testUrl,Is.EqualTo(host));



        }


        //System_Test_Login_OpretLobby
        [Test]
        public async Task System_Test_Login_OpretLobby()
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

          Assert.That(testUrl, Is.EqualTo(host+"Lobby/OpretLobby"));



        }


        //Test_Login_OpretLobby
        [Test]
        public async Task System_Test_Login_OpretLobby_Typed()
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

            driver.FindElement(By.Id("ForLadLobby")).Click();
            driver.FindElement(By.XPath("//div/div/button[1]")).Click();

            string testUrl = driver.Url;

            driver.Quit();

             Assert.That(testUrl, Is.EqualTo(host+ "Home/PostLogInd"));



        }


        //Test_Login_TilslutLobby
        [Test]
        public async Task System_Test_Login_Tilslutlobby()
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

            Assert.That(testUrl, Is.EqualTo(host+"Lobby/TilslutLobby?password=Maxmaxmax&username="+user.Username));



        }

        [Test]
        public async Task System_Test_Login_OpretLobby_Typed_Leave()
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

            lobbyId.SendKeys("TestLobbydwqd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            driver.FindElement(By.Id("ForLadLobby")).Click();



            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo(host + "Lobby/TilslutLobby"));



        }

        //Createlobby AND LEAVE and go back
        [Test]
        public async Task System_Test_Login_OpretLobby_Typed_Leave_Back()
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

            lobbyId.SendKeys("TestLobbydwqd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            driver.FindElement(By.Id("ForLadLobby")).Click();
            driver.FindElement(By.XPath("//div/div/button[1]")).Click();

            


            string testUrl = driver.Url;

            driver.Quit();

            Assert.That(testUrl, Is.EqualTo(host + "Home/PostLogInd"));



        }

        [Test]
        public async Task System_Test_Login_OpretLobby_Type_Lobby_Admin_Istrue()
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

            lobbyId.SendKeys("TestLobbydwquhd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            string btnexitst = driver.FindElement(By.XPath("//div/div/form/div/div/button[1]")).Text;
            string text = "Start spil";

            driver.FindElement(By.Id("ForLadLobby")).Click();

            driver.Quit();

            Assert.That(btnexitst, Is.EqualTo(text));

        }



        [Test]
        public async Task System_Test_Login_OpretLobby_2_usersCanLogin()
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

            lobbyId.SendKeys("TestLobbydwquhd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();


            //////////////////////////Test 2 Joining//////////////
            /// 
            ChromeDriver.Navigate().GoToUrl(host);

            string testUrl = driver.Url;

            typeUser = ChromeDriver.FindElement(By.Name("Username"));
            typePass = ChromeDriver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user2.Username);
            typePass.SendKeys(user.Password);

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/form/div/div/a[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/div/table/tbody/tr/td/form/button[1]")).Click();

            string IsLoggedIn = driver.FindElement(By.XPath("//div/div/form/div/ul/li[1]")).Text;

            string compare = "User: SystemTest310 Signed On!";

              driver.FindElement(By.Id("ForLadLobby")).Click();

               ChromeDriver.FindElement(By.Id("ForLadLobby")).Click();

             driver.Quit();
            ChromeDriver.Quit();

            Assert.That(IsLoggedIn, Is.EqualTo(compare));



        }

        [Test]
        public async Task System_Test_Login_OpretLobby_2_usersCanLogin_AdminLogout_2User_Is_Admin()
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

            lobbyId.SendKeys("TestLobbydwquhd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();


            //////////////////////////Test 2 Joining//////////////
            /// 
            ChromeDriver.Navigate().GoToUrl(host);

            string testUrl = driver.Url;

            typeUser = ChromeDriver.FindElement(By.Name("Username"));
            typePass = ChromeDriver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user2.Username);
            typePass.SendKeys(user.Password);

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/form/div/div/a[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/div/table/tbody/tr/td/form/button[1]")).Click();


            driver.FindElement(By.Id("ForLadLobby")).Click();


            driver.FindElement(By.XPath("//div/div/div/table/tbody/tr/td/form/button[1]")).Click();

            ChromeDriver.Navigate().Refresh();

            string btnexitst = ChromeDriver.FindElement(By.XPath("//div/div/form/div/div/button[1]")).Text;
            string text = "Start spil";

            ChromeDriver.FindElement(By.Id("ForLadLobby")).Click();
            driver.FindElement(By.Id("ForLadLobby")).Click();

            driver.Quit();
            ChromeDriver.Quit();

            Assert.That(btnexitst, Is.EqualTo(text));

        }



        [Test]
        public async Task System_Test_Login_OpretLobby_2Users_Is_loggedIn_Chat()
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

            lobbyId.SendKeys("TestLobbydwquhd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            driver.FindElement(By.Id("messageInput")).SendKeys("hej fra test 1");
            driver.FindElement(By.Id("sendButton")).Click();

            //////////////////////////Test 2 Joining//////////////
            /// 
            ChromeDriver.Navigate().GoToUrl(host);

            string testUrl = driver.Url;

            typeUser = ChromeDriver.FindElement(By.Name("Username"));
            typePass = ChromeDriver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user2.Username);
            typePass.SendKeys(user2.Password);

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/form/div/div/a[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/div/table/tbody/tr/td/form/button[1]")).Click();



            

            ChromeDriver.FindElement(By.Id("messageInput")).SendKeys("hej fra test 2");
            ChromeDriver.FindElement(By.Id("sendButton")).Click();


            string pathTotext = driver.FindElement(By.XPath("//div/div/form/div/ul/li[3]")).Text;
            string text = "SystemTest310 says hej fra test 2";

             driver.FindElement(By.Id("ForLadLobby")).Click();

               ChromeDriver.FindElement(By.Id("ForLadLobby")).Click();

            ChromeDriver.Quit();
            driver.Quit();

            Assert.That(pathTotext, Is.EqualTo(text));



        }



        [Test]
        public async Task System_Test_Login_OpretLobby_2Users_Is_loggedIn_Chat_CanReply()
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

            lobbyId.SendKeys("TestLobbydwquhd");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();


            //////////////////////////Test 2 Joining//////////////
            /// 
            ChromeDriver.Navigate().GoToUrl(host);

            string testUrl = driver.Url;

            typeUser = ChromeDriver.FindElement(By.Name("Username"));
            typePass = ChromeDriver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user2.Username);
            typePass.SendKeys(user2.Password);

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/form/div/div/a[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/div/table/tbody/tr/td/form/button[1]")).Click();





            ChromeDriver.FindElement(By.Id("messageInput")).SendKeys("hej fra test 2");
            ChromeDriver.FindElement(By.Id("sendButton")).Click();

            driver.FindElement(By.Id("messageInput")).SendKeys("hej fra test 1");
            driver.FindElement(By.Id("sendButton")).Click();


            string pathTotext = ChromeDriver.FindElement(By.XPath("//*[@id=\"Messages\"]/li[2]")).Text;
            string text = "SystemTest920 says hej fra test 1";

            driver.FindElement(By.Id("ForLadLobby")).Click();

            ChromeDriver.FindElement(By.Id("ForLadLobby")).Click();

            ChromeDriver.Quit();
            driver.Quit();

            Assert.That(pathTotext, Is.EqualTo(text));

        }




        [Test]
        public async Task System_Test_Login_OpretLobby_2Users_HasOwnLobby()
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

            lobbyId.SendKeys("TestLobby1");

            driver.FindElement(By.XPath("//div/div/div/form/div/div/button[1]")).Click();


            //////////////////////////Test 2 creating//////////////
            /// 
            ChromeDriver.Navigate().GoToUrl(host);

            string testUrl = driver.Url;

            typeUser = ChromeDriver.FindElement(By.Name("Username"));
            typePass = ChromeDriver.FindElement(By.Name("Password"));

            typeUser.SendKeys(user2.Username);
            typePass.SendKeys(user2.Password);

            ChromeDriver.FindElement(By.XPath("//button[1]")).Click();

            ChromeDriver.FindElement(By.XPath("//button[2]")).Click();

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div/input[1]")).SendKeys("TestLobby2");

            ChromeDriver.FindElement(By.XPath("//div/div/div/form/div/div[2]/button[1]")).Click();





            ChromeDriver.FindElement(By.Id("messageInput")).SendKeys("I am lonely 2");
            ChromeDriver.FindElement(By.Id("sendButton")).Click();

            driver.FindElement(By.Id("messageInput")).SendKeys("I am lonely test 1");
            driver.FindElement(By.Id("sendButton")).Click();

           string IsAlone = ChromeDriver.FindElement(By.XPath("//*[@id=\"UsersInLobby\"]")).Text;


            //string pathTotext = ChromeDriver.FindElement(By.XPath("//*[@id=\"Messages\"]/li[2]")).Text;
            //string text = "SystemTest674 says hej fra test 1";

            driver.FindElement(By.Id("ForLadLobby")).Click();

            ChromeDriver.FindElement(By.Id("ForLadLobby")).Click();

            ChromeDriver.Quit();
            driver.Quit();

            Assert.That(IsAlone.Contains("SystemTest674"), Is.False);

        }

        //*********************************Setup methods*******************************//////



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
            driver.Navigate().GoToUrl(host);
        }
    }
}