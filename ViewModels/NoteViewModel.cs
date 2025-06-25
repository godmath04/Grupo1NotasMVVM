using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Grupo1NotasMVVM.ViewModels
{
    internal class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private Models.Note _note;

        public string Text
        {
            get => _note.Text;
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.Date;

        public string Identifier => _note.Filename;

        //Comando para enlazar vista
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        //Constructores 
        public NoteViewModel()
        {
            _note = new Models.Note();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public NoteViewModel(Models.Note note)
        {
            _note = note;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        //Metodo apra guardar

        private async Task Save()
        {
            _note.Date = DateTime.Now;
            _note.Save();
            await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
        }

        //Metodo para borrar
        private async Task Delete()
        {
            _note.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.Note.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        //Actualiza el objeto del modelo de respaldos
        public void Reload()
        {
            _note = Models.Note.Load(_note.Filename);
            RefreshProperties();
        }

        //Los metodos han cambiado
        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}
