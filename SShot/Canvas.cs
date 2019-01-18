﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SShot
{
    class Canvas : Form
    {

    
        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        bool drawing;

        public Canvas()
        {
           
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Opacity = 0.75;
            this.Cursor = Cursors.Cross;
            this.MouseDown += Canvas_MouseDown;
            this.MouseMove += Canvas_MouseMove;
            this.MouseUp += Canvas_MouseUp;
            this.Paint += Canvas_Paint;
            this.KeyDown += Canvas_KeyDown;
            this.DoubleBuffered = true;
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;
        }
        public void minimizeAll(Form1 parentForm)
        {
            foreach (Form1 childForm in parentForm.MdiChildren)
            {
                childForm.WindowState = FormWindowState.Minimized;
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            currentPos = e.Location;
            if (drawing) this.Invalidate();
        }
        public static void pause()
        {
            Console.Read();
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (drawing) e.Graphics.DrawRectangle(Pens.Red, GetRectangle());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Canvas";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Canvas_Load);
            this.ResumeLayout(false);

        }

        private void Canvas_Load(object sender, EventArgs e)
        {
           
        }
    }
}
