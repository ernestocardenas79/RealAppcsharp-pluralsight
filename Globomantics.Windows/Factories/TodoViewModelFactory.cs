using Globomantics.Domain;
using Globomantics.Windows.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

namespace Globomantics.Windows.Factories;
public class TodoViewModelFactory
{
    public static IEnumerable<string> TodoTypes = new[] {
        nameof(Bug),
        nameof(Feature)
    };
    private readonly IServiceProvider serviceProvider;

    public TodoViewModelFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public  ITodoViewModel CreateViewModel(string type, IEnumerable<Todo> tasks,
        Todo? model =default)
    {
        ITodoViewModel? viewmodel = type switch
        {
            nameof(Bug) => serviceProvider.GetService<BugViewModel>(),
            nameof(Feature) => serviceProvider.GetService<FeatureViewModel>(),
            _=> throw new NotImplementedException()
        };

        ArgumentNullException.ThrowIfNull(viewmodel);

        if(tasks is not null && tasks.Any()) { 
            viewmodel.AvailableParentTasks = tasks;
        }

        if(model is not null)
        {
            viewmodel.UpdateModel(model);
        }

        return viewmodel;
    }
}
