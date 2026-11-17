using LifeManagementApp.Views;

namespace LifeManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddTodoPage), typeof(AddTodoPage));
        }
    }
}