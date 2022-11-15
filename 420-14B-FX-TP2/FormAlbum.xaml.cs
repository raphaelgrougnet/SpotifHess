using Microsoft.Win32;
using System;
using System.IO;
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
        #region CONSTANTES

        public const string CHEMIN_IMAGES_ALBUMS = @"C:\data-420-14B-FX\data-tp2-420-14b\albums\\";

        #endregion

        #region ATTRIBUTS
        /// <summary>
        /// Un album à ajouter ou à modifier
        /// </summary>
        private Album _album;

        /// <summary>
        /// L'état du fomulaire
        /// </summary>
        private EtatAlbum _etat;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Un album à ajouter ou à modifier
        /// </summary>
        public Album pAlbum
        {
            get { return _album; }
            set { _album = value; }
        }

        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// Construction du formulaire d'album et il est en mode ajout par défaut
        /// </summary>
        /// <param name="album">Un album</param>
        /// <param name="etat">État du fomulaire</param>
        public FormAlbum(Album album = null, EtatAlbum etat = EtatAlbum.Ajouter)
        {
            InitializeComponent();

            pAlbum = album;
            _etat = etat;
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Objet</param>
        /// <param name="e">Event</param>
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
            btnAjouterModifier.Content = _etat.ToString();

            switch (_etat)
            {
                case EtatAlbum.Ajouter:
                    txtTitre.Clear();
                    txtAnnee.Clear();
                    txtArtiste.Clear();
                    imgAlbum.Source = null;
                    break;
                case EtatAlbum.Modifier:
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
                messageErreur += "Le(s) artiste(s) ne doit pas être vide \n";

            if (imgAlbum.Source is null)
                messageErreur += "Vous devez saisir une image pour votre album";


            if(messageErreur != "")
            {
                MessageBox.Show(messageErreur, "Ajouter un album", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Ajoute ou modifie un album
        /// </summary>
        /// <param name="sender">Objet</param>
        /// <param name="e">Event</param>
        private void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            switch (_etat)
            {
                case EtatAlbum.Ajouter:

                    if (ValidationAlbum())
                    {
                        string titre = txtTitre.Text;
                        ushort annee = ushort.Parse(txtAnnee.Text);
                        string artistes = txtArtiste.Text;

                        BitmapImage imgAl = imgAlbum.Source as BitmapImage;
                        string cheminFichier = imgAl.UriSource.LocalPath;
                        string image = Path.GetFileName(cheminFichier);

                        string ext = Path.GetExtension(image);
                        image = Guid.NewGuid() + ext;
                        File.Copy(cheminFichier, CHEMIN_IMAGES_ALBUMS + image, true);


                        pAlbum = new Album(false, Guid.NewGuid(), titre, annee, image, artistes);

                        DialogResult = true;
                        
                    }
                    break;
                case EtatAlbum.Modifier:

                    if (ValidationAlbum())
                    {
                        pAlbum.Titre = txtTitre.Text;
                        pAlbum.Annee = ushort.Parse(txtAnnee.Text);
                        pAlbum.Artiste = txtArtiste.Text;
                        

                        DialogResult = true;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Bouton qui permet l'utilisateur de parcourir ses dossiers pour une images pour son album
        /// </summary>
        /// <param name="sender">Un objet</param>
        /// <param name="e">Event</param>
        private void btnParcourir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choisissez une image";
            openFileDialog.Filter = "Fichier JPG (*.jpg)|*.jpg|Fichier PNG (*.png)|*.png";

            if ((bool)openFileDialog.ShowDialog())
            {
                string ficher = openFileDialog.FileName;

                BitmapImage biAl = new BitmapImage();

                biAl.BeginInit();
                biAl.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biAl.UriSource = new Uri(ficher);
                biAl.CacheOption = BitmapCacheOption.OnLoad;
                biAl.EndInit();

                imgAlbum.Source = biAl;
            }
        }

        #endregion

        /// <summary>
        /// Annule la modidification ou l'ajout d'un album
        /// </summary>
        /// <param name="sender">Un obejet</param>
        /// <param name="e">Event</param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Voulez-vous vraiment annuler?", "Annuler", MessageBoxButton.YesNo, 
                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }

           
        }
    }
}
