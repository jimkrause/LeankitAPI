﻿using System.Collections.Generic;

namespace Leankit.Entities
{
    public class Lane
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Type { get; set; }
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Width { get; set; }
        public string ParentLaneId { get; set; }
        public List<Card> Cards { get; set; }
        public string Orientation { get; set; }
        public List<int> ChildLaneIds { get; set; }
        public List<int> SiblingLaneIds { get; set; }
        public string CardLimit { get; set; }
        public string LaneState { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}