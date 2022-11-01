using System;
using System.IO;
using TP2_420_14B_FX.Classes;
using Xunit;

namespace TP2_420_14B_FX_TestsUnitaires
{
    public class GestionMusiqueTests
    {

        private Album CreerAlbum()
        {
            Guid id = Guid.NewGuid();


            return new Album(id, "Un album", 2022, "Un artiste", id + ".jpg");


        }

        [Fact]
        public void NbAlbums_Devrait_Retourner_Nombre_Albums()
        {
            //Arrange & Act
            GestionMusique gestionMusique = new GestionMusique();
            uint resultatAttendu = 3;

            //Assert
            Assert.Equal(resultatAttendu, gestionMusique.NbAlbums);
        }

        [Fact]
        public void GestionMusique_Devrait_ChargerAlbums()
        {   

            //Arrange && Act
            GestionMusique gestionMusique = new GestionMusique();
            uint resultatAttendu = 3;

            //Assert
            Assert.Equal(resultatAttendu, gestionMusique.NbAlbums);

        }
        
        [Fact]
        public void ObtenirAlbum_Devrait_Lancer_IndexOutOfRangeException_Si_Index_Invalide()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();

            //Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => gestionMusique.ObtenirAlbum(-1));
            Assert.Throws<IndexOutOfRangeException>(() => gestionMusique.ObtenirAlbum(3));

        }

        [Fact]
        public void ObtenirAlbum_Devrait_Retourner_Album_Si_Index_Valide()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();

            //Act
            Album album1 = gestionMusique.ObtenirAlbum(0);
            Album album2 = gestionMusique.ObtenirAlbum(1);
            Album album3 = gestionMusique.ObtenirAlbum(2);

             //Assert
            Assert.NotNull(album1);
            Assert.NotNull(album2);
            Assert.NotNull(album3);
        }

        [Fact]
        public void GestionMusique_Devrait_ChargerChansons()
        {

            //Arrange && Act
            GestionMusique gestionMusique = new GestionMusique();
            uint resultatAttendu = 6, nbAlbums = 0;

            for (int i = 0; i < gestionMusique.NbAlbums; i++)
            {
                nbAlbums += gestionMusique.ObtenirAlbum(i).NbChansons;
            }

            //Assert
            Assert.Equal(resultatAttendu, nbAlbums);

        }

        [Fact]
        public void AlbumExiste_Devrait_Lancer_ArgumentNullException_Si_Parametre_Nul()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => gestionMusique.AlbumExiste(null));

        }

        [Fact]
        public void AlbumExiste_Devrait_Retourner_True_Si_Album_Identique_Existe()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = new Album(Guid.Parse("17ff4676-bfb4-47c0-96b4-e84092baeb4e"),"Divide", 2017, "Ed Sheeran", "17ff4676-bfb4-47c0-96b4-e84092baeb4e.png");


            //Act
            bool resultat = gestionMusique.AlbumExiste(album);


            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void AlbumExiste_Devrait_Retourner_False_Si_Album_Identique_Existe_Pas()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = CreerAlbum();


            //Act
            bool resultat = gestionMusique.AlbumExiste(album);


            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void AjouterAlbum_Devrait_Lancer_ArgumentNullException_Si_Album_Null()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
           

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => gestionMusique.AjouterAlbum(null));
        }

        [Fact]
        public void AjouterAlbum_Devrait_Lancer_InvalidOperationException_Si_Album_Identique_Existe()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = new Album(Guid.Parse("17ff4676-bfb4-47c0-96b4-e84092baeb4e"), "Divide", 2017, "Ed Sheeran", "17ff4676-bfb4-47c0-96b4-e84092baeb4e.png");


            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => gestionMusique.AjouterAlbum(album));

        }

        [Fact]
        public void AjouterAlbum_Devrait_Ajouter_Album()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = CreerAlbum();
            uint resultatAttendu = gestionMusique.NbAlbums + 1;

            //Act
            gestionMusique.AjouterAlbum(album);


            //Assert
            Assert.Equal(resultatAttendu, gestionMusique.NbAlbums);


        }

        [Fact]
        public void AjouterAlbum_Devrait_Trier_Album_Apres_Ajout()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album1 = CreerAlbum();
            album1.Titre = "Divide";
            album1.Annee = 2018;
         

            //Act
            gestionMusique.AjouterAlbum(album1);


            //Assert
            Assert.Same(album1, gestionMusique.ObtenirAlbum(1));


        }

        [Fact]
        public void SupprimerAlbum_Devrait_Lancer_ArgumentNullException_Quand_Album_Null()
        {

            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => gestionMusique.SupprimerAlbum(null));
        }

        [Fact]
        public void SupprimerAlbum_Devrait_Retourner_False_Quand_Album_Non_Existant()
        {

            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = CreerAlbum();

            //Act 
            bool resultat = gestionMusique.SupprimerAlbum(album);

            //Assert
            Assert.False(resultat);
        }

        [Fact]
        public void SupprimerAlbum_Devrait_Supprimer_Album_Si_Album_Exist()
        {

            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = new Album(Guid.Parse("17ff4676-bfb4-47c0-96b4-e84092baeb4e"), "Divide", 2017, "Ed Sheeran", "17ff4676-bfb4-47c0-96b4-e84092baeb4e.png");
            uint resultatAttendu = gestionMusique.NbAlbums - 1;

            //Act 
           gestionMusique.SupprimerAlbum(album);



            //Assert
            Assert.Equal(resultatAttendu, gestionMusique.NbAlbums);
        }

        [Fact]
        public void SupprimerAlbum_Devrait_Retourner_True_Si_Album_Supprime()
        {

            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = new Album(Guid.Parse("17ff4676-bfb4-47c0-96b4-e84092baeb4e"), "Divide", 2017, "Ed Sheeran", "17ff4676-bfb4-47c0-96b4-e84092baeb4e.png");
           

            //Act 
            bool resultat = gestionMusique.SupprimerAlbum(album);



            //Assert
            Assert.True(resultat);
        }

        [Fact]
        public void EnregistrerChansons_Devrait_Enregistrer_Chansons()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            uint resultatAttendu = 7;

            //Act
            gestionMusique.EnregistrerChansons();


            string[] vectLignes = Utilitaire.ChargerDonnees(GestionMusique.CHEMIN_FICHIER_CHANSONS);


            //Assert
            Assert.Equal(resultatAttendu, (uint) vectLignes.Length);



        }

        [Fact]
        public void EnregistrerAlbums_Devrait_Enregistrer_Albums()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            uint resultatAttendu = gestionMusique.NbAlbums + 1;

            //Act
            gestionMusique.EnregistrerAlbums();


            string[] vectLignes = Utilitaire.ChargerDonnees(GestionMusique.CHEMIN_FICHIER_ALBUMS);


            //Assert
            Assert.Equal(resultatAttendu, (uint)vectLignes.Length);



        }

        [Fact]
        public void TrierAlbums_Devrait_Trier_Albums_Selon_Titre_Et_Annee()
        {
            //Arrange 
            GestionMusique gestionMusique = new GestionMusique();
            Album album = CreerAlbum();
            

            //Act
            gestionMusique.AjouterAlbum(album);
            album.Titre = "Divide";
            album.Annee = 2018;
            gestionMusique.TrierAlbums();

            //Assert
            Assert.Same(album, gestionMusique.ObtenirAlbum(1));
        }


    }
}
