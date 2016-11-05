/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="SGDBoard.cs"
 *  
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using System;
using System.IO;

using Xamarin.Forms;

namespace HybridWebApp
{
    public partial class SGDBoard : ContentPage
	{
        const string FIRE_MEDIA_PICKER = "Load Images";
        const string FIRE_LOADING = "Load Icons";
        public HybridWebView mView;

        public SGDBoard()
        {

            mView = new HybridWebView
            {
                Uri = "index.html",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            mView.RegisterAction(data => {

                /*
                 * Launch media picker if text supplied matches tag
                 */
                if (data.ToString() == FIRE_MEDIA_PICKER)
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }

                    var file = CrossMedia.Current.PickPhotoAsync();

                    /*
                     * If file or result is null, kick back to app
                     */
                    if (file == null || file.Result == null) return;

                    if (File.Exists(@file.Result.Path))
                    {
                        var popup = new PopUpWindow("Please name the Icon", string.Empty, "OK", "Cancel");
                        popup.PopupClosed += (o, closedArgs) =>
                        {

                            /*
                             * If file exists and a proper name is supplied, add to the board
                             */
                            if (closedArgs.Button == "OK" && closedArgs.Text.Trim().Length > 0)
                            {
                                byte[] imageArray = File.ReadAllBytes(@file.Result.Path);
                                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                                var extension = Path.GetExtension(@file.Result.Path);

                                mView.CallJavascript(base64ImageRepresentation, closedArgs.Text, extension);
                            }
                        };

                        popup.Show();
                    }
                }
                else if (data.ToString() == FIRE_LOADING)
                {
                    var mData = DependencyService.Get<SaveAndLoadInterface>().LoadJSON("savedBoards.txt");

                    Console.WriteLine("in board: " + mData);

                    if (mData.Trim().Length > 0)
                    {
                        mView.AddIcons(mData.ToString());
                    }

                }
                else if (IsValidJson(data.ToString()))
                {
                    DependencyService.Get<SaveAndLoadInterface>().SaveJSON("savedBoards.txt", data);
                }
                else
                {
                    DependencyService.Get<TextToSpeechInterface>().Speak(data);
                }
            });

            Content = mView;
        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void CallSaves()
        {
            mView.SaveWithJavascript();
        }
    }
}
