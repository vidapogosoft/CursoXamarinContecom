using BDSQLite1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDSQLite1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStudent : ContentPage
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var stdnew = (Student)BindingContext;
                //Agregar un nuevo registro
                await App.Database.SaveStudent(stdnew);
                //mensaje de confirmacion
                await DisplayAlert("Exito", "Datos registrados en la base de datos", "De Acuerdo");
                //Abrir la navegacion
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",ex.Message,"Aceptar");
            }

        }
    }
}