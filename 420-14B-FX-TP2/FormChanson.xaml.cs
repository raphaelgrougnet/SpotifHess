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
        MediaPlayer _mediaPlayer;

      
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



      
    }
}
