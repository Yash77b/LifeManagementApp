using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private const double KiteWindLimit = 5.0;

        private readonly ITodoRepository repository;
        private readonly IWeatherService weatherService;

        public ObservableCollection<TodoItem> TopTodos { get; } = new();

        [ObservableProperty]
        private string weatherText = "";

        [ObservableProperty]
        private bool isKiteWeather;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string errorMessage = "";

        public HomeViewModel(ITodoRepository repository, IWeatherService weatherService)
        {
            this.repository = repository;
            this.weatherService = weatherService;
        }

        public async Task LoadAsync()
        {
            await LoadTopTodosAsync();
            await LoadWeatherAsync();
        }

        private async Task LoadTopTodosAsync()
        {
            List<TodoItem> top = await repository.GetTop3Async();

            TopTodos.Clear();
            foreach (TodoItem item in top)
                TopTodos.Add(item);
        }

        [RelayCommand]
        private async Task LoadWeatherAsync()
        {
            IsLoading = true;
            ErrorMessage = "";

            try
            {
                WeatherInfo weather = await weatherService.GetCurrentWeatherAsync();
                WeatherText = $"{weather.Temperature:0.0} °C, wind {weather.WindSpeed:0.0} m/s";
                IsKiteWeather = weather.WindSpeed > KiteWindLimit;
            }
            catch (Exception)
            {
                WeatherText = "";
                IsKiteWeather = false;
                ErrorMessage = "Could not load weather. Check your internet connection.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}