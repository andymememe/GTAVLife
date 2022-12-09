using System;
using System.Collections.Generic;
using System.Drawing;
using GTA.Math;
using GTAVLife.Helper;

namespace GTAVLife.GameData
{
    public enum PointType
    {
        Player,
        Vehicle
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
        public EntryPointStatus Status { get; set; }
        public PointType PointType { get; }
        public Vector3 Position { get; }
        public Vector3 PointTo { get; }
        public Color Color { get; }

        public EntryPoint(string name, Vector3 position, Vector3 pointTo, PointType pointType, Color color)
        {
            this.Name = name;
            this.PointType = pointType;
            this.Position = position;
            this.PointTo = pointTo;
            this.Color = color;
            this.Status = EntryPointStatus.Add;
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
            this.EntryPoints.Add(new EntryPoint(name, pos, pointTo, PointType.Player, Color.Red));
        }

        public void AddTestVehicleEntryPoint(string name, Vector3 pos, Vector3 pointTo)
        {
            if (name == null)
            {
                DateTime dt = DateTime.Now;
                name = string.Format("TestVehicle_{0}", dt.Ticks);
            }
            this.EntryPoints.Add(new EntryPoint(name, pos, pointTo, PointType.Vehicle, Color.Blue));
        }

        public void RemoveAllEntryPoint()
        {
           foreach (EntryPoint entryPoint in this.EntryPoints)
           {
                entryPoint.Status = EntryPointStatus.Delete;
           }
        }

        private EntryPointList()
        {
            this.EntryPoints = new List<EntryPoint>();
        }
    }
}