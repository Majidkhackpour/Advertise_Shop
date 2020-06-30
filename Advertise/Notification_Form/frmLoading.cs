using System.Windows.Forms;

namespace Advertise.Notification_Form
{
    public partial class frmLoading : Form
    {
        private class NestedPublicInfo
        {
            internal static frmLoading PublicInfo = new frmLoading();

            public NestedPublicInfo()
            {
            }
        }
        public static frmLoading PublicInfo => NestedPublicInfo.PublicInfo;

        public void ShowForm()
        {
            var a = new frmLoading();
            a.ShowDialog();
        }
        public frmLoading()
        {
            InitializeComponent();
        }

        private void frmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
