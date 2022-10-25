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
               //todo: Implémenter la validation
                _id = value;
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
                //todo: Implémenter la validation
                _titre = value;
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
                //todo: Implémenter la validation
                _annee = value;
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
                //todo: Implémenter la validation
                _image = value.Trim();
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
                //todo: Implémenter la validation
                _artiste = value;
            }
        }

        /// <summary>
        /// Obtient la durée total de l'album
        /// </summary>
        public TimeSpan Duree
        {
            get
            {

                //todo: Implémenter la validation
                throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Obtient le nombre de chansons dans l'album.
        /// </summary>
        public byte NbChansons
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region CONSTRUCTEURS

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Permet d'obtenir une chanson grâce a l'index donné
        /// </summary>
        /// <param name="index">index de la chanson</param>
        /// <returns>La chanson sélectionnée grâce à son index</returns>
        public Chanson ObtenirChanson(byte index)
        {
            return _chansons[index];
        }

        /// <summary>
        /// Permet d'obtenir la chanson suivante en fonction de si le mode aléatoire est activé ou non
        /// </summary>
        /// <returns>Si la chanson jouée est la dernière et que le mode aléatoire est désactivé, retourne la première chanson, sinon la chanson suivante</returns>
        public Chanson ObtenirChansonSuivante()
        {
            if (!Aleatoire)
            {
                if (Position == NbChansons-1)
                {
                    return ObtenirChanson(0);
                }
                return ObtenirChanson((byte)(Position + 1));
            }
            return ObtenirChanson(ObtenirPositionAletoire());
        }
        #endregion
























    }
}
