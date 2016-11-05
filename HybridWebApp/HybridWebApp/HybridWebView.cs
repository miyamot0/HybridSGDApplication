/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="HybridWebView.cs"
 *  
 *  ===================================
 *  
 *  Based on original code derived from David Britch
 *  https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/HybridWebView
 *  Released alongside Xamarin form samples
 *  
 */

using System;
using Xamarin.Forms;

namespace HybridWebApp
{
    public class HybridWebView : View
    {
        Action<string> action;
        public event EventHandler<string[]> DoJavascriptCalls;
        public event EventHandler<string> DoJavascriptSaves;
        public event EventHandler<string> DoAddIcons;

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
          propertyName: "Uri",
          returnType: typeof(string),
          declaringType: typeof(HybridWebView),
          defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public void CallJavascript(string data, string name, string extension)
        {   
            DoJavascriptCalls(this, new string[] { data, name, extension });
        }

        public void AddIcons(string data)
        {
            DoAddIcons(this, data);
        }

        public void SaveWithJavascript()
        {
            DoJavascriptSaves(this, "Attempt Save");
        }

        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }
    }
}
