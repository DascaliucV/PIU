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
        private CheckBox chkDualSim, chk5G;
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
            this.Size = new Size(850, 700);
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

            chkDualSim = new CheckBox()
            {
                Text = "Dual SIM",
                Left = 50,
                Top = 200,
                Width = 100
            };
            this.Controls.Add(chkDualSim);

            chk5G = new CheckBox()
            {
                Text = "Suport 5G",
                Left = 160,
                Top = 200,
                Width = 100
            };
            this.Controls.Add(chk5G);

            btnAdauga = new Button()
            {
                Text = "Salvează",
                Top = 270,
                Left = 50,
                Width = 100
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnRefresh = new Button()
            {
                Text = "Refresh",
                Top = 270,
                Left = 160,
                Width = 100
            };
            btnRefresh.Click += (s, e) => AfiseazaTabel();
            this.Controls.Add(btnRefresh);

            // Butonul de Căutare a fost eliminat de aici.

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

            string extra = "";
            if (chkDualSim.Checked) extra += ",DualSIM";
            if (chk5G.Checked) extra += ",5G";

            File.AppendAllText(FISIER, $"{txtModel.Text},{pret},{txtCuloare.Text},{memorie}{extra}\n");
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
            chkDualSim.Checked = false;
            chk5G.Checked = false;
        }

        private void AfiseazaTabel()
        {
            if (tabel != null)
                foreach (var lbl in tabel) this.Controls.Remove(lbl);

            if (!File.Exists(FISIER)) return;

            var linii = File.ReadAllLines(FISIER);
            tabel = new Label[linii.Length + 1, 6];
            string[] antet = { "Model", "Preț", "Culoare", "Memorie", "Dual SIM", "5G" };

            for (int i = 0; i < antet.Length; i++)
            {
                tabel[0, i] = new Label()
                {
                    Text = antet[i],
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    Width = 110,
                    Height = 30,
                    Left = 50 + i * 115,
                    Top = 320,
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

                string dualSim = linii[i].Contains("DualSIM") ? "DA" : "NU";
                string g5 = linii[i].Contains("5G") ? "DA" : "NU";

                for (int j = 0; j < 6; j++)
                {
                    string val = "";
                    switch (j)
                    {
                        case 0: val = date[0].Trim(); break;
                        case 1: val = date[1].Trim() + " lei"; break;
                        case 2: val = date[2].Trim(); break;
                        case 3: val = date[3].Trim() + " GB"; break;
                        case 4: val = dualSim; break;
                        case 5: val = g5; break;
                    }

                    tabel[i + 1, j] = new Label()
                    {
                        Text = val,
                        Width = 110,
                        Height = 30,
                        Left = 50 + j * 115,
                        Top = 350 + i * 32,
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
