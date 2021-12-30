using System.Threading.Tasks;
using SGGApp.Utilities.ViewModel;

namespace SGGApp.Service.Service.IService
{
    public interface ICourseService
    {
        Task<object> AddCourseRun(CourseAddModel enrollmentNumber);
        Task<object> GetCourseRunsByID(string runId);
        Task<object> GetAttendanceInformation(string runId, string uen, string courseReferenceNumber, string sessionId);
        Task<object> GetCourseSessions(string runId, string uen, string courseReferenceNumber, string sessionMonth);
        Task<object> UpdateCourseRuns(string runId, CourseUpdateModel updateRun);
        Task<object> UploadAttendance(string runId, UploadAttachmentModel updateAttachRunJson);
        Task<object> DeleteCourseRuns(CourseDeleteModel deleteCourse, string courseRunId);
    }
}
