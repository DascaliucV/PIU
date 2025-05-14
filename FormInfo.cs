using System;
using System.Drawing;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "Optiuni Disponibile";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);

            AddFormButton("Inapoi la Start", typeof(FormStart), 50);
            AddFormButton("Adaugare telefon", typeof(FormAdaugare), 100);
            AddFormButton("Cautare telefon", typeof(FormCautare), 150);
        }

        private void AddFormButton(string text, Type formType, int top)
        {
            Button btn = new Button()
            {
                Text = text,
                Width = 200,
                Height = 40,
                Left = 100,
                Top = top,
                BackColor = Color.FromArgb(28, 151, 234),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 170, 250);
            btn.Click += (sender, e) =>
            {
                this.Close();

                var existingForm = Application.OpenForms[formType.Name] as Form;
                if (existingForm != null)
                {
                    existingForm.BringToFront();
                }
                else
                {
                    Form form = (Form)Activator.CreateInstance(formType);
                    form.Show();
                }
            };
            this.Controls.Add(btn);
        }

        private void metroUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
