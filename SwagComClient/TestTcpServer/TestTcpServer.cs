using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestTcpServer
{

	public class file_server
	{
		/// <summary>
		/// The PORT
		/// </summary>
		const int PORT = 9000;

		//The IpAdress
		IPAddress localAddr = IPAddress.Parse("10.0.0.1");


		/// <summary>
		/// The BUFSIZE
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// Opretter en socket.
		/// Venter på en connect fra en klient.
		/// Modtager filnavn
		/// Finder filstørrelsen
		/// Kalder metoden sendFile
		/// Lukker socketen og programmet
		/// </summary>
		public file_server()
		{
			// Initializes a new instance of the <see cref="file_server"/> class.
			TcpListener serverSocket = new TcpListener(localAddr, PORT);
			serverSocket.Start();

			// Enter the listening loop.
			while (true)
			{
				Console.Write("Waiting for a connection... ");

				//wait for someone to connect
				TcpClient tcpclient = serverSocket.AcceptTcpClient();
				Console.WriteLine("Connected!");

				// create Strem
				NetworkStream stream = tcpclient.GetStream();

				//read from stram
				String line = "";
				char ch;

				while ((ch = (char)stream.ReadByte()) != 0)
					line += ch;

				Console.WriteLine("{0} recieved", line);

				//ack
				if (line == "test")
				{
					string reply = "ok";
					System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
					stream.Write(encoding.GetBytes(reply), 0, reply.Length);
					stream.WriteByte(0);
				}
				// Shutdown and end connection
				tcpclient.Close();
			}


		}
			
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main(string[] args)
		{
			Console.WriteLine("Server starts...");
			new file_server();
		}
	}
	
}
