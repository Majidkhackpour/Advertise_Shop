using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advertise.Notification_Form;
using EntityCache.Bussines;
using PacketParser.Services;
using Shop_Windows.Classes;

namespace Advertise.DivarRobot_Form
{
    public partial class frmShowSimcard : Form
    {
        private async Task LoadData(string search = null)
        {
            try
            {
                var liat = await SimcardBussines.GetAllAsync(search);
                SimcardBindingSource.DataSource = liat.OrderBy(q => q.OwnerName).ToSortableBindingList();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmShowSimcard()
        {
            InitializeComponent();
            cmSimcard.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }

        private async void frmShowSimcard_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void mnuInsAdv_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmSimcard();
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void mnuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var frm = new frmSimcard(guid, false);
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                if (MessageBox.Show($@"آیا از حذف شماره {DGrid[dgNumber.Index, DGrid.CurrentRow.Index].Value} اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No) return;
                var sim = await SimcardBussines.GetAsync(guid);
                var res = await sim.RemoveAsync();
                if (res.HasError)
                {
                    frmNotification.PublicInfo.ShowMessage(res.ErrorMessage);
                    return;
                }
                frmLoading.PublicInfo.ShowForm();
                await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void mnuView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var frm = new frmSimcard(guid, true);
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void DGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGrid.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }

        private async void mnuAdv_Click(object sender, EventArgs e)
        {
            if (DGrid.RowCount <= 0) return;
            if (DGrid.CurrentRow == null) return;
            var number = (long)DGrid[dgNumber.Index, DGrid.CurrentRow.Index].Value;
            var div = await DivarAdv.GetInstance();
            if (await div.Login(number))
                await LoadData();
        }

        private async void mnuChat_Click(object sender, EventArgs e)
        {
            if (DGrid.RowCount <= 0) return;
            if (DGrid.CurrentRow == null) return;
            var number = (long)DGrid[dgNumber.Index, DGrid.CurrentRow.Index].Value;
            var div = await DivarAdv.GetInstance();
            if (await div.LoginChat(number,true))
                await LoadData();
        }
    }
}
