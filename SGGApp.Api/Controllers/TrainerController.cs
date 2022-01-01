using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SGGApp.Service.Service.IService;
using SGGApp.Utilities.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGGApp.Api.Controllers
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        /// <summary>
        /// Publish Trainer run(s) with sessions if any
        /// </summary>
        /// <param name="enrollment"></param>
        /// <param name="uen"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to publish Trainer run(s) with sessions (if any).")]
        [HttpPost("/trainingProviders/{uen}/trainers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourseRun([FromRoute, BindRequired] string uen, TrainerAddModel enrollment)
        {
            object response = await trainerService.AddTrainerRun(enrollment, uen);
            return Ok(response);
        }

        /// <summary>
        /// Update course run with sessions
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="updateRuns"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to update course run by training provider UEN, course reference number and course run id.")]
        [HttpPost("trainingProviders/{uen}/trainers/{trainersid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTrainerRuns([FromRoute, BindRequired] string uen,string trainersid, TrainerAddModel enrollment)
        {
            object response = await trainerService.UpdateTrainerRuns(uen, trainersid, enrollment);
            return Ok(response);
        }
    }
}
