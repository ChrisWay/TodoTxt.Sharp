using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Caliburn.Micro.KeyBinding.Input;
using TodoTxt.Sharp.UI.Services;
using TodoTxt.Sharp.UI.ViewModels;

namespace TodoTxt.Sharp.UI
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public AppBootstrapper() {
            _container = new SimpleContainer();

            Initialize();
        }

        protected override void Configure()
        {
            // Setup Key binding triggers for keyboard shortcuts
            var trigger = Parser.CreateTrigger;
            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    var defaults = ConventionManager.GetElementConvention(target.GetType());
                    return defaults.CreateTrigger();
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (splits[0] == "Key")
                {
                    var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                    var trig = new KeyTrigger { Key = key };
                    if (splits.Length > 2 && splits[2] == "Modifier") {
                        trig.Modifiers = (ModifierKeys) Enum.Parse(typeof(ModifierKeys), splits[3], true);
                    }

                    return trig;
                }

                return trigger(target, triggerText);
            };

            ConfigureDependecyInjection(_container);
        }

        protected virtual void ConfigureDependecyInjection(SimpleContainer container) {
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            container.PerRequest<ShellViewModel>();
            container.Singleton<IGetFileNameService, GetFileNameService>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            base.OnStartup(sender, e);

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
