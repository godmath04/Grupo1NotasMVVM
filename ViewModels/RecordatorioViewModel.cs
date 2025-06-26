using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grupo1NotasMVVM.Models;
using Grupo1NotasMVVM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Grupo1NotasMVVM.ViewModels
{
    public class RecordatorioViewModel : ObservableObject
    {
        private readonly RecordatorioService _servicio = new();

        private string _texto;
        public string Texto
        {
            get => _texto;
            set => SetProperty(ref _texto, value);
        }

        private TimeSpan _fechaHora;
        public TimeSpan FechaHora
        {
            get => _fechaHora;
            set => SetProperty(ref _fechaHora, value);
        }

        private bool _activo;
        public bool Activo
        {
            get => _activo;
            set => SetProperty(ref _activo, value);
        }

        private Recordatorio _recordatorioSeleccionado;
        public Recordatorio RecordatorioSeleccionado
        {
            get => _recordatorioSeleccionado;
            set
            {
                SetProperty(ref _recordatorioSeleccionado, value);
                HaySeleccion = value != null;
            }
        }

        private bool _haySeleccion;
        public bool HaySeleccion
        {
            get => _haySeleccion;
            set => SetProperty(ref _haySeleccion, value);
        }

        public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

        public ICommand CargarCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }

        public RecordatorioViewModel()
        {
            CargarCommand = new AsyncRelayCommand(CargarAsync);
            AgregarCommand = new AsyncRelayCommand(AgregarAsync);
            EliminarCommand = new AsyncRelayCommand<Recordatorio>(EliminarAsync);

            CargarCommand.Execute(null);
        }

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

        private async Task EliminarAsync(Recordatorio recordatorio)
        {
            if (recordatorio != null)
            {
                Recordatorios.Remove(recordatorio);
                await _servicio.GuardarAsync(Recordatorios.ToList());
                RecordatorioSeleccionado = null;
            }
        }
    }
}
