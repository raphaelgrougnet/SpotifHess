using System;
using TP2_420_14B_FX.Enums;

namespace TP2_420_14B_FX.Classes
{
    /// <summary>
    /// Représente une chanson
    /// </summary>
    public class Chanson
    {


        #region CONSTANTES



        #endregion

        #region ATTRIBUTS
        /// <summary>
        /// Identifiant unique de la chanson
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Titre de la chason
        /// </summary>
        private string _titre;

        /// <summary>
        /// Style musical dela chanson
        /// </summary>
        private StyleMusical _style;

        /// <summary>
        /// Durée de la chanson
        /// </summary>
        private TimeSpan _duree;

        /// <summary>
        /// Nom du fichier de la chanson
        /// </summary>
        private string _fichier;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini l'identifiant unique de la chanson.
        /// </summary>
        /// <remark>L'identifiant ne peut pas être nul.</remark>
        public Guid Id
        {
            get { return _id; }
            set
            {
                //todo : implémenter la validation
                _id = value;
            }
        }


        /// <summary>
        /// Obtient ou définit le titre de la chanson
        /// </summary>
        /// <remarks>Le titre ne doit jamais contenir d'espaces inutiles n'y être nul, vide ou ne contenir que des espaces.</remarks>
        public string Titre
        {
            get { return _titre; }
            set
            {
                //todo : implémenter la validation
                _titre = value.Trim();
            }
        }



        /// <summary>
        /// Obtient ou définit le style musical de la chanson
        /// </summary>
        public StyleMusical Style
        {
            get { return _style; }
            set
            {
                _style = value;
            }
        }

        /// <summary>
        /// Obtient ou définit la durée de la chanson.
        /// </summary>
        ///<remarks>La durée ne peut pas être nulle.</remarks>
        public TimeSpan Duree
        {
            get { return _duree; }

            set
            {
                //todo : implémenter la validation

                _duree = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le ficheir de la chanson
        /// </summary>
        /// <remarks>Le fichier ne doit jamais contenir d'espaces inutiles, n'y être nul, vide ou ne contenir que des espaces.</remarks>
        public string Fichier
        {
            get { return _fichier; }
            set
            {
                //todo : implémenter la validation

                _fichier = value.Trim();
            }
        }
        #endregion
        
        #region CONSTRUCTEURS   
        

        #endregion

        #region MÉTHODES
        
        #endregion












    }
}
