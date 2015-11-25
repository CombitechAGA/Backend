using System;
using System.Collections.Generic;
using System.Linq;
using AgaBackend.Controllers;
using AgaBackend.Datasource;
using AgaBackend.Dto;
using AgaBackend.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;


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
            //var query = Query.GTE("SimJobId","0"); // get all saved simjobs
            var query = Query.GTE("carId", "0"); // get all saved simjobs

            var result = _simjobdatasource.Find(query);//.OrderBy(f=>f.Timestamp);
            
            var listOfSimJobs = new List<SimJobModel>();

            //_simjobdatasource.RemoveAll(); // TEMP Remove function

            foreach (var simjob in result)
            {
                //listOfSimJobs.Add(new SimJobModel { SimJobId = simjob.SimJobId, carId = simjob.carId, repeatJob = simjob.repeatJob, speedX = simjob.speedX, jobStarted = simjob.jobStarted, Routes = simjob.Routes });
                listOfSimJobs.Add(new SimJobModel { SimJobId = simjob.SimJobId, carId = simjob.carId, repeatJob = simjob.repeatJob, speedX = simjob.speedX, jobStarted = simjob.jobStarted, Routes = simjob.Routes, nrOfRoutes = simjob.nrOfRoutes });
            }

            return listOfSimJobs;
        }

        public List<SimJobModel> GetStartedSimJobs() 
        {
            var query = Query.GTE("carId", "0"); // get all saved simjobs

            var result = _simjobdatasource.Find(query);//.OrderBy(f=>f.Timestamp);

            var listOfSimJobs = new List<SimJobModel>();

            foreach (var simjob in result)
            {
                if (simjob.jobStarted == true)
                {
                    listOfSimJobs.Add(new SimJobModel { SimJobId = simjob.SimJobId, carId = simjob.carId, repeatJob = simjob.repeatJob, speedX = simjob.speedX, jobStarted = simjob.jobStarted, Routes = simjob.Routes, nrOfRoutes = simjob.nrOfRoutes });
                }
            }

            return listOfSimJobs;
        }
        /*
        public SimJobModel GetSimJob(SimJobModel simJob) TODO
        {
            var query = Query.GTE("SimJobId",simJob.simJobId);

            SimJobModel simObject = new SimJobModel();
            var result = _simjobdatasource.Find(query);

            var simJobObject = new SimJobModel { SimJobId = simJob.SimJobId, carId = simJob.carId, repeatJob = simJob.repeatJob, speedX = simJob.speedX, jobStarted = simJob.jobStarted, Routes = simJob.Routes };
            //var simJobObject = new SimJobModel { SimJobId = simJob.SimJobId, carId = simJob.carId, repeatJob = simJob.repeatJob, speedX = simJob.speedX, jobStarted = simJob.jobStarted, RouteList = simJob.RouteList };

            return (simJobObject);
        }
        */
        public void AddSimJobs(IEnumerable<SimJobModel> simjobs) // save a list of simjobs
        {
            foreach (var simjob in simjobs)
            {
                _simjobdatasource.Save(simjob);
            }
        }

        public void SaveSimJob(SimJobModel simjob)  // save one simjob
        {
            if (simjob.simJobId != null)
            { // object already exist
                simjob.SimJobId = ObjectId.Parse(simjob.simJobId);
            }

            simjob.nrOfRoutes = simjob.Routes.Count(); // first save

            _simjobdatasource.Save(simjob);
        }

        public void RemoveSimJobs(IEnumerable<SimJobModel> simjobs) // remove all simjobs
        {
            foreach (var simjob in simjobs) 
            {
                _simjobdatasource.RemoveAll();
            }
        }

        public void RemoveSimJob(SimJobModel simjob) 
        {
            var query = Query.GTE("SimJobId", simjob.SimJobId);  // Change to find ID
            _simjobdatasource.Remove(query);
        }

    }
}   