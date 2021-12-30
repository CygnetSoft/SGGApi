using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "course")]
    public class CourseUpdateModel
    {
        public CourseUpdate course { get; set; }
    }
    public class TrainingProviderUpdate
    {
        public string uen { get; set; }

    }
    public class RegistrationDatesUpdate
    {
        public int opening { get; set; }
        public int closing { get; set; }

    }
    public class CourseDatesUpdate
    {
        public int start { get; set; }
        public int end { get; set; }

    }
    public class ScheduleInfoTypeUpdate
    {
        public string code { get; set; }
        public string description { get; set; }

    }
    public class VenueUpdate
    {
        public string block { get; set; }
        public string street { get; set; }
        public string floor { get; set; }
        public string unit { get; set; }
        public string building { get; set; }
        public string postalCode { get; set; }
        public string room { get; set; }
        public bool wheelChairAccess { get; set; }
        public bool primaryVenue { get; set; }

    }
    public class CourseVacancyUpdate
    {
        public string code { get; set; }
        public string description { get; set; }
    }
    public class FileUpdate
    {
        public string name { get; set; }
        public string content { get; set; }

    }
    public class SessionsUpdate
    {
        public string sessionId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string modeOfTraining { get; set; }
        public VenueUpdate venue { get; set; }
        public string action { get; set; }

    }
    public class PhotoUpdate
    {
        public string name { get; set; }
        public string content { get; set; }

    }
    public class TrainerTypeUpdate
    {
        public string code { get; set; }
        public string description { get; set; }

    }
    public class SsecEQAUpdate
    {
        public string code { get; set; }
        public string description { get; set; }

    }
    public class LinkedSsecEQAsUpdate
    {
        public SsecEQAUpdate ssecEQA { get; set; }

    }
    public class TrainerUpdate
    {
        public int indexNumber { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool inTrainingProviderProfile { get; set; }
        public string domainAreaOfPractice { get; set; }
        public string experience { get; set; }
        public string linkedInURL { get; set; }
        public int salutationId { get; set; }
        public PhotoUpdate photo { get; set; }
        public string email { get; set; }
        public TrainerTypeUpdate trainerType { get; set; }
        public IList<LinkedSsecEQAsUpdate> linkedSsecEQAs { get; set; }
    }
    public class LinkCourseRunTrainerUpdate
    {
        public TrainerUpdate trainer { get; set; }
    }
    public class RunUpdate
    {
        [Required]
        public string action { get; set; }
        public int sequenceNumber { get; set; }
        public string modeOfTraining { get; set; }
        public RegistrationDatesUpdate registrationDates { get; set; }
        public CourseDatesUpdate courseDates { get; set; }
        public ScheduleInfoTypeUpdate scheduleInfoType { get; set; }
        public string scheduleInfo { get; set; }
        public VenueUpdate venue { get; set; }
        public int intakeSize { get; set; }
        public int threshold { get; set; }
        public int registeredUserCount { get; set; }
        public string courseAdminEmail { get; set; }
        public CourseVacancyUpdate courseVacancy { get; set; }
        public FileUpdate file { get; set; }
        public IList<SessionsUpdate> sessions { get; set; }
        public IList<LinkCourseRunTrainerUpdate> linkCourseRunTrainer { get; set; }
    }
    public class CourseUpdate
    {
        [Required]
        public string courseReferenceNumber { get; set; }
        [Required]
        public TrainingProviderUpdate trainingProvider { get; set; }
        [Required]
        public RunUpdate run { get; set; }
    }
}
