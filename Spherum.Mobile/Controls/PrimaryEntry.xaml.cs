namespace Spherum.Mobile.Controls;

public partial class PrimaryEntry
{
    readonly Style _normalBorderStyle;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(PrimaryEntry), defaultValue: string.Empty,
        propertyChanged: HandleTitlePropertyChangedDelegate);

    static void HandleTitlePropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PrimaryEntry control)
        {
            return;
        }

        control.EntryTitle.IsVisible = !string.IsNullOrEmpty(control.Title);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(PrimaryEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText), typeof(string), typeof(PrimaryEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(
        nameof(HasError), typeof(bool), typeof(PrimaryEntry), false, defaultBindingMode: BindingMode.TwoWay, null,
        HandleErrorPropertyChangedDelegate);

    static void HandleErrorPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PrimaryEntry control)
        {
            return;
        }

        var newVal = (bool)newValue;
        control.BorderStyle = newVal ? control.BorderErrorStyle : control._normalBorderStyle;
    }

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder), typeof(string), typeof(PrimaryEntry), defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty KeyBoardTypeProperty =
        BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(PrimaryEntry), Keyboard.Default);

    public Keyboard KeyboardType
    {
        get => (Keyboard)GetValue(KeyBoardTypeProperty);
        set => SetValue(KeyBoardTypeProperty, value);
    }

    public static readonly BindableProperty ReturnTypeProperty =
        BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(PrimaryEntry),
                                defaultValue: ReturnType.Default);

    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    public static readonly BindableProperty ReturnCommandProperty =
        BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(PrimaryEntry));

    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }

    public static readonly BindableProperty TextChangedCommandProperty =
        BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(PrimaryEntry));

    public ICommand? TextChangedCommand
    {
        get => (ICommand)GetValue(TextChangedCommandProperty);
        set => SetValue(TextChangedCommandProperty, value);
    }

    public static readonly BindableProperty EntryStyleProperty =
        BindableProperty.Create(nameof(EntryStyle), typeof(Style), typeof(PrimaryEntry));

    public Style EntryStyle
    {
        get => (Style)GetValue(EntryStyleProperty);
        set => SetValue(EntryStyleProperty, value);
    }

    public static readonly BindableProperty BorderStyleProperty =
        BindableProperty.Create(nameof(BorderStyle), typeof(Style), typeof(PrimaryEntry));

    public Style BorderStyle
    {
        get => (Style)GetValue(BorderStyleProperty);
        set => SetValue(BorderStyleProperty, value);
    }

    public static readonly BindableProperty BorderErrorStyleProperty =
        BindableProperty.Create(nameof(BorderErrorStyle), typeof(Style), typeof(PrimaryEntry));

    public Style BorderErrorStyle
    {
        get => (Style)GetValue(BorderErrorStyleProperty);
        set => SetValue(BorderErrorStyleProperty, value);
    }

    public event EventHandler<string>? TextChanged;

    public event EventHandler<FocusEventArgs>? EntryFocused;

    public event EventHandler<FocusEventArgs>? EntryUnFocused;

    public PrimaryEntry()
    {
        InitializeComponent();

        EntryStyle = (Style)Resources["EntryStyle"];
        BorderStyle = (Style)Resources["EntryBorderStyle"];
        BorderErrorStyle = (Style)Resources["EntryBorderErrorStyle"];
        _normalBorderStyle = BorderStyle;
    }

    void EntryControl_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (TextChangedCommand != null && TextChangedCommand.CanExecute(e.NewTextValue))
        {
            TextChangedCommand.Execute(e.NewTextValue);
        }

        TextChanged?.Invoke(this, e.NewTextValue);
    }

    void EntryControl_OnFocused(object? sender, FocusEventArgs e)
    {
        EntryFocused?.Invoke(this, e);
    }

    void EntryControl_OnUnfocused(object? sender, FocusEventArgs e)
    {
        EntryUnFocused?.Invoke(this, e);
    }
}