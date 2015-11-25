using System;
using System.Collections.Generic;
using AgaBackend.Dto;
using AgaBackend.Models;

namespace AgaBackend.Controllers
{
    public interface ISimJobService
    {
        List<SimJobModel> GetSimJobs();
        List<SimJobModel> GetStartedSimJobs();
        //  SimJobModel GetSimJob(SimJobModel simJob);
        void AddSimJobs(IEnumerable<SimJobModel> simjobs); 
        void SaveSimJob(SimJobModel simjob);
        void RemoveSimJobs(IEnumerable<SimJobModel> simjobs);
        void RemoveSimJob(SimJobModel simjob);
    }
}