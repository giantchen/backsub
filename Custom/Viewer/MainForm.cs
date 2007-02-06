using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Viewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up)) {
                // Rocker Up
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down)) {
                // Rocker Down
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left)) {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right)) {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter)) {
                // Enter
                Application.Exit();
            }
        }
    }
}