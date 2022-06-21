using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace HoonsLib.SQLiteEx
{
  public class SQLiteEx : IDisposable
  {
    private SQLiteConnection _conn;
    public SQLiteEx(string filePath)
    {
      if (!System.IO.File.Exists(filePath))
        SQLiteConnection.CreateFile(filePath);

      _conn = new SQLiteConnection($"Data Source={filePath};Version=3");
      _conn.Open();
    }

    public async Task<long?> Execute(string sql)
    {
      using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn) { CommandText = sql})
      {
        return await cmd.ExecuteScalarAsync() as long?;
      }
    }

    public async Task<DbDataReader> Reader(string sql)
    {
      using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
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
