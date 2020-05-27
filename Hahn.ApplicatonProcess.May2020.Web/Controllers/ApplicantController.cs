using AutoMapper;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.IService;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Request;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{/// <summary>
/// The endpoints here manages applications
/// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/applicant")]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IMapper _mapper;
        private readonly IApplicantService _applicantService;

        public ApplicantController(ILogger<ApplicantController> logger, IMapper mapper, IApplicantService applicantService)
        {
            _logger = logger;
            this._mapper = mapper;
            this._applicantService = applicantService;
        }

        /// <summary>
        /// Create an applicant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicantCreationResponse), StatusCodes.Status201Created)] // Create
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] ApplicantRequest model)
        {
            try
            {
                var applicant = _mapper.Map<Applicant>(model);
                var createdapplicant = await _applicantService.CreateAsync(applicant);

                if (createdapplicant != null)
                {
                    return Created("", new ApplicantCreationResponse
                    {
                        id = createdapplicant.ID,
                        url = $"{ this.Request.Scheme }://{this.Request.Host}{this.Request.PathBase}{ Request.Path.Value}?id={ createdapplicant.ID }"
                    }); ;
                }
                else
                {
                    return BadRequest(new List<string> { "cannot create applicant" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "cannot create applicant" });
            }
        }

        /// <summary>
        /// Update an applicant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <param name="id" example="1">The applicant's id</param>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromQuery] int id, [FromBody] ApplicantRequest model)
        {
            try
            {
                var applicant = _mapper.Map<Applicant>(model);
                applicant.ID = id;
                var checkexist = await _applicantService.ReadSingleAsync(id, false);
                if (checkexist == null)
                    return NotFound();

                if (applicant != null)
                {
                    await _applicantService.UpdateAsync(applicant);
                    return Ok("update successful");
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "cannot update applicant" });
            }
        }

        /// <summary>
        /// Get an applicant with a provided ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <param name="id" example="1">The applicant's id</param>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([FromQuery] int id)
        {
            try
            {
                var applicant = await _applicantService.ReadSingleAsync(id, false);
                if (applicant != null)
                {
                    var response = _mapper.Map<ApplicantResponse>(applicant);
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "cannot get applicant" });
            }
        }

        /// <summary>
        /// Get an applicant with a provided ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// /// <param name="id" example="1">The applicant's id</param>
        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var applicant = await _applicantService.ReadSingleAsync(id, false);
                if (applicant != null)
                {
                    var response = _mapper.Map<ApplicantResponse>(applicant);
                    await _applicantService.DeleteAsync(id);
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "cannot delete applicant" });
            }
        }
    }
}