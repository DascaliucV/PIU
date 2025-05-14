using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class FormAdaugare : Form
    {
        private MetroTextBox txtModel, txtPret, txtCuloare, txtMemorie;
        private MetroLabel lblModel, lblPret, lblCuloare, lblMemorie;
        private MetroLabel errModel, errPret, errCuloare, errMemorie;
        private MetroButton btnAdauga, btnBack, btnActualizeaza;
        private CheckBox chkDualSim, chk5G, chkAndroid, chkIOS;
        private const int MAX_CARACTERE = 15;
        private const int MAX_MEMORIE = 1024;
        private const int MIN_MEMORIE = 1;
        private const string FISIER = "Telefoane.txt";
        private ListBox lstTelefoane;
        private int selectedIndex = -1;

        public FormAdaugare()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "+ Adaugare Telefoane";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panelHeader = new Panel()
            {
                BackColor = Color.FromArgb(28, 151, 234),
                Dock = DockStyle.Top,
                Height = 80
            };
            this.Controls.Add(panelHeader);

            Label lblTitle = new Label()
            {
                Text = "GESTIONARE TELEFOANE",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTitle);

            // ListBox cu scrollbar
            lstTelefoane = new ListBox()
            {
                Left = 10,
                Top = 370,
                Width = 800,
                Height = 300,
                ScrollAlwaysVisible = true,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
            };
            lstTelefoane.SelectedIndexChanged += LstTelefoane_SelectedIndexChanged;
            this.Controls.Add(lstTelefoane);

            string[] etichete = { "Model", "Preț", "Culoare", "Memorie" };
            MetroTextBox[] MetroTextBoxes = new MetroTextBox[4];
            MetroLabel[] lbls = new MetroLabel[4];
            MetroLabel[] errs = new MetroLabel[4];

            btnBack = new MetroButton()
            {
                Text = "⬅ Inapoi la Optiuni",
                Top = 690,
                Left = 50,
                Width = 150
            };
            btnBack.Click += (s, e) => {
                this.Hide();
                FormInfo formInfo = Application.OpenForms["FormInfo"] as FormInfo ?? new FormInfo();
                formInfo.Show();
            };
            this.Controls.Add(btnBack);

            for (int i = 0; i < etichete.Length; i++)
            {
                lbls[i] = new MetroLabel()
                {
                    Text = etichete[i] + ":",
                    Left = 50,
                    Top = 100 + i * 40,
                    Width = 100
                };
                this.Controls.Add(lbls[i]);

                MetroTextBoxes[i] = new MetroTextBox()
                {
                    Left = 160,
                    Top = 100 + i * 40,
                    Width = 150
                };
                this.Controls.Add(MetroTextBoxes[i]);

                errs[i] = new MetroLabel()
                {
                    ForeColor = Color.Red,
                    CustomForeColor = true,
                    Left = 320,
                    Top = 100 + i * 40,
                    Width = 300,
                    Text = ""
                };
                this.Controls.Add(errs[i]);
            }

            txtModel = MetroTextBoxes[0]; txtPret = MetroTextBoxes[1];
            txtCuloare = MetroTextBoxes[2]; txtMemorie = MetroTextBoxes[3];
            lblModel = lbls[0]; lblPret = lbls[1];
            lblCuloare = lbls[2]; lblMemorie = lbls[3];
            errModel = errs[0]; errPret = errs[1];
            errCuloare = errs[2]; errMemorie = errs[3];

            chkDualSim = new CheckBox()
            {
                Text = "Dual SIM",
                Left = 50,
                Top = 245,
                Width = 100
            };
            this.Controls.Add(chkDualSim);

            chk5G = new CheckBox()
            {
                Text = "Suport 5G",
                Left = 160,
                Top = 245,
                Width = 100
            };
            this.Controls.Add(chk5G);

            chkAndroid = new CheckBox()
            {
                Text = "Android",
                Left = 270,
                Top = 245,
                Width = 100
            };
            this.Controls.Add(chkAndroid);

            chkIOS = new CheckBox()
            {
                Text = "iOS",
                Left = 380,
                Top = 245,
                Width = 100
            };
            this.Controls.Add(chkIOS);

            btnAdauga = new MetroButton()
            {
                Text = "Adaugă",
                Top = 290,
                Left = 50,
                Width = 100
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnActualizeaza = new MetroButton()
            {
                Text = "Actualizează",
                Top = 290,
                Left = 160,
                Width = 100,
                Enabled = false
            };
            btnActualizeaza.Click += BtnActualizeaza_Click;
            this.Controls.Add(btnActualizeaza);

           

            AfiseazaLista();
        }

        private void LstTelefoane_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTelefoane.SelectedIndex == -1) return;

            selectedIndex = lstTelefoane.SelectedIndex;
            btnAdauga.Enabled = false;
            btnActualizeaza.Enabled = true;

            string selectedItem = lstTelefoane.SelectedItem.ToString();
            string[] parts = selectedItem.Split(',');

            // Extragem datele din item-ul selectat
            txtModel.Text = parts[0].Replace("Model:", "").Trim();
            txtPret.Text = parts[1].Replace("Preț:", "").Replace("lei", "").Trim();
            txtCuloare.Text = parts[2].Replace("Culoare:", "").Trim();
            txtMemorie.Text = parts[3].Replace("Memorie:", "").Replace("GB", "").Trim();

            chkDualSim.Checked = parts[4].Contains("DA");
            chk5G.Checked = parts[5].Contains("DA");
            chkAndroid.Checked = parts[6].Contains("DA");
            chkIOS.Checked = parts[7].Contains("DA");
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            if (!ValideazaDate()) return;

            string extra = "";
            if (chkDualSim.Checked) extra += ", DualSIM";
            if (chk5G.Checked) extra += ", 5G";
            if (chkAndroid.Checked) extra += ", Android";
            if (chkIOS.Checked) extra += ", iOS";

            File.AppendAllText(FISIER, $"{txtModel.Text},{txtPret.Text},{txtCuloare.Text},{txtMemorie.Text}{extra}\n");
            AfiseazaLista();
            ClearControls();
        }

        private void BtnActualizeaza_Click(object sender, EventArgs e)
        {
            if (selectedIndex == -1 || !ValideazaDate()) return;

            string[] allLines = File.ReadAllLines(FISIER);
            if (selectedIndex >= allLines.Length) return;

            string extra = "";
            if (chkDualSim.Checked) extra += ", DualSIM";
            if (chk5G.Checked) extra += ", 5G";
            if (chkAndroid.Checked) extra += ", Android";
            if (chkIOS.Checked) extra += ", iOS";

            allLines[selectedIndex] = $"{txtModel.Text},{txtPret.Text},{txtCuloare.Text},{txtMemorie.Text}{extra}";
            File.WriteAllLines(FISIER, allLines);

            AfiseazaLista();
            ClearControls();
            btnAdauga.Enabled = true;
            btnActualizeaza.Enabled = false;
            selectedIndex = -1;
        }

        private bool ValideazaDate()
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

            return valid;
        }

        private void ResetEtichete()
        {
            lblModel.ForeColor = lblPret.ForeColor = lblCuloare.ForeColor = lblMemorie.ForeColor = Color.Black;
            errModel.Text = errPret.Text = errCuloare.Text = errMemorie.Text = "";
        }

        private void ClearControls()
        {
            txtModel.Text = txtPret.Text = txtCuloare.Text = txtMemorie.Text = "";
            chkDualSim.Checked = false;
            chk5G.Checked = false;
            chkAndroid.Checked = false;
            chkIOS.Checked = false;
        }

        private void AfiseazaLista()
        {
            lstTelefoane.Items.Clear();
            if (!File.Exists(FISIER)) return;

            var linii = File.ReadAllLines(FISIER);
            foreach (var linie in linii)
            {
                var date = linie.Split(',');
                if (date.Length < 4) continue;

                string dualSim = linie.Contains("DualSIM") ? "DA" : "NU";
                string g5 = linie.Contains("5G") ? "DA" : "NU";
                string android = linie.Contains("Android") ? "DA" : "NU";
                string ios = linie.Contains("iOS") ? "DA" : "NU";

                string afisare = $"Model: {date[0].Trim()}, Preț: {date[1].Trim()} lei, Culoare: {date[2].Trim()}, " +
                                $"Memorie: {date[3].Trim()} GB, " +
                                $"Dual SIM: {dualSim}, 5G: {g5}, Android: {android}, iOS: {ios}";
                lstTelefoane.Items.Add(afisare);
            }
        }
    }
}