using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using Microsoft.VisualBasic;

namespace LifeManagementApp.ViewModels
{
    public partial class EditTodoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ITodoRepository repository;
        private TodoItem? todo;

        public List<int> Priorities { get; } = new() { 1, 2, 3, 4, 5 };

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string title = "";

        [ObservableProperty]
        private int priority = 3;

        [ObservableProperty]
        private DateTime dueDate = DateTime.Today;

        [ObservableProperty]
        private bool isDone;

        public EditTodoViewModel(ITodoRepository repository)
        {
            this.repository = repository;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Todo", out object? value) && value is TodoItem item)
            {
                todo = item;
                Title = item.Title;
                Priority = item.Priority;
                DueDate = item.DueDate;
                IsDone = item.IsDone;
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            if (todo == null)
                return;

            todo.Title = Title.Trim();
            todo.Priority = Priority;
            todo.DueDate = DueDate;
            todo.IsDone = IsDone;

            await repository.UpdateAsync(todo);
            await Shell.Current.GoToAsync("..");
        }
    }
}