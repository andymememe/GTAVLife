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

    public enum EntryType
    {
        Player,
        Vehicle,
        TowedVehicle,
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
        public EntryType EntryType { get; }
        public PointType PointType { get; }
        public EntryPointStatus Status { get; set; }
        public Vector3 Position { get; }
        public Vector3 PointTo { get; }
        public Color Color { get; }
        public BlipSprite BlipSpirte { get; }
        public Checkpoint Checkpoint { get; set; }
        public Blip Blip { get; set; }

        public EntryPoint(string name, PointType pointType, EntryType entryType, Vector3 position, Vector3 pointTo, Color color, BlipSprite blipSprite)
        {
            this.Name = name;
            this.PointType = pointType;
            this.EntryType = entryType;
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

        public void AddTestPlayerEntryPoint(Vector3 pos, Vector3 pointTo)
        {
            DateTime dt = DateTime.Now;
            String name = string.Format("TestPlayer_{0}", dt.Ticks);

            this.EntryPoints.Add(new EntryPoint(name, PointType.TestPlayer, EntryType.Player, pos, pointTo, Color.Red, BlipSprite.CaptureAmericanFlag));
        }

        public void AddTestVehicleEntryPoint(Vector3 pos, Vector3 pointTo)
        {
            DateTime dt = DateTime.Now;
            String name = string.Format("TestVehicle_{0}", dt.Ticks);
            
            this.EntryPoints.Add(new EntryPoint(name, PointType.TestVehicle, EntryType.Vehicle, pos, pointTo, Color.Blue, BlipSprite.CarShowroom));
        }

        public void RemoveAllEntryPoint()
        {
           foreach (EntryPoint entryPoint in this.EntryPoints.FindAll(
                delegate(EntryPoint point)
                {
                    return point.Status == EntryPointStatus.Enabled;
                }
           ))
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