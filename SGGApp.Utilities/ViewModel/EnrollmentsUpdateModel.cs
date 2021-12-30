using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{

    [SwaggerSchema(Title = "UpdateRequest")]

    public class EnrollmentsUpdateModel
    {
        public EnrolmentEnrollmentsUpdate enrolment { get; set; }
    }
    public class RunEnrollmentsUpdate
    {
        public string id { get; set; }

    }
    public class CourseEnrollmentsUpdate
    {
        public RunEnrollmentsUpdate run { get; set; }

    }
    public class EmployeeContactNumberEnrollmentsUpdate
    {
        public string countryCode { get; set; }
        public string areaCode { get; set; }
        public string phoneNumber { get; set; }
    }
    public class TraineeContactNumberEnrollmentsUpdate
    {
        public string countryCode { get; set; }
        public string areaCode { get; set; }
        public string phone { get; set; }
    }
    public class TraineeEnrollmentsUpdate
    {
        public TraineeContactNumberEnrollmentsUpdate contactNumber { get; set; }
        public string email { get; set; }
    }
    public class ContactEnrollmentsUpdate
    {
        public string fullName { get; set; }
        public EmployeeContactNumberEnrollmentsUpdate contactNumber { get; set; }
        public string email { get; set; }

    }
    public class EmployerEnrollmentsUpdate
    {
        public ContactEnrollmentsUpdate contact { get; set; }

    }
    public class FeesEnrollmentsUpdate
    {
        public int discountAmount { get; set; }
        public string feecollectionStatus { get; set; }

    }
    public class EnrolmentEnrollmentsUpdate
    {
        public string action { get; set; }
        public CourseEnrollmentsUpdate course { get; set; }
        public TraineeEnrollmentsUpdate trainee { get; set; }
        public EmployerEnrollmentsUpdate employer { get; set; }
        public FeesEnrollmentsUpdate fees { get; set; }
        public string enrolmentDate { get; set; }

    }
}
