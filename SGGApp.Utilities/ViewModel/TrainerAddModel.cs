using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "run")]

    public class TrainerAddModel
    {
        //public TrainerAddBase course { get; set; }
        public TrainerAddRun trainer { get; set; }
    }
    public class TrainerAddBase
    {
       // public string courseReferenceNumber { get; set; }
        //public TrainerAddTrainingProvider uen { get; set; }

        [Required]
        public List<TrainerAddRun> runs { get; set; }
    }

    public class TrainerAddRun
    { 
        public string name { get; set; }
        public string domainAreaOfPractice { get; set; }
        public string experience { get; set; }
        public string linkedInURL { get; set; }
        public string email { get; set; }
        public int salutationId { get; set; }
        public List<TrainerAddQualifications> qualifications { get; set; }
        public TrainerAddPhoto photo { get; set; }
    }

    public class TrainerAddTrainingProvider
    {
        [Required]
        public string uen { get; set; }
    }

    public class TrainerAddPhoto
    {
        public string name { get; set; }
        public string content { get; set; }
    }
    public class TrainerAddQualifications
    {
        public string description { get; set; }
        public TrainerQualificationsLevel level { get; set; }
    }
    public class TrainerQualificationsLevel
    {
        public string code { get; set; }
    }

}
