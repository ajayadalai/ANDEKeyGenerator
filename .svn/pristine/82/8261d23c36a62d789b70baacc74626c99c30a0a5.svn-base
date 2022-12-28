using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class KeyGenerationRepository : RepositoryBase
    {
        public KeyGenerationRepository() : base(ConfigConstants.DBPath) { }

        public override string TableName
        {
            get
            {
                return "OrganaisationKeys";
            }
        }


        public int AddKeyDetailsValues(KeyDetailsModel values)
        {
            using (var dataAccess = new DataAccess(DBPath))
            {              
                string command = string.Empty;
               
                command = string.Format(@"INSERT INTO {0} (OrganaisationName,ApplicationName,Key,KeyIssueDateTime) Values(""{1}"",""{2}"",""{3}"",""{4}"")", TableName, values.OrganaisationName,values.ApplicationName,values.Key,values.KeyIssueDateTime);                
                var args = new Dictionary<string, object>
                {
                    { "@value1", values.ToJson()}
                };
                return dataAccess.ExecuteSQL(command, args);
            }
        }


        public List<string> GetKey(string key)
        {
            List<string> retVal = null;
            DataTable table = null;
            string command = string.Empty;
            using (var dataAccess = new DataAccess(DBPath))
            {

                command = string.Format(@"SELECT * FROM {0} where Key=""{1}""", TableName,key);

                table = dataAccess.ExecuteDataTable(command);
            }
            if (table != null && table.Rows.Count > 0)
            {
                retVal = Convert.ToString(table.Rows[0]["ValueList"].ToString()).ToObjects<string>();
            }
            return retVal;
        }

        public List<KeyDetailsModel> GetAllKeys()
        {
            List<KeyDetailsModel> retVal = null;
            DataTable table = null;
            string command = string.Empty;
            using (var dataAccess = new DataAccess(DBPath))
            {

                command = string.Format(@"SELECT * FROM {0} ", TableName);

                table = dataAccess.ExecuteDataTable(command);
            }
            if (table != null && table.Rows.Count > 0)
            {
               
                retVal = (from rw in table.AsEnumerable()
                                     select new KeyDetailsModel()
                                     {
                                         ApplicationName= rw["ApplicationName"].ToString(),
                                         Key= rw["Key"].ToString(),
                                         OrganaisationName = rw["OrganaisationName"].ToString(),
                                         // KeyIssueDateTime=new DateTime(Convert.ToInt64(rw["KeyIssueDateTime"])).ToLocalTime().ToString()
                                         KeyIssueDateTime = rw["KeyIssueDateTime"].ToString()
                                     }).ToList();
            }
            return retVal;
        }
    }
}
