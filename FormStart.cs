using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class FormStart : Form
    {
        private Label lblOra;
        private Label lblData;
        private Timer timer1;

        public FormStart()
        {
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "Magazin de Telefoane - Start";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);


            lblOra = new Label()
            {
                Text = "Ora:     ",
                Left = 20,
                Top = 20,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Regular)
            };
            this.Controls.Add(lblOra);

            lblData = new Label()
            {
                Text = "Data:     ",
                Left = 20,
                Top = 50,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Regular)
            };
            this.Controls.Add(lblData);

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();

            Button btnPlay = new Button()
            {
                Text = "▶ Intra",
                Width = 150,
                Height = 40,
                Left = 125,
                Top = 150,
                BackColor = Color.FromArgb(28, 151, 234),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 170, 250);
            btnPlay.Click += BtnPlay_Click;
            this.Controls.Add(btnPlay);

            this.Load += FormStart_Load;

            this.FormClosed += (s, e) => timer1.Dispose();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblOra.Text = "Ora: " + DateTime.Now.ToLongTimeString();
            lblData.Text = "Data: " + DateTime.Now.ToLongDateString();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo = new FormInfo();
                formInfo.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la deschiderea formularului: {ex.Message}");
            }
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            lblOra.Text = "Ora: " + DateTime.Now.ToLongTimeString();
            lblData.Text = "Data: " + DateTime.Now.ToLongDateString();
        }
    }
}