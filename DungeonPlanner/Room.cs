using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DungeonPlanner
{
    [Serializable]
    public class Room
    {
        public Bitmap RoomImage;
        public Point DrawLocation;
        public int Rotation;
        public bool IsTunnel = false;
        public Rectangle GetBoundary
        {
            get { return new Rectangle(DrawLocation.X, DrawLocation.Y, RoomImage.Width, RoomImage.Height); }
        }
        public Color RarityColor;
        public int CreatureSlotsSmall = 0;
        public int CreatureSlotsMedium = 0;
        public int CreatureSlotsLarge = 0;
        public int CreatureSlotsTotal
        {
            get { return CreatureSlotsSmall + CreatureSlotsMedium + CreatureSlotsLarge; }
        }

        public Room(Bitmap roomImage, Point drawLoc, Color raritycolor, int rotation)
        {
            RoomImage = roomImage;
            DrawLocation = drawLoc;
            RarityColor = raritycolor;
            
            // Rotation
            Rotation = rotation;
            RoomImage = RoomImage.Rotate(rotation);
            //DPI
            RoomImage.SetResolution(DungeonMap.DPI, DungeonMap.DPI);
        }

        /// <summary>
        /// Tunnel constructor
        /// </summary>
        public Room(Point drawLoc)
        {
            IsTunnel = true;
            DrawLocation = drawLoc;
            Rotation = 0;
            RoomImage = DungeonMap.TunnelImage;
            RarityColor = Color.Yellow;

            //DPI
            RoomImage.SetResolution(DungeonMap.DPI, DungeonMap.DPI);
        }
    }
}