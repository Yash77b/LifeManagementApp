using CommunityToolkit.Mvvm.ComponentModel;

namespace LifeManagementApp.Models
{
    public partial class TodoItem : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string title = "";

        [ObservableProperty]
        private int priority = 3;

        [ObservableProperty]
        private DateTime dueDate = DateTime.Today;

        [ObservableProperty]
        private bool isDone;
    }
}