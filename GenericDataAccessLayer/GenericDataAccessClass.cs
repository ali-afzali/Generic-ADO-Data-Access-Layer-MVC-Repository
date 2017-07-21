using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;


namespace GenericDataAccessLayer
{
    class GenericDataAccessClass <T> where T : IPoco, new()
    {
        
        public string generatedSqlCommand //for test
        {
            get { return _executedCommand; }
        }
        private string _executedCommand = ""; // for test

        private readonly string _connStr;
        private Dictionary<string, object> fields;
        private T CtorPoco;

        public GenericDataAccessClass(T poco)
        {
            CtorPoco = poco;
            fields = poco.Fields;
            _connStr = SqlCommandGenerator.ConnectionString;
        }
        public void Add(params T[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                string addStr = SqlCommandGenerator.AddStr(CtorPoco);

                foreach (T poco in items)
                {
                    cmd.CommandText = addStr;
                    int i1 = 1;
                    foreach (var x in fields)
                    {
                        cmd.Parameters.AddWithValue($"@f{i1}", x.Value);
                        i1++;
                    }
                    _executedCommand = cmd.CommandText; //for test
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                T[] pocoArray = new T[1000];
                int index = 0;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = SqlCommandGenerator.GettAllStr(CtorPoco);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
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
                return pocoArray.Where(t => t != null).ToList();
            }
        }


        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T[] pocos = GetAll().ToArray();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params T[] items)
        {
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    conn.Open();
                    foreach (T poco in items)
                    {
                        cmd.CommandText = SqlCommandGenerator.DeleteStr(CtorPoco);
                        cmd.Parameters.AddWithValue("@Id", poco.Id);

                         cmd.ExecuteNonQuery();
                        _executedCommand = cmd.CommandText; //for test
                    }
                }
            }
        }

        public void Update(params T[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (T poco in items)
                {
                    cmd.CommandText = SqlCommandGenerator.UpdateStr(CtorPoco);
                    
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    int i1 = 1;
                    foreach (var x in fields)
                    {
                        cmd.Parameters.AddWithValue($"@f{i1}", x.Value);
                        i1++;
                    }

                    // cmd.ExecuteNonQuery();
                    _executedCommand = cmd.CommandText; //for test
                }
            }
        }

    }
}
