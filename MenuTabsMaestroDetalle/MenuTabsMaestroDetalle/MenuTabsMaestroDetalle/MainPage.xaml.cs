using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MenuTabsMaestroDetalle.Views;

namespace MenuTabsMaestroDetalle
{
    [Obsolete]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MyMenu();
        }


       public void MyMenu()
       {
            Detail = new NavigationPage(new Home());

            List<Menu> menu = new List<Menu>
            {
                new Menu { Page = new Home(), MenuTitle = "Home" },
                new Menu { Page = new MiPerfil(), MenuTitle = "Mi Perfil" }
            };

            ListMenu.ItemsSource = menu;

       }

        public class Menu
        {
            public string MenuTitle { get; set; }

            public Page Page { get; set; }

        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;

            if (menu != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(menu.Page);
            }

        }
    }
}
