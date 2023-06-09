using System.Globalization;
using ZLMClaims.Validations;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    public ValidatableObject<string> WhatHappened { get; private set; }

    private readonly ClaimFormStep1ViewModel _viewModel;
    public ClaimFormStep1Page(ClaimFormStep1ViewModel vm)

    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1Page] [Constructor] Start");

        BindingContext = vm;
        _viewModel = vm;
        AddValidations();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void AddValidations()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1Page] [AddValidations] Start");

        /*
        WhatHappened = new ValidatableObject<string>(); // Maak een nieuw ValidatableObject aan
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1Page] [AddValidations] WhatHappened validated");
        WhatHappened.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Field what happened is required."
        });

        // Koppel het ValidatableObject aan de binding van de Editor
        QWhatHappened.BindingContext = WhatHappened;
        QWhatHappened.SetBinding(Editor.TextProperty, new Binding("Value", source: WhatHappened));

        // Koppel de Errors van het ValidatableObject aan de Text van de Label
        QWhatHappenedErrorLabel.BindingContext = WhatHappened;
        QWhatHappenedErrorLabel.SetBinding(Label.TextProperty, new Binding("Errors", source: WhatHappened));
        */
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1Page] [AddValidations] Einde");
    }


}