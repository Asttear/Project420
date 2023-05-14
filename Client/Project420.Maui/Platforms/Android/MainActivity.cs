using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Project420.Maui
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetStatusBarColor(Android.Graphics.Color.White);
            Window.SetNavigationBarColor(Android.Graphics.Color.White);
            base.OnCreate(savedInstanceState);
        }
    }
}