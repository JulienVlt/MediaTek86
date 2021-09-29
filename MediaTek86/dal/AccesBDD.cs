using System;
using System.Collections.Generic;
using System.Text;
using MediaTek86.Conn;

namespace MediaTek86.dal
{
    class AccesBDD
    {
        /// <summary>
        /// Chaîne de connexion
        /// </summary>
        readonly private static string connectionString = "Server='****'; User Id=****; Password=****;Database=****; Port=****";

        /// <summary>
        /// Controle login et pwd de l'utilisateur
        /// </summary>
        /// <param name="login">Identifiant</param>
        /// <param name="pwd">Mot de passe</param>
        /// <returns></returns>
        public static Boolean ControleAuthentification(string login, string pwd)
        {
            string req = "select * from responsable ";
            req += "where login=@login and pwd=SHA2(@pwd, 256)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@login", login);
            parameters.Add("@pwd", pwd);
            Connexion curs = Connexion.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);
            if (curs.Read())
            {
                curs.Close();
                return true;
            }
            else
            {
                curs.Close();
                return false;
            }
        }


    }
}
