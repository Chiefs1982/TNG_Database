using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class RibbonButtons : Control
    {
        //Bools
        private bool _pressed = false;
        private bool _hovering = false;

        //Imagesize variable
        private int imageSize = 24;

        //Image of the button
        private Image fileName;
        
        //Default Size
        Size controlSize = new Size(60, 36);

        public RibbonButtons()
        {
            InitializeComponent();
        }
        

        //Enum for the selected look of the button
        public enum ImageForButton
        {
            Home,
            Search,
            MasterList,
            Deleted,
            ImportArchive,
            ImportTapes,
            ImportProjects,
            DatabaseBackup,
            Preferences,
            Reload
        }

        //To keep track of selected value for the button
        private ImageForButton imageType = ImageForButton.Home;

        /// <summary>
        /// Sets the type of button to display.
        /// </summary>
        [Description("Sets the type of button to display"), Category("Appearance"),
        DefaultValue(typeof(ImageForButton), "Home"), Browsable(true)]
        public ImageForButton ImageType
        {
            get { return imageType; }
            set
            {
                if (value == imageType)
                {
                    return;
                }

                imageType = value;

                Invalidate();
            }
        }

        //Gets and sets the text property
        public new string Text
        {
            get { return base.Text; }
            set
            {
                if(value == base.Text)
                {
                    return;
                }

                base.Text = value;

                Invalidate();
            }
        }

        //Set the default size of the control
        protected override Size DefaultSize
        {
            get
            {
                return new Size(90, 36);
            }
        }

        //Size of control
        public new Size Size
        {
            get { return controlSize; }
            set
            {
                if(value.Height == controlSize.Height && value.Width == controlSize.Width)
                {
                    return;
                }

                controlSize = value;

                Invalidate();
            }
        }

        //Keep track when the button is pressed
        private bool Pressed
        {
            get { return _pressed; }
            set
            {
                if(value == _pressed)
                {
                    return;
                }

                _pressed = value;

                Invalidate();
            }
        }

        //Keep track when the button is being hovered over by the mouse
        private bool Hovering
        {
            get { return _hovering; }
            set
            {
                if (value == _hovering)
                {
                    return;
                }

                _hovering = value;

                Invalidate();
            }
        }

        /// <summary>
        /// Set the size of the image inside the button.
        /// </summary>
        [Description("Set the size of the image inside the button"), Category("Appearance"),
        DefaultValue(24), Browsable(true)]
        public int ImageSize
        {
            get { return imageSize; }
            set
            {
                if(value == imageSize)
                {
                    return;
                }

                imageSize = value;

                Invalidate();
            }
        }

        //Override Mouse Move event
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Hovering = true;
        }

        //Override Mouse Leave event
        protected override void OnMouseLeave(EventArgs e)
        {
            Hovering = false;
        }

        //Override on Mouse Down event
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Pressed = true;
            }

            if (imageType == ImageForButton.Reload)
            {
                Debug.WriteLine("Ribbon Reload Button Pressed");
                if (fileName != null)
                {
                    Debug.WriteLine("Button image is not null");
                    //turn the Bitmap into a Graphics object
                }
            }

            //base.OnMouseDown(e);
        }

        //Override for Mouse up event
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Pressed = false;
            }
            //base.OnMouseUp(e);
        }

        //Override for OnPaint event
        protected override void OnPaint(PaintEventArgs pe)
        {
            int xPosition = 0;

            Graphics gfx = pe.Graphics;
            Rectangle rc = ClientRectangle;
            

            Color pressedColor;
            if (Pressed)
            {
                pressedColor = SystemColors.Highlight;
            }
            else if (Hovering)
            {
                pressedColor = SystemColors.ControlDark;
            }
            else
            {
                pressedColor = Parent.BackColor;
            }

            gfx.FillRectangle(new SolidBrush(pressedColor), rc);
            Font font = new Font("Verdana", (float)rc.Height * 0.25f, FontStyle.Bold,GraphicsUnit.Pixel);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Far;


            
            //gfx.DrawImage()
            //pb.ImageLocation = @"icons\closeBox.png";

            //Image image = Image.FromFile(@"icons\closeBox.png");

            //PictureBox pb = new PictureBox();

            //pb.ImageLocation = @"icons\closeBox.png";
            try
            {

                if(imageType == ImageForButton.Home)
                {
                    fileName = Properties.Resources.HomeIcon;
                    Text = "Home";
                }
                else if(imageType == ImageForButton.Search)
                {
                    fileName = Properties.Resources.SearchIcon;
                    Text = "Search";
                }else if(imageType == ImageForButton.MasterList)
                {
                    fileName = Properties.Resources.TapeDriveIcon;
                    Text = "Archive";
                }else if(imageType == ImageForButton.Deleted)
                {
                    fileName = Properties.Resources.TrashIcon;
                    Text = "Deleted";
                }
                else if (imageType == ImageForButton.ImportArchive)
                {
                    fileName = Properties.Resources.ImportArchiveIcon;
                    Text = "Import Archive";
                }
                else if (imageType == ImageForButton.ImportTapes)
                {
                    fileName = Properties.Resources.ImportTapesIcon;
                    Text = "Import Tapes";
                }
                else if (imageType == ImageForButton.ImportProjects)
                {
                    fileName = Properties.Resources.DBImportIcon;
                    Text = "Import Projects";
                }
                else if (imageType == ImageForButton.DatabaseBackup)
                {
                    fileName = Properties.Resources.DataBackupIcon;
                    Text = "DB Backup";
                }
                else if (imageType == ImageForButton.Preferences)
                {
                    fileName = Properties.Resources.PreferencesIcon;
                    Text = "Settings";
                }else if (imageType == ImageForButton.Reload)
                {
                    fileName = Properties.Resources.ReloadIcon;
                    Text = "Refresh";
                }
                else
                {
                    fileName = Properties.Resources.HomeIcon;
                    Text = "Home";
                }

                xPosition = ((rc.Width - ImageSize) /2);

                //Bitmap file = (Bitmap)Image.FromFile(@"icons\xout.png");
                //plusShort_myNumbers.png
                
                gfx.DrawImage(fileName, xPosition, 1, ImageSize, ImageSize);
                
                //(file, 10, 10, rc, GraphicsUnit.Pixel);
                gfx.DrawString(Text, font, new SolidBrush(Color.Black), new RectangleF((float)rc.Left, (float)rc.Top, (float)rc.Width, (float)rc.Height), sf);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            

            base.OnPaint(pe);

        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }
    }
}
