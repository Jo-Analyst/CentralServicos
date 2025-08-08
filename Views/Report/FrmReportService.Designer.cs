using CentralServicos.Views.Report.Data;
using CentralServicos.Views.Report.Data.DsServiceTableAdapters;

namespace Interface
{
    partial class FrmReportService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportService));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dtServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsService = new CentralServicos.Views.Report.Data.DsService();
            this.dtServiceTableAdapter = new CentralServicos.Views.Report.Data.DsServiceTableAdapters.DtServiceTableAdapter();
            this.dtQuantityServicesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtQuantityServicesTableAdapter = new CentralServicos.Views.Report.Data.DsServiceTableAdapters.dtQuantityServicesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dtServiceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtQuantityServicesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsService";
            reportDataSource1.Value = this.dtServiceBindingSource;
            reportDataSource2.Name = "dtQuantityServices";
            reportDataSource2.Value = this.dtQuantityServicesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CentralServicos.Views.Report.Relatório do Atendimento Diário.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // dtServiceBindingSource
            // 
            this.dtServiceBindingSource.DataMember = "DtService";
            this.dtServiceBindingSource.DataSource = this.dsService;
            // 
            // dsService
            // 
            this.dsService.DataSetName = "DsService";
            this.dsService.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtServiceTableAdapter
            // 
            this.dtServiceTableAdapter.ClearBeforeFill = true;
            // 
            // dtQuantityServicesBindingSource
            // 
            this.dtQuantityServicesBindingSource.DataMember = "dtQuantityServices";
            this.dtQuantityServicesBindingSource.DataSource = this.dsService;
            // 
            // dtQuantityServicesTableAdapter
            // 
            this.dtQuantityServicesTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReportService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReportService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReportService";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReportService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtServiceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtQuantityServicesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dtServiceBindingSource;
        private DsService dsService;
        private DtServiceTableAdapter dtServiceTableAdapter;
        private System.Windows.Forms.BindingSource dtQuantityServicesBindingSource;
        private dtQuantityServicesTableAdapter dtQuantityServicesTableAdapter;
    }
}