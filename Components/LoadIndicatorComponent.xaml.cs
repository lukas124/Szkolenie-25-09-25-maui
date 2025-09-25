namespace MauiStart.Components;

public partial class LoadIndicatorComponent
{ 
    public LoadIndicatorComponent()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty MessageProperty =
        BindableProperty.Create(
            nameof(Message),
            typeof(string),
            typeof(LoadIndicatorComponent),
            defaultValue: "Loading...");

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    // Bindable property for IsRunning/visibility
    public static readonly BindableProperty IsRunningProperty =
        BindableProperty.Create(
            nameof(IsRunning),
            typeof(bool),
            typeof(LoadIndicatorComponent),
            defaultValue: false);

    public bool IsRunning
    {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }
}