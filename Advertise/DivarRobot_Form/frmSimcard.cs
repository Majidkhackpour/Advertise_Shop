using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advertise.Notification_Form;
using EntityCache.Bussines;
using PacketParser.Services;

namespace Advertise.DivarRobot_Form
{
    public partial class frmSimcard : Form
    {
        private SimcardBussines cls;

        private async Task LoadCombo()
        {
            try
            {
                var advList = await DivarCategoryBussines.GetAllAsync(Guid.Empty);
                advList = advList.OrderBy(q => q.Name).ToList();
                Adv1BindingSource.DataSource = advList;
                Chat1BindingSource.DataSource = advList;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async Task SetData()
        {
            try
            {
                await LoadCombo();
                txtName.Text = cls?.OwnerName;
                txtNumber.Text = cls?.Number.ToString();
                cmbAdv1.SelectedValue = cls?.AdvCat1 ?? Guid.Empty;
                cmbAdv2.SelectedValue = cls?.AdvCat2 ?? Guid.Empty;
                cmbAdv3.SelectedValue = cls?.AdvCat3 ?? Guid.Empty;
                cmbChat1.SelectedValue = cls?.ChatCat1 ?? Guid.Empty;
                cmbChat2.SelectedValue = cls?.ChatCat2 ?? Guid.Empty;
                cmbChat3.SelectedValue = cls?.ChatCat3 ?? Guid.Empty;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        public frmSimcard()
        {
            InitializeComponent();
            cls = new SimcardBussines();
        }
        public frmSimcard(Guid guid, bool isShowMode)
        {
            InitializeComponent();
            cls = SimcardBussines.Get(guid);
            grpAccount.Enabled = !isShowMode;
            btnFinish.Enabled = !isShowMode;
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txtName);
        }

        private void txtNumber_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txtNumber);
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txtNumber);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txtName);
        }

        private async void frmSimcard_Load(object sender, EventArgs e)
        {
            await SetData();
        }

        private async void cmbAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAdv1.SelectedValue == null) return;
                var advList = await DivarCategoryBussines.GetAllAsync((Guid)cmbAdv1.SelectedValue);
                advList = advList.OrderBy(q => q.Name).ToList();
                Adv2BindingSource.DataSource = advList;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void cmbAdv2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAdv2.SelectedValue == null) return;
                var advList = await DivarCategoryBussines.GetAllAsync((Guid)cmbAdv2.SelectedValue);
                advList = advList.OrderBy(q => q.Name).ToList();
                Adv3BindingSource.DataSource = advList;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void cmbChat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbChat1.SelectedValue == null) return;
                var advList = await DivarCategoryBussines.GetAllAsync((Guid)cmbChat1.SelectedValue);
                advList = advList.OrderBy(q => q.Name).ToList();
                Chat2BindingSource.DataSource = advList;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void cmbChat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbChat2.SelectedValue == null) return;
                var advList = await DivarCategoryBussines.GetAllAsync((Guid)cmbChat2.SelectedValue);
                advList = advList.OrderBy(q => q.Name).ToList();
                Chat3BindingSource.DataSource = advList;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmSimcard_KeyDown(object sender, KeyEventArgs e)
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

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (cls.Guid == Guid.Empty)
                    cls.Guid = Guid.NewGuid();

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    frmNotification.PublicInfo.ShowMessage("مالک نمی تواند خالی باشد");
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNumber.Text))
                {
                    frmNotification.PublicInfo.ShowMessage("شماره نمی تواند خالی باشد");
                    txtNumber.Focus();
                    return;
                }
                if (!await SimcardBussines.CheckNumber(cls.Guid, txtNumber.Text.ParseToLong()))
                {
                    frmNotification.PublicInfo.ShowMessage("شماره وارد شده تکراری است");
                    txtName.Focus();
                    return;
                }

                cls.OwnerName = txtName.Text.Trim();
                cls.Number = txtNumber.Text.ParseToLong();
                cls.AdvCat1 = (Guid?)cmbAdv1.SelectedValue;
                cls.AdvCat2 = (Guid?)cmbAdv2.SelectedValue;
                cls.AdvCat3 = (Guid?)cmbAdv3.SelectedValue;
                cls.ChatCat1 = (Guid?)cmbChat1.SelectedValue;
                cls.ChatCat2 = (Guid?)cmbChat2.SelectedValue;
                cls.ChatCat3 = (Guid?)cmbChat3.SelectedValue;
                var res = await cls.SaveAsync();
                if (res.HasError)
                {
                    frmNotification.PublicInfo.ShowMessage(res.ErrorMessage);
                    return;
                }
                DialogResult = DialogResult.OK;
                frmLoading.PublicInfo.ShowForm();
                Close();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
