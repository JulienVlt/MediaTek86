using MediaTek86.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MediaTek86.Vue
{
    public partial class FrmConnexion : Form
    {
        public Controle controle;

        public FrmConnexion(Controle controle)
        {
            this.controle = controle;
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            if (txtbID.Text != string.Empty && txtbMdp.Text != string.Empty)
            {
                controle.verifAuthentification(txtbID.Text, txtbMdp.Text);
            }

            else
            {
                MessageBox.Show("Erreur de connexion, veuillez réessayer.", "Echec de connexion");
                txtbMdp.Focus();
            }
        }
    }
}
