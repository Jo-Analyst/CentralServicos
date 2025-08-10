﻿using DataBase;
using System;
using System.Windows.Forms;

namespace Interface
{
    public partial class FrmSaveUser : Form
    {

        public bool isSaved { get; set; }
        int userId;
        public FrmSaveUser()
        {
            InitializeComponent();
        }

        public FrmSaveUser(int id, string name, string cpf, string phone, string address)
        {
            InitializeComponent();
            userId = id;
            txtName.Text = name;
            mkCPF.Text = cpf;
            mkPhone.Text = phone;
            txtAddress.Text = address;
            btnSave.Focus();
            this.btnSave.TabStop = true;

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Insira o nome do Usuário", "CENTRAL DE ATENDIMENTOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (mkCPF.MaskCompleted)
                {
                    if (!CPF.Validate(mkCPF.Text))
                    {
                        MessageBox.Show("CPF inválido", "CENTRAL DE ATENDIMENTOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    else if (User.FindByCpfForUser(Security.Cry(mkCPF.Text), userId).Rows.Count > 0)
                    {
                        MessageBox.Show("Este CPF já está cadastrado", "CENTRAL DE ATENDIMENTOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }


                new User()
                {
                    id = userId,
                    name = txtName.Text.Trim(),
                    CPF = mkCPF.MaskCompleted ? Security.Cry(mkCPF.Text) : "",
                    address = txtAddress.Text.Trim(),
                    phone = mkPhone.MaskCompleted ? mkPhone.Text : "",
                }.Save();

                isSaved = true;

                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro no sistema ao cadastrar o usuário", "Notificação de aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSaveUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(sender, e);
        }
    }
}
