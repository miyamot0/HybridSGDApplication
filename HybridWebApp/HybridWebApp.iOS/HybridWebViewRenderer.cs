/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="HybridWebViewRenderer.cs"
 *  
 *  This Source Code Form is subject to the terms of the Mozilla Public
 *  License, v. 2.0. If a copy of the MPL was not distributed with this
 *  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *  
 *  ===================================================
 *  
 *  Based on original code derived from David Britch
 *  https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/HybridWebView
 *  Released alongside Xamarin form samples
 *  
 */

using Foundation;
using HybridWebApp;
using HybridWebApp.iOS;
using System.IO;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace HybridWebApp.iOS
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        WKUserContentController userController;

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                userController = new WKUserContentController();
                var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
                userController.AddUserScript(script);
                userController.AddScriptMessageHandler(this, "invokeAction");

                var config = new WKWebViewConfiguration { UserContentController = userController };

                //config.WebsiteDataStore = null;
                var webView = new WKWebView(Frame, config);
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler("invokeAction");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }

            if (e.NewElement != null)
            {
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", Element.Uri));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
            }

            (e.NewElement as HybridWebView).DoJavascriptCalls += (sender, data) =>
            {
                if (Control != null)
                {
                    Device.BeginInvokeOnMainThread(() => {

                        var mString = string.Format("addNewIcon('{0}', '{1}', '{2}')", data[0].ToString(), data[1].ToString(), data[2].ToString());
                        Control.EvaluateJavaScript(new NSString(mString), null);
                    });
                }
            };

            (e.NewElement as HybridWebView).DoJavascriptSaves += (sender, data) =>
            {
                if (Control != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control.EvaluateJavaScript(new NSString("saveStatus()"), null);
                    });
                }
            };
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.InvokeAction(message.Body.ToString());
        }
    }
}