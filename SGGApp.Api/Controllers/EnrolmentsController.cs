using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SGGApp.Service.Service.IService;
using SGGApp.Utilities.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Api.Controllers
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]", Name = "Enrolments")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly IEnrolmentsService enrollmentService;
        public EnrolmentsController(IEnrolmentsService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }
        /// <summary>
        /// Search for enrolment record(s)
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Retrieve information including course, trainee, and employer for enrolment records. Retrieve multiple records using search criteria. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnrollmentSearch(EnrollmentsSearchModel enrollment)
        {
            object response = await enrollmentService.EnrollmentSearch(enrollment);
            return Ok(response);
        }
        /// <summary>
        /// Query a specific enrolment record
        /// </summary>
        /// <param name="referenceNumber "></param>
        /// <returns></returns>
        // GET sgg/gov/<EnrollermentController>/5
        [SwaggerOperation(Description = "Retrieve information including course, trainee, and employer for an enrolment record. Query a single record by passing enrolment reference number. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpGet("details/{referenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnrollmentView(string referenceNumber)
        {
            object response = await enrollmentService.EnrollmentView(referenceNumber);
            return Ok(response);
        }
        // POST sgg/gov/<EnrollermentController>
        /// <summary>
        /// Create an enrolment record
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Course, trainee and employer details are submitted to create an enrolment. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnrollmentAdd([FromBody] EnrollmentsAddModel enrollment)
        {
            object response = await enrollmentService.EnrollmentAdd(enrollment);
            return Ok(response);
        }
        /// <summary>
        /// Update or cancel an enrolment record
        /// </summary>
        /// <param name="enrollment"></param>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        // PUT sgg/gov/<EnrollermentController>/5
        [SwaggerOperation(Description = "Course, trainee and employer details are submitted to update an enrolment. Alternatively, pass in enrolment.action as 'Cancel' for cancelling an enrolment. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("update/{referenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnrollmentUpdate([FromBody] EnrollmentsUpdateModel enrollment, string referenceNumber)
        {
            object response = await enrollmentService.EnrollmentUpdate(enrollment, referenceNumber);
            return Ok(response);
        }
        /// <summary>
        /// Update enrolment record
        /// </summary>
        /// <param name="enrollment"></param>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Update fee collection status for a specific enrolment record")]
        [HttpPost("feeCollections/{referenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnrollmentFeesCollect([FromBody] EnrollmentsFeesCollectModel enrollment, [FromRoute, BindRequired] string referenceNumber)
        {
            object response = await enrollmentService.EnrollmentFeesCollect(enrollment, referenceNumber);
            return Ok(response);
        }
    }
}
