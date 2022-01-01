using SGGApp.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGGApp.Service.Service.IService
{
    public interface ITrainerService
    {
        Task<object> AddTrainerRun(TrainerAddModel enrollment, string uen);
        Task<object> UpdateTrainerRuns(string uen, string trainersid, TrainerAddModel enrollment);
    }
}
