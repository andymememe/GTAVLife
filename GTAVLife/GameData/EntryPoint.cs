using System;
using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.Math;
using GTAVLife.Helper;

namespace GTAVLife.GameData
{
    public enum PointType
    {
        None,
        TestPlayer,
        TestVehicle
    }

    public enum EntryPointStatus
    {
        Delete = -2,
        Add = -1,
        Disabled = 0,
        Enabled = 1
    }

    public class EntryPoint
    {
        public string Name { get; }
        public bool ForVehicle { get; }
        public PointType PointType { get; }
        public EntryPointStatus Status { get; set; }
        public Vector3 Position { get; }
        public Vector3 PointTo { get; }
        public Color Color { get; }
        public BlipSprite BlipSpirte { get; }
        public Checkpoint Checkpoint { get; set; }
        public Blip Blip { get; set; }

        public EntryPoint(string name, PointType pointType, bool forVehicle, Vector3 position, Vector3 pointTo, Color color, BlipSprite blipSprite)
        {
            this.Name = name;
            this.PointType = pointType;
            this.ForVehicle = forVehicle;
            this.BlipSpirte = blipSprite;
            this.Position = position;
            this.PointTo = pointTo;
            this.Color = color;
            this.Status = EntryPointStatus.Add;
            this.Checkpoint = null;
            this.Blip = null;
        }
    }

    public class EntryPointList : SimpleSingletonBase<EntryPointList>
    {   public List<EntryPoint> EntryPoints { get; }

        public void AddTestPlayerEntryPoint(string name, Vector3 pos, Vector3 pointTo)
        {
            if (name == null)
            {
                DateTime dt = DateTime.Now;
                name = string.Format("TestPlayer_{0}", dt.Ticks);
            }
            this.EntryPoints.Add(new EntryPoint(name, PointType.TestPlayer, true, pos, pointTo, Color.Red, BlipSprite.CaptureAmericanFlag));
        }

        public void AddTestVehicleEntryPoint(string name, Vector3 pos, Vector3 pointTo)
        {
            if (name == null)
            {
                DateTime dt = DateTime.Now;
                name = string.Format("TestVehicle_{0}", dt.Ticks);
            }
            this.EntryPoints.Add(new EntryPoint(name, PointType.TestVehicle, false, pos, pointTo, Color.Blue, BlipSprite.CarShowroom));
        }

        public void RemoveAllEntryPoint()
        {
           foreach (EntryPoint entryPoint in this.EntryPoints)
           {
                entryPoint.Status = EntryPointStatus.Delete;
           }
        }

        public PointType GetPointType(int index)
        {
            if (index >= 0 && index < this.EntryPoints.Count)
            {
                return this.EntryPoints[index].PointType;
            }

            return PointType.None;
        }

        private EntryPointList()
        {
            this.EntryPoints = new List<EntryPoint>();
        }
    }
}