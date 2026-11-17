using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using Microsoft.VisualBasic;

namespace LifeManagementApp.ViewModels
{
    public partial class AddTodoViewModel : ObservableObject
    {
        private readonly ITodoRepository repository;

        public List<int> Priorities { get; } = new() { 1, 2, 3, 4, 5 };

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string title = "";

        [ObservableProperty]
        private int priority = 3;

        [ObservableProperty]
        private DateTime dueDate = DateTime.Today;

        public AddTodoViewModel(ITodoRepository repository)
        {
            this.repository = repository;
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            TodoItem item = new TodoItem
            {
                Title = Title.Trim(),
                Priority = Priority,
                DueDate = DueDate,
                IsDone = false
            };

            await repository.AddAsync(item);
            await Shell.Current.GoToAsync("..");
        }
    }
}