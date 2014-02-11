using Rosier.Glucose.Phone.ViewModels;

namespace Rosier.Glucose.Phone.Pages
{
    public class MainTypedBasePage : BasePage<MainViewModel>
    {
        public MainTypedBasePage()
        {
            this.ViewModel = new MainViewModel();
        }
    }
}
