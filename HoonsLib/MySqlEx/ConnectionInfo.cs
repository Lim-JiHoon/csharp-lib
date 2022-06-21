using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoonsLib.MySqlEx
{
  public class ConnectionInfo
  {
    public string Server { get; set; } = "";
    public string Uid { get; set; } = "";
    public string Pwd { get; set; } = "";
    public int Port { get; set; } = 3106;
    public string? Database { get; set; } = null;

    public static ConnectionInfo? Default;
    public static void SetDefault(string server, string uid, string pwd, int port = 3106, string? database = null)
    {
      Default = new ConnectionInfo() { Server = server, Uid = uid, Pwd = pwd, Port = port, Database = database };
    }
  }
}
