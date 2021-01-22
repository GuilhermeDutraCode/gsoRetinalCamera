﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsoRetinalCameraSaver
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var hasFoundFolder = false;
            while (!hasFoundFolder)
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.FolderLocation))
                {
                    var d = new FolderBrowserDialog();
                    d.Description = "Select folder to monitor...";
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.FolderLocation = d.SelectedPath;
                        Properties.Settings.Default.Save();
                    }
                    else if(d.ShowDialog()== DialogResult.Cancel)
                    {
                        Application.Exit();
                        return;
                    }
                }
                if (Directory.Exists(Properties.Settings.Default.FolderLocation))
                {
                    hasFoundFolder = true;
                }
                else
                {
                    MessageBox.Show(this,"The folder is not valid, select another location", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            fileWatch.Path = Properties.Settings.Default.FolderLocation;
            fileWatch.Created += FileWatch_Created;


        }

        private void FileWatch_Created(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(e.FullPath)) return;
            var wnd = new FrmNewFile(e.FullPath);
            wnd.TopMost = true;
            wnd.ShowDialog(this);
            
        }

        private void tmrHide_Tick(object sender, EventArgs e)
        {
            Hide();
            tmrHide.Stop();
            foreach(var _ in Directory.EnumerateFiles(Properties.Settings.Default.FolderLocation))
            {
                var wnd = new FrmNewFile(_);
                wnd.TopMost = true;
                wnd.ShowDialog(this);
            }
        }

        private void fileWatch_Changed(object sender, FileSystemEventArgs e)
        {
           
        }
    }
}
