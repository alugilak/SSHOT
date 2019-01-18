using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SShot
{
    class Utility
    {
        public static void SaveAsImages(List<Bitmap> images)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int count = 1;
                    foreach (Bitmap img in images)
                    {
                        img.Save(dialog.SelectedPath + "\\Snap_" + (count++) + ".bmp");
                    }
                }
            }
        }
       
    }    
}
