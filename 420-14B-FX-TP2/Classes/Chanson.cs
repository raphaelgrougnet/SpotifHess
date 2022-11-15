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
                if (value != Guid.Empty)
                {
                    _id = value;
                }
                else
                {
                    throw new ArgumentNullException("Id", "L'id ne peut pas être nul");
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
                else
                {
                    throw new ArgumentNullException("Titre", "Le titre ne peut pas être nul");
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
                if (value != TimeSpan.Zero)
                {
                    _duree = value;
                }
                else
                {
                    throw new ArgumentNullException("Durée", "La durée ne peut pas être nulle");
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
                else
                {
                    throw new ArgumentNullException("Fichier", "Le fichier ne peut pas être nul");
                }
            }
        }


        #endregion

        #region CONSTRUCTEURS   

        /// <summary>
        /// Constructeur de chanson
        /// </summary>
        /// <param name="id">L'id de la chanson</param>
        /// <param name="titre">Titre de la chanson</param>
        /// <param name="style">Style de la chanson</param>
        /// <param name="duree">Durée de la chanson</param>
        /// <param name="fichier">Lien d'accès au fichier de la chanson</param>
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

        /// <summary>
        /// Surcharge de la méthode ToString déjà existante
        /// </summary>
        /// <returns>Le titre, le style et la durée du la chanson, formatée avec des espaces</returns>
        public override string ToString()
        {
            string tempTitre = Titre.PadRight(40);
            string tempStyle = Style.ToString().PadRight(20);

            return String.Format(tempTitre + tempStyle + Duree);
        }


        /// <summary>
        /// Définie l'operator == pour les chansons
        /// </summary>
        /// <param name="chansonGauche">Une chanson</param>
        /// <param name="chansonDroite">Une autre chanson</param>
        /// <returns>Retourne vrai si les chansons sont égales sinon retourn faux</returns>
        public static bool operator == (Chanson chansonGauche, Chanson chansonDroite)
        {
            if (Object.ReferenceEquals(chansonGauche, chansonDroite))
                return true;
            else if (chansonGauche is null || chansonDroite is null)
                return false;
            else if (chansonGauche.Titre == chansonDroite.Titre && chansonGauche.Style == chansonDroite.Style &&
                    chansonGauche.Duree == chansonDroite.Duree)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Définie l'operator != pour les chansons
        /// </summary>
        /// <param name="chansonGauche">Une chanson</param>
        /// <param name="chansonDroite">Une autre chanson</param>
        /// <returns>Retourne vrai si les chanson sont différents sinon retourne faux</returns>
        public static bool operator !=(Chanson chansonGauche, Chanson chansonDroite)
        {
            return !(chansonGauche == chansonDroite);
        }



        /// <summary>
        /// Définie le Equals pour les chansons
        /// </summary>
        /// <param name="obj">Un objet</param>
        /// <returns>Retourne vrai si les deux chansons sont identiques sinon retourne faux</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Chanson))
                return false;

            return this == (Chanson)obj;
        }


    


        #endregion












    }
}
