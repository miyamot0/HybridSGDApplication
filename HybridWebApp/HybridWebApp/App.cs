/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  
 *  HybridWebApp - Selection based communication aide
 *  File="App.cs"
 *  
 *  This Source Code Form is subject to the terms of the Mozilla Public
 *  License, v. 2.0. If a copy of the MPL was not distributed with this
 *  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *  
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
