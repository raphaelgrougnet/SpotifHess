using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_420_14B_FX.Classes
{
    class GestionMusique
    {

        #region CONSTANTES
        const string CHEMIN_FICHIER_ALBUMS = @"C:\data-420-14B-FX\data-tp2-420-14b\albums.csv";
        const string CHEMIN_FICHIER_CHANSONS = @"C:\data-420-14B-FX\data-tp2-420-14b\chansons.csv";
        const string CHEMIN_DOSSIER_MP3 = @"C:\data-420-14B-FX\data-tp2-420-14b\mp3";
        const string CHEMIN_IMAGES_ALBUMS = @"C:\data-420-14B-FX\data-tp2-420-14b\albums";
        #endregion

        #region ATTRIBUTS
        private List<Album> _albums;


        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public List<Album> Albums
        {
            get { return _albums; }
            set { _albums = value; }
        }

        public int NbAlbums
        {
            get { return Albums.Count; }
        }
        #endregion

        #region CONSTRUCTEURS
        public GestionMusique()
        {
            //Completer avec les classes ChargerAlbums et ChagerChansons
        }
        #endregion

        #region MÉTHODES

        #endregion

    }
}
