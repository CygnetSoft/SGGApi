using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "SearchRequest")]
    public class AssessmentSearchModel
    {

        public MetaAssessmentSearch meta { get; set; }
        public SortByAssessmentSearch sortBy { get; set; }
        public ParametersAssessmentSearch parameters { get; set; }
        public AssessmentsSearch assessments { get; set; }
    }
    public class MetaAssessmentSearch
    {
        public string lastUpdateDateTo { get; set; }
        public string lastUpdateDateFrom { get; set; }

    }
    public class SortByAssessmentSearch
    {
        public string field { get; set; }
        public string order { get; set; }

    }
    public class ParametersAssessmentSearch
    {
        [Required]
        public int page { get; set; }
        [Required]
        public int pageSize { get; set; }

    }
    public class RunAssessmentSearch
    {
        public string id { get; set; }

    }
    public class CourseAssessmentSearch
    {
        public RunAssessmentSearch run { get; set; }
        public string referenceNumber { get; set; }

    }
    public class TraineeAssessmentSearch
    {
        public string id { get; set; }

    }
    public class EnrolmentAssessmentSearch
    {
        public string referenceNumber { get; set; }

    }
    public class TrainingPartnerAssessmentSearch
    {
        [Required]
        public string uen { get; set; }
        public string code { get; set; }

    }
    public class AssessmentsSearch
    {
        public CourseAssessmentSearch course { get; set; }
        public TraineeAssessmentSearch trainee { get; set; }
        public EnrolmentAssessmentSearch enrolment { get; set; }
        public string skillCode { get; set; }
        [Required]
        public TrainingPartnerAssessmentSearch trainingPartner { get; set; }

    }
}
