﻿// Form1.cs complet pentru tema cerută
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class Form1 : Form
    {
        private TextBox txtModel, txtPret, txtCuloare, txtMemorie;
        private Label lblModel, lblPret, lblCuloare, lblMemorie;
        private Label errModel, errPret, errCuloare, errMemorie;
        private Button btnAdauga, btnRefresh;
        private const int MAX_CARACTERE = 15;
        private const int MAX_MEMORIE = 1024;
        private const int MIN_MEMORIE = 1;
        private const string FISIER = "Telefoane.txt";

        private Label[,] tabel;

        public Form1()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "Magazin Telefoane";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            string[] etichete = { "Model", "Preț", "Culoare", "Memorie" };
            TextBox[] textboxes = new TextBox[4];
            Label[] lbls = new Label[4];
            Label[] errs = new Label[4];

            for (int i = 0; i < etichete.Length; i++)
            {
                lbls[i] = new Label()
                {
                    Text = etichete[i] + ":",
                    Left = 50,
                    Top = 20 + i * 40,
                    Width = 100
                };
                this.Controls.Add(lbls[i]);

                textboxes[i] = new TextBox()
                {
                    Left = 160,
                    Top = 20 + i * 40,
                    Width = 150
                };
                this.Controls.Add(textboxes[i]);

                errs[i] = new Label()
                {
                    ForeColor = Color.Red,
                    Left = 320,
                    Top = 20 + i * 40,
                    Width = 300,
                    Text = ""
                };
                this.Controls.Add(errs[i]);
            }

            txtModel = textboxes[0]; txtPret = textboxes[1];
            txtCuloare = textboxes[2]; txtMemorie = textboxes[3];
            lblModel = lbls[0]; lblPret = lbls[1];
            lblCuloare = lbls[2]; lblMemorie = lbls[3];
            errModel = errs[0]; errPret = errs[1];
            errCuloare = errs[2]; errMemorie = errs[3];

            btnAdauga = new Button()
            {
                Text = "Adaugă",
                Top = 190,
                Left = 50,
                Width = 100
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnRefresh = new Button()
            {
                Text = "Refresh",
                Top = 190,
                Left = 160,
                Width = 100
            };
            btnRefresh.Click += (s, e) => AfiseazaTabel();
            this.Controls.Add(btnRefresh);

            AfiseazaTabel();
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            bool valid = true;

            ResetEtichete();

            if (string.IsNullOrWhiteSpace(txtModel.Text) || txtModel.Text.Length > MAX_CARACTERE)
            {
                lblModel.ForeColor = Color.Red;
                errModel.Text = $"Obligatoriu (max {MAX_CARACTERE} caractere)";
                valid = false;
            }
            if (!int.TryParse(txtPret.Text, out int pret) || pret <= 0)
            {
                lblPret.ForeColor = Color.Red;
                errPret.Text = "Trebuie să fie un număr pozitiv";
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtCuloare.Text) || txtCuloare.Text.Length > MAX_CARACTERE)
            {
                lblCuloare.ForeColor = Color.Red;
                errCuloare.Text = $"Obligatoriu (max {MAX_CARACTERE} caractere)";
                valid = false;
            }
            if (!int.TryParse(txtMemorie.Text, out int memorie) || memorie < MIN_MEMORIE || memorie > MAX_MEMORIE)
            {
                lblMemorie.ForeColor = Color.Red;
                errMemorie.Text = $"Valoare între {MIN_MEMORIE} și {MAX_MEMORIE}";
                valid = false;
            }

            if (!valid) return;

            File.AppendAllText(FISIER, $"{txtModel.Text},{pret},{txtCuloare.Text},{memorie}\n");
            AfiseazaTabel();
            ClearTextBoxes();
        }

        private void ResetEtichete()
        {
            lblModel.ForeColor = lblPret.ForeColor = lblCuloare.ForeColor = lblMemorie.ForeColor = Color.Black;
            errModel.Text = errPret.Text = errCuloare.Text = errMemorie.Text = "";
        }

        private void ClearTextBoxes()
        {
            txtModel.Text = txtPret.Text = txtCuloare.Text = txtMemorie.Text = "";
        }

        private void AfiseazaTabel()
        {
            // Șterge vechile label-uri
            if (tabel != null)
                foreach (var lbl in tabel) this.Controls.Remove(lbl);

            if (!File.Exists(FISIER)) return;

            var linii = File.ReadAllLines(FISIER);
            tabel = new Label[linii.Length + 1, 4];
            string[] antet = { "Model", "Preț", "Culoare", "Memorie" };

            for (int i = 0; i < 4; i++)
            {
                tabel[0, i] = new Label()
                {
                    Text = antet[i],
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    Width = 150,
                    Height = 30,
                    Left = 50 + i * 160,
                    Top = 250,
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle
                };
                this.Controls.Add(tabel[0, i]);
            }

            for (int i = 0; i < linii.Length; i++)
            {
                var date = linii[i].Split(',');
                if (date.Length < 4) continue;

                for (int j = 0; j < 4; j++)
                {
                    tabel[i + 1, j] = new Label()
                    {
                        Text = j == 1 ? date[j].Trim() + " lei" : j == 3 ? date[j].Trim() + " GB" : date[j].Trim(),
                        Width = 150,
                        Height = 30,
                        Left = 50 + j * 160,
                        Top = 280 + i * 32,
                        BackColor = i % 2 == 0 ? Color.White : Color.Lavender,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    this.Controls.Add(tabel[i + 1, j]);
                }
            }
        }
    }
}
