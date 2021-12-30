using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Utilities.ViewModel
{
    [SwaggerSchema(Title = "UpdateFeeCollectionRequest")]
    public class EnrollmentsFeesCollectModel
    {
        [Required]
        public EnrolmentFeesCollect enrolment { get; set; }
    }
    public class FeesCollection
    {
        public string collectionStatus { get; set; }

    }
    public class EnrolmentFeesCollect
    {
        public FeesCollection fees { get; set; }

    }
}
