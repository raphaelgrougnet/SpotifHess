using System;
using TP2_420_14B_FX.Classes;
using TP2_420_14B_FX.Enums;
using Xunit;

namespace TP2_420_14B_FX_TestsUnitaires
{
    public class ChansonTests
    {
        private Chanson CreerChanson()
        {
            Guid id = Guid.NewGuid();
            return new Chanson(Guid.NewGuid(), "Une chanson", TP2_420_14B_FX.Enums.StyleMusical.Country, new TimeSpan(0, 1, 0), id + ".mp3");


        }

        [Fact]
        public void Id_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Egale_Zero()
        {
            Chanson chanson = CreerChanson();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new Chanson(Guid.Empty, "Une chanson", TP2_420_14B_FX.Enums.StyleMusical.Country, new TimeSpan(0, 1, 0), "unechanson.mp3"));
        }

       

        [Fact]
        public void Titre_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Null_Vide_Ou_WhiteSpace()
        {
            //Arrange 
            Chanson chanson = CreerChanson();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => chanson.Titre = null);
            Assert.Throws<ArgumentNullException>(() => chanson.Titre = "");
            Assert.Throws<ArgumentNullException>(() => chanson.Titre = "     ");


        }

        [Fact]
        public void Titre_Set_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange 
            Chanson chanson = CreerChanson();
            string resultatAttendu = chanson.Titre;

            //Act 
            chanson.Titre = " " + chanson.Titre + " ";

            //Assert
            Assert.Equal(resultatAttendu, chanson.Titre);
        }

        [Fact]
        public void Titre_Set_Devrait_Modifier_Titre()
        {
            //Arrange 
            Chanson chanson = CreerChanson();
            string resultatAttendu = "un autre album";

            //Act 
            chanson.Titre = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, chanson.Titre);
        }

        [Fact]
        public void Duree_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Zero()
        {
            //Arrange 
            Chanson chanson = CreerChanson();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => chanson.Duree = new TimeSpan());
            
        }

        [Fact]
        public void Duree_Set_Devrait_Modifier_Duree()
        {
            //Arrange 
            Chanson chanson = CreerChanson();

            TimeSpan resultatAttendu = new TimeSpan(0, 2, 0);

            //Act
            chanson.Duree = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, chanson.Duree);
          

        }

        [Fact]
        public void Fichier_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Null_Vide_Ou_WhiteSpace()
        {
            //Arrange 
            Chanson chanson = CreerChanson();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => chanson.Fichier = null);
            Assert.Throws<ArgumentNullException>(() => chanson.Fichier = "");
            Assert.Throws<ArgumentNullException>(() => chanson.Fichier = "     ");


        }

        [Fact]
        public void Fichier_Set_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange 
            Chanson chanson = CreerChanson();
            string resultatAttendu = chanson.Fichier;

            //Act 
            chanson.Fichier = " " + chanson.Fichier + " ";

            //Assert
            Assert.Equal(resultatAttendu, chanson.Fichier);
        }

        [Fact]
        public void Fichier_Set_Devrait_Modifier_Titre()
        {
            //Arrange 
            Chanson chanson = CreerChanson();
            string resultatAttendu = "unautrefichier.jpg";

            //Act 
            chanson.Fichier = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, chanson.Fichier);
        }

        [Fact]
        public void Constructeur_Devrait_Creer_Chanson_Quand_Proprietes_Valides()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string titre = "Une Chanson";
            StyleMusical style = StyleMusical.Country;
            TimeSpan duree = new TimeSpan(0, 1, 0);
            string fichier = id + ".mp3";

            //Act
            Chanson chanson = new Chanson(id, titre, style, duree, fichier);

            //Assert
            Assert.Equal(titre, chanson.Titre);
            Assert.Equal(id, chanson.Id);
            Assert.Equal(style, chanson.Style);
            Assert.Equal(duree, chanson.Duree);
            Assert.Equal(fichier, chanson.Fichier);

        }

        [Fact]
        public void Equals_Devrait_Retourner_False_Si_Obj_Null()
        {
            //Arrange
            Chanson chanson = CreerChanson();

            //Act
            bool resultat = chanson.Equals(null);

            //Assert
            Assert.False(resultat);

        }

        [Fact]
        public void Equals_Devrait_Retourner_True_Si_Meme_Objet()
        {
            //Arrange
            Chanson chanson = CreerChanson();

            //Act
            bool resultat = chanson.Equals(chanson);

            //Assert
            Assert.True(resultat);

        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_True_Si_ChansonGAuche_Null_Et_ChansonDroite_Null()
        {
            //Arrange
            Chanson chanson1 = null, chanson2 = null;

            //Act
            bool resultat = chanson1 == chanson2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_False_Si_ChansonGauche_Null_Et_ChansonDroite_Non_Null()
        {
            //Arrange
            Chanson chanson1 = null;
            Chanson chanson2 = CreerChanson();

            //Act
            bool resultat = chanson1 == chanson2;

            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_False_Si_ChansonGauche_Et_ChansonDroite_Differents()
        {
            //Arrange
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Deuxième chanson";

            //Act
            bool resultat = chanson1 == chanson2;

            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_True_Si_ChansonGauche_Et_ChansonDroite_Identique()
        {
            //Arrange
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();


            //Act
            bool resultat = chanson1 == chanson2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurDifferent_Devrait_Retourner_True_Si_ChansonGauche_Et_ChansonDroite_Differents()
        {
            //Arrange
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();

            chanson2.Titre = "Deuxième chanson";

            //Act
            bool resultat = chanson1 != chanson2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurDifferent_Devrait_Retourner_False_Si_ChansonGauche_Et_ChansonDroite_Identique()
        {
            //Arrange
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();


            //Act
            bool resultat = chanson1 != chanson2;

            //Assert
            Assert.False(resultat);
        }

    }
}
