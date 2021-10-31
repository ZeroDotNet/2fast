﻿using Microsoft.UI.Xaml.Controls;
using Project2FA.UWP.Services;
using Project2FA.UWP.Services.Enums;
using Project2FA.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Project2FA.UWP.Views
{
    public sealed partial class WelcomePage : Page
    {
        WelcomePageViewModel ViewModel => DataContext as WelcomePageViewModel;
        public WelcomePage()
        {
            this.InitializeComponent();
            this.Loaded += WelcomePage_Loaded;
        }

        private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
        {
            App.ShellPageInstance.ShellViewInternal.Header = ViewModel;
            App.ShellPageInstance.ShellViewInternal.HeaderTemplate = ShellHeaderTemplate;
            ViewModel.IsTutorialOpen = true;
        }

        private void WelcomePageTutorialTip_ActionButtonClick(TeachingTip sender, object args)
        {

        }
    }
}
