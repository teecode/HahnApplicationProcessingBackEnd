using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.IService
{
    public interface ICountryService
    {
        Task<bool> ValidateCountry(string countryname);
    }
}
