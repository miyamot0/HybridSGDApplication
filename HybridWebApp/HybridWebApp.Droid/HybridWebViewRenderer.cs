/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="HybridWebViewRenderer.cs"
 *  
 *  ===================================
 *  
 *  Based on original code derived from David Britch
 *  https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/HybridWebView
 *  Released alongside Xamarin form samples
 *  
 */

using Xamarin.Forms.Platform.Android;
using HybridWebApp;
using Xamarin.Forms;
using HybridWebApp.Droid;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace HybridWebApp.Droid
{
    /// <summary>
    /// Android-based custom renderer
    /// </summary>
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
    {
        /// <summary>
        /// Injected bridge for C#/JS interop
        /// </summary>
        const string JavaScriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";

        /// <summary>
        /// DOM update events
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                Android.Webkit.WebView webView = new Android.Webkit.WebView(Forms.Context);

                webView.Settings.LoadWithOverviewMode = true;
                webView.Settings.UseWideViewPort = true;
                webView.Settings.AllowFileAccess = true;
                webView.Settings.DatabaseEnabled = true;
                webView.Settings.DomStorageEnabled = true;
                webView.Settings.AllowFileAccessFromFileURLs = true;
                webView.Settings.AllowUniversalAccessFromFileURLs= true;
                webView.Settings.JavaScriptEnabled = true;

                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }

            if (e.NewElement != null)
            {
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                Control.LoadUrl(string.Format("file:///android_asset/Content/{0}", Element.Uri));
                
                InjectJS(JavaScriptFunction);
            }

            (e.NewElement as HybridWebView).DoJavascriptCalls += (sender, data) =>
            {
                if (Control != null)
                {                    
                    Device.BeginInvokeOnMainThread(() => {
                        var mString = string.Format("addNewIcon('{0}', '{1}', '{2}')", data[0].ToString(), data[1].ToString(), data[2].ToString());
                        Control.EvaluateJavascript(mString, null);
                    });
                }
            };

            (e.NewElement as HybridWebView).DoAddIcons += (sender, data) =>
            {
                if (Control != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var mString = "loadIconsFromJSON(" + data.ToString() + ")";
                        Control.EvaluateJavascript(mString, null);
                    });
                }
            };

            (e.NewElement as HybridWebView).DoJavascriptSaves += (sender, data) =>
            {
                if (Control != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control.EvaluateJavascript("saveStatus()", null);
                    });
                }
            };
        }

        /// <summary>
        /// Load javascript line and inserts into DOM
        /// </summary>
        /// <param name="script"></param>
        void InjectJS(string script)
        {
            if (Control != null)
            {
                Control.LoadUrl(string.Format("javascript: {0}", script));
            }
        }
    }
}