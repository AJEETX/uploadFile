using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Service
{
    public interface IConfigService
    {
        T GetSection<T>(string sectionName) where T : class;
    }
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public T GetSection<T>(string sectionName) where T : class
        {
            if (string.IsNullOrEmpty(sectionName)) return null;

            T section = null;

            try
            {
                section = Activator.CreateInstance(typeof(T)) as T;
                _configuration.GetSection(sectionName).Bind(section);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }

            return section;
        }
    }
}
