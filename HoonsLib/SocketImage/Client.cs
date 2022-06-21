using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MySocket
{
  public class Client
  {
    private readonly string ip;
    private readonly int port;

    public Client(string ip, int port)
    {
      this.ip = ip;
      this.port = port;
    }

    public void Send(byte[] buffer)
    {
      var ipep = new IPEndPoint(IPAddress.Parse(ip), port);
      byte[] binary = new byte[buffer.Length];
      using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
      {
        client.Connect(ipep);
        client.Send(new byte[] { 0 });
        client.Send(BitConverter.GetBytes(binary.Length));
        client.Send(new byte[] { 1 });
        client.Send(buffer);


        buffer = new byte[1];
        client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
      };     
    }
    private void Receive(IAsyncResult result)
    {
      
    }
  }
}
