namespace Spherum.Mobile.Views.Base;

public class BaseContentPage : ContentPage
{
    public static readonly BindableProperty LoadingMessageProperty =
        BindableProperty.Create(nameof(LoadingMessage), typeof(string), typeof(BaseContentPage),
                                propertyChanged: OnLoadingMessageChanged);

    static void OnLoadingMessageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var control = (BaseContentPage)bindable;
            var newVal = (string)newValue;
            var overlay = control.InternalChildren.FirstOrDefault().FindByName<LoadingOverlay>("LoadingOverlayKey");
            var label = overlay?.FindByName<Label>("LoadingLabel");
            if (label is null)
            {
                return;
            }

            label.Text = newVal;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public string? LoadingMessage
    {
        get => (string)GetValue(LoadingMessageProperty);
        set => SetValue(LoadingMessageProperty, value);
    }

    public static readonly BindableProperty LoadingLabelStyleProperty =
        BindableProperty.Create(nameof(LoadingLabelStyle), typeof(Style), typeof(BaseContentPage),
                                propertyChanged: LoadingLabelStyleChanged);

    static void LoadingLabelStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var control = (BaseContentPage)bindable;
            var newVal = (Style)newValue;
            var overlay = control.InternalChildren.FirstOrDefault().FindByName<LoadingOverlay>("LoadingOverlayKey");
            var label = overlay?.FindByName<Label>("LoadingLabel");
            if (label is null)
            {
                return;
            }

            label.Style = newVal;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public Style? LoadingLabelStyle
    {
        get => (Style)GetValue(LoadingLabelStyleProperty);
        set => SetValue(LoadingLabelStyleProperty, value);
    }


    public new static readonly BindableProperty IsBusyProperty =
        BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(BaseContentPage),
                                propertyChanged: OnIsBusyChanged);

    static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var control = (BaseContentPage)bindable;
            var context = (BaseViewModel)control.BindingContext;
            var newVal = (bool)newValue;
            var overlay = control.InternalChildren.FirstOrDefault().FindByName<LoadingOverlay>("LoadingOverlayKey");

            if (overlay is null)
            {
                return;
            }

            if (newVal && context.UseObtrusiveOverlay)
            {
                overlay.IsVisible = true;

                return;
            }

            overlay.IsVisible = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public new bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public BaseContentPage()
    {
        ControlTemplate = (ControlTemplate)Application.Current?.Resources["BaseViewTemplate"]!;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
#if IOS
        // disable iOS swipe gesture to navigate away from the page

        if (Platform.GetCurrentUIViewController() is not UIKit.UINavigationController vc)
        {
            return;
        }

        vc.InteractivePopGestureRecognizer.Enabled = false;
#endif
    }
}