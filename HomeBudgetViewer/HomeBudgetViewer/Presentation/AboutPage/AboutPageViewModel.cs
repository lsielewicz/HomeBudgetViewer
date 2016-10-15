using System;

namespace HomeBudgetViewer.Presentation.AboutPage
{
    public class AboutPageViewModel : AppViewModelBase
    {
        public AboutPageViewModel()
        {
            
        }

        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => "Łukasz Sielewicz";

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

    }
}
