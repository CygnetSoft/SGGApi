using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "UploadAttendance_Data")]
    public class UploadAttachmentModel
    {
        public string uen { get; set; }
        public CourseAttachment course { get; set; }
        public string corppassId { get; set; }
    }
    public class Status
    {
        public int code { get; set; }
    }
    public class IdType
    {
        public string code { get; set; }
    }
    public class ContactNumber
    {
        public string mobile { get; set; }
        public string areaCode { get; set; }
        [Required]
        public int countryCode { get; set; }
    }
    public class SurveyLanguage
    {
        public string code { get; set; }
    }
    public class Trainee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        [Required]
        public IdType idType { get; set; }
        public ContactNumber contactNumber { get; set; }
        public double numberOfHours { get; set; }
        [Required]
        public SurveyLanguage surveyLanguage { get; set; }
    }
    public class Attendance
    {
        public Status status { get; set; }
        public Trainee trainee { get; set; }
    }
    public class CourseAttachment
    {
        public string sessionID { get; set; }
        [Required]
        public Attendance attendance { get; set; }
        public string referenceNumber { get; set; }
    }
}
