﻿namespace ZLMClaims;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// The localizationResourceManager uses binding, so the context needs to be set
		BindingContext = this;

		// Register route to step1 so navigation can take place
        Routing.RegisterRoute(nameof(Views.ClaimFormStep1Page), typeof(Views.ClaimFormStep1Page));
    }

	public LocalizationResourceManager LocalizationResourceManager 
		=> LocalizationResourceManager.Instance;
}
