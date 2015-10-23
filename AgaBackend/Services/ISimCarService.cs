using System;
using System.Collections.Generic;
using AgaBackend.Models;

namespace AgaBackend.Controllers
{
    public interface ISimCarService
    {
        List<RouteModel> GetRoutes(DateTime from);
    }
}