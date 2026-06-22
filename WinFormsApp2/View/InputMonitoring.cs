using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class InputMonitoring : Form
    {
        private WinFormsApp2.UserController.MonitoringController controller;

        public InputMonitoring()
        {
            InitializeComponent();
            controller = new WinFormsApp2.UserController.MonitoringController(this);
            Load += InputMonitoring_Load;
        }

        private void InputMonitoring_Load(object sender, EventArgs e)
        {
            controller.TampilDataInput();

            try { controller.LoadDropdowns(); } catch {  }

            dataGridView1.CellClick += DataGridView1_CellClick;

            // Set placeholder untuk ID Monitoring agar user tahu harus input manual
            if (textBox1 != null)
            {
                textBox1.PlaceholderText = "ID Monitoring (wajib diisi)";
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate ID
                if (row.Cells["ID"] != null)
                {
                    string id = row.Cells["ID"].Value?.ToString() ?? "";
                    if (textBox1 != null)
                        textBox1.Text = id;
                }

                // Populate Tanaman
                if (row.Cells.Contains(row.Cells["TANAMAN"]))
                {
                    string tanaman = row.Cells["TANAMAN"].Value?.ToString() ?? "";
                    if (comboBoxTanaman != null)
                        comboBoxTanaman.Text = tanaman;
                }

                // Populate Kondisi
                if (row.Cells["KONDISI"] != null)
                {
                    string kondisi = row.Cells["KONDISI"].Value?.ToString() ?? "";
                    if (comboBoxPetugas != null)
                        comboBoxPetugas.Text = kondisi;
                }

                // Populate Hama
                if (row.Cells.Contains(row.Cells["HAMA"]))
                {
                    string hama = row.Cells["HAMA"].Value?.ToString() ?? "";
                    if (comboBox1 != null)
                        comboBox1.Text = hama;
                }

                // Populate Cuaca
                if (row.Cells["CUACA"] != null)
                {
                    string cuaca = row.Cells["CUACA"].Value?.ToString() ?? "";
                    if (comboBox2 != null)
                        comboBox2.Text = cuaca;
                }

                // Populate Tanggal
                if (row.Cells["TANGGAL"] != null)
                {
                    var val = row.Cells["TANGGAL"].Value;
                    if (val != null && val != DBNull.Value)
                    {
                        if (val is DateTime dt)
                        {
                            dateTimePicker1.Value = dt;
                        }
                        else
                        {
                            DateTime parsed;
                            if (DateTime.TryParse(val.ToString(), out parsed))
                                dateTimePicker1.Value = parsed;
                        }
                    }
                }

                btnEdit.Enabled = true;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            controller.SimpanFromInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            controller.EditFromInput();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            textBox1?.Clear();
            textBox2?.Clear();
            comboBoxTanaman?.SelectedIndex = -1;
            comboBoxPetugas?.SelectedIndex = -1;
            comboBox1?.SelectedIndex = -1;
            comboBox2?.SelectedIndex = -1;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
        }

        private void comboBoxPetugas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            controller.HapusFromInput();
        }
    }
}
