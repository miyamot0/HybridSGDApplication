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

using AVFoundation;
using HybridWebApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechImplementation))]
namespace HybridWebApp.iOS
{
    public class TextToSpeechImplementation : TextToSpeechInterface
    {
        public TextToSpeechImplementation() { }

        public void Speak(string text)
        {
            var speechSynthesizer = new AVSpeechSynthesizer();

            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            };

            speechSynthesizer.SpeakUtterance(speechUtterance);
        }
    }
}
