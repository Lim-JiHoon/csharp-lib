using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MySocket
{
  public class Server : Socket
  {
    readonly Action<Bitmap> action;

    public Server(Action<Bitmap> action) : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    {
      base.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
      base.Listen(10);
      BeginAccept(Accept, this);
      this.action = action;
    }

    private void Accept(IAsyncResult result)
    {
      var client = new Client(EndAccept(result), action);
      BeginAccept(Accept, this);
    }

    class File
    {
      protected State state = State.STATE;
      public byte[]? Binary { get; set; }
    }
    class Client : File
    {
      private Socket socket;
      private readonly Action<Bitmap> action;
      private byte[] buffer;
      private int seek = 0;
      public Client(Socket socket, Action<Bitmap> action)
      {
        this.socket = socket;
        this.action = action;
        buffer = new byte[1];
        this.socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
      }

      private void Receive(IAsyncResult result)
      {
        if (socket.Connected)
        {
          int size = this.socket.EndReceive(result);
          if (state == State.STATE)
          {
            switch (buffer[0])
            {
              case 0:
                state = State.BUFFERSIZE;
                buffer = new byte[4];
                break;
              case 1:
                state = State.BUFFER;
                buffer = new byte[Binary.Length];
                break;
            }
          }
          else if (state == State.BUFFERSIZE)
          {
            Binary = new byte[BitConverter.ToInt32(buffer, 0)];
            buffer = new byte[1];
            state = State.STATE;
          }
          else if (state == State.BUFFER)
          {
            MemoryStream ms = new MemoryStream(buffer);
            var image = Image.FromStream(ms);
            action.Invoke((Bitmap)image);

            socket.Send(new byte[1]); // 테스트

            socket.Disconnect(false);
            socket.Close();
            socket.Dispose();
            return;
          }
          this.socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
        }
      }
    }

  }

  enum State
  {
    STATE,
    BUFFERSIZE,
    BUFFER
  }
}
