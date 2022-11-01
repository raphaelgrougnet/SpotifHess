using System;
using System.Collections.Generic;
using System.Text;
using TP2_420_14B_FX.Enums;

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
            ChargerChansons();
        }
        #endregion

        #region MÉTHODES
        private void ChargerChansons()
        {
            //Je supprime la première ligne innutile
            string[] vectDonnesChansons = Utilitaire.ChargerDonnees(CHEMIN_FICHIER_CHANSONS);

            string[] newVectDonneesChansons = new string[vectDonnesChansons.Length - 1];

            vectDonnesChansons[0] = "";

            int i = 0;
            foreach (string ligne in vectDonnesChansons)
            {
                if (ligne != "")
                {
                    newVectDonneesChansons[i] = ligne;
                }
            }

            vectDonnesChansons = newVectDonneesChansons;
            //Fin de la suppression de la première ligne

            string[] ligneSplited;

            foreach (string ligne in vectDonnesChansons)
            {
                ///Fragmentation de la ligne en string
                ligneSplited = ligne.Split();
                Guid guid = new Guid(ligneSplited[0]);
                string titre = ligneSplited[1];
                StyleMusical style;
                Enum.TryParse<StyleMusical>(ligneSplited[2],out style);
                TimeSpan duree = TimeSpan.Parse(ligneSplited[3]);
                string fichier = ligneSplited[4];
                
                Chanson chanson = new Chanson(guid,titre,style,duree,fichier);

                Album album = ObtenirAlbum(Guid.Parse(ligneSplited[5]));

                album.AjouterChanson(chanson);
            }
            

        }

        #endregion

    }
}
