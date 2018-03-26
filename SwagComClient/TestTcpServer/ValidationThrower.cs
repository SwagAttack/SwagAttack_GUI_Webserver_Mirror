﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTcpServer
{
	/// <summary>
	/// Template for throwing in relation to user validation
	/// </summary>
	public abstract class BaseThrow
	{
		/// <summary>
		/// The recipe
		/// </summary>
		/// <param name="value">the setters value</param>
		public void Validate(string value)
		{
			throwArgumentIf(string.IsNullOrEmpty(value), "value cannot be empty!");
			validateMore(value);
		}
		/// <summary>
		/// this method is the one to overide in derived classes, use this to specify specific throws
		/// </summary>
		/// <param name="value">value from setter</param>
		protected abstract void validateMore(string value);
		/// <summary>
		/// A helper method
		/// </summary>
		/// <param name="condition">when to throw a message</param>
		/// <param name="message">The message to throw</param>
		protected void throwArgumentIf(bool condition, string message)
		{
			if (condition)
				throw new ArgumentException(message);
		}
	}

	public class UsernameThrow : BaseThrow
	{

		protected override void validateMore(string value)
		{
			throwArgumentIf(value.Length < 8, "value cannot be less than 8 characters");
			throwArgumentIf(value.Length > 20, "value cannot be more than 20 characters");
			throwArgumentIf(value.Any(x => !Char.IsLetterOrDigit(x)), "value only be letters from a to z or numbers");
		}

	}

	public class PasswordThrow : BaseThrow
	{
		protected override void validateMore(string value)
		{
			throwArgumentIf(value.Length < 8, "value cannot be less than 8 characters");
		}
	}

	public class EmailThrow : BaseThrow
	{
		protected override void validateMore(string value)
		{
			throwArgumentIf(value.Length < 5, "value cannot be less than 5 characters");
			throwArgumentIf(!(value.Contains("@") && value.Contains(".")), "Not a valid Email");
		}
	}

	public class NameThrow : BaseThrow
	{
		protected override void validateMore(string value)
		{
			throwArgumentIf((value.Contains(".") || value.Length < 2), "Initials is not allowed");
			throwArgumentIf(value.Any(x => !Char.IsLetter(x)), "Name can only contain letters");
		}
	}

}
