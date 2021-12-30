using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{

    [SwaggerSchema(Title = "CreateRequest")]
    public class EnrollmentsAddModel
    {
        public Enrolment enrolment { get; set; }
    }
    public class TrainingPartnerEnroll
    {
        [Required]
        public string code { get; set; }
        [Required]
        public string uen { get; set; }

    }
    public class Run
    {
        public string id { get; set; }

    }
    public class CourseEnroll
    {
        [Required]
        public string referenceNumber { get; set; }
        public Run run { get; set; }

    }
    public class IdTypeEnroll
    {
        public string type { get; set; }

    }
    public class ContactNumberEnroll
    {
        [Required]
        public string countryCode { get; set; }
        public string areaCode { get; set; }
        [Required]
        public string phoneNumber { get; set; }

    }

    public class ContactEnroll
    {
        public string fullName { get; set; }
        public ContactNumberEnroll contactNumber { get; set; }
        public string emailAddress { get; set; }

    }
    public class Employer
    {
        public string uen { get; set; }
        public ContactEnroll contact { get; set; }

    }
    public class FeesEnroll
    {
        public double discountAmount { get; set; }
        [Required]
        public string collectionStatus { get; set; }

    }
    public class TraineeEnroll
    {
        [Required]
        public IdTypeEnroll idType { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string dateOfBirth { get; set; }

        public string fullName { get; set; }
        public ContactNumberEnroll contactNumber { get; set; }
        [Required]
        public string emailAddress { get; set; }
        [Required]
        public string sponsorshipType { get; set; }
        public Employer employer { get; set; }
        public FeesEnroll fees { get; set; }
        public string enrolmentDate { get; set; }

    }
    public class Enrolment
    {
        public TrainingPartnerEnroll trainingPartner { get; set; }
        public CourseEnroll course { get; set; }
        public TraineeEnroll trainee { get; set; }

    }
}
