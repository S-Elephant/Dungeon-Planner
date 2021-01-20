using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DungeonPlanner
{
    [Serializable]
    public class DataContainer
    {
        public List<Room> Rooms;
    }

    public class Statistics
    {
        public int CreaturesSmall,CreaturesMedium, CreaturesLarge, CreaturesTotal;
        public int RoomCount, TunnelCount;
        public double AvgCreaturesPerRoom;
    }

    public class DungeonMap : DrawControl
    {
        #region Properties
        public const int MAP_SIZE = 25;
        public const int GRID_SIZE = 18;
        public static readonly Point GRID_SIZE_POINT = new Point(GRID_SIZE, GRID_SIZE);
        public List<Room> Rooms = new List<Room>();
        private readonly bool IsInRuntime = System.Diagnostics.Process.GetCurrentProcess().ProcessName != "devenv";
        public static Bitmap TunnelImage;
        private Bitmap HoverImage = null;

        private int m_Rotation = 0;
        public int Rotation
        {
            get { return m_Rotation; }
            set
            {
                m_Rotation = value;
                UpdateHoverImage();
            }
        }

        private string m_NextRoomToAdd = null;
        public string NextRoomToAdd
        {
            get { return m_NextRoomToAdd; }
            set
            {
                m_NextRoomToAdd = value;
                UpdateHoverImage();
            }
        }
        
        private bool m_IsRoomMode = true;
        /// <summary>
        /// When false it means that it's in tunnel-mode.
        /// </summary>
        public bool IsRoomMode
        {
            get { return m_IsRoomMode; }
            set
            {
                m_IsRoomMode = value;
                UpdateHoverImage();
            }
        }
        #endregion

        public DungeonMap() : base()
        {
            if (IsInRuntime)
            {
                TunnelImage = DrawUtils.BitmapFromFile("Rooms/tunnel.jpg").AlphaBlendAbsolute(128);
            }
            MouseClick += new System.Windows.Forms.MouseEventHandler(DungeonMap_MouseClick);
            MouseMove += new System.Windows.Forms.MouseEventHandler(DungeonMap_MouseMove);
        }

        public Statistics GetStats()
        {
            Statistics result = new Statistics();

            foreach (Room r in Rooms)
            {
                if (!r.IsTunnel)
                {
                    result.CreaturesSmall += r.CreatureSlotsSmall;
                    result.CreaturesMedium += r.CreatureSlotsMedium;
                    result.CreaturesLarge += r.CreatureSlotsLarge;
                    result.CreaturesTotal += r.CreatureSlotsTotal;
                    result.RoomCount++;
                }
                else
                    result.TunnelCount++;
            }

            result.AvgCreaturesPerRoom = (Rooms.Count <= 2) ? 0 : (result.CreaturesTotal / ((double)Rooms.Where(r => !r.IsTunnel).Count() - 2));

            return result;
        }

        public void Serialize(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            DataContainer dc = new DataContainer{ Rooms = Rooms };
            bFormatter.Serialize(stream, dc);
            stream.Close();
        }

        public void DeSerialize(string path)
        {
            DataContainer dc;
            Stream stream = File.Open(path, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            dc = (DataContainer)bFormatter.Deserialize(stream);
            stream.Close();

            // Clear the old rooms
            Rooms.Clear();

            // Add the new rooms
            foreach (Room r in dc.Rooms)
            {
                Rooms.Add(r);
            }
        }

        private void UpdateHoverImage()
        {
            if (NextRoomToAdd != null)
            {
                if (IsRoomMode)
                {
                    Room temp = StringToRoom(NextRoomToAdd, Point.Empty);
                    HoverImage = new Bitmap(temp.RoomImage);
                    HoverImage.AlphaBlendAbsolute(128);
                }
                else
                {
                    if (IsInRuntime)
                    {
                        HoverImage = TunnelImage;
                    }
                }

                if (TheImage != null)
                {
                    ReDrawRooms();
                }
            }
        }

        void DungeonMap_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (TheImage != null)
            {
                ReDrawRooms();
                Room r = RoomUnderMouse(IsRoomMode);

                Graphics g = Graphics.FromImage(TheImage);
                if (r != null)
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(128, r.RarityColor), 5f), r.GetBoundary);
                }

                // Draw hover
                g.DrawImage(HoverImage, Utils.SnapToGrid(Utils.GetLocalMousePos(this), GRID_SIZE_POINT));
                // Release resources
                g.Dispose();
            }
        }
        
        /// <summary>
        /// Returns the first one found or null if none
        /// </summary>
        /// <returns></returns>
        public Room RoomUnderMouse(bool isRoomMode)
        {
            Point mouseLoc = Utils.GetLocalMousePos(this);

            foreach (Room r in Rooms)
            {
                if ((isRoomMode && !r.IsTunnel) || (!IsRoomMode && r.IsTunnel))
                {
                    if (Utils.PointIsInRect(mouseLoc, r.GetBoundary))
                    {
                        return r;
                    }
                }
            }
            return null;
        }

        private Room StringToRoom(string name, Point gridLocation)
        {
            Room result;
            switch (name.ToLower())
            {
                case null:
                    return null;
                case "dungeon entrance":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/dungeonEntrance.png"), gridLocation, Color.White, Rotation);
                    break;
                case "dungeon exit":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/dungeonExit.png"), gridLocation, Color.White, Rotation);
                    break;
                case "ducal dining room":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath + "/Rooms/ducalDiningRoom.png"), gridLocation, Color.Blue, Rotation) { CreatureSlotsSmall = 2, CreatureSlotsLarge = 2 };
                    break;
                case "ladies chamber":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath + "/Rooms/ladiesChamber.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 2, CreatureSlotsLarge = 1 };
                    break;
                case "heart room":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/heartRoom.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 3 };
                    break;
                case "servant's entrance":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/servantsEntrance.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 1 };
                    break;
                case "servant's chamber":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/servantsChamber.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 1 };
                    break;
                case "fortification room":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/fortificationRoom.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 4 };
                    break;
                case "gun room":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/gunRoom.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 2 };
                    break;
                case "ignoble pit chamber":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/ignoblePitChamber.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 3};
                    break;
                case "knights hall":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/knightsHall.png"), gridLocation, Color.Blue, Rotation) { CreatureSlotsMedium = 4 };
                    break;
                case "ordinary pantry":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/ordinaryPantry.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 2};
                    break;
                case "ordinary rock chamber":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/ordinaryRockChamber.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 3 };
                    break;
                case "rare dungeon":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/rareDungeon.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 2, CreatureSlotsLarge = 1 };
                    break;
                case "rare parlor":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/rareParlor.png"), gridLocation, Color.Green, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 2 };
                    break;
                case "small cavern":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/smallCavern.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 1 };
                    break;
                case "small escape chamber":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/smallEscapeChamber.png"), gridLocation, Color.White, Rotation) { CreatureSlotsSmall = 1, CreatureSlotsMedium = 1 };
                    break;
                case "small grotto room":
                    result = new Room(DrawUtils.BitmapFromFile(Application.StartupPath+"/Rooms/smallGrottoRoom.png"), gridLocation, Color.White, Rotation) { CreatureSlotsMedium = 2 };
                    break;
                default:
                    throw new NotImplementedException();
            }
            return result;
        }

        public void AddRoom(string name, Point gridLocation)
        {
            Rooms.Add(StringToRoom(name, gridLocation));
            ReDrawRooms();
        }

        void DungeonMap_MouseClick(object sender, MouseEventArgs e)
        {
            Point gridCoord = Utils.SnapToGrid(Utils.GetLocalMousePos(this), GRID_SIZE_POINT);

            if (e.Button == MouseButtons.Left)
            {
                if (IsRoomMode)
                {
                    AddRoom(NextRoomToAdd, gridCoord);
                }
                else
                {
                    PlaceTunnel(gridCoord);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Rooms.Remove(RoomUnderMouse(IsRoomMode));
                ReDrawRooms();
            }
        }

        private void PlaceTunnel(Point atThisCoordinate)
        {
            Rooms.Add(new Room(atThisCoordinate));
            ReDrawRooms();
        }

        private void DrawGrid(Graphics g)
        {
            using (Pen gridPen = new Pen(Brushes.White))
            {
                for (int x = 0; x < TheImage.Width; x += GRID_SIZE) { g.DrawLine(gridPen, new Point(x, 0), new Point(x, TheImage.Height)); }
                for (int y = 0; y < TheImage.Height; y += GRID_SIZE) { g.DrawLine(gridPen, new Point(0, y), new Point(TheImage.Width, y)); }
            }
        }

        public void ReDrawRooms()
        {
            // Clear
            Fill(Color.Black);

            Graphics g = Graphics.FromImage(TheImage);
            DrawGrid(g);

            // Rooms
            foreach (Room room in Rooms)
            {
                TheImage.DrawOnto(room.RoomImage, room.DrawLocation);
            }

            // Border
            g.DrawRectangle(Pens.Red, new Rectangle(GRID_SIZE, GRID_SIZE, GRID_SIZE * MAP_SIZE, GRID_SIZE * MAP_SIZE));

            g.Dispose();
        }
    }
}
