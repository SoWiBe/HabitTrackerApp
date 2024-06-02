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
        protected override void OnStartup(StartupEventArgs e)
        {
            
            var builder = AutoFac.Default.Builder;
            AutoFac.Default.Build();
            base.OnStartup(e);
        }
    }
}