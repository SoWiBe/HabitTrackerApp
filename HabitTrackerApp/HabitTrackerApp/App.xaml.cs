using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using HabitTrackerApp.Di;

namespace HabitTrackerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _container = AutoFac.Configure();
        }
    }
}