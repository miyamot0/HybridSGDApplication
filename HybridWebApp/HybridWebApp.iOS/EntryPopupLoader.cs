﻿/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="EntryPopupLoader.cs"
 *  
 *  This Source Code Form is subject to the terms of the Mozilla Public
 *  License, v. 2.0. If a copy of the MPL was not distributed with this
 *  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *  
 *  ===================================================
 *  
 *  Credit to Peter Brachwitz  
 *  https://forums.xamarin.com/profile/241540/PeterBrachwitz
 *  Originally shared publicly at https://forums.xamarin.com/discussion/35838/how-to-do-a-simple-inputbox-dialog
 *  
 */

using HybridWebApp.iOS;
using System;
using System.Linq;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(EntryPopupLoader))]
namespace HybridWebApp.iOS
{
    public class EntryPopupLoader : PopUpWindowInterface
    {
        public void ShowPopup(PopUpWindow popup)
        {
            var alert = new UIAlertView
            {
                Title = popup.Title,
                Message = popup.Text,
                AlertViewStyle = UIAlertViewStyle.PlainTextInput
            };

            foreach (var b in popup.Buttons)
            {
                alert.AddButton(b);
            }

            alert.Clicked += (s, args) =>
            {
                popup.OnPopupClosed(new PopUpWindowArgs
                {
                    Button = popup.Buttons.ElementAt(Convert.ToInt32(args.ButtonIndex)),
                    Text = alert.GetTextField(0).Text
                });
            };
            alert.Show();
        }
    }
}
