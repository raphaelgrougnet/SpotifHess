using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using TP2_420_14B_FX.Classes;
using TP2_420_14B_FX.Enums;

namespace TP2_420_14B_FX
{
    /// <summary>
    /// Logique d'interaction pour FromAlbum.xaml
    /// </summary>
    public partial class FormAlbum : Window
    {

        /// <summary>
        /// Un album à ajouter ou à modifier
        /// </summary>
        private Album _album;

        /// <summary>
        /// L'état du fomulaire
        /// </summary>
        private EtatAlbum _etat;

        /// <summary>
        /// Un album à ajouter ou à modifier
        /// </summary>
        public Album pAlbum
        {
            get { return _album; }
            set { _album = value; }
        }
        
        /// <summary>
        /// Construction du formulaire d'album et il est en mode ajout par défaut
        /// </summary>
        /// <param name="album"></param>
        /// <param name="etat"></param>
        public FormAlbum(Album album = null, EtatAlbum etat = EtatAlbum.Ajout)
        {
            InitializeComponent();

            pAlbum = album;
            _etat = etat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaire();
        }

        /// <summary>
        /// Initialise le formulaire selon son état
        /// </summary>
        private void InitialiserFormulaire()
        {
            lblTitre.Content = _etat.ToString() + " un album";
            btnAjouterModifier.Content = _etat.ToString() + " un album";

            switch (_etat)
            {
                case EtatAlbum.Ajout:
                    txtTitre.Clear();
                    txtAnnee.Clear();
                    txtArtiste.Clear();
                    imgAlbum.Source = null;
                    break;
                case EtatAlbum.Modidifier:
                    if(pAlbum != null)
                    {
                        txtTitre.Text = pAlbum.Titre;
                        txtAnnee.Text = pAlbum.Annee.ToString();
                        txtArtiste.Text = pAlbum.Artiste;
                    }
                    break;
                
            }
        }

        /// <summary>
        /// La validation dans l'ajout d'un album
        /// </summary>
        /// <returns>Retourne faux s'il y a eu des erreurs ou vrai s'il n'a aucune erreur</returns>
        private bool ValidationAlbum()
        {
            string messageErreur = "";

            if (string.IsNullOrWhiteSpace(txtTitre.Text))
                messageErreur += "Le titre ne peut pas être vide. \n";

            ushort annee;
            if (!ushort.TryParse(txtAnnee.Text, out annee) || annee < Album.ANNEE_MIN && annee > DateTime.Now.Year)
                messageErreur += $"L'année doit être une valeur numérique et doit être plus grand ou égal que " +
                    $"l'année {Album.ANNEE_MIN} et plus petit ou égal que {DateTime.Now.Year}. \n";

            if (string.IsNullOrWhiteSpace(txtArtiste.Text))
                messageErreur += "Le(s) artiste(s) ne doit pas être vide";


            if(messageErreur != "")
            {
                MessageBox.Show(messageErreur, "Ajouter un album", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
