﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyLibrary
{
    public partial class Login : Form
    {

        public event EventHandler UserLoggedIn;
        public Login()
        {
            InitializeComponent();
        }

        private bool UserIsValid()
        {
            XElement korisnici = XElement.Load("korisnici.xml");
            var users = from user in korisnici.Elements()
                        select new
                        {
                            username = (string)user.Element("korisnickoIme"),
                            password = (string)user.Element("lozinka")
                        };
            foreach (var user in users)
            {
                if (string.Compare(user.username, textBoxUsername.Text, true) == 0
                && user.password == textBoxPass.Text)
                    return true;
            }
            return false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (UserIsValid())
            {
                if (UserLoggedIn != null)
                {
                    UserLoggedIn(this, EventArgs.Empty);
                }
                Close();
                return;
            }
            else
            {
                MessageBox.Show(this, "Username or password not valid!", "User Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
