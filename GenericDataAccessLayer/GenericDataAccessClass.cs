using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;


namespace GenericDataAccessLayer
{
    class GenericDataAccessClass<T> where T : IPoco, new()
    {

        public string generatedSqlCommand //for test
        {
            get { return _executedCommand; }
        }
        private string _executedCommand = ""; // for test
        //the default value must be "" to avaoid eror in (new SqlConnection(_sqlConnStr)) when teh DB is Oledb 
        private readonly string _sqlConnStr = "";
        private readonly string _oledbConnStr = "";
        private Dictionary<string, object> fields;
        private T CtorPoco;

        //private dynamic connection

        public GenericDataAccessClass(T poco)
        {
            CtorPoco = poco;
            fields = poco.Fields;
            if (CtorPoco.DbType == "SQL")
                _sqlConnStr = DbCommandGenerator.SqlConnectionString;
            else if (CtorPoco.DbType == "OleDB")
                _oledbConnStr = DbCommandGenerator.OleDbConnectionString;
        }

        public void Add(params T[] items)
        {

            dynamic conn = new SqlConnection(_sqlConnStr);
            dynamic cmd = new SqlCommand();
            if (CtorPoco.DbType == "OleDB")
            {
                conn = new OleDbConnection(_oledbConnStr);
                cmd = new OleDbCommand();
            }


            conn.Open();
            string addStr = DbCommandGenerator.AddStr(CtorPoco);


            foreach (T poco in items)
            {
                cmd.CommandText = addStr;
                int i1 = 1;
                foreach (var x in fields)
                {
                    cmd.Parameters.AddWithValue($"@f{i1}", x.Value);
                    i1++;
                }
                cmd.Connection = conn;
                _executedCommand = cmd.CommandText; //for test
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            dynamic conn = new SqlConnection(_sqlConnStr);
            dynamic cmd = new SqlCommand();
            if (CtorPoco.DbType == "OleDB")
            {
                conn = new OleDbConnection(_oledbConnStr);
                cmd = new OleDbCommand();
            }

            T[] pocoArray = new T[1000];
            int index = 0;

            cmd.Connection = conn;

            cmd.CommandText = DbCommandGenerator.GettAllStr(CtorPoco);
            conn.Open();
            DbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                List<dynamic> rawItems = new List<dynamic>();

                T poco = new T();
                int i = 0;
                foreach (var x in CtorPoco.Fields)
                {
                    dynamic y = rdr.IsDBNull(i) ? null : rdr.GetValue(i);
                    rawItems.Add(y);
                    i++;
                }
                poco.SetItems(rawItems);
                pocoArray[index] = poco;
                index++;
            }
            _executedCommand = cmd.CommandText; //for test
            conn.Close();
            return pocoArray.Where(t => t != null).ToList();
        }


        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T[] pocos = GetAll().ToArray();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params T[] items)
        {
            {
                dynamic conn = new SqlConnection(_sqlConnStr);
                dynamic cmd = new SqlCommand();
                if (CtorPoco.DbType == "OleDB")
                {
                    conn = new OleDbConnection(_oledbConnStr);
                    cmd = new OleDbCommand();
                }

                cmd.Connection = conn;
                conn.Open();
                foreach (T poco in items)
                {
                    cmd.CommandText = DbCommandGenerator.DeleteStr(CtorPoco);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                    _executedCommand = cmd.CommandText; //for test
                }

            }
        }

        public void Update(params T[] items)
        {
            dynamic conn = new SqlConnection(_sqlConnStr);
            dynamic cmd = new SqlCommand();
            if (CtorPoco.DbType == "OleDB")
            {
                conn = new OleDbConnection(_oledbConnStr);
                cmd = new OleDbCommand();
            }
            cmd.Connection = conn;
            conn.Open();
            foreach (T poco in items)
            {
                cmd.CommandText = DbCommandGenerator.UpdateStr(CtorPoco);

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                int i = 1;
                foreach (var x in fields)
                {
                    cmd.Parameters.AddWithValue($"@f{i}", x.Value);
                    i++;
                }

                cmd.ExecuteNonQuery();
                _executedCommand = cmd.CommandText; //for test
            }

        }

    }
}
