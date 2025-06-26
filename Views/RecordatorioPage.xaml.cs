using Grupo1NotasMVVM.ViewModels;

namespace Grupo1NotasMVVM.Views;

public partial class RecordatorioPage : ContentPage
{
	public RecordatorioPage()
	{
		InitializeComponent();
        BindingContext = new RecordatorioViewModel();
    }
}