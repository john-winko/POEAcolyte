﻿using System;
using System.Drawing;
using System.Windows.Forms;
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace PoeAcolyte.UI.Components
{
    public sealed class FrameControl : Control
    {
        public EventHandler ClickHandler;

        // public string Description
        // {
        //     get => _lblDescription.Text;
        //     set => _lblDescription.Text = value;
        // }

        private Label _lblDescription;

        public FrameControl(string description, Point location, Size size)
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.Transparent;

            Resize += OnResize;
            _lblDescription = new Label()
            {
                Size = new Size(50, 20),
                Location = new Point(15, 15),
                Text = description,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            _lblDescription.Click += (sender, args) =>
            {
                ClickHandler?.Invoke(sender, args);
            };
            Controls.Add(_lblDescription);
            Location = location;
            Size = size;
            EnableDragging();
        }

        private bool _dragging;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;

        private void EnableDragging()
        {
            _lblDescription.MouseDown += (sender, args) => 
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            };
            _lblDescription.MouseMove += (sender, args) =>
            {
                if (!_dragging) return;
                var dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(dif));
            };
            _lblDescription.MouseUp += (sender, args) => { _dragging = false;};
        }

        private void OnResize(object sender, EventArgs e)
        {
            _lblDescription.Size = new Size()
            {
                Width = Width - 30,
                Height = Height - 30
            };
            // set text to a maximum size that should still fit into description box
            var w = 2 * Width / _lblDescription.Text.Length;
            var h = 2 * Height / _lblDescription.Text.Length;
            var x = w > h ? h : w;
            _lblDescription.Font = new Font("Calibri", x, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var p = new Pen(Color.Red, 5);
            //p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        }

        const int WM_NCHITTEST = 0x84;
        const int WM_SETCURSOR = 0x20;
        const int WM_NCLBUTTONDBLCLK = 0xA3;

        protected override void WndProc(ref Message m)
        {
            int borderWidth = 10;
            if (m.Msg == WM_SETCURSOR) /*Setting cursor to SizeAll*/
            {
                if ((m.LParam.ToInt32() & 0xffff) == 0x2 /*Move*/)
                {
                    Cursor.Current = Cursors.SizeAll;
                    m.Result = (IntPtr)1;
                    return;
                }
            }

            if ((m.Msg == WM_NCLBUTTONDBLCLK)) /*Disable Mazimiz on Double click*/
            {
                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                var pos = PointToClient(new Point(m.LParam.ToInt32() & 0xffff,
                    m.LParam.ToInt32() >> 16));
                if (pos.X <= ClientRectangle.Left + borderWidth &&
                    pos.Y <= ClientRectangle.Top + borderWidth)
                    m.Result = new IntPtr(13); //TOPLEFT
                else if (pos.X >= ClientRectangle.Right - borderWidth &&
                         pos.Y <= ClientRectangle.Top + borderWidth)
                    m.Result = new IntPtr(14); //TOPRIGHT
                else if (pos.X <= ClientRectangle.Left + borderWidth &&
                         pos.Y >= ClientRectangle.Bottom - borderWidth)
                    m.Result = new IntPtr(16); //BOTTOMLEFT
                else if (pos.X >= ClientRectangle.Right - borderWidth &&
                         pos.Y >= ClientRectangle.Bottom - borderWidth)
                    m.Result = new IntPtr(17); //BOTTOMRIGHT
                else if (pos.X <= ClientRectangle.Left + borderWidth)
                    m.Result = new IntPtr(10); //LEFT
                else if (pos.Y <= ClientRectangle.Top + borderWidth)
                    m.Result = new IntPtr(12); //TOP
                else if (pos.X >= ClientRectangle.Right - borderWidth)
                    m.Result = new IntPtr(11); //RIGHT
                else if (pos.Y >= ClientRectangle.Bottom - borderWidth)
                    m.Result = new IntPtr(15); //Bottom
                else
                    m.Result = new IntPtr(2); //Move
            }
        }
    }
}
