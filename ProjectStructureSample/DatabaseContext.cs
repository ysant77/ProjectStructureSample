using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectStructureSample
{
    public  class DatabaseContext 
    {
        private readonly string _connectionString;
        private static DatabaseContext _databaseContext = null;
        private SqlConnection _sqlConnection;
        public Dictionary<string, Type> Properties { get; set; }
        private DatabaseContext()
        {
            _connectionString = App.ConnectionString;
            _sqlConnection = new SqlConnection(_connectionString);
            Properties = new Dictionary<string, Type>();
        }

        public static DatabaseContext GetInstance()
        {
            if (_databaseContext == null)
                _databaseContext = new DatabaseContext();
            return _databaseContext;
        }

        public IDictionary<string, string> GetClassProperties(string procedureName)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();
            var command = new SqlCommand("getAllProject1Models", _sqlConnection);
            //var command = new SqlCommand(procedureName, _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            var adapter = new SqlDataAdapter(command);
            var datatable = new DataTable();
            adapter.Fill(datatable);
            var dict = new Dictionary<string, string>();
            for(int index = 0; index < datatable.Columns.Count; index++)
            {
                dict.Add(datatable.Columns[index].ColumnName, datatable.Columns[index].DataType.Name);
            }
            
            _sqlConnection.Close();
            return dict;
        }

        public DataTable GetProjectModel(string procedureName)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();
            //var command = new SqlCommand("getAllProject1Models", _sqlConnection);
            var command = new SqlCommand(procedureName, _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            var adapter = new SqlDataAdapter(command);
            var datatable = new DataTable();
            adapter.Fill(datatable);
            Properties.Clear();
            
            for (int index = 0; index < datatable.Columns.Count; index++)
            {
                Properties.Add(datatable.Columns[index].ColumnName, datatable.Columns[index].DataType);
            }
            
            return datatable;
        }

    }
}
