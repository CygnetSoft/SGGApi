using System.Collections.Generic;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "run")]
    public class CourseAddModel
    {
        public CourseAddBase course { get; set; }
    }
    public class CourseAddTrainingProvider
    {
        public string uen { get; set; }
    }

    public class CourseAddRegistrationDates
    {
        public int opening { get; set; }
        public int closing { get; set; }
    }

    public class CourseAddCourseDates
    {
        public int start { get; set; }
        public int end { get; set; }
    }

    public class CourseAddScheduleInfoType
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class CourseAddVenue
    {
        public string block { get; set; }
        public string street { get; set; }
        public string floor { get; set; }
        public string unit { get; set; }
        public string building { get; set; }
        public int postalCode { get; set; }
        public string room { get; set; }
        public bool wheelChairAccess { get; set; }
        public bool primaryVenue { get; set; }
    }

    public class CourseAddCourseVacancy
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class CourseAddFile
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        public string content { get; set; }
    }

    public class CourseAddSession
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string startTime { get; set; }
        public string modeOfTraining { get; set; }
        public string endTime { get; set; }
        public CourseAddVenue venue { get; set; }
    }

    public class CourseAddPhoto
    {
        public string name { get; set; }
        public string content { get; set; }
    }

    public class CourseAddTrainerType
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class CourseAddSsecEQA
    {
        public string code { get; set; }
    }

    public class CourseAddLinkedSsecEQA
    {
        public string description { get; set; }
        public CourseAddSsecEQA ssecEQA { get; set; }
    }

    public class CourseAddTrainer
    {
        public int indexNumber { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool inTrainingProviderProfile { get; set; }
        public string domainAreaOfPractice { get; set; }
        public string experience { get; set; }
        public string linkedInURL { get; set; }
        public int salutationId { get; set; }
        public CourseAddPhoto photo { get; set; }
        public string email { get; set; }
        public CourseAddTrainerType trainerType { get; set; }
        public List<CourseAddLinkedSsecEQA> linkedSsecEQAs { get; set; }
    }

    public class LinkCourseRunTrainer
    {
        public CourseAddTrainer trainer { get; set; }
    }

    public class CourseAddRun
    {
        public int sequenceNumber { get; set; }
        public string modeOfTraining { get; set; }
        public CourseAddRegistrationDates registrationDates { get; set; }
        public CourseAddCourseDates courseDates { get; set; }
        public CourseAddScheduleInfoType scheduleInfoType { get; set; }
        public string scheduleInfo { get; set; }
        public CourseAddVenue venue { get; set; }
        public int intakeSize { get; set; }
        public string courseAdminEmail { get; set; }
        public CourseAddCourseVacancy courseVacancy { get; set; }
        public CourseAddFile file { get; set; }
        public List<CourseAddSession> sessions { get; set; }
        public List<LinkCourseRunTrainer> linkCourseRunTrainer { get; set; }
    }

    public class CourseAddBase
    {
        public string courseReferenceNumber { get; set; }
        public CourseAddTrainingProvider trainingProvider { get; set; }
        public List<CourseAddRun> runs { get; set; }
    }
}
