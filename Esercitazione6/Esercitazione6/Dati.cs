using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Esercitazione6
{
    public class Dati 
    {
        //Creazione stringa di connessione
        const string connectionStringP = @"Persist Security Info = false; Integrated Security = true; Initial Catalog = Polizia; Server = .\SQLEXPRESS";

        //Metodo recupero dati, modalità connessa
        public static void ConnectedPoliz()
        {
            //Creare la connessione
            using (SqlConnection connection = new SqlConnection(connectionStringP))
            {
                //Aprire la connessione
                connection.Open();

                //Creare un Command (comando)
                SqlCommand command = new SqlCommand();
                //dobbiamo dargli tre info:
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM AgenteDiPolizia";


                //Esecuzione del Command 
                SqlDataReader reader = command.ExecuteReader();
                //Lettura dati
                while (reader.Read())
                {
                    Agente agente = new Agente(
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["CF"].ToString(),
                        reader["AnniServizio"].ToString());

                    agente.Visualizzazione();

                }

                //Chiusura della connessione e reader:
                reader.Close();
                connection.Close(); 

            }
        }

        //Metodo assegnazione agente-area, modalità connessa
        public static void ConnectedAreaAgente()
        {
            //input utente
            Console.WriteLine("Inserisci l'area: ");
            string area = Console.ReadLine().ToString();

            string select = "SELECT AgenteDiPolizia.Nome, AgenteDiPolizia.Cognome, AgenteDiPolizia.CF, AgenteDiPolizia.AnniServizio, g.CodiceArea FROM(SELECT AreaMetropolitana.ID, CodiceArea, Assegnazione.AgenteID FROM AreaMetropolitana INNER JOIN Assegnazione ON AreaMetropolitana.ID = Assegnazione.AreaID WHERE CodiceArea = @area) AS g INNER JOIN AgenteDiPolizia ON AgenteDiPolizia.ID = g.AgenteID";


            using (SqlConnection connection = new SqlConnection(connectionStringP))
            {
                //Aprire la connessione
                connection.Open();

                //Creare un Command (comando)
                SqlCommand command = new SqlCommand();
               
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = select;

                command.Parameters.AddWithValue("@area", area);


                //Esecuzione del Command
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dati
                while (reader.Read())
                {
                    Agente agente = new Agente(
                       reader["Nome"].ToString(),
                       reader["Cognome"].ToString(),
                       reader["CF"].ToString(),
                       reader["AnniServizio"].ToString());

                    agente.Visualizzazione();
                }

                //Chiusura della connessione e reader:
                reader.Close();
                connection.Close(); 




            }
        }

        //Metodo anni servizio, modalità connessa
        public static void ConnectedAnniServizio()
        {
            //input utente
            Console.WriteLine("Inserisci gli anni di servizio: ");
            int anni = Int32.Parse(Console.ReadLine());

            //Creare la connessione
            using (SqlConnection connection = new SqlConnection(connectionStringP))
            {
                //Aprire la connessione
                connection.Open();

                //Creare un Command (comando)
                SqlCommand command = new SqlCommand();
                //dobbiamo dargli tre info:
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM AgenteDiPolizia WHERE AnniServizio > @anni -1";

                command.Parameters.AddWithValue("@anni", anni);

                //Esecuzione del Command
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dati
                while (reader.Read())
                {
                    Agente agente = new Agente(
                       reader["Nome"].ToString(),
                       reader["Cognome"].ToString(),
                       reader["CF"].ToString(),
                       reader["AnniServizio"].ToString());

                    agente.Visualizzazione();
                }

                //Chiusura della connessione e reader:
                reader.Close();
                connection.Close();



            }

        }

        //Metodo aggiunta, modalità disconnessa
        public static void DisconnectedNuovoAgente()
        {

            //Creazione connessione
            using (SqlConnection connection = new SqlConnection(connectionStringP))
            {
                //Inserire i valori
                Console.WriteLine("Inserisci il Nome: ");
                string Nome = Console.ReadLine();
                Console.WriteLine("Inserisci il Cognome: ");
                string Cognome = Console.ReadLine();
                Console.WriteLine("Inserisci il CodiceFiscale: (16 cifre)");
                string CF = Console.ReadLine();
                Console.WriteLine("Inserisci la Data di nascita: yyyy-mm-dd ");
                DateTime DataNascita = DateTime.Parse(Console.ReadLine());
                //Console.WriteLine("Inserisci la Data di Nascita: (yyyy-mm-dd)");
                //string data = Console.ReadLine();
                Console.WriteLine("Inserisci gli anni di servizio: ");
                int Anni = Int32.Parse(Console.ReadLine());


                //Adapter
                SqlDataAdapter adapter = new SqlDataAdapter();

                //Creazione comandi da associare all'adapter
                //Comando per selezionare tutti i film
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM AgenteDiPolizia";

                //Comando per inserire un film
                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "INSERT INTO AgenteDiPolizia VALUES(@Nome, @Cognome, @CF, @DataNascita, @Anni)";
               
                command1.Parameters.Add("@Nome", SqlDbType.NVarChar, 255, "Nome");
                command1.Parameters.Add("@Cognome", SqlDbType.NVarChar, 255, "Cognome");
                command1.Parameters.Add("@CF", SqlDbType.Char, 255, "CF");
                command1.Parameters.Add("@DataNascita", SqlDbType.Date, 255, "DataNascita");
                command1.Parameters.Add("@Anni", SqlDbType.Int, 255, "AnniServizio");

                //Associazione dei comandi all'adapter
                adapter.SelectCommand = command;
                adapter.InsertCommand = command1;

                //Dataset  
                DataSet dataSet = new DataSet();

                try
                {
                    //Sta sfruttando la connessione
                    connection.Open();
                    adapter.Fill(dataSet, "AgenteDiPolizia");

                    DataRow agente = dataSet.Tables["AgenteDiPolizia"].NewRow(); //prendo una riga vuota
                    agente["Nome"] = Nome;//andiamo a riempirla
                    agente["Cognome"] = Cognome;
                    agente["CF"] = CF;
                    agente["DataNascita"] = DataNascita;
                    agente["AnniServizio"] = Anni;

                    dataSet.Tables["AgenteDiPolizia"].Rows.Add(agente);

                    //Update sul db -> sto sfruttando la connessione  (adapter)
                    adapter.Update(dataSet, "AgenteDiPolizia"); //mando le modifiche sul database

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally  
                {
                    //Chiusura Connessione
                    connection.Close();
                }

            }
        }
    }
}
