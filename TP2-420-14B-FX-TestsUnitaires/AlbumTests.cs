using System;
using TP2_420_14B_FX.Classes;
using Xunit;

namespace TP2_420_14B_FX_TestsUnitaires
{
    public class AlbumTests
    {

        private Album CreerAlbum()
        {
            Guid id = Guid.NewGuid();

          
            return new Album(id, "Un album", 2022, "Un artiste", id + ".jpg");

            
        }

        private Chanson CreerChanson()
        {
            Guid id = Guid.NewGuid();
            return new Chanson(Guid.NewGuid(), "Une chanson", TP2_420_14B_FX.Enums.StyleMusical.Country, new TimeSpan(0, 1, 0), id + ".mp3");
            

        }

        [Fact]
        public void Id_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Egale_Zero()
        {
            //Arrange
            Guid id = new Guid();
            
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new Album(id,"Un album",2022,"un artiste","image.jpg"));
        }

        [Fact]
        public void Id_Set_Devrait_Modifier_Id_Quand_Value_Different_De_Zero()
        {
            //Arrange && Act
            Album album = CreerAlbum();

            //Assert
            Assert.NotEqual(Guid.Empty, album.Id);

        }

        [Fact]
        public void Titre_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Null_Vide_Ou_WhiteSpace()
        {
            //Arrange 
            Album album = CreerAlbum();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => album.Titre = null);
            Assert.Throws<ArgumentNullException>(() => album.Titre = "");
            Assert.Throws<ArgumentNullException>(() => album.Titre = "     ");


        }

        [Fact]
        public void Titre_Set_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = album.Titre;

            //Act 
            album.Titre = " " + album.Titre + " ";

            //Assert
            Assert.Equal(resultatAttendu, album.Titre);
        }

        [Fact]
        public void Titre_Set_Devrait_Modifier_Titre()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = "un autre album";

            //Act 
            album.Titre = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, album.Titre);
        }

        [Fact]
        public void Annee_Set_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Value_inferieur_A_1500_Ou_Superieur_A_Annee_Courante()
        {
            //Arrange 
            Album album = CreerAlbum();
            

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => album.Annee = 1499);
            Assert.Throws<ArgumentOutOfRangeException>(() => album.Annee = (ushort) DateTime.Now.AddYears(1).Year);


        }

        [Fact]
        public void Annee_Set_Devrait_Modifier_Annee()
        {
            //Arrange 
            Album album = CreerAlbum();
            ushort resultatAttendu = 2021;

            //Act
            album.Annee = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, album.Annee);


        }

        [Fact]
        public void Image_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Null_Vide_Ou_WhiteSpace()
        {
            //Arrange 
            Album album = CreerAlbum();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => album.Image = null);
            Assert.Throws<ArgumentNullException>(() => album.Image = "");
            Assert.Throws<ArgumentNullException>(() => album.Image = "     ");


        }

        [Fact]
        public void Image_Set_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = album.Image;

            //Act 
            album.Image = " " + album.Image + " ";

            //Assert
            Assert.Equal(resultatAttendu, album.Image);
        }

        [Fact]
        public void Image_Set_Devrait_Modifier_Titre()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = "uneautreimage.jpg";

            //Act 
            album.Image = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, album.Image);
        }


        [Fact]
        public void Artiste_Set_Devrait_Lancer_ArgumentNullException_Quand_Value_Null_Vide_Ou_WhiteSpace()
        {
            //Arrange 
            Album album = CreerAlbum();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => album.Artiste = null);
            Assert.Throws<ArgumentNullException>(() => album.Artiste = "");
            Assert.Throws<ArgumentNullException>(() => album.Artiste = "     ");


        }

        [Fact]
        public void Artiste_Set_Devrait_Retirer_Espaces_Inutiles()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = album.Artiste;

            //Act 
            album.Artiste = " " + album.Artiste + " ";

            //Assert
            Assert.Equal(resultatAttendu, album.Artiste);
        }

        [Fact]
        public void Artiste_Set_Devrait_Modifier_Titre()
        {
            //Arrange 
            Album album = CreerAlbum();
            string resultatAttendu = "Un autre artiste";

            //Act 
            album.Artiste = resultatAttendu;

            //Assert
            Assert.Equal(resultatAttendu, album.Artiste);
        }

       [Fact]
       public void NbChansons_Get_Devrait_Retourner_Nombre_De_Chansons()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            int resultatAttendu = 1;

            //Assert
            album.AjouterChanson(chanson);

            //Assert
            Assert.Equal(resultatAttendu, album.NbChansons);


        }

        [Fact]
        public void Duree_Get_Devrait_Retourner_Duree_Totale()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            TimeSpan duree = chanson.Duree;

            //Act
            album.AjouterChanson(chanson);

            //Assert
            Assert.Equal(duree, album.Duree);

        }

        [Fact]
        public void Constructeur_Devrait_Creer_Album_Quand_Proprietes_Valides()
        {
            //Arrange 
            Guid id = Guid.NewGuid();
            string titre = "Un Titre";
            ushort annee = (ushort)DateTime.Now.Year;
            string artiste = "Un artiste";
            string image = id + ".jpg";
            bool aleatoire = false;
            byte position = 0;

            //Act
            Album album = new Album(id, titre, annee, artiste, image);

            //Assert
            Assert.Equal(id, album.Id);
            Assert.Equal(titre, album.Titre);
            Assert.Equal(annee, album.Annee);
            Assert.Equal(artiste, album.Artiste);
            Assert.Equal(image, album.Image);
            Assert.Equal(position, album.Position);
            Assert.Equal(aleatoire, album.Aleatoire);
            Assert.Equal(0, album.NbChansons);
        }

        [Fact]
        public void ObtenirChanson_Devrait_Lancer_IndexOutOfRangeException_Quand_Index_Invalide()
        {
            //Arrange
            Album album = CreerAlbum();
            

            //Act and Assert
            Assert.Throws<IndexOutOfRangeException>(() => album.ObtenirChanson(0));
        }

        [Fact]
        public void ObtenirChanson_Devrait_Retourner_Chanson_Quand_Index_Valide()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();

            //Act
            album.AjouterChanson(chanson);


            //Act and Assert
            Assert.Same(album.ObtenirChanson(0), chanson);
        }

        [Fact]
        public void ObtenirChanson_Devrait_Modifier_Position()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Une autre chanson";
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
            int resultAttendu = 1;

            //Act
            album.ObtenirChanson(1);

            //Act and Assert
            Assert.Equal(resultAttendu, album.Position);
        }


        

        [Fact]
        public void ObtenirChansonSuivante_Devrait_Retourner_Chanson_Suivante_Quand_Lecture_Non_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Une autre chanson";
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);

            //Act
            Chanson chansonSuivante = album.ObtenirChansonSuivante();

            //Assert
            Assert.Same(chanson2, chansonSuivante);

        }

        [Fact]
        public void ObtenirChansonSuivante_Devrait_Null_Quand_Aucune_Chanson()
        {
            //Arrange
            Album album = CreerAlbum();
           
            //Act
            Chanson chanson = album.ObtenirChansonSuivante();


            //Assert
            Assert.Null(chanson);

        }

        [Fact]
        public void ObtenirChansonSuivante_Devrait_Retourner_Premiere_Chanson_Quand_Position_Egale_Derniere_Chanson_Et_Lecture_Non_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Une autre chanson";
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
            Chanson chanson = album.ObtenirChanson(1);

            //Act
            chanson = album.ObtenirChansonSuivante();
            

            //Assert
            Assert.Same(chanson1, chanson);

        }

        [Fact]
        public void ObtenirChansonSuivante_Devrait_Retourner_Chanson_Differente_Quand_Lecture_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            album.Aleatoire = true;
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Deuxi�me chanson";
            Chanson chanson3 = CreerChanson();
            chanson3.Titre = "Troisi�me chanson";
       
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
            album.AjouterChanson(chanson3);


            //Act
            Chanson chanson = album.ObtenirChansonSuivante();


            //Assert
            Assert.NotSame(chanson1, chanson);

        }

        [Fact]
        public void ObtenirChansonPrecedente_Devrait_Retourner_Chanson_Precedente_Quand_Lecture_Non_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Une autre chanson";
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
            Chanson chason = album.ObtenirChanson(1);

            //Act
            Chanson chansonPrecedente = album.ObtenirChansonPrecedente();

            //Assert
            Assert.Same(chanson1, chansonPrecedente);

        }

        [Fact]
        public void ObtenirChansonPrecedente_Devrait_Retourner_Premiere_Chanson_Quand_Position_Egale_Premiere_Chanson_Et_Lecture_Non_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Une autre chanson";
            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
    

            //Act
            Chanson chanson = album.ObtenirChansonPrecedente();


            //Assert
            Assert.Same(chanson1, chanson);

        }

        [Fact]
        public void ObtenirChansonPrecedente_Devrait_Retourner_Chanson_Differente_Quand_Lecture_Aleatoire()
        {
            //Arrange
            Album album = CreerAlbum();
            album.Aleatoire = true;
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "Deuxi�me chanson";
            Chanson chanson3 = CreerChanson();
            chanson3.Titre = "Troisi�me chanson";

            album.AjouterChanson(chanson1);
            album.AjouterChanson(chanson2);
            album.AjouterChanson(chanson3);


            //Act
            Chanson chanson = album.ObtenirChansonPrecedente();


            //Assert
            Assert.NotSame(chanson1, chanson);

        }

        [Fact]
        public void ObtenirChansonPrecedente_Devrait_Retourner_Null_Quand_Aucune_Chanson()
        {
            //Arrange
            Album album = CreerAlbum();
            album.Aleatoire = true;
            
            //Act
            Chanson chanson = album.ObtenirChansonPrecedente();


            //Assert
            Assert.Null(chanson);

        }

        [Fact]
        public void AjouterChanson_Devrait_Lancer_ArgumentNullException_Quand_Chanson_Null()
        {

            //Arrange
            Album album = CreerAlbum();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => album.AjouterChanson(null));
        }

        [Fact]
        public void AjouterChanson_Devrait_Lancer_InvalidOperationException_Quand_Chanson_Identique_Existe_Dans_Album()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
            Chanson chanson2 = CreerChanson();
            album.AjouterChanson(chanson1);

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => album.AjouterChanson(chanson2));
        }

        [Fact]
        public void AjouterChanson_Devrait_Ajouter_Chanson()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson1 = CreerChanson();
           
            //Act
            album.AjouterChanson(chanson1);

            //Act and Assert
            Assert.Same(chanson1, album.ObtenirChanson(0));
        }

        [Fact]
        public void SupprimerChanson_Devrait_Lancer_ArgumentNullException_Quand_Chanson_Null()
        {

            //Arrange
            Album album = CreerAlbum();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => album.SupprimerChanson(null));
        }

        [Fact]
        public void SupprimerChanson_Devrait_Supprimer()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            album.AjouterChanson(chanson);

            //Act 
            album.SupprimerChanson(chanson);

            //Assert
            Assert.Equal(0, album.NbChansons);

        }

        [Fact]
        public void SupprimerChanson_Devrait_Retourner_True_Quand_Chanson_Supprimee()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            album.AjouterChanson(chanson);

            //Act 
            bool resultat = album.SupprimerChanson(chanson);

            //Assert
            Assert.True(resultat);

        }

        [Fact]
        public void SupprimerChanson_Ne_Devrait_Pas_Supprimer_Chason_Si_Chanson_Existe_Pas()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "adfasdfa";

            album.AjouterChanson(chanson);
     

            //Act 
            album.SupprimerChanson(chanson2);

            //Assert
            Assert.Equal(1, album.NbChansons);

        }

        [Fact]
        public void SupprimerChanson_Devrait_Retourner_False_Quand_Chanson_Existe_Pas()
        {

            //Arrange
            Album album = CreerAlbum();
            Chanson chanson = CreerChanson();
            Chanson chanson2 = CreerChanson();
            chanson2.Titre = "asdfasdf";
            album.AjouterChanson(chanson);

            //Act 
            bool resultat = album.SupprimerChanson(chanson2);

            //Assert
            Assert.False(resultat);

        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Un_Quand_Obj_Null()
        {
            //Arrange
            Album album = CreerAlbum();
          

            int resultatAttendu = 1;


            //Act
            int resultat = album.CompareTo(null);

            //Assert
            Assert.Equal(resultatAttendu, resultat);

        }

        [Fact]
        public void CompareTo_Devrait_Lancer_ArgumentException_Quand_Obj_Pas_Un_Album()
        {
            //Arrange
            Album album = CreerAlbum();

            //Act & Arrange
            Assert.Throws<ArgumentException>(() => album.CompareTo("test"));

        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Moin_Un_Quand_Titre_Plus_Petit()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album1.Titre = "Album A";
            album2.Titre = "Album B";

            int resultatAttendu = -1;


            //Act
            int resultat = album1.CompareTo(album2);

            //Assert
            Assert.Equal(resultatAttendu, resultat);
            
        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Un_Quand_Titre_Plus_Grand()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album1.Titre = "Album A";
            album2.Titre = "Album B";

            int resultatAttendu = 1;


            //Act
            int resultat = album2.CompareTo(album1);

            //Assert
            Assert.Equal(resultatAttendu, resultat);

        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Moins_Un_Quand_Titre_Identique_Mais_Anne_Inferieure()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album1.Annee = (ushort)(album1.Annee - 1);
      

            int resultatAttendu = -1;


            //Act
            int resultat = album1.CompareTo(album2);

            //Assert
            Assert.Equal(resultatAttendu, resultat);

        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Un_Quand_Titre_Identique_Mais_Anne_Superieure()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album1.Annee = (ushort)(album1.Annee -1);


            int resultatAttendu = 1;


            //Act
            int resultat = album2.CompareTo(album1);

            //Assert
            Assert.Equal(resultatAttendu, resultat);

        }

        [Fact]
        public void CompareTo_Devrait_Retourner_Zero_Quand_Titre_Et_Annee_Identique()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
         


            int resultatAttendu = 0;


            //Act
            int resultat = album2.CompareTo(album1);

            //Assert
            Assert.Equal(resultatAttendu, resultat);

        }

        [Fact]
        public void Equals_Devrait_Retourner_False_Si_Obj_Null()
        {
            //Arrange
            Album album1 = CreerAlbum();

            //Act
            bool resultat = album1.Equals(null);

            //Assert
            Assert.False(resultat);

        }

        [Fact]
        public void Equals_Devrait_Retourner_True_Si_Meme_Objet()
        {
            //Arrange
            Album album1 = CreerAlbum();

            //Act
            bool resultat = album1.Equals(album1);

            //Assert
            Assert.True(resultat);

        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_True_Si_AlbumGauche_Null_Et_AlbumDroite_Null()
        {
            //Arrange
            Album album1 = null, album2 = null;

            //Act
            bool resultat = album1 == album2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_False_Si_AlbumGauche_Null_Et_AlbumDroite_Non_Null()
        {
            //Arrange
            Album album1 = null;
            Album album2 = CreerAlbum();

            //Act
            bool resultat = album1 == album2;

            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_False_Si_AlbumGauche_Et_AlbumDroite_Differents()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album2.Titre = "Deuxi�me album";

            //Act
            bool resultat = album1 == album2;

            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void OperateurEgale_Devrait_Retourner_True_Si_AlbumGauche_Et_AlbumDroite_Identique()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            

            //Act
            bool resultat = album1 == album2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurDifferent_Devrait_Retourner_True_Si_AlbumGauche_Et_AlbumDroite_Differents()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();
            album2.Titre = "Deuxi�me album";

            //Act
            bool resultat = album1 != album2;

            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void OperateurDifferent_Devrait_Retourner_False_Si_AlbumGauche_Et_AlbumDroite_Identique()
        {
            //Arrange
            Album album1 = CreerAlbum();
            Album album2 = CreerAlbum();


            //Act
            bool resultat = album1 != album2;

            //Assert
            Assert.False(resultat);
        }







    }
}
