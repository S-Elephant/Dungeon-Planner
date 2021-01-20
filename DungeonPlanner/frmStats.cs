using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlanner
{
    public partial class frmStats : Form
    {
        DungeonMap dm;

        public frmStats(ref DungeonMap dm)
        {
            InitializeComponent();
            this.dm = dm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void RefreshStats()
        {
            Statistics stats = dm.GetStats();

            txtStats.Text = string.Format("Creatures:{0}", Environment.NewLine);
            txtStats.Text += string.Format("Small: {0}{1}", stats.CreaturesSmall, Environment.NewLine);
            txtStats.Text += string.Format("Medium: {0}{1}", stats.CreaturesMedium, Environment.NewLine);
            txtStats.Text += string.Format("Large: {0}{1}", stats.CreaturesLarge, Environment.NewLine);
            txtStats.Text += string.Format("Total: {0}{1}", stats.CreaturesTotal, Environment.NewLine);
            txtStats.Text += string.Format("Avg # of creatures per room: {0}{1}", stats.AvgCreaturesPerRoom.ToString("0.000"), Environment.NewLine);
            
            txtStats.Text += Environment.NewLine;
            txtStats.Text += string.Format("Objects:{0}", Environment.NewLine);
            txtStats.Text += string.Format("Rooms: {0}{1}", stats.RoomCount, Environment.NewLine);
            txtStats.Text += string.Format("Tunnel segments: {0}{1}", stats.TunnelCount, Environment.NewLine);
        }

        private void frmStats_Load(object sender, EventArgs e)
        {
            RefreshStats();
        }

        private void timRefresh_Tick(object sender, EventArgs e)
        {
            RefreshStats();
        }
    }
}
