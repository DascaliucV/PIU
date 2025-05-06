using System;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class Form3 : Form
    {
        private RadioButton rbtnAdaugare;
        private RadioButton rbtnCautare;

        public Form3()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "Selectează Acțiunea";
            this.Size = new System.Drawing.Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            rbtnAdaugare = new RadioButton()
            {
                Text = "Adăugare",
                Left = 50,
                Top = 40,
                AutoSize = true
            };
            rbtnAdaugare.CheckedChanged += RbtnAdaugare_CheckedChanged;
            this.Controls.Add(rbtnAdaugare);

            rbtnCautare = new RadioButton()
            {
                Text = "Căutare",
                Left = 50,
                Top = 80,
                AutoSize = true
            };
            rbtnCautare.CheckedChanged += RbtnCautare_CheckedChanged;
            this.Controls.Add(rbtnCautare);
        }

        private void RbtnAdaugare_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAdaugare.Checked)
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }
        }

        private void RbtnCautare_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCautare.Checked)
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
        }
    }
}
