using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TP2_420_14B_FX.Classes;

namespace TP2_420_14B_FX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region CONSTANTES
        public const string IMAGE_JOUER = "Play.png";
        public const string IMAGE_PAUSE = "Pause.png";
        public const string IMAGE_ALEATOIRE = "Shuffle.png";
        public const string IMAGE_ALEATOIRE_SELECT = "Shuffle_selected.png";
        public const string RESSOURCE_URI = @"pack://application:,,,/Resources/";

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Média player pour jouer la musique
        /// </summary>
        MediaPlayer _mediaPlayer;


        /// <summary>
        /// Timer utiliser pour mettre à jour le temps restant de la musique qui joue.
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();


        /// <summary>
        /// Gestionnaire de musique
        /// </summary>
        private GestionMusique _gestionMusique;


        #endregion

        #region PROPRIÉTÉS ET INDEXEURS



        #endregion

        #region CONSTRUCTEURS

        public MainWindow()
        {
            InitializeComponent();

            //Initialisation du média player
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaEnded += OnMediaEnded;


            //Initialisation du timer
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;


            InitialiserLecteurMusique();

        }

        #endregion

        #region ÉVÉNEMENTS


        /// <summary>
        /// Événement lancé lors de l'affichage du formulaire
        /// </summary>
        /// <param name="sender">FormGestionMusique</param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //GestionMusique _gestionMusique = new GestionMusique();
            //todo: Afficher la liste des albums.

            _gestionMusique = new GestionMusique();

            AfficherListeAlbums();
        }

        #region ÉVÉNEMENTS_MEDIA_PLAYER

        /// <summary>
        /// Événement lancé par le timer pour la mise à jour de la durée de la chanson
        /// </summary>
        /// <param name="sender">_timer</param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if (_mediaPlayer.Source != null)
            {
                if (_mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    sldDuree.Maximum = _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                    lblTempsCourant.Content = _mediaPlayer.Position.ToString(@"mm\:ss");
                    lblDuree.Content = _mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                    sldDuree.Value = _mediaPlayer.Position.TotalSeconds;

                }
            }

            else
                lblTempsCourant.Content = "00:00";
        }

        /// <summary>
        /// Événement exécuté lors de la fin d'une chanson.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMediaEnded(object sender, EventArgs e)
        {

            JouerChansonSuivante();

        }

        /// <summary>
        /// Événement levé lorsque le slider est déplacé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {

            if (_mediaPlayer.Source != null)
            {
                //Ajustement de l'affichage de la position.
                _mediaPlayer.Position = TimeSpan.FromSeconds(sldDuree.Value);

            }
        }

        /// <summary>
        /// Événement lancé lors du click sur le bouton pour jouer ou mettre sur pause une chanson
        /// </summary>
        /// <param name="sender">ImgPlayPause</param>
        /// <param name="e"></param>
        private void imgPlayPause_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (lstChansons.SelectedItem != null)
            {
                BitmapImage bi = imgPlayPause.Source as BitmapImage;

                string image = bi != null ? System.IO.Path.GetFileName(bi.UriSource.LocalPath) : IMAGE_JOUER;

                if (image == IMAGE_PAUSE)
                {
                    _mediaPlayer.Pause();
                    image = IMAGE_JOUER;

                }
                else
                {
                    _mediaPlayer.Play();
                    image = IMAGE_PAUSE;
                }

                Uri uri = new Uri(RESSOURCE_URI + image);
                imgPlayPause.Source = new BitmapImage(uri);
            }


        }

        /// <summary>
        /// Événement lancé lors du click de l'image aléatoire.
        /// </summary>
        /// <param name="sender">imgAleatoire</param>
        /// <param name="e"></param>
        private void imgAleatoire_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lstAlbums.SelectedItem != null)
            {
                LectureAleatoire(!(lstAlbums.SelectedItem as Album).Aleatoire);
            }

        }


        /// <summary>
        /// Événement lancé lors du click sur l'image pour jouer la chanson précédente
        /// </summary>
        /// <param name="sender">imgPrecdente</param>
        /// <param name="e"></param>
        private void imgPrecedente_MouseDown(object sender, MouseButtonEventArgs e)
        {
            JouerChansonPrecedente();
        }

        /// <summary>
        /// Événement lancé lors du click sur l'image pour jouer la chanson suivante
        /// </summary>
        /// <param name="sender">imgSuivante</param>
        /// <param name="e"></param>
        private void imgSuivante_MouseDown(object sender, MouseButtonEventArgs e)
        {
            JouerChansonSuivante();
        }
        #endregion

        #region ÉVÉNEMENTS_ALBUMS
        /// <summary>
        /// Événement lancé lors de la sélection d'un élément dans la liste des albums
        /// </summary>
        /// <param name="sender">ListBox contenant les albums</param>
        /// <param name="e"></param>
        private void lstAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AfficherDetailsAlbum();
        }

        /// <summary>
        /// Événement lancé lors du click sur l'image du bouton pour ajouter un album.
        /// </summary>
        /// <param name="sender">imgAjouterAlbum</param>
        /// <param name="e"></param>
        private void imgAjouterAlbum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AjouterAlbum();
        }

        /// <summary>
        /// Événement lancé lors du click sur l'image pour modifier un album.
        /// </summary>
        /// <param name="sender">imgModifierAlbum</param>
        /// <param name="e"></param>
        private void imgModifierAlbum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ModifierAlbum();
        }

        /// <summary>
        /// Événement lancer lors du click sur l'image pour la suppresion d'un album.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSupprimerAlbum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SupprimerAlbum();
        }
        #endregion

        #region ÉVÉNEMENTS_CHANSONS
        /// <summary>
        /// Événement lancé lors de la sélection d'un élément dans la liste chansons
        /// </summary>
        /// <param name="sender">ListeBox contenant les chansons</param>
        /// <param name="e"></param>
        private void lstChansons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstChansons.SelectedIndex != -1)
            {
                JouerChansonSelectionnee();
            }
            

        }

        /// <summary>
        /// Événement lancer lors du click sur l'image pour l'ajout d'une chanson
        /// </summary>
        /// <param name="sender">imgAjouterChanson</param>
        /// <param name="e"></param>
        private void imgAjouerChanson_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AjouterChanson();
        }

        /// <summary>
        /// Événement lancé lors du click sur l'image pour la modification d'une chanson
        /// </summary>
        /// <param name="sender">imgModificationChanson</param>
        /// <param name="e"></param>
        private void imgModifierChanson_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ModifierChanson();
        }

        /// <summary>
        /// Événement lancer lors du click sur l'image pour la suppression d'une chanson.
        /// </summary>
        /// <param name="sender">imgSupprimerChanson</param>
        /// <param name="e"></param>
        private void imgSupprimerChanson_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SupprimerChanson();
        }
        #endregion

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Permet d'initialiser le media player ainsi que le timer associé.
        /// </summary>
        private void InitialiserLecteurMusique()
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Close();
                Uri uri = new Uri(RESSOURCE_URI + IMAGE_JOUER);
                imgPlayPause.Source = new BitmapImage(uri);

            }

        }

        /// <summary>
        /// Permet vider le contenu des champs affichant les détails d'un album
        /// ainsi que le liste des chansons et réinitialise le lecteur de musique.
        /// </summary>
        private void InitialiserDetailsAlbums()
        {
            //Implémenter la méthode InitialiserDetailsAlbums
            InitialiserLecteurMusique();
            lstChansons.Items.Clear();
            lblTitreAlbum.Content = "";
            lblAnnee.Content = "";
            lblArtistes.Content = "";
            lblDureeAlbum.Content = "";
            imgAlbum.Source = null;

        }

        /// <summary>
        /// Permet l'affichage de la liste des albums triés
        /// et l'initialisation des détails d'un album.
        /// </summary>
        private void AfficherListeAlbums()
        {
            //Implémenter la méthode AfficherListeAlbums FAIT

            lstAlbums.Items.Clear();

            foreach(Album album in _gestionMusique.Albums)
            {
                lstAlbums.Items.Add(album);
            }
        }

        /// <summary>
        /// Permet de modifier l'état de lecture d'un album entre aléatoire ou non
        /// </summary>
        /// <param name="aleatoire"></param>
        private void LectureAleatoire(bool aleatoire)
        {
            if (lstAlbums.SelectedItem != null)
            {
                Album album = lstAlbums.SelectedItem as Album;

                //Todo: modifier l'état de l'album pour la lecture FAIT
                album.Aleatoire = aleatoire;

                //Ajustement de l'image selon si la lecteure est aléatoire ou non.
                string image = aleatoire ? IMAGE_ALEATOIRE_SELECT : IMAGE_ALEATOIRE;
                Uri uri = new Uri(RESSOURCE_URI + image);
                imgAleatoire.Source = new BitmapImage(uri);
            }
        }

        /// <summary>
        /// Permet l'affichage des détails d'un album sélectionné ainsi 
        /// que la liste des chansons qu'il contient.
        /// </summary>
        /// <remarks>L'album sélectionné ne doit pas être nul.</remarks>
        private void AfficherDetailsAlbum()
        {
            //Implémenter la méthode AfficherDetailsAlbum
            if (lstAlbums.SelectedIndex != -1)
            {
                Album selectAlbum = (Album)lstAlbums.SelectedItem;
                InitialiserDetailsAlbums();
                lblTitreAlbum.Content = selectAlbum.Titre;
                lblAnnee.Content = selectAlbum.Annee;
                lblArtistes.Content = selectAlbum.Artiste;
                lblDureeAlbum.Content = selectAlbum.Duree;

                BitmapImage biImageAlbum = new BitmapImage();
                biImageAlbum.BeginInit();
                biImageAlbum.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biImageAlbum.UriSource = new Uri(GestionMusique.CHEMIN_IMAGES_ALBUMS + "\\" + selectAlbum.Image);
                biImageAlbum.CacheOption = BitmapCacheOption.OnLoad;
                biImageAlbum.EndInit();

                imgAlbum.Source = biImageAlbum;

                AfficherListeChansons();
            }

        }

        /// <summary>
        /// Méthode permettant l'ajout d'un album.
        /// </summary>
        private void AjouterAlbum()
        {
            //Implémenter la méthode AjouterAlbum
            FormAlbum frmAlbum = new FormAlbum();

            if(frmAlbum.ShowDialog() == true)
            {
                Album nouvAlbum = frmAlbum.pAlbum;

                _gestionMusique.AjouterAlbum(nouvAlbum);

                _gestionMusique.EnregistrerAlbums();
                
                AfficherListeAlbums();

                MessageBox.Show("L'ajout de votre album a été effectué avec succès! " + nouvAlbum.ToString(), 
                    "Ajout d'un album", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Permet la modification d'un album sélectionné dans la liste.
        /// </summary>
        /// <remarks>L'album sélectionné doit pas être nul. Si oui, alors un message est affiché à l'utlisateur.
        /// Sion, alors l'album est modifié, les données sur l'album sont enregistrés, l'affichage de la
        /// liste est mise à jour et un message de confirmation est affiché à l'utiisateur.</remarks>
        private void ModifierAlbum()
        {
            //Implémenter la méthode ModifierAlbum
            if (lstAlbums.SelectedItem != null)
            {
                Album albumSelected = lstAlbums.SelectedItem as Album;

                FormAlbum frmAlbum = new FormAlbum(albumSelected, Enums.EtatAlbum.Modifier);

                if ((bool)frmAlbum.ShowDialog())
                {
                    _gestionMusique.EnregistrerChansons();
                    _gestionMusique.EnregistrerAlbums();
                    AfficherListeAlbums();
                    InitialiserDetailsAlbums();

                    MessageBox.Show($"L'album {albumSelected} a bien été modifié.",
                        "Modification d'un album", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un album à modifier", "Modifier un Album", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Permet la suppression d'un album sélectionné dans la liste.
        /// </summary>
        /// <remarks>L'album sélectionné ne doit pas être nul. Si oui alors un message est affiché à l'utilisaeur.
        /// Sinon, on demande à l'utilisateur de confirmer la supression. Si oui, alors on supprime l'album et ses chansons,
        /// on enregistre les données, on met à jour l'affichage de la liste et on affiche un message de confirmation à 
        /// l'utilisateur.</remarks>
        private void SupprimerAlbum()
        {
            //Implémenter la méthode SupprimerAlbum
            if(lstAlbums.SelectedItem != null && MessageBox.Show("Voulez-vous vraiment supprimer cet album?", 
                "Supprimer Album", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                Album albumSelected = lstAlbums.SelectedItem as Album;

                foreach(Chanson chanson in lstChansons.Items)
                {
                    File.Delete(GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + chanson.Fichier);
                    albumSelected.SupprimerChanson(chanson);
                }
                File.Delete(GestionMusique.CHEMIN_IMAGES_ALBUMS + "\\" + albumSelected.Image);
                _gestionMusique.SupprimerAlbum(albumSelected);
                _gestionMusique.EnregistrerChansons();
                _gestionMusique.EnregistrerAlbums();
                AfficherListeAlbums();
                InitialiserDetailsAlbums();
                MessageBox.Show("La suppression de l'album s'est effectué avec succès", "Suppression Album",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un album à supprimer", "Supprimer un Album", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Permet l'affichage de la liste des chansons pour un album sélectionné
        /// </summary>
        /// <remarks>L'album sélectionné ne peut pas être nul. Le lecteur de musique doit être réinitialisé.</remarks>
        private void AfficherListeChansons()
        {
            lstChansons.Items.Clear();
            if (lstAlbums.SelectedIndex != 1)
            {
                Album albumSelected = (Album)lstAlbums.SelectedItem;
                for (byte i = 0; i < albumSelected.NbChansons; i++)
                {
                    lstChansons.Items.Add(albumSelected.ObtenirChanson(i));
                }
            }
            
            

        }

        /// <summary>
        /// Permet l'ajout d'une chanson.
        /// </summary>
        /// <remarks>Un album doit être sélectionné dans la liste. Sinon, un message est affiché à l'utilisateur. 
        /// Si oui, alors on ajoute l'album, on enregistre les données de la chansons, on met à jour la liste des chansons
        /// et on affiche un message de confimation.</remarks>
        private void AjouterChanson()
        {
            if (lstAlbums.SelectedIndex != -1)
            {
                FormChanson frmChanson = new FormChanson(null,Enums.EtatChanson.Ajouter);
                lstChansons.SelectedIndex = -1;
                _mediaPlayer.Close();
                //InitialiserLecteurMusique();
                frmChanson.ShowDialog();

                if (frmChanson.DialogResult == true)
                {

                    
                    Album selectAlbum = (Album)lstAlbums.SelectedItem;
                    Chanson newSong = frmChanson.ChansonAjoutModif;
                    File.Copy((string)frmChanson.lblFichier.Content, GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + frmChanson.ChansonAjoutModif.Fichier);
                    selectAlbum.AjouterChanson(newSong);
                    
                    lstChansons.Items.Clear();
                    AfficherListeChansons();
                    AfficherDetailsAlbum();
                    _gestionMusique.EnregistrerChansons();
                    MessageBox.Show("L'ajout de la chanson a été effectué avec succès !", "Ajouter une chansons", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Album dans la liste ou en créer un.", "Ajouter une chansons", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Implémenter la méthode AjouterChanson FAIT
            
        }

        /// <summary>
        /// Permet la modification d'un chanson sélectionné dans la liste.
        /// </summary>
        /// <remarks> la chanson et l'album ne peut pas être nulle. Si oui alors on affiche un message à l'utilisateur.
        /// Sinon, alors on modifie la chanson, enregistre les données, met à jour la liste et affiche un message de confirmation.</remarks>
        private void ModifierChanson()
        {
            if (lstAlbums.SelectedIndex != -1)
            {
                if (lstChansons.SelectedIndex != -1)
                {
                    
                    FormChanson frmChanson = new FormChanson((Chanson) lstChansons.SelectedItem, Enums.EtatChanson.Modifier);
                    lstChansons.SelectedIndex = -1;
                    _mediaPlayer.Close();
                    InitialiserLecteurMusique();
                    frmChanson.ShowDialog();
                    if (frmChanson.DialogResult == true)
                    {
                        if ((string)frmChanson.lblFichier.Content != frmChanson.ChansonAjoutModif.Fichier)
                        {
                            File.Copy((string)frmChanson.lblFichier.Content, GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + frmChanson.ChansonAjoutModif.Fichier, true);
                        }

                        lstChansons.Items.Clear();
                        AfficherListeChansons();
                        AfficherDetailsAlbum();
                        _gestionMusique.EnregistrerChansons();
                        MessageBox.Show("La modification a été effectué avec succès !", "Modifier une chansons", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner une chanson dans la liste ou en créer une.", "Modifier une chansons", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            //Implémenter la méthode ModifierChanson FAIT
            
        }

        /// <summary>
        /// Permet de supprimer une chanson sélectionné dans la liste.
        /// </summary>
        /// <remarks>La chanson sélectionnée ne peut pas être nulle. Si oui, alors on affiche un message à l'utilisateur.
        /// Sinon, alors on demande à l'utilisateur de confirmer la suppression. Si oui, alors on supprimer la chanson, 
        /// enregistre les données de la chanson, met à jour l'affichage et affiche un message de confirmation à l'utilisateur.</remarks>
        private void SupprimerChanson()
        {
            if (lstChansons.SelectedIndex != -1)
            {
                InitialiserLecteurMusique();
                Album selectAlbum = (Album)lstAlbums.SelectedItem;
                Chanson selectChanson = (Chanson)lstChansons.SelectedItem;
                MessageBoxResult resultat = MessageBox.Show($"Voulez-vous vraiment supprimer la chanson {selectChanson.Titre} ?",
                    "Supprimer une chanson", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultat ==  MessageBoxResult.Yes)
                {
                    File.Delete(GestionMusique.CHEMIN_DOSSIER_MP3 + "\\" + selectChanson.Fichier);
                    selectAlbum.SupprimerChanson((Chanson)lstChansons.SelectedItem);
                    AfficherDetailsAlbum();
                    AfficherListeChansons();
                    _gestionMusique.EnregistrerChansons();

                    MessageBox.Show("La suppression de la chanson s'est effectuée avec succès !", "Supprimer une chanson", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("La suppression de la chanson ne s'est pas effectuée.", "Supprimer une chanson", MessageBoxButton.OK, MessageBoxImage.Information);

                }


            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une chanson dans la liste.", "Supprimer une chanson", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            //Implémenter la méthode SupprimerChanson
            
        }

        /// <summary>
        /// Méthode permettant de jouer une chanson à partir du Media Player
        /// </summary>
        /// <remarks>La chanson ne doit pas être nulle</remarks>
        /// <param name="chanson">La chanson à jouer</param>
        private void JouerChanson(Chanson chanson)
        {
            if (chanson != null)
            {

                timer.Stop();

                _mediaPlayer.Stop();

                //Mise à jour de la sélection de la chanson dans la liste des chansons.
                lstChansons.SelectedItem = chanson;

             
                //todo: obtenir le nom du fichier pour l'ouverture.
                string fichier = GestionMusique.CHEMIN_DOSSIER_MP3+"\\"+chanson.Fichier;

                //Overture du fichier
                _mediaPlayer.Open(new Uri(fichier));

                //Jouer 
                _mediaPlayer.Play();

                //Modification de l'image du bouton jouer / pause
                imgPlayPause.Source = new BitmapImage(new Uri(RESSOURCE_URI + IMAGE_PAUSE));

                //Démarrage du timer pour la mise à jour de la durée.
                timer.Start();
            }

        }

        /// <summary>
        /// Permet de jouer une chanson sélectionnée dans la liste des chansons
        /// </summary>
        /// <remarks>L'album et la chanson sélectionné ne peuvent doivent pas êtres nuls</remarks>
        private void JouerChansonSelectionnee()
        {
            if (lstAlbums.SelectedItem != null)
            {
                Chanson selectChanson = ((Album) lstAlbums.SelectedItem).ObtenirChanson((byte)lstChansons.SelectedIndex);
                JouerChanson(selectChanson);
            }
        }

        /// <summary>
        /// Permet de jouer la chanson précédente dans l'album
        /// et de jouer la chanson
        /// </summary>
        /// <remarks>L'album et la chanson sélectionné ne peuvent doivent pas êtres nuls</remarks>
        private void JouerChansonPrecedente()
        {
            if (lstAlbums.SelectedItem != null)
            {
                Chanson previousSong = ((Album)lstAlbums.SelectedItem).ObtenirChansonPrecedente();
                JouerChanson(previousSong);
            }
        }

        /// <summary>
        /// Permet de jouer la chanson suivante de l'album
        /// </summary>
        private void JouerChansonSuivante()
        {
            if (lstAlbums.SelectedItem != null)
            {
                Chanson nextSong = ((Album)lstAlbums.SelectedItem).ObtenirChansonSuivante();
                JouerChanson(nextSong);
            }
            
        }

        #endregion


    }
}
