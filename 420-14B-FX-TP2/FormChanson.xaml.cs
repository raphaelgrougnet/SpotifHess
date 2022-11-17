using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TP2_420_14B_FX.Classes;
using TP2_420_14B_FX.Enums;

namespace TP2_420_14B_FX
{




    /// <summary>
    /// Logique d'interaction pour frmChanson.xaml
    /// </summary>
    public partial class FormChanson : Window
    {
        MediaPlayer _mediaPlayer;

        private Chanson _chansonAjoutModif;

        public Chanson ChansonAjoutModif
        {
            get { return _chansonAjoutModif; }
            set { _chansonAjoutModif = value; }
        }

        
        
            

        public FormChanson( Chanson chanson = null, EtatChanson etat = EtatChanson.Modifier)
        {
            InitializeComponent();
            lblTitreForm.Content = etat.ToString()+" une chanson";
            btnAjouterModifier.Content = etat.ToString();
            foreach (StyleMusical style in (StyleMusical[])Enum.GetValues(typeof(StyleMusical)))
            {
                cboStyle.Items.Add(style);
            }
            ChansonAjoutModif = chanson;

            if (etat == EtatChanson.Modifier)
            {
                txtTitre.Text = chanson.Titre;
                cboStyle.SelectedItem = chanson.Style;
                lblDuree.Content = chanson.Duree.ToString(@"hh\:mm\:ss");
                lblFichier.Content = chanson.Fichier;

            }
            
            
            //Initialiseation du lecteur utiliser pour ouvrir le ficheir selectionné afin
            //de déterminer la durée de la chanson.
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaOpened += MediaOpened;
        }

       
   
        /// <summary>
        /// Événement exécuté lors de l'ouvertur d'un fichier par le lecteur. Permet d'afficher la durée de la chanson.
        /// </summary>
        /// <param name="sender">_mediaPlayer</param>
        /// <param name="e"></param>
        private void MediaOpened(object sender, EventArgs e)
        {
            if (_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                //todo: Afficher le durée de la chanson dans le label correspondant. FAIT
                lblDuree.Content = _mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            }

        }

        private void btnParcourir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Choisir une chanson";
                openFileDialog.Filter = "Fichier mp3 (*.mp3)|*.mp3";

                if ((bool)openFileDialog.ShowDialog())
                {

                    string fichier = openFileDialog.FileName;
                    _mediaPlayer.Open(new Uri(fichier));
                    lblFichier.Content = fichier;


                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Une erreur est survenue. {exp.Message}");
            }
            
        }

        private void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChansonAjoutModif == null)
                {

                    if (ValiderChanson())
                    {
                        Guid guid = Guid.NewGuid();
                        string titre = txtTitre.Text.Trim();
                        StyleMusical style = (StyleMusical)cboStyle.SelectedItem;
                        TimeSpan duree = TimeSpan.Parse((string)lblDuree.Content);
                        string fichier = guid + ".mp3";
                        ChansonAjoutModif = new Chanson(guid, titre, style, duree, fichier);

                    }

                }
                else
                {


                    if (ValiderChanson())
                    {

                        string titre = txtTitre.Text.Trim();
                        StyleMusical style = (StyleMusical)cboStyle.SelectedItem;
                        TimeSpan duree = TimeSpan.Parse((string)lblDuree.Content);

                        ChansonAjoutModif.Titre = titre;
                        ChansonAjoutModif.Style = style;
                        ChansonAjoutModif.Duree = duree;



                    }
                }

                DialogResult = true;
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show($"Une erreur s'est produite. {ane.Message}");
            }
            catch (IndexOutOfRangeException ioore)
            {
                MessageBox.Show($"Une erreur s'est produite. {ioore.Message}");
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show($"Une erreur s'est produite. {ae.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite. {ex.Message}");
            }

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler?", "Annuler", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
        }

        private bool ValiderChanson()
        {
            string message = "";
            
            if (String.IsNullOrWhiteSpace(txtTitre.Text))
            {
                message += "Le titre de la chanson ne peut pas être nul, vide ou ne contenir que des espaces\n";
            }

            
            if (cboStyle.SelectedIndex == -1)
            {
                message += "Sélectionnez un style musical\n";
            }
            if (String.IsNullOrWhiteSpace((string)lblFichier.Content))
            {
                message += "Vous devez sélectionner un fichier";
            }

            if (message != "")
            {
                MessageBox.Show(message, "Ajouter une chanson", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
            return true;
        }
    }
}
