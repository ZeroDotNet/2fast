﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using Prism.Commands;
using System;
using Prism.Ioc;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Project2FA.UWP.Strings;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Project2FA.UWP.Services;
using System.Threading.Tasks;
using Project2FA.UWP.Views;
using Prism.Services.Dialogs;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.Input;

namespace Project2FA.UWP.Utils
{
    public static class ErrorDialogs
    {
        /// <summary>
        /// Displays that the system time is not correct
        /// </summary>
        /// <returns></returns>
        public static Task SystemTimeNotCorrectError()
        {
            IDialogService dialogService = App.Current.Container.Resolve<IDialogService>();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = Resources.AccountCodePageWrongTimeTitle;
            dialog.Content = Resources.AccountCodePageWrongTimeContent;
            dialog.PrimaryButtonText = Resources.AccountCodePageWrongTimeBTN;
#pragma warning disable AsyncFixer03 // Fire-and-forget async-void methods or delegates
            dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:dateandtime"));
            });
#pragma warning restore AsyncFixer03 // Fire-and-forget async-void methods or delegates
            dialog.SecondaryButtonText = Resources.Confirm;
            return dialogService.ShowDialogAsync(dialog, new DialogParameters());
        }
        public static async Task ShowUnauthorizedAccessError()
        {
            IDialogService dialogService = App.Current.Container.Resolve<IDialogService>();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = Resources.AuthorizationFileSystemContentDialogTitle;
            MarkdownTextBlock markdown = new MarkdownTextBlock();
            markdown.Text = Resources.AuthorizationFileSystemContentDialogDescription;
            dialog.Content = markdown;
#pragma warning disable AsyncFixer03 // Fire-and-forget async-void methods or delegates
            dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
                Prism.PrismApplicationBase.Current.Exit();
            });
#pragma warning restore AsyncFixer03 // Fire-and-forget async-void methods or delegates
            dialog.PrimaryButtonText = Resources.AuthorizationFileSystemContentDialogPrimaryBTN;
            dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            dialog.SecondaryButtonText = Resources.AuthorizationFileSystemContentDialogSecondaryBTN;
            dialog.SecondaryButtonCommand = new DelegateCommand(() =>
            {
                Prism.PrismApplicationBase.Current.Exit();
            });
            ContentDialogResult result = await dialogService.ShowDialogAsync(dialog, new DialogParameters());
            if (result == ContentDialogResult.None)
            {
                Prism.PrismApplicationBase.Current.Exit();
            }
        }

        /// <summary>
        /// Displays a wrong password error message and option to change the password
        /// </summary>
        public static async Task ShowPasswordError()
        {
            IDialogService dialogService = App.Current.Container.Resolve<IDialogService>();
            MarkdownTextBlock markdown = new MarkdownTextBlock();
            markdown.Text = Resources.PasswordInvalidMessage;
            ContentDialog dialog = new ContentDialog
            {
                Title = Resources.PasswordInvalidHeader,
                Content = markdown,
                PrimaryButtonText = Resources.ChangePassword,
                PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style,

#pragma warning disable AsyncFixer03 // Fire-and-forget async-void methods or delegates
                PrimaryButtonCommand = new DelegateCommand(async () =>
                {
                    var dialog = new ChangeDatafilePasswordContentDialog();
                    var param = new DialogParameters();
                    param.Add("isInvalid", true);
                    await dialogService.ShowDialogAsync(dialog, param);
                }),
#pragma warning restore AsyncFixer03 // Fire-and-forget async-void methods or delegates

                SecondaryButtonText = Resources.CloseApp,
                SecondaryButtonCommand = new DelegateCommand(() =>
                {
                    Prism.PrismApplicationBase.Current.Exit();
                })
            };

            ContentDialogResult result = await dialogService.ShowDialogAsync(dialog, new DialogParameters());
            if (result == ContentDialogResult.None)
            {
                ShowPasswordError();
            }
        }

        

        public static Task UnauthorizedAccessDialog()
        {
            var dialog = new ContentDialog
            {
                Title = Strings.Resources.AuthorizationFileSystemContentDialogTitle
            };
            var markdown = new MarkdownTextBlock
            {
                Text = Strings.Resources.AuthorizationFileSystemContentDialogDescription
            };
            dialog.Content = markdown;
            dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
                Prism.PrismApplicationBase.Current.Exit();
            });
            dialog.Style = App.Current.Resources["MyContentDialogStyle"] as Style;
            dialog.PrimaryButtonText = Strings.Resources.AuthorizationFileSystemContentDialogPrimaryBTN;
            dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            dialog.SecondaryButtonText = Strings.Resources.AuthorizationFileSystemContentDialogSecondaryBTN;
            dialog.SecondaryButtonCommand = new DelegateCommand(() =>
            {
                Prism.PrismApplicationBase.Current.Exit();
            });
            var dialogService = App.Current.Container.Resolve<IDialogService>();
            return dialogService.ShowDialogAsync(dialog, new DialogParameters());
        }

        public static Task UnauthorizedAccessUseLocalFileDialog()
        {
            var dialog = new ContentDialog
            {
                Title = Strings.Resources.AuthorizationFileSystemContentDialogTitle
            };
            var markdown = new MarkdownTextBlock
            {
                Text = Strings.Resources.AuthorizationFileSystemContentDialogDescription
            };
            dialog.Style = App.Current.Resources["MyContentDialogStyle"] as Style;
            dialog.Content = markdown;
            dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
                Prism.PrismApplicationBase.Current.Exit();
            });
            dialog.CloseButtonCommand = new DelegateCommand(() =>
            {
                dialog.Hide();
            });
            dialog.CloseButtonText = Resources.ButtonTextCancel;
            dialog.PrimaryButtonText = Strings.Resources.AuthorizationFileSystemContentDialogPrimaryBTN;
            dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            //dialog.SecondaryButtonText = Strings.Resources.AuthorizationFileSystemContentDialogSecondaryBTN;
            //dialog.SecondaryButtonCommand = new DelegateCommand(() =>
            //{
            //    Prism.PrismApplicationBase.Current.Exit();
            //});
            var dialogService = App.Current.Container.Resolve<IDialogService>();
            return dialogService.ShowDialogAsync(dialog, new DialogParameters());
        }

        public async static Task ShowUnexpectedError(Exception exc)
        {
            var dialog = new ContentDialog
            {
                Title = Resources.ErrorHandle
            };
            var errorTextBlock = new MarkdownTextBlock()
            {
                Margin = new Thickness(0, 8, 0, 8)
            };
            errorTextBlock.Text = "#" + exc.Message  + "\n"
                + exc.StackTrace + "\n"
                + exc.InnerException;
            var stackpanel = new StackPanel();
            var clipboardButton = new Button();
            clipboardButton.Margin = new Thickness(0, 10, 0, 0);
            clipboardButton.Content = Resources.ErrorCopyToClipboard;
            clipboardButton.Click += (s, e) =>
            {
                DataPackage dataPackage = new DataPackage
                {
                    RequestedOperation = DataPackageOperation.Copy
                };
                dataPackage.SetText(errorTextBlock.Text);
                Clipboard.SetContent(dataPackage);
            };
            stackpanel.Children.Add(new TextBlock() { Text = Resources.ErrorHandleDescription, TextWrapping = TextWrapping.WrapWholeWords });
            stackpanel.Children.Add(clipboardButton);
            stackpanel.Children.Add(errorTextBlock);

            var githubButton = new Button
            {
                Content = "Github"
            };
            githubButton.Click += async (s, e) =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/2fast-team/2fast/issues"));
            };
            var feedbackHub = new Button
            {
                Margin = new Thickness(4, 0, 0, 0),
                Content = "Feedback-Hub"
            };
            feedbackHub.Click += async (s, e) =>
            {
                // https://docs.microsoft.com/en-us/windows/uwp/monetize/launch-feedback-hub-from-your-app
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            };
            var buttonStackPanel = new StackPanel();
            buttonStackPanel.Orientation = Orientation.Horizontal;
            buttonStackPanel.Children.Add(githubButton);
            buttonStackPanel.Children.Add(feedbackHub);
            stackpanel.Children.Add(buttonStackPanel);

            dialog.Style = App.Current.Resources["MyContentDialogStyle"] as Style;
            dialog.Content = stackpanel;
            dialog.PrimaryButtonText = Resources.RestartApp;
            dialog.SecondaryButtonText = Resources.CloseApp;
            dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            {
                await CoreApplication.RequestRestartAsync("Exception");
            });
            dialog.SecondaryButtonCommand = new DelegateCommand(() =>
            {
                Prism.PrismApplicationBase.Current.Exit();
            });
            var result = await App.Current.Container.Resolve<IDialogService>().ShowDialogAsync(dialog, new DialogParameters());
            if (result == ContentDialogResult.None)
            {
                ShowUnexpectedError(exc);
            }
        }

        public async static Task ShowUnexpectedError(string errorDetail)
        {
            var dialog = new ContentDialog
            {
                Title = Resources.ErrorHandle
            };
            var errorMarkdownTextBlock = new MarkdownTextBlock()
            {
                Margin = new Thickness(0, 4, 0, 0)
            };
            errorMarkdownTextBlock.Text = errorDetail;
            //errorMarkdownTextBlock.IsReadOnly = true;
            var stackpanel = new StackPanel();
            var clipboardButton = new Button();
            clipboardButton.Margin = new Thickness(0, 10, 0, 0);
            clipboardButton.Content = Resources.ErrorCopyToClipboard;
            clipboardButton.Click += (s, e) =>
            {
                DataPackage dataPackage = new DataPackage
                {
                    RequestedOperation = DataPackageOperation.Copy
                };
                dataPackage.SetText(errorMarkdownTextBlock.Text);
                Clipboard.SetContent(dataPackage);
            };
            stackpanel.Children.Add(new TextBlock()
            {
                Text = Resources.ErrorHandleDescriptionLastSession,
                TextWrapping = TextWrapping.WrapWholeWords }
            );
            stackpanel.Children.Add(clipboardButton);
            stackpanel.Children.Add(errorMarkdownTextBlock);

            var githubButton = new Button
            {
                Content = "GitHub"
            };
            githubButton.Click += async (s, e) =>
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/2fast-team/2fast/issues"));
            };
            var feedbackHub = new Button
            {
                Margin = new Thickness(4, 0, 0, 0),
                Content = "Feedback-Hub"
            };
            feedbackHub.Click += async (s, e) =>
            {
                // https://docs.microsoft.com/en-us/windows/uwp/monetize/launch-feedback-hub-from-your-app
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            };
            var buttonStackPanel = new StackPanel();
            buttonStackPanel.Orientation = Orientation.Horizontal;
            buttonStackPanel.Children.Add(githubButton);
            buttonStackPanel.Children.Add(feedbackHub);
            stackpanel.Children.Add(buttonStackPanel);

            dialog.Style = App.Current.Resources["MyContentDialogStyle"] as Style;
            dialog.Content = stackpanel;
            dialog.PrimaryButtonText = Resources.Confirm;
            //dialog.SecondaryButtonText = Resources.CloseApp;
            //dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            //dialog.PrimaryButtonCommand = new DelegateCommand(async () =>
            //{
            //    await CoreApplication.RequestRestartAsync("Exception");
            //});
            //dialog.SecondaryButtonCommand = new DelegateCommand(() =>
            //{
            //    Prism.PrismApplicationBase.Current.Exit();
            //});
            var result = await App.Current.Container.Resolve<IDialogService>().ShowDialogAsync(dialog, new DialogParameters());
            SettingsService.Instance.UnhandledExceptionStr = string.Empty;
        }

        public async static Task<ContentDialogResult> WritingDatafileError(bool isNewFile)
        {
            IDialogService dialogService = App.Current.Container.Resolve<IDialogService>();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = Resources.Error;
            MarkdownTextBlock markdown = new MarkdownTextBlock();
            markdown.Text = string.Format(Resources.WriteDatafileErrorDesc);
            dialog.Content = markdown;
            dialog.PrimaryButtonCommand = new RelayCommand(() =>
            {
                if (!isNewFile)
                {
                    DataService.Instance.WriteLocalDatafile();
                }
            });
            dialog.PrimaryButtonText = Resources.WriteDatafileErrorBTNRetry;
            dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            dialog.SecondaryButtonText = Resources.WriteDatafileErrorBTNCancel;
            dialog.SecondaryButtonCommand = new RelayCommand(() =>
            {
                // ReloadDatafile();
            });
            return await dialogService.ShowDialogAsync(dialog, new DialogParameters());
        }
    }
}
