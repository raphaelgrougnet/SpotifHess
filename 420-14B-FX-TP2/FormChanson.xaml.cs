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

        private bool parcourru = false;
        
            

        public FormChanson( Chanson chanson = null, EtatChanson etat = EtatChanson.Modifier)
        {
            InitializeComponent();
            lblTitreForm.Content = etat.ToString()+" une chanson";
            btnAjouterModifier.Content = etat.ToString();
            foreach (StyleMusical style in (StyleMusical[])Enum.GetValues(typeof(StyleMusical)))
            {
                cboStyle.Items.Add(style);
            }
            _chansonAjoutModif = chanson;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choisir une chanson";
            openFileDialog.Filter = "Fichier mp3 (*.mp3)|*.mp3";

            if ((bool)openFileDialog.ShowDialog())
            {
                
                string fichier = openFileDialog.FileName;
                _mediaPlayer.Open(new Uri(fichier));
                lblFichier.Content = fichier;
                parcourru = true;
                
            }
        }

        private void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            if (_chansonAjoutModif == null)
            {
                Guid guid = Guid.NewGuid();
                string titre = txtTitre.Text.Trim();
                
                if (cboStyle.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException("Sélectionnez un style musical");
                }
                StyleMusical style = (StyleMusical)cboStyle.SelectedItem;
                TimeSpan duree = TimeSpan.Parse((string)lblDuree.Content);
                string fichier = guid + ".mp3";
                if (ValiderChanson(titre,style,fichier))
                {
                    ChansonAjoutModif = new Chanson(guid, titre, style, duree, fichier);
                    File.Copy((string) lblFichier.Content, GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + _chansonAjoutModif.Fichier);
                }
                
            }
            else
            {
                Guid guid = _chansonAjoutModif.Id;
                string titre = txtTitre.Text.Trim();

                if (cboStyle.SelectedIndex == -1)
                {
                    throw new IndexOutOfRangeException("Sélectionnez un style musical");
                }
                StyleMusical style = (StyleMusical)cboStyle.SelectedItem;
                TimeSpan duree = TimeSpan.Parse((string)lblDuree.Content);
                string fichier = guid + ".mp3";

               
                if (ValiderChanson(titre, style, fichier))
                {

                    _chansonAjoutModif = new Chanson(guid, titre, style, duree, fichier);
                    if (parcourru)
                    {
                        File.Copy((string) lblFichier.Content, GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + _chansonAjoutModif.Fichier, true);
                        parcourru = false;
                    }
                    else
                    {
                        File.Copy(GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + fichier, GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + _chansonAjoutModif.Fichier, true);
                    }
                    
                }
            }
            
            DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool ValiderChanson(string titre, StyleMusical style, string fichier)
        {
            string message = "";
            
            if (String.IsNullOrWhiteSpace(titre))
            {
                message += "Le titre de la chanson ne peut pas être nul, vide ou ne contenir que des espaces\n";
            }
            if (!Enum.IsDefined(typeof(StyleMusical), style))
            {
                message += "Le style de musique choisi n'est pas défini";
            }
            if (String.IsNullOrWhiteSpace(fichier))
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
