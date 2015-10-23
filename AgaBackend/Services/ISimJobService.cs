using System;
using System.Collections.Generic;
using AgaBackend.Dto;
using AgaBackend.Models;

namespace AgaBackend.Controllers
{
    public interface ISimJobService
    {
        List<SimJobModel> GetSimJobs();
        void AddSimJobs(IEnumerable<SimJobModel> simjobs); // save a new simjob
    }
}