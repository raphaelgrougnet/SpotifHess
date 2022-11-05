using System;
using System.Collections.Generic;

namespace TP2_420_14B_FX.Classes
{
    /// <summary>
    /// Représente un album de musique
    /// </summary>
    public class Album 
    {

        #region CONSTANTES

        /// <summary>
        /// L'année minimum que année peut prendre
        /// </summary>
        public const ushort ANNEE_MIN = 1500;


        #endregion

        #region ATTRIBUTS
        /// <summary>
        /// Générateur de nombre aléatoire pour obtenir un chanson.
        /// </summary>
        private static Random _random;

        /// <summary>
        /// Position courante pour l'obention d'une chanson dans l'album.
        /// </summary>
        private byte _position;

        /// <summary>
        /// Indique si la l'obtention des chansons se faite de manière aléatoire.
        /// </summary>
        private bool _aleatoire;



        /// <summary>
        /// Identifiant unique d'un album
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Titre de l'album
        /// </summary>
        private string _titre;

        /// <summary>
        /// Année de lancement de l'album
        /// </summary>
        private ushort _annee;

        /// <summary>
        /// Nom de l'image
        /// </summary>
        private string _image;

        /// <summary>
        /// Nom de l'artiste
        /// </summary>
        private string _artiste;

        /// <summary>
        /// Contient la liste des chansons
        /// </summary>
        private List<Chanson> _chansons;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        /// <summary>
        /// Obtient la position de la chanson courante dans l'album.
        /// </summary>
        public byte Position
        {
            get { return _position; }

        }

        /// <summary>
        /// Obtient ou définit si l'obtention d'une chanson se 
        /// fait de manière aléatoire ou non.
        /// </summary>
        public bool Aleatoire
        {
            get { return _aleatoire; }
            set { _aleatoire = value; }
        }

        /// <summary>
        /// Obtient ou définit l'identifiant unique d'un album
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set
            {

                if (value != Guid.Empty)
                {
                    _id = value;
                }
                else
                {
                    throw new ArgumentNullException("Guid", "Le Guid ne peut pas être nulle");
                }
                
            }
        }

        /// <summary>
        /// Obtien ou définit le titre d'un album.
        /// </summary>
        /// <remarks>Le titre doit jamais contenir d'espace inutile, être nul, vide ou ne contenir que des espaces.</remarks>
        public string Titre
        {
            get { return _titre; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value.Trim()))
                {
                    _titre = value.Trim();
                }
                else
                {
                    throw new ArgumentNullException("Titre", "Le titre ne peut pas être vide, nulle ou ne contenir que des espaces");
                }
               
            }
        }

        /// <summary>
        /// Obtien ou  définit l'année du lancement de l'album.
        /// </summary>
        public ushort Annee
        {
            get { return _annee; }
            set
            {
                if (value >= ANNEE_MIN && value <= DateTime.Now.Year)
                {
                    _annee = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Année", $"L'année doit être comprise entre 1500 et {DateTime.Now.Year}");
                }
                
            }
        }

        /// <summary>
        /// Obtient ou définit le nom de l'image
        /// </summary>
        /// <remarks>Le nom de l'image ne doit jamais contenir d'espaces inutiles n'y être nul, vide ou ne contenir que des espaces</remarks>
        public string Image
        {
            get { return _image; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value.Trim()))
                {
                    _image = value.Trim();
                }
                else
                {
                    throw new ArgumentNullException("Titre", "Le titre ne peut pas être vide, nulle ou ne contenir que des espaces");
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le nom de l'artiste
        /// </summary>
        /// <remarks>Le nom de l'artiste ne doit jamais contenir des espaces inutiles n'y être nul, vide ou ne contenir que des espaces.</remarks>
        public string Artiste
        {
            get { return _artiste; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value.Trim()))
                {
                    _artiste = value.Trim();
                }
                else
                {
                    throw new ArgumentNullException("Artiste", "Le nom de l'artiste ne peut pas être vide, nulle ou ne contenir que des espaces");
                }
                
            }
        }

        /// <summary>
        /// Obtient la durée total de l'album
        /// </summary>
        public TimeSpan Duree
        {
            get
            {
                TimeSpan duree = TimeSpan.Parse("00:00:00");
                foreach (Chanson chanson in _chansons)
                {
                    duree += chanson.Duree;
                }
                return duree;
            }

        }

        /// <summary>
        /// Obtient le nombre de chansons dans l'album.
        /// </summary>
        public byte NbChansons
        {
            get
            {
                return (byte)_chansons.Count;
            }
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur d'album
        /// </summary>
        /// <param name="aleatoire">Obtient ou définit si l'obtention d'une chanson se 
        /// fait de manière aléatoire ou non.</param>
        /// <param name="id">L'id de l'album</param>
        /// <param name="titre">Titre de l'album</param>
        /// <param name="annee">Date de sortie de l'album</param>
        /// <param name="image">Image de l'album</param>
        /// <param name="artiste">Artiste de l'album</param>
        public Album(bool aleatoire, Guid id, string titre, ushort annee, string image, string artiste)
        {
            Aleatoire = aleatoire;
            Id = id;
            Titre = titre;
            Annee = annee;
            Image = image;
            Artiste = artiste;
            _position = 0;
            _random = new Random();
            _chansons = new List<Chanson>();
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Permet d'obtenir une chanson grâce a l'index donné
        /// </summary>
        /// <param name="index">index de la chanson</param>
        /// <returns>La chanson sélectionnée grâce à son index</returns>
        public Chanson ObtenirChanson(byte index)
        {
            if (index > NbChansons)
            {
                throw new IndexOutOfRangeException("L'index dépasse le nombre de chansons");
            }
            else
            {
                _position = index;
                return _chansons[index];
            }
               
            
        }

        /// <summary>
        /// Permet d'obtenir la chanson suivante en fonction de si le mode aléatoire est activé ou non
        /// </summary>
        /// <returns>Si la chanson jouée est la dernière et que le mode aléatoire est désactivé, retourne la première chanson, sinon la chanson suivante</returns>
        public Chanson ObtenirChansonSuivante()
        {

            if (!Aleatoire)
            {
                if (Position == NbChansons - 1)
                {
                    return ObtenirChanson(0);
                }
                return ObtenirChanson((byte)(Position + 1));
            }
            return ObtenirChanson(ObtenirPositionAleatoire());
        }

        /// <summary>
        /// Cherche si la chanson existe déjà
        /// </summary>
        /// <param name="pChanson">Un chanson</param>
        /// <returns>Return true si la chanson existe dans l'album ou false si elle n'existe pas</returns>
        private bool ChansonExiste(Chanson pChanson)
        {
            try
            {
                if (_chansons.Equals(pChanson))
                    return true;
            }
            catch(ArgumentNullException estNull)
            {
                throw estNull;
            }
            
            return false;

        }

        //public Chanson ObtenirChanson(byte index)
        //{
        //    try
        //    {
        //        _position = index;
        //        int i = 0;

        //        foreach (Chanson pChanson in _chansons)
        //        {
        //            if (i == _position)
        //            {
        //                return pChanson;
        //            }
        //        }
        //    }
        //    catch(IndexOutOfRangeException outOfRange)
        //    {
        //        throw outOfRange;
        //    }
            
        //}

        /// <summary>
        /// Obtient une position aléatoire
        /// </summary>
        /// <returns>Un nombre aléatoire </returns>
        private byte ObtenirPositionAleatoire()
        {
            byte index = _position;
            if(_chansons.Count == 0)
            {
                return 0;
            }
            else if(_chansons.Count == 1)
            {
                return 1;
            }
            else
            {
                while(index == _position)
                {
                    _random = new Random();
                    byte index1 = Convert.ToByte(_random.Next(0, (_chansons.Count + 1)));

                    index = index1;
                }
                return index;
            }
        }

        #endregion


        /// <summary>
        /// Permet d'obtenir la chanson précédente en fonction de si le mode aléatoire est activé ou non
        /// </summary>
        /// <returns>Si la chanson jouée est la première et que le mode aléatoire est désactivé, retourne la première chanson, sinon la chanson suivante</returns>
        public Chanson ObtenirChansonPrecedente()
        {
            if (!Aleatoire)
            {
                if (Position == 0)
                {
                    return ObtenirChanson(0);
                }
                return ObtenirChanson((byte)(Position - 1));
            }
            return ObtenirChanson(ObtenirPositionAleatoire());
        }

        /// <summary>
        /// Ajoute une nouvelle chanson si elle n'est pas null ni si elle existe déjà
        /// </summary>
        /// <param name="pChanson">Un chanson</param>
        public void AjouterChanson(Chanson pChanson)
        {
            if (ChansonExiste(pChanson))
            {
                throw new Exception("La chanson existe déjà dans l'album");
            }
            else
            {
                _chansons.Add(pChanson);
            }
        }

        public bool SupprimerChanson(Chanson pChanson)
        {

        }





















    }
}
