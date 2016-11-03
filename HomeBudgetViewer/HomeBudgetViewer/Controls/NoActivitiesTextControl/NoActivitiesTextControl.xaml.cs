using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HomeBudgetViewer.Controls.NoActivitiesTextControl
{
    public sealed partial class NoActivitiesTextControl : UserControl
    {
        public static DependencyProperty ControlImageLengthProperty = DependencyProperty.Register(
            "ControlImageLength",
            typeof(double), 
            typeof(NoActivitiesTextControl), 
            new PropertyMetadata(100.0));

        public static DependencyProperty ControlTextSizeProperty = DependencyProperty.Register(
            "ControlTextSize",
            typeof(double),
            typeof(NoActivitiesTextControl),
            new PropertyMetadata(14.0));

        public double ControlImageLenght
        {
            get { return (double) this.GetValue(ControlImageLengthProperty); }
            set
            {
                this.SetValue(ControlImageLengthProperty,value);
            }
        }

        public double ControlTextSize
        {
            get { return (double) this.GetValue(ControlTextSizeProperty); }
            set
            {
                this.SetValue(ControlTextSizeProperty,value);
            }
        }

        public NoActivitiesTextControl()
        {
            this.InitializeComponent();
        }
    }
}
