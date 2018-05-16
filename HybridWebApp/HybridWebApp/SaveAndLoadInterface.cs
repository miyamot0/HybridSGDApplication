/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="SaveAndLoadInterface.cs"
 *  
 *  This Source Code Form is subject to the terms of the Mozilla Public
 *  License, v. 2.0. If a copy of the MPL was not distributed with this
 *  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *  
 *  ===================================================
 *  
 *  Based on original code shared by Craig Dunn
 *  https://github.com/xamarin/xamarin-forms-samples/tree/master/WorkingWithFiles
 *  Released alongside Xamarin form samples
 *  
 */

namespace HybridWebApp
{
    public interface SaveAndLoadInterface
    {
        void SaveJSON(string filename, string text);
        string LoadJSON(string filename);
    }
}
