/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="TextToSpeechImplementation.cs"
 *  
 *  This Source Code Form is subject to the terms of the Mozilla Public
 *  License, v. 2.0. If a copy of the MPL was not distributed with this
 *  file, You can obtain one at http://mozilla.org/MPL/2.0/.
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