using Miro.Client.Interfaces;
using System;
using System.Configuration;

namespace Miro.Client.Services
{
    public class ConfigManager : IConfigManager
    {
        public string? BaseUrl { get; }

        public ConfigManager() 
        {
            try
            {
                BaseUrl = ConfigurationManager.AppSettings["baseUrlAddress"];
            }
            catch(Exception ex)
            {
                throw new Exception();   
            }
        }
    }
}
