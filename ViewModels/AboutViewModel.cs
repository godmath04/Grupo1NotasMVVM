using CommunityToolkit.Mvvm.Input;
using Grupo1NotasMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Grupo1NotasMVVM.ViewModels
{
    internal class AboutViewModel
    {
        //public string Nombre => "Luis Elian Pineda ";
        //public string Descripcion => "Un about regenerico";

        //public string Icono => "pinguino.png";
        public ObservableCollection<About> Miembros { get; }
        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);

            Miembros = new ObservableCollection<About>
            {
                new About { Nombre = "Luis Elian Pineda", Edad = 24, DeporteFavorito = "Ajedrez", Foto = "pinguino.png" },
                new About { Nombre = "Galo Guevara", Edad = 24, DeporteFavorito = "Futbol", Foto = "galo1.jpg" },
                new About { Nombre = "Javier Arias", Edad = 22, DeporteFavorito = "Futbol", Foto = "javi1.jpg" },
                new About { Nombre = "Julian Torres", Edad = 22, DeporteFavorito = "Basquet", Foto = "julian1.png" }
            };
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync("https://aka.ms/maui");


    }
}
