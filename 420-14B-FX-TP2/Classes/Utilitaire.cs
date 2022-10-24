using System;
using System.IO;

namespace TP2_420_14B_FX.Classes
{
   
    /// <summary>
    /// Classe utilitaire
    /// </summary>
    public static class Utilitaire
    {


        #region MÉTHODES
        /// <summary>   
        /// Permet charger les données dans un vecteur de chaînes de caractères.
        /// </summary>
        /// <param name="pCheminFichier">Chemin d'accès au fichier</param>
        /// <returns>Un vecteur contenant le lignes du fichier. Null si aucune données dnas le fichier.</returns>
        public static String[] ChargerDonnees(String pCheminFichier)
        {
            if (File.Exists(pCheminFichier))
            {
                // Création du flux en lecture du fichier dont le chemin d'accès est "cheminFichier".
                StreamReader fluxLecture = new StreamReader(pCheminFichier);

                // Lecture du fichier en une seule instruction.
                String fichierTexte = fluxLecture.ReadToEnd();

                // Fermeture du flux vers le fichier.
                fluxLecture.Close();

                // Retrait des "carriage return" ('\r'), s'il y en a.
                fichierTexte = fichierTexte.Replace("\r", "");

                // Création d'un vecteur de chaînes de caractères contenant chaque ligne individuellement.
                String[] vectLignes = fichierTexte.Split('\n');

                // Nombre de lignes non vides dans le fichier.
                int nbLignes;

                if (vectLignes[vectLignes.Length - 1] == "")
                    nbLignes = vectLignes.Length - 1; // On ne considère pas la dernière ligne si elle est vide.
                else
                    nbLignes = vectLignes.Length;

                if (nbLignes > 0)
                {
                    // Création du vecteur contenant les données du fichier; la taille est déterminée par le nombre de lignes (non vides) du fichier.
                    String[] vectDonnees = new String[nbLignes];

                    for (int i = 0; i < vectDonnees.Length; i++)
                    {
                        vectDonnees[i] = vectLignes[i];
                    }

                    // On retourne le vecteur contenant les données créé.
                    return vectDonnees;

                }
            }

            return null;
        }

        /// <summary>
        /// Permet d'enregister des données sérialisées dans un fichier
        /// </summary>
        /// <param name="pCheminFichier">Chemin d'accès au fichier</param>
        /// <param name="pDonneesSerialises">Données à enregistrer dans le fichier</param>

        public static void EnregistrerDonnees(String pCheminFichier, string pDonneesSerialises)
        {
            
            // Écriture de tous les membres.
            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
            fluxEcriture.Write(pDonneesSerialises);
            fluxEcriture.Close();
                      
        }
        #endregion

    }  // Fin de la classe
}
