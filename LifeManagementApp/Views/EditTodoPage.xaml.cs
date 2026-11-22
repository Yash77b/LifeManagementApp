using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views
{
    public partial class EditTodoPage : ContentPage
    {
        public EditTodoPage(EditTodoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}