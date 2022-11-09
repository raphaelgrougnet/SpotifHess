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
            _albums = new List<Album>();
            ChargerAlbum();
            ChargerChansons();
            
        }
        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de charger toutes les chansons (contenues dans les fichiers sources) dans une List.
        /// </summary>
        private void ChargerChansons()
        {
            //Je supprime la première ligne inutile
            string[] vectDonnesChansons = Utilitaire.ChargerDonnees(CHEMIN_FICHIER_CHANSONS);

            string[] newVectDonneesChansons = new string[vectDonnesChansons.Length - 1]; 

            vectDonnesChansons[0] = "";

            int i = 0;
            foreach (string ligne in vectDonnesChansons)
            {
                if (ligne != "")
                {
                    newVectDonneesChansons[i] = ligne;
                    i++;
                }
            }

            vectDonnesChansons = newVectDonneesChansons;
            //Fin de la suppression de la première ligne

            string[] ligneSplited;

            foreach (string ligne in vectDonnesChansons)
            {
                ///Fragmentation de la ligne en string
                ligneSplited = ligne.Split(';');
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

        /// <summary>
        /// Charge les albums dans la liste des albums
        /// </summary>
        private void ChargerAlbum()
        {
            string[] vectDonnesAlbums = Utilitaire.ChargerDonnees(CHEMIN_FICHIER_ALBUMS);

            string[] vectNouvDonnesAlbums = new string[vectDonnesAlbums.Length - 1];

            vectDonnesAlbums[0] = "";

            int j = 0;

            foreach(string ligne in vectDonnesAlbums)
            {
                if(ligne != "")
                {
                    vectNouvDonnesAlbums[j] = ligne;

                    j++;
                }
            }

            vectDonnesAlbums = vectNouvDonnesAlbums;

            for(int i = 0; i < vectDonnesAlbums.Length; i++)
            {
                string[] vectChamp = vectDonnesAlbums[i].Split(";");

                bool aleatoire = false;

                Guid id = Guid.Parse(vectChamp[0].Trim());

                string titre = vectChamp[1].Trim();

                string artiste = vectChamp[2].Trim();

                ushort annee = ushort.Parse(vectChamp[3].Trim());

                string image = vectChamp[4].Trim();



                Album nouvAlbum = new Album(aleatoire, id, titre, annee, image, artiste);

                _albums.Add(nouvAlbum);
            }

            
        }

        /// <summary>
        /// Ajoute un nouveau album dans la liste
        /// </summary>
        /// <param name="album">Un album</param>
        public void AjouterAlbum(Album album)
        {
            if (!AlbumExiste(album))
                _albums.Add(album);
            else if (album is null)
                throw new ArgumentNullException("Album", "Un album ne peut pas être null");
            else
                throw new Exception("L'album existe déjà");
        }

        /// <summary>
        /// Obtient un album grâce à son Id
        /// </summary>
        /// <param name="id">Code unique d'un album</param>
        /// <returns>Retourne un album selon son id s'il n'existe pas, retourne null</returns>
        private Album ObtenirAlbum(Guid id)
        {
            foreach(Album album in _albums)
            {
                if(album.Id == id)
                {
                    return album;
                }
            }

            return null;
        }

        /// <summary>
        /// Obtient un album selon la position spécifiée
        /// </summary>
        /// <param name="index">Position spécificée</param>
        /// <returns>Retoune un album</returns>
        public Album ObtenirAlbum(int index)
        {

            for(int i = 0; i < _albums.Count; i++)
            {
                if (i == index)
                    return _albums[i];
            }

            throw new IndexOutOfRangeException($"Il n'y a aucun album associé avec cette index, l'index est plus que {NbAlbums}");
        }

        /// <summary>
        /// Vérifie si l'album existe déjà dans la liste d'albums
        /// </summary>
        /// <param name="pAlbum">Un album</param>
        /// <returns>Retourne vrai s'il existe sinon retourne faux</returns>
        public bool AlbumExiste(Album pAlbum)
        {
            foreach (Album album in _albums)
            {
                if (album == pAlbum)
                    return true;
            }

            if (pAlbum is null)
                throw new ArgumentNullException("Album", "Un album ne peut pas être null");

            return false;
        }

        /// <summary>
        /// Trie la liste d'album en ordre croissant de Titre et d'année
        /// </summary>
        public void TrierAlbums()
        {
            _albums.Sort();
        }

        #endregion

    }
}
