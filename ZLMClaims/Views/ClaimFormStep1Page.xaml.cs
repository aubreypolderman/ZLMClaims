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

        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

}