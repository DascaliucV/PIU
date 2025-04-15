using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class Form2 : Form
    {
        private TextBox txtModel;
        private Button btnCauta;
        private ListBox listRezultate;
        private const string FISIER = "Telefoane.txt";

        public Form2()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "Căutare Telefon";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lbl = new Label()
            {
                Text = "Model telefon:",
                Left = 30,
                Top = 30,
                Width = 100
            };
            this.Controls.Add(lbl);

            txtModel = new TextBox()
            {
                Left = 140,
                Top = 25,
                Width = 200
            };
            this.Controls.Add(txtModel);

            btnCauta = new Button()
            {
                Text = "Caută",
                Left = 360,
                Top = 24,
                Width = 80
            };
            btnCauta.Click += BtnCauta_Click;
            this.Controls.Add(btnCauta);

            listRezultate = new ListBox()
            {
                Left = 30,
                Top = 70,
                Width = 410,
                Height = 260
            };
            this.Controls.Add(listRezultate);
        }

        private void BtnCauta_Click(object sender, EventArgs e)
        {
            listRezultate.Items.Clear();
            string modelCautat = txtModel.Text.Trim().ToLower();

            if (File.Exists(FISIER))
            {
                var linii = File.ReadAllLines(FISIER);

                var rezultate = linii
                    .Where(l => l.ToLower().Contains(modelCautat))
                    .ToList();

                if (rezultate.Count == 0)
                {
                    listRezultate.Items.Add("Niciun telefon găsit.");
                }
                else
                {
                    foreach (var linie in rezultate)
                    {
                        var split = linie.Split(',');
                        if (split.Length >= 4)
                            listRezultate.Items.Add($"{split[0]} - {split[1]} lei - {split[2]} - {split[3]} GB");
                    }
                }
            }
        }
    }
}
