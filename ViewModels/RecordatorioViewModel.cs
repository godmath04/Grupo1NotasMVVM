using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grupo1NotasMVVM.Models;
using Grupo1NotasMVVM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Grupo1NotasMVVM.ViewModels
{
    public partial class RecordatorioViewModel : ObservableObject
    {
        private readonly RecordatorioService _servicio = new();

        [ObservableProperty]
        private string texto;

        [ObservableProperty]
        private TimeSpan fechaHora;

        [ObservableProperty]
        private bool activo;

        public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

        public RecordatorioViewModel()
        {
            CargarCommand = new AsyncRelayCommand(CargarAsync);
            AgregarCommand = new AsyncRelayCommand(AgregarAsync);
            EliminarCommand = new AsyncRelayCommand<Recordatorio>(EliminarAsync);

            CargarCommand.Execute(null);
        }

        public ICommand CargarCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }

        private async Task CargarAsync()
        {
            var lista = await _servicio.CargarAsync();
            Recordatorios.Clear();
            foreach (var r in lista)
                Recordatorios.Add(r);
        }

        private async Task AgregarAsync()
        {
            var nuevo = new Recordatorio
            {
                Texto = Texto,
                FechaHora = FechaHora,
                Activo = Activo
            };

            Recordatorios.Add(nuevo);
            await _servicio.GuardarAsync(Recordatorios.ToList());
            Texto = string.Empty;
            FechaHora = TimeSpan.Zero;
            Activo = false;
        }

        private async Task EliminarAsync(Recordatorio r)
        {
            Recordatorios.Remove(r);
            await _servicio.GuardarAsync(Recordatorios.ToList());
        }
    }
}
