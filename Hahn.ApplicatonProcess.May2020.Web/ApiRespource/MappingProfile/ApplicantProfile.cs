using AutoMapper;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Request;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.ApiRespource.MappingProfile
{
    public class ApplicantProfile : Profile
    {
        public ApplicantProfile()
        {
            CreateMap<ApplicantRequest, Applicant>();
            CreateMap<Applicant, ApplicantResponse>();
        }
    }
}
