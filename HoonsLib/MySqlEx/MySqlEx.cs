using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static HoonsLib.MySqlEx.ConnectionInfo;

namespace HoonsLib.MySqlEx
{
  public class MySqlEx : IDisposable
  {
    private MySqlConnection _conn;
    public MySqlEx()
    {
      string connectionString = $"server={Default?.Server};uid={Default?.Uid};pwd={Default?.Pwd};port={Default?.Port};database={Default?.Database};";
      _conn = new MySqlConnection(connectionString);
      _conn.Open();
    }

    public async Task<long?> Execute(string sql)
    {
      using (MySqlCommand cmd = new MySqlCommand(sql, _conn) { CommandText = sql })
      {
        return await cmd.ExecuteScalarAsync() as long?;
      }
    }

    public async Task<DbDataReader> Reader(string sql)
    {
      using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
      {
        return await cmd.ExecuteReaderAsync();
      }
    }

    public void Dispose()
    {
      if (_conn != null)
      {
        _conn.Close();
        _conn.Dispose();
      }
    }
  }
}
