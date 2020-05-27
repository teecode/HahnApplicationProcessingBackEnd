using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Response
{
    public class ApplicantResponse
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string familyName { get; set; }
        public string address { get; set; }
        public string countryOfOrigin { get; set; }
        public string emailAddress { get; set; }
        public int age { get; set; }
        public bool hired { get; set; } = false;
    }
}
