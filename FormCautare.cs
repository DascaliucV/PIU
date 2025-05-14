using System;
using System.Drawing;
using System.Data;
using MetroFramework.Controls;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class FormCautare : Form
    {
        private TextBox txtCautare;
        private Button btnCauta, btnBack;
        private ListBox listRezultate;
        private ComboBox comboCriteriu;
        private RadioButton radioAndroid, radioIOS;
        private const string FISIER = "Telefoane.txt";

        public FormCautare()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            this.Text = "🔍 Căutare Telefoane";
            this.Size = new Size(700, 600); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header Panel
            Panel panelHeader = new Panel()
            {
                BackColor = Color.FromArgb(28, 151, 234),
                Dock = DockStyle.Top,
                Height = 80
            };
            this.Controls.Add(panelHeader);

            // Title Label
            Label lblTitle = new Label()
            {
                Text = "CĂUTARE TELEFOANE",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTitle);

            btnBack = new Button()
            {
                Text = "◄ Înapoi",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(30, 120), 
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            btnBack.Click += (s, e) => {
                this.Close();
                var formInfo = Application.OpenForms["FormInfo"] as FormInfo ?? new FormInfo();
                formInfo.Show();
            };
            this.Controls.Add(btnBack);

            Panel searchPanel = new Panel()
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 100),
                Location = new Point(40, 170) 
            };
            this.Controls.Add(searchPanel);

            Label lblCriteriu = new Label()
            {
                Text = "Cautare dupa:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            searchPanel.Controls.Add(lblCriteriu);

            comboCriteriu = new ComboBox()
            {
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 30),
                Location = new Point(20, 45)
            };
            comboCriteriu.Items.AddRange(new object[] {
                "Model",
                "Preț (exact)",
                "Preț (sub)",
                "Preț (peste)",
                "Culoare",
                "Memorie (exact)",
                "Memorie (sub)",
                "Memorie (peste)",
                "Dual SIM",
                "5G"
            });
            comboCriteriu.SelectedIndex = 0;
            searchPanel.Controls.Add(comboCriteriu);

            Label lblCautare = new Label()
            {
                Text = "Termen cautare:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(250, 20),
                AutoSize = true
            };
            searchPanel.Controls.Add(lblCautare);

            txtCautare = new TextBox()
            {
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(250, 45)
            };
            searchPanel.Controls.Add(txtCautare);

            btnCauta = new Button()
            {
                Text = "CAUTĂ",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(28, 151, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(520, 45),
                Cursor = Cursors.Hand
            };
            btnCauta.FlatAppearance.BorderSize = 0;
            btnCauta.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 170, 250);
            btnCauta.Click += BtnCauta_Click;
            searchPanel.Controls.Add(btnCauta);

            Panel osPanel = new Panel()
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 50),
                Location = new Point(40, 290) 
            };
            this.Controls.Add(osPanel);

            Label lblOS = new Label()
            {
                Text = "Sistem de operare:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            osPanel.Controls.Add(lblOS);

            radioAndroid = new RadioButton()
            {
                Text = "Android",
                Font = new Font("Segoe UI", 10),
                Location = new Point(180, 15),
                AutoSize = true
            };
            osPanel.Controls.Add(radioAndroid);

            radioIOS = new RadioButton()
            {
                Text = "iOS",
                Font = new Font("Segoe UI", 10),
                Location = new Point(280, 15),
                AutoSize = true
            };
            osPanel.Controls.Add(radioIOS);

            RadioButton radioNone = new RadioButton()
            {
                Text = "Toate",
                Font = new Font("Segoe UI", 10),
                Location = new Point(350, 15),
                AutoSize = true,
                Checked = true // Set as default
            };
            osPanel.Controls.Add(radioNone);

            Panel resultsPanel = new Panel()
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 300),
                Location = new Point(40, 360) // Actualizat poziția
            };
            this.Controls.Add(resultsPanel);

            listRezultate = new ListBox()
            {
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Size = new Size(680, 290),
                Location = new Point(10, 5),
                DrawMode = DrawMode.OwnerDrawVariable,
                ItemHeight = 100
            };
            listRezultate.DrawItem += ListRezultate_DrawItem;
            resultsPanel.Controls.Add(listRezultate);
        }

        private void ListRezultate_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? Color.FromArgb(230, 240, 255) : Color.White;
            Color foreColor = isSelected ? Color.FromArgb(28, 151, 234) : Color.Black;

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            string text = listRezultate.Items[e.Index].ToString();
            using (SolidBrush brush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }

            e.Graphics.DrawLine(new Pen(Color.FromArgb(240, 240, 240)),
                new Point(e.Bounds.Left, e.Bounds.Bottom - 1),
                new Point(e.Bounds.Right, e.Bounds.Bottom - 1));

            e.DrawFocusRectangle();
        }

        private void BtnCauta_Click(object sender, EventArgs e)
        {
            listRezultate.Items.Clear();
            string valoareCautare = txtCautare.Text.Trim().ToLower();
            string criteriu = comboCriteriu.SelectedItem.ToString();

            if (!File.Exists(FISIER))
            {
                listRezultate.Items.Add("Nu există telefoane înregistrate în baza de date.");
                return;
            }

            var linii = File.ReadAllLines(FISIER);
            var rezultate = linii.Where(linie => {
                var parts = linie.Split(',');
                if (parts.Length < 4) return false;

                if (radioAndroid.Checked && !linie.Contains("Android")) return false;
                if (radioIOS.Checked && !linie.Contains("iOS")) return false;

                switch (criteriu)
                {
                    case "Model":
                        return parts[0].Trim().ToLower().Contains(valoareCautare);

                    case "Preț (exact)":
                        return int.TryParse(valoareCautare, out int pretExact) &&
                               int.TryParse(parts[1].Trim(), out int pret) &&
                               pret == pretExact;

                    case "Preț (sub)":
                        return int.TryParse(valoareCautare, out int pretMaxim) &&
                               int.TryParse(parts[1].Trim(), out int pretCurent) &&
                               pretCurent <= pretMaxim;

                    case "Preț (peste)":
                        return int.TryParse(valoareCautare, out int pretMinim) &&
                               int.TryParse(parts[1].Trim(), out int pretTelefon) &&
                               pretTelefon >= pretMinim;

                    case "Culoare":
                        return parts[2].Trim().ToLower().Contains(valoareCautare);

                    case "Memorie (exact)":
                        return int.TryParse(valoareCautare, out int memorieExacta) &&
                               int.TryParse(parts[3].Trim(), out int memorie) &&
                               memorie == memorieExacta;

                    case "Memorie (sub)":
                        return int.TryParse(valoareCautare, out int memorieMaxima) &&
                               int.TryParse(parts[3].Trim(), out int memorieCurenta) &&
                               memorieCurenta <= memorieMaxima;

                    case "Memorie (peste)":
                        return int.TryParse(valoareCautare, out int memorieMinima) &&
                               int.TryParse(parts[3].Trim(), out int memorieTelefon) &&
                               memorieTelefon >= memorieMinima;

                    case "Dual SIM":
                        return linie.Contains("DualSIM");

                    case "5G":
                        return linie.Contains("5G");

                    default:
                        return false;
                }
            }).ToList();

            if (rezultate.Count == 0)
            {
                listRezultate.Items.Add("Niciun rezultat găsit pentru criteriul selectat.");
            }
            else
            {
                foreach (var linie in rezultate)
                {
                    var split = linie.Split(',');
                    string dualSim = linie.Contains("DualSIM") ? "DA" : "NU";
                    string g5 = linie.Contains("5G") ? "DA" : "NU";
                    string os = linie.Contains("Android") ? "Android" : (linie.Contains("iOS") ? "iOS" : "Altul");

                    listRezultate.Items.Add(
                        $"📱 Model: {split[0].Trim()} ({os})\n" +
                        $"💰 Preț: {split[1].Trim()} lei\n" +
                        $"🎨 Culoare: {split[2].Trim()}\n" +
                        $"💾 Memorie: {split[3].Trim()} GB\n" +
                        $"📶 Dual SIM: {dualSim}    |    📡 5G: {g5}"
                    );
                }
            }
        }
    }
}