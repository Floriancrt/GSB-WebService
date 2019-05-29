using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace GSBWEB
{
    /// <summary>
    /// Description résumée de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]

        public int nbMedicaments()
        {
            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = "SELECT COUNT(*) FROM medicament";
            Int32 nb = Convert.ToInt32(cmd1.ExecuteScalar());
            cmd1.ExecuteNonQuery();
            cmd1.Parameters.Clear();

            return nb;
        }





        [WebMethod]

        public String[] afficherMedicaments()
        {
            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = "SELECT COUNT(*) FROM medicaments";
            int medoc = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();
            int i = 0;
            String[] medocs = new String[medoc];
            conn.Open();
            MySqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = "SELECT * FROM medicaments";
            MySqlDataReader tableau = cmd2.ExecuteReader();

            if (tableau.HasRows)
            {

                while (tableau.Read())
                {

                    medocs[i] = tableau["idMedicament"].ToString() + "|" + tableau["libelleMedicament"].ToString() + "|" + tableau["idFamille"].ToString();
                    i++;
                }




            }

            return medocs;
        }

        [WebMethod]

        public String[] GetEffets(string numero)
        {
            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.Parameters.AddWithValue("@numero", numero);
            cmd1.CommandText = @"
            SELECT COUNT(*)
            FROM avoir
            WHERE idMedicament = @numero";

            int taille = Convert.ToInt32(cmd1.ExecuteScalar());
            string[] effets = new string[taille];

            cmd1.CommandText = @"
            SELECT libelleEffet, descriptionEffet
            FROM effets,avoir
            WHERE avoir.idEffet = effets.idEffet
            AND idMedicament = @numero";

            MySqlDataReader reader = cmd1.ExecuteReader();


            if (reader.HasRows)
            {
                int i = 0;

                while (reader.Read())
                {
                    effets[i] = reader["libelleEffet"].ToString() + "|" + reader["descriptionEffet"].ToString();
                    i++;


                }

            }
            conn.Close();
            return effets;

        }





        [WebMethod]

        public String[] GetContreIndication(string numero)
        {
            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.Parameters.AddWithValue("@numero", numero);
            cmd1.CommandText = @"
            SELECT COUNT(*)
            FROM contenir
            WHERE idMedicament = @numero";

            int taille = Convert.ToInt32(cmd1.ExecuteScalar());
            string[] contreIndications = new string[taille];


            cmd1.CommandText = @"
            SELECT libelleContreIndication, descriptionContreIndication
            FROM contreindications,contenir
            WHERE contreindications.idContreIndication = contenir.idContreIndication
            AND idMedicament = @numero";


            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;

                while (reader.Read())
                {
                    contreIndications[i] = reader["libelleContreIndication"].ToString() + "|" + reader["descriptionContreIndication"].ToString();
                    i++;


                }

            }
            conn.Close();
            return contreIndications;


        }





        [WebMethod]

        public String[] GetComposants(string numero)
        {
            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.Parameters.AddWithValue("@numero", numero);
            cmd1.CommandText = @"
            SELECT COUNT(*)
            FROM composer
            WHERE idMedicament = @numero";

            int taille = Convert.ToInt32(cmd1.ExecuteScalar());
            string[] composants = new string[taille];


            cmd1.CommandText = @"
            SELECT libelleComposant,quantite
            FROM composants,composer
            WHERE composer.idComposant = composants.idComposant
            AND idMedicament = @numero";


            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;

                while (reader.Read())
                {
                    composants[i] = reader["libelleComposant"].ToString() + "|" + reader["quantite"].ToString();
                    i++;


                }

            }
            conn.Close();
            return composants;


        }

        [WebMethod]

        public String GetUtilisateurs(string login, string mdp)
        {

            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            string role = string.Empty;

            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.Parameters.AddWithValue("@login", login);

            cmd1.CommandText = @"
            SELECT *
            FROM utilisateurs
            WHERE login = @login";

            MySqlDataReader reader = cmd1.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                if(reader["mdp"].ToString() == mdp)
                {
                    role = reader["role"].ToString();
                }

            }

            conn.Close();
            return role;
        }


        [WebMethod]


        public String[] afficherAC()
        {


            String connString = "Server = 127.0.0.1;Database = gsb;Port = 8889;Uid = root;Password = root;SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();

            // nombre de résultats
            cmd.CommandText = @"
            SELECT COUNT(*)
            FROM activitescomp";

            string[] ac = new string[Convert.ToInt32(cmd.ExecuteScalar())]; // création du tableau

            // requête de sélection
            cmd.CommandText = @"
            SELECT idAC, etat, budgetActivitesComp, commentaires, salle, date_activite, salle, etat, idResponsable, bilan
            FROM activites
            INNER JOIN activitescomp ON activitescomp.idAC = activites.idActivite";

            // lecture des résultats
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int i = 0;

                while (reader.Read())
                {
                    ac[i] = reader["idAC"].ToString() + "|" + reader["date_activite"].ToString() + "|" + reader["bilan"].ToString() + "|" + reader["idResponsable"].ToString() + "|" + reader["budgetActivitesComp"].ToString() + "|" + reader["etat"].ToString() + "|" + reader["salle"].ToString() + "|" + reader["commentaires"].ToString();
                    i++;
                }
            }

            conn.Close();
            return ac;




        }



    }




}





    /**
           [WebMethod]

          
           public string AjouterAC(String id, String nom, String prenom, String adresse, String dateEmbauche, String idPersonnel, String idActivite, String bilan, String numOrdre, String etat, String budgetActivitesComp, String commentaires, String salle, String idResponsable, String dateActivite)
           {
               String connString = "Server = 127.0.0.1;Database = gsb;Uid = root;Password = ;SslMode = none";
               MySqlConnection conn = new MySqlConnection(connString);
               conn.Open();

               try
               {
                   MySqlCommand cmd1 = conn.CreateCommand();
                   cmd1.CommandText = "INSERT INTO personnel(id,nom,prenom,adresse,dateEmbauche) VALUES(@id, @nom, @prenom,@adresse,@dateEmbauche)";
                   cmd1.Parameters.AddWithValue("@id", id);
                   cmd1.Parameters.AddWithValue("@nom", nom);
                   cmd1.Parameters.AddWithValue("@prenom", prenom);
                   cmd1.Parameters.AddWithValue("@adresse", adresse);
                   cmd1.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                   cmd1.ExecuteNonQuery();
                   cmd1.Parameters.Clear();


                   MySqlCommand cmd2 = conn.CreateCommand();
                   cmd2.CommandText = "INSERT INTO responsable(idPersonnel) VALUES(@idPersonnel)";
                   cmd2.Parameters.AddWithValue("@idPersonnel", idPersonnel);
                   cmd2.ExecuteNonQuery();
                   cmd2.Parameters.Clear();


                   MySqlCommand cmd3 = conn.CreateCommand();
                   cmd3.CommandText = "INSERT INTO activites(idActivite,bilan) VALUES(@idActivite,@bilan)";
                   cmd3.Parameters.AddWithValue("@idActivite", idActivite);
                   cmd3.Parameters.AddWithValue("@bilan", bilan);
                   cmd3.ExecuteNonQuery();
                   cmd3.Parameters.Clear();




                   MySqlCommand cmd4 = conn.CreateCommand();
                   cmd4.CommandText = "INSERT INTO activitescomp(numOrdre,etat,budgetActivitesComp,commentaires,salle,idActivite,idResponsable,dateActivite) values(@numOrdre, @etat, @budgetActivitesComp,@commentaires,@salle,@idActivite,@idResponsable,@dateActivite)";
                   cmd4.Parameters.AddWithValue("@numOrdre", numOrdre);
                   cmd4.Parameters.AddWithValue("@etat", etat);
                   cmd4.Parameters.AddWithValue("@budgetActivitesComp", budgetActivitesComp);
                   cmd4.Parameters.AddWithValue("@commentaires", commentaires);
                   cmd4.Parameters.AddWithValue("@salle", salle);
                   cmd4.Parameters.AddWithValue("@idActivite", idActivite);
                   cmd4.Parameters.AddWithValue("@idResponsable", idResponsable);
                   cmd4.Parameters.AddWithValue("@dateActivite", dateActivite);
                   cmd4.Parameters.AddWithValue("@idPersonnel", idPersonnel);
                   cmd4.ExecuteNonQuery();
                   cmd4.Parameters.Clear();

                   return "l'activité complémentaire à été ajouté";

               }

               catch (Exception except)
               {

                   return "Erreur" + except.Message;


               }
               finally
               {

                   conn.Close();

               }

           }


               [WebMethod]

               public String[] afficherAC()
               {
                   String connString = "Server = 127.0.0.1;Database = gsb;Uid = root;Password = ;SslMode = none";
                   MySqlConnection conn = new MySqlConnection(connString);
                   conn.Open();

                   MySqlCommand cmd1 = conn.CreateCommand();
                   cmd1.CommandText = "SELECT COUNT(*) FROM activitescomp";
                   int activitésC = Convert.ToInt32(cmd1.ExecuteScalar());
                   conn.Close();
                   int i = 0;
                   String[] AC = new String[activitésC];
                   conn.Open();
                   MySqlCommand cmd2 = conn.CreateCommand();
                   cmd2.CommandText = "SELECT * FROM activitescomp";
                   MySqlDataReader tableau = cmd2.ExecuteReader();

                   if (tableau.HasRows)
                   {

                       while (tableau.Read())
                       {

                           AC[i] = tableau["numOrdre"].ToString() + " " + tableau["etat"].ToString()+ " " +tableau["budgetActivitesComp"].ToString()+"  "+tableau["commentaires"].ToString()+"  "+tableau["salle"].ToString()+"  "+tableau["idActivite"].ToString()+"  "+tableau["idResponsable"].ToString()+"  " +tableau["dateActivite"].ToString();
                       }




                   }

                   return AC;
               }
       */






        




        






        
        

            
            




    




