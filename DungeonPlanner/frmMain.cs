using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace DungeonPlanner
{
    public partial class frmMain : Form
    {
        private const string CaptionPrefix = "Squirting Elephant's Dungeon Planner (released in 2011). ";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            AssemblyName name = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            lblversion.Text = string.Format("Version: {0}.{1}.{2}", name.Version.Major, name.Version.Minor, name.Version.Build);

            dungeonMap1.TheImage = new Bitmap(486, 486);//DungeonMap.MAP_SIZE * DungeonMap.GRID_SIZE, DungeonMap.MAP_SIZE * DungeonMap.GRID_SIZE
            dungeonMap1.TheImage.SetResolution(DungeonMap.DPI, DungeonMap.DPI);
            dungeonMap1.AutoResize();
            dungeonMap1.NextRoomToAdd = "Servant's Entrance";

            frmStats frmStats = new frmStats(ref dungeonMap1);
            frmStats.Show();
            Focus();
        }

        private void dungeonMap1_MouseMove(object sender, MouseEventArgs e)
        {
            Point snappedMousePos = Utils.SnapToGrid(Utils.GetLocalMousePos(dungeonMap1), DungeonMap.GRID_SIZE_POINT);
            Point index = new Point(snappedMousePos.X / DungeonMap.GRID_SIZE, snappedMousePos.Y / DungeonMap.GRID_SIZE);
            Text = CaptionPrefix + index.ToString();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            dungeonMap1.Rotation += 90;
            if (dungeonMap1.Rotation > 270)
            {
                dungeonMap1.Rotation = 0;
            }
        }

        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRooms.SelectedIndex != -1)
            {
                dungeonMap1.NextRoomToAdd = lstRooms.SelectedItem.ToString();
            }
        }

        private void chkDrawTunnel_CheckedChanged(object sender, EventArgs e)
        {
            lstRooms.Enabled = dungeonMap1.IsRoomMode = !chkDrawTunnel.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*" };
            if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                dungeonMap1.Serialize(sfd.FileName);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*" };
            if (MessageBox.Show("Are you sure? All current work will be lost.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    dungeonMap1.DeSerialize(ofd.FileName);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? All current work will be lost.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dungeonMap1.Rooms.Clear();
            }
        }
    }
}
