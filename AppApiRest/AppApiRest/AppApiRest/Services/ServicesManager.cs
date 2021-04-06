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

        public async Task<int> SaveRegistro(clsPerfilDUO item, bool isNewItem)
        {
            int Grabado;

            Grabado = await restService.SaveRegistro(item, isNewItem);

            return Grabado;
        }

    }
}
