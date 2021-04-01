using System;
using System.Collections.Generic;
using System.Text;


using System.Threading.Tasks;
using AppApiRest.Model;

namespace AppApiRest.Services
{
    public class ServicesManager
    {

        IRestService restService;

        public ServicesManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<clsRegistrados>> GetApiRegistrados()
        {
            return restService.GetRegistrados();
        }

    }
}
