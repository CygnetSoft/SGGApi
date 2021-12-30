using System.Threading.Tasks;
using SGGApp.Utilities.ViewModel;

namespace SGGApp.Service.Service.IService
{
    public interface IEnrolmentsService
    {
        Task<object> EnrollmentSearch(EnrollmentsSearchModel enrollment);
        Task<object> EnrollmentView(string enrollmentNumber);
        Task<object> EnrollmentAdd(EnrollmentsAddModel enrollmentNumber);
        Task<object> EnrollmentUpdate(EnrollmentsUpdateModel enrollmentNumber, string enrollmentNo);
        Task<object> EnrollmentFeesCollect(EnrollmentsFeesCollectModel enrollmentNumber, string enrollmentNo);
    }
}
