using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "UpdateRequest")]
    public class AssessmentUpdateModel
    {
        public UpdateAssessment assessment { get; set; }
    }
    public class UpdateTrainee
    {
        public string fullName { get; set; }
    }
    public class UpdateAssessment
    {
        public string grade { get; set; }
        public int score { get; set; }
        public string action { get; set; }
        public string result { get; set; }
        public UpdateTrainee trainee { get; set; }
        public string skillCode { get; set; }
        public string assessmentDate { get; set; }

    }
}
