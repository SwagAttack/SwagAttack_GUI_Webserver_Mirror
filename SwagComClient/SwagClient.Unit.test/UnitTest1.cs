using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security;
using System.Text;
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;


namespace SwagClient.Unit.test
{
	[TestClass]
	public class SwagCommunicator
	{
		private TcpClient _tcpClietnFake;
		//private NetworkStream _stream;

		private ISwagCommunicator _uut; 

		[SetUp]
		public void Setup()
		{
			_tcpClietnFake = new TcpClient("127.0.0.1",1100);
			//_stream = Substitute.For<NetworkStream>();

			_uut = new ClassLibrary1.SwagClient("127.0.0.1");
		}

		[Test]
		public void SendString_TCP()
		{
			Byte[] ack = new byte[256];

			//arrange
			//_stream.Read(ack, 0, ack.Length).Returns(0).AndDoes(x => ack = Encoding.ASCII.GetBytes("ok"));
			
			//act
			
			//asser
			Assert.That(_uut.SendString("test"), Is.True);
		}

		[Test]
		public void SendString_logfile()
		{

			//arrange
			var path = System.IO.Path.GetDirectoryName(
				System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

			//act
			_uut.SendString("test");

			string text = File.ReadAllText(@"{path}\log.txt");

			var result = text.Split('\n').Reverse().Take(2).ToArray();
			
			//asser
			Assert.That(result[1], Is.EqualTo("test"));
		}
	}

}
