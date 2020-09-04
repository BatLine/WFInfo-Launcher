using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WFinfo
{
    public partial class frmMain : Form
    {
        public frmMain() { InitializeComponent(); }

        private void frmMain_Load(object sender, EventArgs e)
        {
            hide();
            runWarframe();
            tmrCheck.Start();
        }

        private void tmrCheck_Tick(object sender, EventArgs e)
        {
            if (!checkIfWarframeIsRunning())
            {
                Thread.Sleep(2000);
                if (!checkIfWarframeIsRunning())
                {
                    if (checkIfWFInfoIsRunning())
                    {
                        stopWFInfo();
                        tmrCheck.Stop();
                        Application.Exit();
                    }
                }
            } else
            {
                if (checkIfProcessIsRunning("warframe.x64"))
                {
                    if (!checkIfWFInfoIsRunning())
                    {
                        Thread.Sleep(5000);
                        runWFinfo();
                    }
                }
            }
        }

        private void runWarframe() { Process.Start("steam://rungameid/230410"); }

        private void runWFinfo()
        {
            try {
                Process.Start(Application.StartupPath + @"\WFInfo.exe");
            } catch (Exception) {
                MessageBox.Show("Error starting wfinfo, make sure it is in the same directory.");
                Application.Exit();
            }
        }

        private bool checkIfWarframeIsRunning() {
            if ((checkIfProcessIsRunning("warframe")) || (checkIfProcessIsRunning("launcher")) || (checkIfProcessIsRunning("warframe.x64")))
                return true;
            return false;
        }

        private bool checkIfWFInfoIsRunning() { return checkIfProcessIsRunning("wfinfo"); }

        private void stopWFInfo() {
            foreach(Process _p in Process.GetProcessesByName("wfinfo")) {
                try {
                    _p.Kill();
                } catch (Exception) { }
            }
        }

        private bool checkIfProcessIsRunning(string pName)
        {
            Process[] pname = Process.GetProcessesByName(pName);
            if (pname.Length > 0)
                return true;
            return false;
        }

        private void hide()
        {
            //makes window invisible
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.Size = new Size(0, 0);
            this.Opacity = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Hide();
        }
    }
}