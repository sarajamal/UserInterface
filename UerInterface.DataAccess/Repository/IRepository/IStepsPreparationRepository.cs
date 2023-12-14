using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IStepsPreparationRepository : IRepository<PreparationSteps>
    {

        void Update(PreparationSteps obj);
        void Delete(PreparationSteps obl);
        int GetLastStepId();

       

    }
}
