using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ClassLibrary1
{
    public class SwagClient : ISwagCommunicator
    {
	 //   private TcpClient _client;
	 //   private readonly NetworkStream _stream;
	    private string _serverAddr;
	    private int PORT = 9000;

		
		public SwagClient(string serveAddr)
		{
			Console.WriteLine("SwagClient made");
			_serverAddr = serveAddr;

		}
		
	    public string SendString(string input)
	    {
		    TcpClient client = new TcpClient(_serverAddr, PORT);

		    // Get a client stream for reading and writing.
		    NetworkStream stream = client.GetStream();

		  
			    Console.WriteLine($"Sending {input}");
			
				// Sending Input bytes to server 
				 SendTextTCP(stream, input);

			///	stream.Write(data, 0, data.Length);

				Console.WriteLine("listing for reply");
				//Making string for reply & listen for acknoglement
				string ack = ReadTcp(stream);
			
		    //log the line as string. 
			LogLine($" sending: {input}");

			if (ack == "ok")
			{
				Console.WriteLine("send succes");
				LogLine("send succes");
			}
			else { 
				Console.WriteLine("send failed");
				LogLine("send failed");
				}
			return ack;
		}

	    public string RecieveString()
	    {
		    TcpClient client = new TcpClient(_serverAddr, PORT);
		    NetworkStream stream = client.GetStream();

			return ReadTcp(stream);
		  
	    }

		private static void SendTextTCP(NetworkStream outToServer, String line)
	    {
		    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
		    outToServer.Write(encoding.GetBytes(line), 0, line.Length);
		    outToServer.WriteByte(0);
	    }

		private static String ReadTcp(NetworkStream stream)
	    {
		    char ch;
		    String reply = "";

		    while ((ch = (char)stream.ReadByte()) != 0)
		    {
			    reply += ch;
		    }

		    return reply;
	    }

	    private void LogLine(string input)
	    {
		   
		    DateTime timeStamp = DateTime.Now;
			
			string path = @"log.txt";
			if (!File.Exists(path))
			{
				// Create a file to write to.
				using (StreamWriter sw = File.CreateText(path))
				{
					sw.WriteLine($" {timeStamp}: Log created");

				}
			}
			using (StreamWriter file = File.AppendText(path))
			{
				file.WriteLine($"{timeStamp}: {input}");
			}
			
	    }
    }
}
