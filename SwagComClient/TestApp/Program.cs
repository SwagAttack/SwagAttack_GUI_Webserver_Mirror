using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Test app started");
			//SwagClient sC = new SwagClient("10.0.0.1");
			SwagClient sC = new SwagClient("127.0.0.1");

			bool test = sC.SendString("test");

			if (test)
			{
				Console.WriteLine("Test = true");
			}
			else
			{
				Console.WriteLine("Test = false");
			}

			Console.ReadKey();
		}
	}
}
