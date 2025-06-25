using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Grupo1NotasMVVM.Models;

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
                new About { Nombre = "Luis Elian Pineda", Edad = 21, DeporteFavorito = "Ajedrez", Foto = "pinguino.png" },
                new About { Nombre = "Galo Guevara", Edad = 22, DeporteFavorito = "Futbol", Foto = "galo1.jpg" }
            };
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync("https://aka.ms/maui");


    }
}
