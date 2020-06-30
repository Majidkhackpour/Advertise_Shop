using System;
using System.Windows.Forms;
using Advertise.Notification_Form;
using PacketParser.Services;

namespace Advertise.Email_Form
{
    public partial class frmSendEmail : Form
    {
        public frmSendEmail(string reciever)
        {
            InitializeComponent();
            txtReciever.Text = reciever;
        }

        private void txtReciever_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txtReciever);
        }

        private void txtSubject_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txtSubject);
        }

        private void txtDesc_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txtDesc);
        }

        private void txtDesc_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txtDesc);
        }

        private void txtSubject_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txtSubject);
        }

        private void txtReciever_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txtReciever);
        }

        private void frmSendEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!btnFinish.Focused && !btnCancel.Focused)
                            SendKeys.Send("{Tab}");
                        break;
                    case Keys.F5:
                        btnFinish.PerformClick();
                        break;
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReciever.Text))
                {
                    frmNotification.PublicInfo.ShowMessage("ایمیل گیرنده نمی تواند خالی باشد");
                    txtReciever.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSubject.Text))
                {
                    frmNotification.PublicInfo.ShowMessage("موضوع ایمیل نمی تواند خالی باشد");
                    txtSubject.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDesc.Text))
                {
                    frmNotification.PublicInfo.ShowMessage("متن ایمیل نمی تواند خالی باشد");
                    txtDesc.Focus();
                    return;
                }


                SendEmail.Send(txtReciever.Text, txtSubject.Text, txtDesc.Text);

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}
