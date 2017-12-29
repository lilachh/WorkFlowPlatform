using System;
using System.Configuration;

namespace WorkFlowEngine.DBUtility
{
    /// <summary>
    /// This class provides methods to get constants or config info in web.config
    /// </summary>
    public class PubConstant
    {        
        /// <summary>
        /// Get connection string in web.config
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                //string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];       
                string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString; 
            }
        }

        /// <summary>
        /// Get config info in web.config by configuration key
        /// </summary>
        /// <param name="configName">configuration key</param>
        /// <returns>configuration value</returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }
    }
}
