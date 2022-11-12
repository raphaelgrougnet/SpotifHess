using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using TP2_420_14B_FX.Classes;
using TP2_420_14B_FX.Enums;

namespace TP2_420_14B_FX
{
    /// <summary>
    /// Logique d'interaction pour frmChanson.xaml
    /// </summary>
    public partial class FormChanson : Window
    {



        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        MediaPlayer _mediaPlayer;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        private Guid _id;
        private string _titre;
        private StyleMusical _styleMusical;
        private TimeSpan _duree;
        private string _fichier;



        public Guid Id
        {
            get { return _id; }
            set 
            {
                if (value == Guid.Empty)
                {
                    throw new ArgumentNullException("Id", "L'id de la chanson ne peut pas être nul");
                }
                _id = value; 
            }
        }

        public string Titre
        {
            get { return _titre; }
            set
            {
                if (String.IsNullOrWhiteSpace(value.Trim()))
                {
                    throw new ArgumentNullException("Titre", "Le titre ne peut pas être vide, nul");
                }
                _titre = value.Trim(); 
            }
        }

        public StyleMusical StyleMusical
        {
            get { return _styleMusical; }
            set
            {
                if (!Enum.IsDefined(typeof(StyleMusical), value))
                {
                    
                }
                _styleMusical = value; 
            }
        }

        public TimeSpan Duree
        {
            get { return _duree; }
            set { _duree = value; }
        }

        public string Fichier
        {
            get { return _fichier; }
            set { _fichier = value; }
        }



        #endregion

        #region CONSTRUCTEURS

        #endregion

        #region MÉTHODES




        public FormChanson( Chanson chanson = null)
        {
            InitializeComponent();

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
                //todo: Afficher le durée de la chanson dans le label correspondant.
                // =  _mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
            }

        }


        #endregion

        private void txtTitre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
