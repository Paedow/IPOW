﻿using System;
using System.Runtime.Serialization;
using Godot;
using Newtonsoft.Json;
using IPOWLib.Pathing;
using System.Collections.Generic;

namespace IPOWLib.JMap
{
    public class MapData
    {
        public string Name;
        public int Width, Height;
        public MapTileInfo DefaultTile;

        Dictionary<MapTileInfo, PointI> _data = new Dictionary<MapTileInfo, PointI>();
        [JsonIgnore]
        public PointI Size;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Size = new PointI(Width, Height);
        }
    }
}