using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MenuTabsMaestroDetalle.Interface;
using System.IO;
using Xamarin.Forms;
using MenuTabsMaestroDetalle.Droid;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace MenuTabsMaestroDetalle.Droid
{
    public class LocalFileHelper : ILocHelper
    {

        public string GetLocalFilePath(string filename)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string libFolder = Path.Combine(docFolder);

            return Path.Combine(libFolder, filename);

        }

    }
}