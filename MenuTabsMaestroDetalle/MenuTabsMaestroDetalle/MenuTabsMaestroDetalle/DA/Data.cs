using System;
using System.Collections.Generic;
using System.Text;


using SQLite;
using MenuTabsMaestroDetalle.Model;
using System.Threading.Tasks;

namespace MenuTabsMaestroDetalle.DA
{
    public class Data
    {
        readonly SQLiteAsyncConnection database;

        public Data(string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);

            database.CreateTableAsync<Registrado>().Wait();
            database.CreateTableAsync<RegistradoFotos>().Wait();

        }

        public Task<int> RegistroDatos(Registrado NewDato)
        {
            return database.InsertAsync(NewDato);
        }

        public Task<int> UpdateDatos(Registrado UpdDato)
        {
            return database.UpdateAsync(UpdDato);
        }

        public Task<int> DeleteDatos(Registrado DelDato)
        {
            return database.DeleteAsync(DelDato);
        }

        public Task<List<Registrado>> GetRegistrado()
        {
            var lista = database.Table<Registrado>().ToListAsync();

            return lista;
        }

        public Task<List<Registrado>> GetRegistradoById(int Idregistrado)
        {
            var lista = database.Table<Registrado>().Where(z => z.IdRegistrado == Idregistrado).ToListAsync();

            return lista;
        }


        public Task<int> RegistroFotos(RegistradoFotos NewDato)
        {
            return database.InsertAsync(NewDato);
        }

        public Task<List<RegistradoFotos>> GetRegistradoFotosById(int Idregistrado)
        {
            var lista = database.Table<RegistradoFotos>().Where(z => z.IdRegistrado == Idregistrado).ToListAsync();

            return lista;
        }

    }
}
