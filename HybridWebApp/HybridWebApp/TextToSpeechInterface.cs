/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="TextToSpeechInterface.cs"
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

namespace HybridWebApp
{
    public interface TextToSpeechInterface
    {
        void Speak(string text);
    }
}
