using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MediaTek86.Conn
{
    /// <summary>
    /// Connexion et exploitation de la BDD par requêtes
    /// </summary>
    class Connexion
    {
        /// <summary>
        /// Objet instance de Connexion qui permet le singleton.
        /// </summary>
        private static Connexion instance = null;
        /// <summary>
        /// Connexion à la BDD
        /// </summary>
        private MySqlConnection connection;
        /// <summary>
        /// Exécution d'une requête SQL
        /// </summary>
        private MySqlCommand command;
        /// <summary>
        /// Curseur, contient des valeurs suite à une requête SELECT 
        /// </summary>
        private MySqlDataReader reader;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="stringConn">Chaîne de caractères pour se connecter à la BDD</param>
        private Connexion(string stringConn)
        {
            try
            {
                connection = new MySqlConnection(stringConn);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Crée une instance unique de la classe
        /// </summary>
        /// <param name="stringConn">Chaîne de caractères pour se connecter à la BDD</param>
        /// <returns>Singleton</returns>
        public static Connexion GetInstance(string stringConn)
        {
            if (instance is null)
            {
                instance = new Connexion(stringConn);
            }
            return instance;
        }

        /// <summary>
        /// Exécution d'une requête autre que SELECT
        /// </summary>
        /// <param name="stringQuery">Requête autre que SELECT</param>
        /// <param name="parameters">Dictionnaire contenant les paramètres</param>
        public void ReqUpdate(string stringQuery, Dictionary<string, object> parameters)
        {
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Exécute une requête SELECT. Valorise le curseur.
        /// </summary>
        /// <param name="stringQuery"Requête SELECT</param>
        public void ReqSelect(string stringQuery, Dictionary<string, object> parameters)
        {
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Parcourt les lignes suivantes du curseur.
        /// </summary>
        /// <returns>Vrai si en cours / Faux si fin de curseur atteinte</returns>
        public bool Read()
        {
            if (reader is null)
            {
                return false;
            }
            try
            {
                return reader.Read();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne le contenu d'un champ dont le nom est passé en paramètre
        /// </summary>
        /// <param name="nameField">Nom champ</param>
        /// <returns>Contenu du champ</returns>
        public object Field(string nameField)
        {
            if (reader is null)
            {
                return null;
            }
            try
            {
                return reader[nameField];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Ferme le curseur
        /// </summary>
        public void Close()
        {
            if (!(reader is null))
            {
                reader.Close();
            }
        }
    }
}
