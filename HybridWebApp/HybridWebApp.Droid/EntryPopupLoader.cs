/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="EntryPopupLoader.cs"
 *    
 *  ===========================================
 *  
 *  Credit to Peter Brachwitz  
 *  https://forums.xamarin.com/profile/241540/PeterBrachwitz
 *  Originally shared publicly at https://forums.xamarin.com/discussion/35838/how-to-do-a-simple-inputbox-dialog
 *  
 */

using Android.App;
using Android.Widget;
using HybridWebApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(EntryPopupLoader))]
namespace HybridWebApp.Droid
{
    public class EntryPopupLoader : PopUpWindowInterface
    {
        public void ShowPopup(PopUpWindow popup)
        {
            var alert = new AlertDialog.Builder(Forms.Context);
            var edit = new EditText(Forms.Context) { Text = popup.Text };
            alert.SetView(edit);
            alert.SetTitle(popup.Title);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new PopUpWindowArgs
                {
                    Button = "OK",
                    Text = edit.Text
                });
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new PopUpWindowArgs
                {
                    Button = "Cancel",
                    Text = edit.Text
                });
            });
            alert.Show();
        }
    }
}