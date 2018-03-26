using System;
using System.Collections.Generic;
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
		
	    public bool SendString(string input)
	    {
		    TcpClient client = new TcpClient(_serverAddr, PORT);

		    // Get a client stream for reading and writing.
		    NetworkStream stream = client.GetStream();

		  
			    Console.WriteLine($"Sending {input}");

			    //Converting string to byte
			   // UTF8Encoding encoding = new UTF8Encoding();
			   // Byte[] data = encoding.GetBytes(input);


				// Sending Input bytes to server 
				 writeTextTCP(stream, input);

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
					return true;
				}
		    Console.WriteLine("send failed");
			LogLine("send failed");
		    return false;
		}

	    private static void writeTextTCP(NetworkStream outToServer, String line)
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
		    var path = System.IO.Path.GetDirectoryName(
			    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(@"{path}\log.txt") )
			{
				foreach (var lines in input)
				{
					file.WriteLine(lines);
				}
			}
			
	    }
    }
}
