/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="SaveAndLoad.cs"
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

using HybridWebApp.Droid;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace HybridWebApp.Droid
{
    public class SaveAndLoad : SaveAndLoadInterface
    {
        public void SaveJSON(string filename, string text)
        {
            var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path;
            //var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(externalPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadJSON(string filename)
        {
            var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path;
            //var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(externalPath, filename);

            if (File.Exists(filePath))
            {
                return System.IO.File.ReadAllText(filePath);
            }
            else
            {
                return "";
            }

        }
    }
}