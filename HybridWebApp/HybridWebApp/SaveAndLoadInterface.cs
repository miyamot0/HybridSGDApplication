/*
 *  Copyright July 1, 2016 Shawn Gilroy 
 *  HybridWebApp - Selection based communication aide
 *  File="SaveAndLoadInterface.cs"
 *  
 *  ===================================
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
