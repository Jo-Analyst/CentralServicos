using DataBase;
using Interface.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Interface
{
    public partial class FrmCustomerService : Form
    {

        int userId, page = 1, pageMaximum = 1, serviceId;

        public FrmCustomerService(int userId, string name)
        {
            InitializeComponent();
            this.userId = userId;
            lblName.Text = name;
        }

        private void FrmCustomerService_Load(object sender, EventArgs e)
        {
            dgvHistory.Focus();
            cbPage.Text = "1";
            cbRows.Text = "5";
            loadEvents();
            this.cbRows.SelectedIndexChanged += cbRows_SelectedIndexChanged;
            this.cbPage.SelectedIndexChanged += new System.EventHandler(this.cbPage_SelectedIndexChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = !string.IsNullOrWhiteSpace(rtDescription.Text);

            if (!isValid)
            {
                MessageBox.Show("Descreva qual atendimento foi realizado.", "Serviço Central", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cbEntryAndExit.Checked)
            {
                if (dtTimeOfService.Value > dtDepartureTime.Value)
                {
                    MessageBox.Show("A hora de saída não pode ser menor que a hora do atendimento", "Serviço Central", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (dtTimeOfService.Value == dtDepartureTime.Value)
                {
                    MessageBox.Show("A hora de saída não pode ser igual a hora do atendimento", "Serviço Central", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Service service = new Service();
            try
            {
                service.id = serviceId;
                service.description = rtDescription.Text.Trim();
                service.dateService = dtDateService.Value;
                service.timeOfService = cbEntryAndExit.Checked ? dtTimeOfService.Value.ToString("HH:mm:ss") : "---";
                service.departureTime = cbEntryAndExit.Checked ? dtDepartureTime.Value.ToString("HH:mm:ss") : "---";
                service.userId = userId;

                service.Save();

                loadEvents();

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void loadDgvHistory()
        {
            try
            {
                dgvHistory.Rows.Clear();

                int quantRows = int.Parse(cbRows.Text);
                int pageSelected = (page - 1) * quantRows;

                DataTable services = Service.FindByUserId(userId, pageSelected, quantRows);

                foreach (DataRow dr in services.Rows)
                {
                    int index = dgvHistory.Rows.Add();
                    dgvHistory.Rows[index].Cells["ColEdit"].Value = Properties.Resources.edit;
                    dgvHistory.Rows[index].Cells["ColDelete"].Value = Properties.Resources.delete;
                    dgvHistory.Rows[index].Cells["ColId"].Value = dr["id"].ToString();
                    dgvHistory.Rows[index].Cells["ColDescription"].Value = dr["description"].ToString();
                    dgvHistory.Rows[index].Cells["ColDateService"].Value = dr["date_service"].ToString();
                    dgvHistory.Rows[index].Cells["ColTimeOfService"].Value = dr["time_of_service"].ToString();
                    dgvHistory.Rows[index].Cells["ColDepartureTime"].Value = dr["departure_time"].ToString();
                    dgvHistory.Rows[index].Height = 45;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro no sistema. Tente novamente mais tarde", "Serviço Central", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            dgvHistory.CurrentRow.Selected = false;

            int id = Convert.ToInt32(dgvHistory.CurrentRow.Cells[2].Value);
            if (dgvHistory.CurrentCell.ColumnIndex == 0)
            {
                ClearFields();
                serviceId = int.Parse(dgvHistory.CurrentRow.Cells["ColId"].Value.ToString());
                rtDescription.Text = dgvHistory.CurrentRow.Cells["ColDescription"].Value.ToString();
                dtDateService.Value = DateTime.Parse(dgvHistory.CurrentRow.Cells["ColDateService"].Value.ToString());
                dtTimeOfService.Value = dgvHistory.CurrentRow.Cells["ColTimeOfService"].Value.ToString() != "---" ? DateTime.Parse(dgvHistory.CurrentRow.Cells["ColTimeOfService"].Value.ToString()) : DateTime.Now;
                dtDepartureTime.Value = dgvHistory.CurrentRow.Cells["ColDepartureTime"].Value.ToString() != "---" ? DateTime.Parse(dgvHistory.CurrentRow.Cells["ColDepartureTime"].Value.ToString()) : DateTime.Now;
                btnSave.Text = "Editar";
                lkCancel.Visible = true;

                if (dgvHistory.CurrentRow.Cells["ColDepartureTime"].Value.ToString() == "---")
                {
                    cbEntryAndExit.Checked = false;
                    cbEntryAndExit_CheckedChanged(sender, e);
                }

                else 
                    cbEntryAndExit.Checked = true;
            }


            if (dgvHistory.CurrentCell.ColumnIndex == 1)
            {
                DialogResult dr = MessageBox.Show($"Deseja mesmo excluir este atendimento?", "Serviço Central", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        Service.Delete(id);
                        ClearFields();
                        loadEvents();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Houve um erro no sistema. Tente novamente", "Notificação de aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cbRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadEvents();
            if (page == pageMaximum)
            {
                DisabledBtnArrowLeft();
                DisabledBtnArrowRight();
            }
        }

        private void cbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = int.Parse(cbPage.Text);
            if (pageMaximum == 1) return;

            loadDgvHistory();

            if (page == 1)
            {
                DisabledBtnArrowLeft();
                EnabledBtnArrowRight();
            }
            else if (page == pageMaximum)
            {
                DisabledBtnArrowRight();
                EnabledBtnArrowLeft();
            }

            else
            {
                EnabledBtnArrowLeft();
                EnabledBtnArrowRight();
            }
        }

        private void loadEvents()
        {
            CheckNumberOfPages(int.Parse(cbRows.SelectedItem.ToString()));
            UpdateComboBoxItems();
            loadDgvHistory();

        }

        private void UpdateComboBoxItems()
        {
            cbPage.Items.Clear();
            for (int i = 1; i <= pageMaximum; i++)
            {
                cbPage.Items.Add(i);
            }

            cbPage.Text = (page > pageMaximum ? pageMaximum : page).ToString();
        }

        private void CheckNumberOfPages(int numberRows)
        {
            PageData.quantityRowsSelected = numberRows;
            pageMaximum = PageData.SetPageQuantityServices(userId);
            if (pageMaximum > 1)
                EnabledBtnArrowRight();

        }

        private void btnArrowRight_Click(object sender, EventArgs e)
        {
            if (page < pageMaximum)
            {
                page++;
            }

            cbPage.Text = page.ToString();

            if (page == pageMaximum)
            {

                DisabledBtnArrowRight();

            }

            else
            {
                btnArrowLeft.Enabled = true;
                btnArrowLeft.Image = Properties.Resources.left_arrow_white;

            }

            EnabledBtnArrowLeft();
            dgvHistory.Focus();
            loadDgvHistory();
        }

        private void EnabledBtnArrowLeft()
        {
            btnArrowLeft.Enabled = true;
            btnArrowLeft.Image = Properties.Resources.left_arrow_white;
        }

        private void DisabledBtnArrowRight()
        {
            btnArrowRight.Enabled = false;
            btnArrowRight.Image = Properties.Resources.right_arrow_grey;
        }

        private void btnArrowLeft_Click(object sender, EventArgs e)
        {
            if (page > 1)
            {
                page--;
            }

            cbPage.Text = page.ToString();

            if (page == 1)
            {
                DisabledBtnArrowLeft();
                EnabledBtnArrowRight();
            }
            else
            {
                EnabledBtnArrowLeft();
            }

            dgvHistory.Focus();
            loadDgvHistory();
        }

        private void ClearFields()
        {
            serviceId = 0;
            btnSave.Text = "Salvar";
            rtDescription.Clear();
            dtDateService.Value = DateTime.Now;
            dtTimeOfService.Value = DateTime.Now;
            dtDepartureTime.Value = DateTime.Now;
            lkCancel.Visible = false;
        }

        private void FrmCustomerService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.Right && btnArrowRight.Enabled) btnArrowRight_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.Left && btnArrowLeft.Enabled) btnArrowLeft_Click(sender, e);
        }

        private void cbEntryAndExit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEntryAndExit.Checked)
            {
                dtDepartureTime.Enabled = true;
                dtTimeOfService.Enabled = true;
            }
            else
            {
                dtDepartureTime.Enabled = false;
                dtTimeOfService.Enabled = false;
            }
        }

        private void lkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearFields();
        }

        private void DisabledBtnArrowLeft()
        {
            btnArrowLeft.Enabled = false;
            btnArrowLeft.Image = Properties.Resources.left_arrow_grey;
        }

        private void dgvHistory_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvHistory.Cursor = e.ColumnIndex == 0 || e.ColumnIndex == 1 ? Cursors.Hand : Cursors.Arrow;
        }

        private void EnabledBtnArrowRight()
        {
            btnArrowRight.Enabled = true;
            btnArrowRight.Image = Properties.Resources.right_arrow_white;
        }
    }
}