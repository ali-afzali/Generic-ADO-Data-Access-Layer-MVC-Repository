using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    /// <summary>
    /// Generate SQL commands for a specific POCO
    /// </summary>
    class DbCommandGenerator
    {
        
        public static string OleDbConnectionString
        {get{return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\test.accdb; Persist Security Info=False;";}}

        public static string SqlConnectionString
        { get { return @"Data Source = ALI\HUMBERBRIDGING; Initial Catalog = JOB_PORTAL_DB; Integrated Security = True"; } }

        /// <summary>
        /// Generate (SELECT *) Statement for an specefic POCO
        /// </summary>
        /// <param name="poco">Constructor's POCO</param>
        /// <returns>SQL (SELECT *) INSERT Command</returns>
        internal static string GettAllStr(IPoco poco)
        {
            return $@"SELECT * FROM {poco.TableName}";
        }

        /// <summary>
        /// Generate delete statement for an specefic POCO
        /// @Id is requared
        /// </summary>
        /// <param name="poco">Constructor's POCO </param>
        /// <returns>SQL Delete Command</returns>
        internal static string DeleteStr(IPoco poco)
        {
            return $@"DELETE FROM {poco.TableName} WHERE Id = @Id";
        }

        /// <summary>
        /// Generate INSERT Statement for an specefic POCO
        /// @Fi is requared 
        /// </summary>
        /// <param name="poco">Constructor's POCO</param>
        /// <returns>SQL INSERT Command</returns>
        internal static string AddStr(IPoco poco)
        {
            string itrmsStr = "", valueStr = "";
            int i = 1;
            foreach (var x in poco.Fields)
            {
                itrmsStr += $"[{x.Key}],";
                valueStr += $"@F{i},";
                i++;
            }
            itrmsStr = itrmsStr.Remove(itrmsStr.Length - 1);
            valueStr = valueStr.Remove(valueStr.Length - 1);

            return $@"INSERT INTO {poco.TableName} ({itrmsStr}) VALUES ({valueStr})";
         }

        /// <summary>
        /// Generate Update Statement for an specefic POCO
        /// @Fi and @Id are requared 
        /// </summary>
        /// <param name="poco">Constructor's POCO</param>
        /// <returns>SQL UPDATE Command</returns>
        internal static string UpdateStr(IPoco poco)
        {
            string updateStr = "";
            int i = 1;
            foreach (var x in poco.Fields)
            {
                if (i !=1)
                    updateStr += $"[{x.Key}] = @F{i},";;
                i++;
            }
            updateStr = updateStr.Remove(updateStr.Length - 1);
            return $@"UPDATE {poco.TableName} Set {updateStr} WHERE Id = @Id";
        }
      
    }
}
