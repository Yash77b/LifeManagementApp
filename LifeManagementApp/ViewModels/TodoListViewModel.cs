using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels
{
    public partial class TodoListViewModel : ObservableObject
    {
        private readonly ITodoRepository repository;
        private bool sortByDate = false;

        public ObservableCollection<TodoItem> Todos { get; } = new();

        public TodoListViewModel(ITodoRepository repository)
        {
            this.repository = repository;
        }

        public async Task LoadAsync()
        {
            List<TodoItem> items = await repository.GetAllAsync();
            ShowSorted(items);
        }

        [RelayCommand]
        private void SortByPriority()
        {
            sortByDate = false;
            ShowSorted(Todos.ToList());
        }

        [RelayCommand]
        private void SortByDate()
        {
            sortByDate = true;
            ShowSorted(Todos.ToList());
        }

        private void ShowSorted(List<TodoItem> items)
        {
            List<TodoItem> sorted;

            if (sortByDate)
                sorted = items.OrderBy(t => t.DueDate).ToList();
            else
                sorted = items.OrderByDescending(t => t.Priority).ToList();

            Todos.Clear();
            foreach (TodoItem item in sorted)
                Todos.Add(item);
        }
    }
}