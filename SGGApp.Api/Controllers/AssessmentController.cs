using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGGApp.Service.Service.IService;
using SGGApp.Utilities.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Api.Controllers
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService assessmentService;
        public AssessmentController(IAssessmentService assessmentService)
        {
            this.assessmentService = assessmentService;
        }
        /// <summary>
        /// Retrieve an assessment record by reference number
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Assessment record details including course, trainee and results are retrieved. Able to query a single record by passing assessment referenceNumber. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpGet("details/{referenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssessmentView([FromRoute] string referenceNumber)
        {
            object response = await assessmentService.AssessmentView(referenceNumber);
            return Ok(response);
        }
        /// <summary>
        /// Retrieve assessment record(s)
        /// </summary>
        /// <param name="assesment"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Assessment record details including course, trainee and results are retrieved. Able to query multiple records by passing query parameters. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssessmentSearch(AssessmentSearchModel assesment)
        {
            object response = await assessmentService.AssessmentSearch(assesment);
            return Ok(response);
        }
        /// <summary>
        /// Create an assessment record
        /// </summary>
        /// <param name="assesment"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Course, trainee and assessment details are submitted in a request to create the assessment record. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssessmentCreate(AssessmentAddModel assesment)
        {
            object response = await assessmentService.AssessmentCreate(assesment);
            return Ok(response);
        }
        /// <summary>
        /// Update or void an existing assessment record. To void a result, the request fields can be left blank but the void action must be specified.
        /// </summary>
        /// <param name="assessment"></param>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Course, trainee and assessment details are submitted in a request to update the assessment record. Only course details as seen in the Training Partners Gateway should be used.")]
        [HttpPost("update/{referenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssessmentUpdate([FromBody] AssessmentUpdateModel assessment, [FromRoute] string referenceNumber)
        {
            object response = await assessmentService.AssessmentUpdate(assessment, referenceNumber);
            return Ok(response);
        }
    }
}
