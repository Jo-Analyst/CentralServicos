using System.Windows.Forms;

namespace Interface
{
    public partial class FrmCentralService : Form
    {
        public FrmCentralService()
        {
            InitializeComponent();
        }

        private void usuárioToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FrmUsers frmUsers = new FrmUsers();
            frmUsers.ShowDialog();
        }

        private void FrmCentralService_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void atendimentoToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            new ServicesPerformed().ShowDialog();
        }
    }
}
