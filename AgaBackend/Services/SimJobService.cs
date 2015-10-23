using System;
using System.Collections.Generic;
using System.Linq;
using AgaBackend.Controllers;
using AgaBackend.Datasource;
using AgaBackend.Dto;
using AgaBackend.Models;
using MongoDB.Driver.Builders;

namespace AgaBackend.Services
{
    public class SimJobService : ISimJobService
    {
        private readonly IMongoDatasource<SimJobModel> _simjobdatasource;

        public SimJobService(IMongoDatasource<SimJobModel> simjobDatasource)
        {
            _simjobdatasource = simjobDatasource;
        }

        public List<SimJobModel> GetSimJobs()
        {
            var query = Query.GTE("SimJobId",0); // get all saved simjobs

            var result = _simjobdatasource.Find(query);//.OrderBy(f=>f.Timestamp);

            var listOfSimJobs = new List<SimJobModel>();

            foreach (var simjob in result)
            {
                listOfSimJobs.Add(new SimJobModel { SimJobId = simjob.SimJobId, repeatJob = simjob.repeatJob, speedX = simjob.speedX, jobStarted = simjob.jobStarted, Routes = simjob.Routes });
            }

            return listOfSimJobs;
        }

        public void AddSimJobs(IEnumerable<SimJobModel> simjobs) // save a new simjob
        {
            foreach (var simjob in simjobs)
            {
                _simjobdatasource.Save(simjob);
            }
        }
    }
}   