using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SShot
{
    public partial class Form1 : Form
    {

        private Rectangle canvasBounds = Screen.GetBounds(Point.Empty);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int TAKE_SNAP_HOTKEY_ID = 1;

        private ScreenCapture objScreenCapture;
        private int snapCount;
        private List<Bitmap> snaps;

        public Form1()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
            
        }

        public void RestoreAndRefresh()
        {
            this.Show();

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == TAKE_SNAP_HOTKEY_ID)
            {
                TakeSnap();
            }
            base.WndProc(ref m);
        }

     
        private void TakeSnap()
        {
            var snap = objScreenCapture.GetSnapShot();
            this.snaps.Add(snap);
            AddToPreview(snap);
        }
        public static void pause()
        {
            Console.Read();
        }
        private void AddToPreview(Bitmap snap)
        {
            imageList1.Images.Add(snap);
            listView1.Items.Add(new ListViewItem("Snap_" + (++snapCount), imageList1.Images.Count - 1)).EnsureVisible();

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, TAKE_SNAP_HOTKEY_ID);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void takeSnapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread.Sleep(1000);
            string ordner = (Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\" + "Bildschirmfotos");

            DirectoryInfo di = Directory.CreateDirectory(ordner);

           
           

            Bitmap memoryImage;
            memoryImage = new Bitmap(1920, 1080);
            Size s = new Size(memoryImage.Width, memoryImage.Height);

            Graphics memoryGraphics = Graphics.FromImage(memoryImage);

            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            string str = "";
            try
            {
                str = string.Format(ordner) +
                      (@"\" + textBox1.Text + ".png");
            }
            catch (Exception er)
            {
                MessageBox.Show("Sorry, there was an error: " + er.Message);

            }

            memoryImage.Save(str);

            this.Show();
            

            // Pause the program to show the message. 
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();


            System.Threading.Thread.Sleep(1000);
            objScreenCapture = new ScreenCapture();
            snapCount = 0;
            snaps = new List<Bitmap>();

            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            RegisterHotKey(this.Handle, TAKE_SNAP_HOTKEY_ID, 6, (int)Keys.Z);
            TakeSnap();
            string ordner = (Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\" + "Bildschirmfotos");

            DirectoryInfo di = Directory.CreateDirectory(ordner);

            foreach (Image image in listView1.LargeImageList.Images)
            {
                string filename = string.Format(ordner) +
                          (@"\" + textBox1.Text + ".png"); // make this whatever you need...
                image.Save(filename);
                this.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ordner = (Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\" + "Bildschirmfotos");
            DirectoryInfo di = Directory.CreateDirectory(ordner);
            Process.Start("explorer.exe", ordner);
        }
    }
        }
   