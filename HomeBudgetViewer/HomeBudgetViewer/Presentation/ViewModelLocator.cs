using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HomeBudgetViewer.Presentation.AboutPage;
using HomeBudgetViewer.Presentation.MainPage;
using HomeBudgetViewer.Presentation.SettingsPage;
using Microsoft.Practices.ServiceLocation;

namespace HomeBudgetViewer.Presentation
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(()=>SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            else
            {
               
            }

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<AboutPageViewModel>();
        }

        public AboutPageViewModel AboutPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AboutPageViewModel>(); }
        }

        public MainPageViewModel MainPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        public SettingsPageViewModel SettingsPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsPageViewModel>(); }
        }

    }
}
