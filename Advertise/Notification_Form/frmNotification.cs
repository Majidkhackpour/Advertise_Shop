using System;
using System.Drawing;
using System.Windows.Forms;
using PacketParser.Services;

namespace Advertise.Notification_Form
{
    public partial class frmNotification : Form
    {
        private int x = 1;
        public frmNotification()
        {
            InitializeComponent();
        }
        private class NestedPublicInfo
        {
            internal static frmNotification PublicInfo = new frmNotification();

            public NestedPublicInfo()
            {
            }
        }
        public static frmNotification PublicInfo => NestedPublicInfo.PublicInfo;
        public void ShowMessage(string message)
        {
            var a = new frmNotification {lblText = {Text = message}};
            a.Show();
        }
        private void ClosingTimer_Tick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void frmNotification_Load(object sender, System.EventArgs e)
        {
            Styler.Start();
            ClosingTimer.Start();
        }

        private void Styler_Tick(object sender, System.EventArgs e)
        {
            try
            {
                x += 20;
                var workingArea = Screen.GetWorkingArea(this);
                Location = new Point(workingArea.Right - x, workingArea.Bottom - Size.Height - 30);
                if (Location.X == workingArea.Right - Size.Width
                    || Location.X < workingArea.Right - Size.Width)
                    Styler.Stop();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void lblText_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
