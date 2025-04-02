using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Modele;
using NivelStocareDate;

namespace MagazinTelefoane_UI_WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Configurare formă
            this.Text = "Listă Telefoane";
            this.Size = new Size(800, 500);
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Adăugare titlu
            var lblTitlu = new Label()
            {
                Text = "TELEFOANE DISPONIBILE",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                Width = 700,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 20,
                Left = (this.Width - 700) / 2
            };
            this.Controls.Add(lblTitlu);

            // Încărcare și afișare date
            IncarcaSiAfiseazaTelefoane();
        }

        private void IncarcaSiAfiseazaTelefoane()
        {
            try
            {
                string caleFisier = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Telefoane.txt");

                if (!File.Exists(caleFisier))
                {
                    MessageBox.Show("Fișierul Telefoane.txt nu există!");
                    return;
                }

                string[] linii = File.ReadAllLines(caleFisier);
                if (linii.Length == 0)
                {
                    MessageBox.Show("Nu există telefoane în fișier!");
                    return;
                }

                // Creare antet tabel
                CreazaAntetTabel();

                // Afișare telefoane
                int top = 100; // Poziția de start pentru randurile cu telefoane
                for (int i = 0; i < linii.Length; i++)
                {
                    string[] date = linii[i].Split(',');
                    if (date.Length >= 4)
                    {
                        // Rand pentru fiecare telefon
                        CreazaRandTabel(
                            date[0].Trim(), // Nume
                            date[1].Trim() + " lei", // Pret
                            date[2].Trim(), // Culoare
                            date[3].Trim() + " GB", // Memorie
                            ref top,
                            i % 2 == 0 ? Color.White : Color.Lavender // Alternare culori
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private void CreazaAntetTabel()
        {
            string[] antet = { "Model", "Preț", "Culoare", "Memorie" };
            int left = 50;
            int top = 70;

            for (int i = 0; i < antet.Length; i++)
            {
                var lbl = new Label()
                {
                    Text = antet[i],
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.DarkBlue,
                    BackColor = Color.LightSteelBlue,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 150,
                    Height = 30,
                    Left = left + (i * 160),
                    Top = top
                };
                this.Controls.Add(lbl);
            }
        }

        private void CreazaRandTabel(string nume, string pret, string culoare, string memorie, ref int top, Color culoareFundal)
        {
            string[] valori = { nume, pret, culoare, memorie };
            int left = 50;

            for (int i = 0; i < valori.Length; i++)
            {
                var lbl = new Label()
                {
                    Text = valori[i],
                    Font = new Font("Arial", 9),
                    BackColor = culoareFundal,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 150,
                    Height = 30,
                    Left = left + (i * 160),
                    Top = top
                };
                this.Controls.Add(lbl);
            }

            top += 32; // Spațiu între randuri
        }
    }
}