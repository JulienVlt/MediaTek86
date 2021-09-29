using MediaTek86.dal;
using MediaTek86.Vue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MediaTek86.Control
{
    public class Controle
    {
        readonly private Vue.FrmConnexion frmConnexion;

        public Controle()
        {
            frmConnexion = new Vue.FrmConnexion(this);
            frmConnexion.ShowDialog();
        }

        public void verifAuthentification(string login, string pwd)
        {
            if (AccesBDD.ControleAuthentification(login, pwd))
            {
                frmConnexion.Hide();
                new FrmGestionPersonnel(this);
            }
            else
            {
                MessageBox.Show("Erreur de connexion, veuillez réessayer.", "Echec de connexion");
            }

        }
    }
}
