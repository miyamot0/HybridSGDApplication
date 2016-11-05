/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  
 *  HybridWebApp - Selection based communication aide
 *  File="App.cs"
 */
using Xamarin.Forms;

namespace HybridWebApp
{
    public class App : Application
	{
		public App ()
		{            
            MainPage = new SGDBoard();
		}

		protected override void OnStart () {}

		protected override void OnSleep ()
        {
            (MainPage as SGDBoard).CallSaves();
        }

		protected override void OnResume () {}
	}
}
