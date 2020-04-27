﻿using System;
using System.Collections.Generic;

namespace TimeLogger.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public ICollection<TimeLog> TimeLogs { get; set; }
    }
}