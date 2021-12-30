using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{

    [SwaggerSchema(Title = "SearchRequest")]
    public class EnrollmentsSearchModel
    {
        public EnrolmentSearch enrolment { get; set; }
        public MetaSearch meta { get; set; }
        public SortBySearch sortBy { get; set; }
        public ParametersSearch parameters { get; set; }
    }
    public class TrainingPartnerSearch
    {
        public string uen { get; set; }
        public string code { get; set; }

    }
    public class RunSearch
    {
        public string id { get; set; }

    }
    public class CourseSearch
    {
        public string referenceNumber { get; set; }
        public Run run { get; set; }

    }
    public class IdTypeSearch
    {
        public string type { get; set; }
    }
    public class EmployerSearch
    {
        public string uen { get; set; }
    }
    public class FeesSearch
    {
        public string feeCollectionStatus { get; set; }

    }
    public class TraineeSearch
    {
        public string id { get; set; }
        public IdTypeSearch idType { get; set; }
        public string sponsorshipType { get; set; }
        public EmployerSearch employer { get; set; }
        public FeesSearch fees { get; set; }
        public string enrolmentDate { get; set; }
    }
    public class EnrolmentSearch
    {
        public string status { get; set; }
        public TrainingPartnerSearch trainingPartner { get; set; }
        public CourseSearch course { get; set; }
        public TraineeSearch trainee { get; set; }

    }
    public class MetaSearch
    {
        public string lastUpdateDateFrom { get; set; }
        public string lastUpdateDateTo { get; set; }
    }
    public class SortBySearch
    {
        public string field { get; set; }
        public string order { get; set; }
    }
    public class ParametersSearch
    {
        [Required]
        public int page { get; set; }
        [Required]
        public int pageSize { get; set; }
    }
}
