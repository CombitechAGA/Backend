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
    public class SimCarService : ISimCarService
    {
        private readonly IMongoDatasource<RouteModel> _routedatasource;
        private readonly IMongoDatasource<Snapshot> _snapshotdatasource;

        public SimCarService(IMongoDatasource<RouteModel> routeDatasource, IMongoDatasource<Snapshot> snapshotDatasource)
        {
            _routedatasource = routeDatasource;
            _snapshotdatasource = snapshotDatasource;
        }

        public List<RouteModel> GetRoutes(DateTime from) // return list of stored caluclated routes
        {
            DateTime datetime = DateTime.MinValue;

            var query = Query.GTE("timestamp", from);

            var result = _routedatasource.Find(query);//.OrderBy(f=>f.Timestamp);

            var listOfRoutes = new List<RouteModel>();

            foreach (var route in result)
            {
                listOfRoutes.Add(new RouteModel { RouteId = route.RouteId, carID = route.carID, timestamp = route.timestamp, Snapshots = route.Snapshots });
            }

            if (listOfRoutes.Count() > 0)
            {
                datetime = listOfRoutes.Last().timestamp; 
            }
 
            var calculatedRoutes = CalculateRoutes(datetime); // find new routes succeding last stored entry
            listOfRoutes.AddRange(calculatedRoutes); // add new routes to saved routes

            return listOfRoutes;
        }

#if false
        public List<RouteDto> GetRoutes(DateTime from) // return list of stored caluclated routes
        {
            var query = Query.GTE("timestamp", from);

            var result = _routedatasource.Find(query);//.OrderBy(f=>f.Timestamp);

            var listOfRoutes = new List<RouteDto>();
  
            foreach (var route in result)
            {
                listOfRoutes.Add(new RouteDto { CarId = route.carID, RouteId = route.RouteId, Date = route.timestamp });
            }

            var calculatedRoutes = CalculateRoutes(listOfRoutes.Last().Date); // find new routes succeding last stored entry
            // lägg till
            return listOfRoutes;
        }
#endif
        public void AddRoutes(IEnumerable<RouteModel> routes) // save a new calculated route
        {
            foreach (var route in routes)
            {
                _routedatasource.Save(route);
            }
        }

        public void SetupRoute(List<Snapshot> snapshotlist) // prepare a new route
        {
            List<RouteModel> detectedRoutes = new List<RouteModel>();
            var routeItem = new RouteModel();

            routeItem.RouteId = snapshotlist.Last().ObjectId;
            routeItem.carID = snapshotlist.Last().carID;
            routeItem.timestamp = snapshotlist.Last().timestamp; // last timestamp of snapshot in route
            routeItem.Snapshots = snapshotlist;
            detectedRoutes.Add(routeItem);

            AddRoutes(detectedRoutes); // save the new route
        }

        public IEnumerable<RouteModel> CalculateRoutes(DateTime from)
        {
            List<RouteModel> detectedRoutes = new List<RouteModel>();
            var query = Query.GTE("timestamp", from);
            var result = _snapshotdatasource.Find(query).OrderBy(f => f.timestamp).GroupBy(a => a.carID);

            foreach (var car in result)
            {
                bool carSpeedIsZero = false, createRoute = false, carIsMoving = false;
                DateTime zeroStamp = new DateTime();
                List<Snapshot> snapShotList = new List<Snapshot>();
  
                foreach (var snapshot in car)
                {
                    snapShotList.Add(snapshot); // add snapshot to snapshotlist in route

                    if (Math.Abs(snapshot.speed) == 0)
                    {
                        if (!carSpeedIsZero) // first snapshot found with speed 0
                        {
                            zeroStamp = snapshot.timestamp;
                            carSpeedIsZero = true;
                        }
                        else
                        {
                            if (snapshot.timestamp.Subtract(zeroStamp).TotalSeconds > 120) // time from first snapshot with speed 0 until current snapshot is at least 120 secs:  end of route
                            {
                                carSpeedIsZero = false;
                                if (carIsMoving) // dont store route when all snapshot speed is zero
                                {
                                    SetupRoute(snapShotList);
                                    carIsMoving = false;
                                    snapShotList.Clear();
                                }
                            }
                        }
                    }
                    else
                        carIsMoving = true; // could be parametrized
                }

                if (carIsMoving) // dont store route when all snapshot speed is zero
                {
                    SetupRoute(snapShotList);
                }
            }
            return detectedRoutes;
        }
    }
}

#if false
            foreach (var car in result)
            {
                bool carSpeedIsZero = false;
                bool endOfRoute = false;
                DateTime zeroStamp = new DateTime();
                List<Snapshot> snapShotList = new List<Snapshot>();
               
                foreach (var snapshot in car)
                {
                   if (Math.Abs(snapshot.speed) == 0) 
                   {
                       if (!carSpeedIsZero) // first snapshot found with speed 0
                       {
                           zeroStamp = snapshot.timestamp;
                           carSpeedIsZero = true;
                       }
                       else
                       {
                           if (snapshot.timestamp.Subtract(zeroStamp).TotalSeconds > 120) // time from first snapshot with speed 0 until current snapshot is at least 120 secs. New route.
                           { 
                               endOfRoute = true;
                               carSpeedIsZero = false;
                           }
                       }
                   }
                   snapShotList.Add(snapshot); // add snapshot to snapshotlist

                   if (car.Last(snapshot) ||  endOfRoute)
                   {
                       var routeItem = new RouteModel();

                       routeItem.RouteId = snapshot.ObjectId;
                       routeItem.carID = snapshot.carID;
                       routeItem.timestamp = snapshot.timestamp; // last timestamp of snapshot in route
                       routeItem.Snapshots = snapShotList;
                       detectedRoutes.Add(routeItem);
                       endOfRoute = false;
                       snapShotList.Clear();
                   }
                }
            }
#endif

