using Globomantics.Domain;
using Globomantics.Windows.Factories;
using Globomantics.Windows.ViewModels;
using GloboMantics.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Globomantics.Windows;

public partial class App : Application
{
    public static User CurrentUser { get; set; } = default!;
    public IServiceProvider ServiceProvider { get; init; } 
    public IConfiguration Configuration { get; init; }

    public App()
    {
        var builder = new ConfigurationBuilder();

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IRepository<Bug>, TodoInMemoryRepository<Bug>>();
        serviceCollection.AddSingleton<IRepository<Feature>, TodoInMemoryRepository<Feature>>();
        serviceCollection.AddSingleton<IRepository<TodoTask>, TodoInMemoryRepository<TodoTask>>();

        serviceCollection.AddTransient<TodoViewModelFactory>();
        serviceCollection.AddTransient<FeatureViewModel>();
        serviceCollection.AddTransient<BugViewModel>();
        serviceCollection.AddTransient<MainViewModel>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient(_ => ServiceProvider!);
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        mainWindow?.Show();
    }
}
