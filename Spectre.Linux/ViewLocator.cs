using Avalonia.Controls;
using Avalonia.Controls.Templates;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Spectre.Linux;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModels", "Linux.Views").Replace("Page", "").Replace("ViewModel", "Page", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null) {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ObservableObject;
    }
}
