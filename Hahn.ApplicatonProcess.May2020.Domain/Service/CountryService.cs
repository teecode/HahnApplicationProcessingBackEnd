using Hahn.ApplicatonProcess.May2020.Data.Utility.HttpClient;
using Hahn.ApplicatonProcess.May2020.Domain.IService;
using Hahn.ApplicatonProcess.May2020.Domain.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Service
{
    public class CountryService : ICountryService
    {
        private AppConfigurationData _appConfigurationData;
        public ILogger<CountryService> _logger { get; }

        public CountryService(IOptionsSnapshot<AppConfigurationData> appConfigurationData, ILogger<CountryService> logger)
        {
            _appConfigurationData = appConfigurationData.Value;
            _logger = logger;
        }

        public async Task<bool> ValidateCountry(string countryname)
        {
            try
            {
                string url = _appConfigurationData.countryapi.Replace("{{name}}", countryname);
                var response = await HttpRequestFactory.Get(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}