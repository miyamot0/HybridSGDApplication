/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="JSBridge.cs"
 *  
 *  ===================================
 *  
 *  Based on original code derived from David Britch
 *  https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/HybridWebView
 *  Released alongside Xamarin form samples
 *  
 */

using System;
using Android.Webkit;
using Java.Interop;

namespace HybridWebApp.Droid
{
    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                hybridRenderer.Element.InvokeAction(data);
            }
        }
    }
}