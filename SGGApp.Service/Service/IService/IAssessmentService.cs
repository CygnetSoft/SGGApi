using System.Threading.Tasks;
using SGGApp.Utilities.ViewModel;

namespace SGGApp.Service.Service.IService
{
    public interface IAssessmentService
    {
        Task<object> AssessmentView(string assessmentNo);
        Task<object> AssessmentSearch(AssessmentSearchModel assessment);
        Task<object> AssessmentCreate(AssessmentAddModel assessment);
        Task<object> AssessmentUpdate(AssessmentUpdateModel assessment, string assessmentNo);
    }
}
