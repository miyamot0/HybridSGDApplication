/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="TextToSpeechImplementation.cs"
 *  
 *  ===================================================
 *  
 *  Based on code samples shared by Xamarin
 *  https://developer.xamarin.com/guides/xamarin-forms/dependency-service/text-to-speech/
 *    
 */

using Android.Speech.Tts;
using Xamarin.Forms;
using System.Collections.Generic;
using HybridWebApp.Droid;

[assembly: Dependency(typeof(TextToSpeechImplementation))]
namespace HybridWebApp.Droid
{
    public class TextToSpeechImplementation : Java.Lang.Object, TextToSpeechInterface, TextToSpeech.IOnInitListener
    {
        TextToSpeech mTTS;
        string sentenceStrip;

        public TextToSpeechImplementation()
        {
            var mContext = Forms.Context;
            mTTS = new TextToSpeech(mContext, this);
        }

        public void Speak(string text)
        {
            var mContext = Forms.Context;
            sentenceStrip = text;

            if (mTTS == null)
            {
                mTTS = new TextToSpeech(mContext, this);
            }
            else
            {
                mTTS.Speak(sentenceStrip, QueueMode.Flush, new Dictionary<string, string>());
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                mTTS.Speak(sentenceStrip, QueueMode.Flush, new Dictionary<string, string>());
            }
        }
        #endregion
    }
}