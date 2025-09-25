using System.Windows.Input;

namespace MauiStart.Components;

public partial class ButtonWithIcon : Frame
{
    #region Bindable properties
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ButtonWithIcon),
        null);
    
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(ButtonWithIcon),
        string.Empty);

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ButtonWithIcon));
    
    #endregion

    public ImageSource Icon
    {
        get { return (ImageSource)GetValue(IconProperty); }
        set => SetValue(IconProperty, value);
    }
    
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set => SetValue(TextProperty, value);
    }
    
    public ICommand Command
    {
        get { return (Command)GetValue(CommandProperty); }
        set => SetValue(CommandProperty, value);
    }

    public ButtonWithIcon()
    {
        InitializeComponent();
    }
}