using Domain.Models;
using GUICommLayer.Interfaces;
using GUI_Index.Controllers;
using GUI_Index.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

//https://dotnetliberty.com/index.php/2016/01/04/how-to-unit-test-asp-net-5-mvc-6-modelstate/


namespace WebserverUnitTests
{
	[TestFixture]
	class KontoControllerUnitTest
    {
	    private IUserProxy _fakeUserProxy;
	    private RegistorViewModel _rvm;
	    private User _savedUser;


		private KontoController _uut;

	    [SetUp]
	    public void Init()
	    {
		    _fakeUserProxy = Substitute.For<IUserProxy>();
			_uut = new KontoController(_fakeUserProxy);

			_rvm = new RegistorViewModel()
		    {
			    Username = "TestTango200",
			    Email = "test@tango.dk",
			    GivenName = "Test",
			    LastName = "Tango",
			    Password = "tangoPassword",
			    ConfirmPassword = "tangoPassword"
		    };
		    _savedUser = new User()
		    {
			    Username = "savedUser",
			    Email = "savedUser@Anothoer.dk",
			    GivenName = "Saved",
			    LastName = "User",
			    Password = "savedUserPas",
		    };
		}

	    [Test]
	    public void OpretKonto()
	    {
			var result = _uut.OpretKonto() as ViewResult;

		    Assert.AreEqual("OpretKonto", result.ViewName);
		}

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_Email()
	    {
			//Arrange
			
			
		    /*User savedUser = null;*/
			_fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => _savedUser.Email = x.Email));

			//Ack
			_uut.OpretKonto(_rvm);
		    //Assert
		    Assert.That(_savedUser.Email, Is.EqualTo(_rvm.Email));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_GivenName()
	    {
		    //Arrange
			
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => _savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(_savedUser.GivenName, Is.EqualTo(_rvm.GivenName));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_LastName()
	    {
		    //Arrange
			
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => _savedUser = x));

			//Ack
			_uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(_savedUser.LastName, Is.EqualTo(_rvm.LastName));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_Username()
	    {
		    //Arrange
			
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => _savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(_savedUser.Username, Is.EqualTo(_rvm.Username));
	    }
	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_Password()
	    {
		    //Arrange
			
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => _savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(_savedUser.Password, Is.EqualTo(_rvm.Password));
	    }

	   /* [Test]
	    public void Chexh stort begyn()
	    {
		    
	    }*/

	}
}
