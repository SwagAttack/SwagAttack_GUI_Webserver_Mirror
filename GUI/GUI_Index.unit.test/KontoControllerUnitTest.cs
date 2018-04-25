using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUI_Index.Controllers;
using GUI_Index.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
//https://dotnetliberty.com/index.php/2016/01/04/how-to-unit-test-asp-net-5-mvc-6-modelstate/


namespace WebserverUnitTests
{
	[TestFixture]
	class KontoControllerUnitTest
    {
	    private readonly IUserProxy _fakeUserProxy = Substitute.For<IUserProxy>();
	    private RegistorViewModel _rvm;

	    private KontoController _uut;

	    [SetUp]
	    public void Init()
	    {
			_uut = new KontoController(_fakeUserProxy);

			_rvm = new RegistorViewModel()
		    {
			    Username = "TestTango200",
			    Email = "test@tango.dk",
			    GivenName = "test",
			    LastName = "tango",
			    Password = "tangoPassword",
			    ConfirmPassword = "tangoPassword"

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

			User savedUser = new User()
			{
				Username = "TestTango200",
				Email = "pretest@Anothoer.dk",
				GivenName = "test",
				LastName = "tango",
				Password = "tangoPassword",
			};
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => savedUser.Email = x.Email));

			//Ack
			_uut.OpretKonto(_rvm);
		    //Assert
		    Assert.That(savedUser.Email, Is.EqualTo(_rvm.Email));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_GivenName()
	    {
		    //Arrange

		    User savedUser = null;
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(savedUser.GivenName, Is.EqualTo(_rvm.GivenName));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_LastName()
	    {
		    //Arrange

		    User savedUser = null;
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(savedUser.LastName, Is.EqualTo(_rvm.LastName));
	    }

	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_Username()
	    {
		    //Arrange

		    User savedUser = null;
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(savedUser.Username, Is.EqualTo(_rvm.Username));
	    }
	    [Test]
	    public void OpretKonto_With_Correct_RVM_call_proxy_Right_Password()
	    {
		    //Arrange

		    User savedUser = null;
		    _fakeUserProxy.CreateInstanceAsync(Arg.Do<User>(x => savedUser = x));

		    //Ack
		    _uut.OpretKonto(_rvm);
		    //Assert

		    Assert.That(savedUser.Password, Is.EqualTo(_rvm.Password));
	    }



	}
}
