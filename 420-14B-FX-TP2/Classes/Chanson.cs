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
                //todo : implémenter la validation FAIT
                if (value != null)
                {
                    _id = value;
                }
                
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
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _titre = value.Trim();
                }
                //todo : implémenter la validation FAIT
                
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
                //todo : implémenter la validation FAIT
                switch (value)
                {
                    case StyleMusical.Country:
                        _style = value;
                        break;
                    case StyleMusical.Dubstep:
                        _style = value;
                        break;
                    case StyleMusical.Electronic:
                        _style = value;
                        break;
                    case StyleMusical.Fork:
                        _style = value;
                        break;
                    case StyleMusical.HipHop:
                        _style = value;
                        break;
                    case StyleMusical.IndieRock:
                        _style = value;
                        break;
                    case StyleMusical.Jazz:
                        _style = value;
                        break;
                    case StyleMusical.Metal:
                        _style = value;
                        break;
                    case StyleMusical.Pop:
                        _style = value;
                        break;
                    case StyleMusical.Punk:
                        _style = value;
                        break;
                    case StyleMusical.RB:
                        _style = value;
                        break;
                    case StyleMusical.Rock:
                        _style = value;
                        break;
                    case StyleMusical.Reggae:
                        _style = value;
                        break;
                    case StyleMusical.Techno:
                        _style = value;
                        break;
                    
                }
                
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
                //todo : implémenter la validation FAIT
                if (value != null)
                {
                    _duree = value;
                }
               
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
                //todo : implémenter la validation FAIT
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _fichier = value.Trim();
                }
            }
        }


        #endregion

        #region CONSTRUCTEURS   

        public Chanson(Guid id, string titre, StyleMusical style, TimeSpan duree, string fichier)
        {
            Id = id;
            Titre = titre;
            Style = style;
            Duree = duree;
            Fichier = fichier;
        }

        #endregion

        #region MÉTHODES

        public override string ToString()
        {
            string tempTitre = Titre.PadRight(40 - Titre.Length);
            string tempStyle = Style.ToString().PadRight(20 - Style.ToString().Length);

            return String.Format(tempTitre + tempTitre + Duree);
        }

        #endregion












    }
}
