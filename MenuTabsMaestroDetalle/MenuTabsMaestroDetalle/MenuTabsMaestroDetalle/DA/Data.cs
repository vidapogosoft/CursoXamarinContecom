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

        }

        public Task<int> RegistroDatos(Registrado NewDato)
        {
            return database.InsertAsync(NewDato);
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


    }
}
