using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "CreateRequest")]
    public class AssessmentAddModel
    {
        public AssessmentAdd assessment { get; set; }
    }
    public class TrainingPartnerAssessmentCreate
    {
        [Required]
        public string code { get; set; }
        [Required]
        public string uen { get; set; }

    }
    public class RunAssessmentCreate
    {
        [Required]
        public string id { get; set; }

    }
    public class CourseAssessmentCreate
    {
        [Required]
        public string referenceNumber { get; set; }
        public RunAssessmentCreate run { get; set; }

    }
    public class TraineeAssessmentCreate
    {
        [Required]
        public string idType { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string fullName { get; set; }

    }
    public class AssessmentAdd
    {
        [Required]
        public TrainingPartnerAssessmentCreate trainingPartner { get; set; }
        [Required]
        public CourseAssessmentCreate course { get; set; }
        [Required]
        public TraineeAssessmentCreate trainee { get; set; }
        [Required]
        public string result { get; set; }
        public int score { get; set; }
        public string grade { get; set; }
        [Required]
        public string assessmentDate { get; set; }
        public string skillCode { get; set; }

    }
}
