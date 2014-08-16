using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Splat;
using TodoTxt.Sharp.UI.Services;
using TodoTxt.Sharp.UI.ViewModels;

namespace TodoTxt.Sharp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            Locator.CurrentMutable.RegisterConstant(new GetFileNameService(), typeof(IGetFileNameService));
            Locator.CurrentMutable.Register(() => {
                return new MainWindowViewModel(Locator.Current.GetService<IGetFileNameService>());
            }, typeof(MainWindowViewModel));
        }
    }
}
