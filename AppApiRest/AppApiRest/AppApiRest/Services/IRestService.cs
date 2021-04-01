using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using AppApiRest.Model;

namespace AppApiRest.Services
{
    public interface IRestService
    {
        Task<List<clsRegistrados>> GetRegistrados();

    }
}
