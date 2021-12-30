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
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        /// <summary>
        /// Publish course run(s) with sessions if any
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to publish course run(s) with sessions (if any).")]
        [HttpPost("runs/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourseRun([FromBody] CourseAddModel enrollment)
        {
            object response = await courseService.AddCourseRun(enrollment);
            return Ok(response);
        }
        /// <summary>
        /// Update course run with sessions
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="updateRuns"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to update course run by training provider UEN, course reference number and course run id.")]
        [HttpPost("runs/update-runs/{runId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCourseRuns([FromRoute, BindRequired] string runId, CourseUpdateModel updateRuns)
        {
            object response = await courseService.UpdateCourseRuns(runId, updateRuns);
            return Ok(response);
        }
        /// <summary>
        /// Update course run with sessions
        /// </summary>
        /// <param name="deleteCourse"></param>
        /// <param name="courseRunId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to delete course run by training provider UEN, course reference number and course run id.")]
        [HttpPost("runs/delete-course/{courseRunId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCourseRuns(CourseDeleteModel deleteCourse, [FromRoute, BindRequired] string courseRunId)
        {
            object response = await courseService.DeleteCourseRuns(deleteCourse, courseRunId);
            return Ok(response);
        }
        /// <summary>
        /// Upload course session attendance information
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="uploadAttachRunJson"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Attendance" }, Description = "This API is used for training provider to upload course session attendance information.")]
        [HttpPost("runs/upload-attendance/{runId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadAttendance([FromRoute, BindRequired] string runId, UploadAttachmentModel uploadAttachRunJson)
        {
            object response = await courseService.UploadAttendance(runId, uploadAttachRunJson);
            return Ok(response);
        }
        /// <summary>
        /// Retrieve course sessions
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="uen"></param>
        /// <param name="courseReferenceNumber"></param>
        /// <param name="sessionMonth"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Attendance" }, Description = "This API is used to retrieve course sessions based on course reference number, course run ID and month")]
        [HttpGet("runs/sessions/{runId}/{uen}/{courseReferenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseSessions([FromRoute, BindRequired] string runId, [FromRoute] string uen, [FromRoute] string courseReferenceNumber, [FromQuery] string sessionMonth)
        {
            object response = await courseService.GetCourseSessions(runId, uen, courseReferenceNumber, sessionMonth);
            return Ok(response);
        }
        /// <summary>
        /// Retrieve course run based on run ID
        /// </summary>
        /// <param name="runId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This API is used to retrieve course run details based on course reference number and course run ID")]
        [HttpGet("runs/{runId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseRunsByID([FromRoute, BindRequired] string runId)
        {
            object response = await courseService.GetCourseRunsByID(runId);
            return Ok(response);
        }
        [SwaggerOperation(Description = "This API is used to retrieve course session attendance information based on course reference number, course run ID and session ID.")]
        [HttpGet("runs/sessions/attendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAttendanceInformation([FromQuery, BindRequired] string runId, [FromQuery, BindRequired] string uen, [FromQuery, BindRequired] string courseReferenceNumber, [FromQuery] string sessionId)
        {
            object response = await courseService.GetAttendanceInformation(runId, uen, courseReferenceNumber, sessionId);
            return Ok(response);
        }
    }
}
