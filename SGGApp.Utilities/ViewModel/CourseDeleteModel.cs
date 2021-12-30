using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "DeleteRequest_Data")]
    public class CourseDeleteModel
    {
        [Required]
        public CourseDelete course { get; set; }
    }
    public class TrainingProviderDelete
    {
        public string uen { get; set; }
    }
    public class RunDelete
    {
        public string action { get; set; }
    }
    public class CourseDelete
    {
        public string courseReferenceNumber { get; set; }
        public TrainingProviderDelete trainingProvider { get; set; }
        public RunDelete run { get; set; }
    }
}
