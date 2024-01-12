﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVas.DAL.Entities
{
    public class Assignment
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public void SetAssignmentModel(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public Assignment(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}