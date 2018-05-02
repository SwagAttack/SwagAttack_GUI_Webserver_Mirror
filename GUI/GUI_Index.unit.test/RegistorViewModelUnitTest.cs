using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GUI_Index.ViewModels;
using NUnit.Framework;

namespace WebserverUnitTests
{
	[TestFixture]
	class RegistorViewModelUnitTest
	{
		private RegistorViewModel _uut;

		private System.ComponentModel.DataAnnotations.ValidationContext _context;
		private List<ValidationResult> _result;

		[SetUp]
	    public void Init()
	    {
			_uut = new RegistorViewModel()
			{
				Username = "TestTango200",
				Email = "test@tango.dk",
				GivenName = "test",
				LastName = "tango",
				Password = "tangoPassword",
				ConfirmPassword = "tangoPassword"

			};
		    _context = new System.ComponentModel.DataAnnotations.ValidationContext(_uut, null, null);
		    _result = new List<ValidationResult>();

		}

		[Test]
		public void RegistorViewModel_correct()
		{
			var valid = Validator.TryValidateObject(_uut, _context, _result, true);

			Assert.True(valid);
		}

		//More than 20 charactor
		[TestCase("AVeryVeryVeryVeryLongUsername", false)]
		//Less than 8 charactor
		[TestCase("AUser", false)]
		//Between 8 and 20
		[TestCase("aUsername", true)]
		public void Username_validation(string username, bool localresult)
		{
			_uut.Username = username;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);
		}


		//Initials
		[TestCase("A", false)]
		//NOt only letters
		[TestCase("Name2", false)]
		//Small letters
		[TestCase("name", true)]
		//Big letters
		[TestCase("NAME", true)]
		public void Firstname(string firstname, bool localresult)
		{
			_uut.GivenName = firstname;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);

		}
		//Initials
		[TestCase("A", false)]
		//NOt only letters
		[TestCase("Name2", false)]
		//Small letters
		[TestCase("name", true)]
		//Big letters
		[TestCase("NAME", true)]
		public void LastName(string lastname, bool localresult)
		{
			_uut.LastName = lastname;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);

		}
		//Less than five charactor
		[TestCase("N@2", false)]
		//No @
		[TestCase("mail", false)]
		public void Email(string email, bool localresult)
		{
			_uut.Email = email;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);

		}
		
		//Less than 8
		[TestCase("Pass", false)]
		//More than 8
		[TestCase("tangoPassword", true)]
		public void Password(string password, bool localresult)
		{
			_uut.Password = password;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);

		}

		//Not the samme as Password
		[TestCase("Pass", false)]
		//the samme as Password
		[TestCase("tangoPassword", true)]
		public void ConfirmPassword(string password, bool localresult)
		{
			_uut.Password = password;

			var valid = Validator.TryValidateObject(_uut, _context, _result, localresult);

			Assert.True(valid);

		}


	}
}
