﻿using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Entities
{
    public class ImportHistory : IEntity<Guid>
    {
        public Guid Id { get; set ; }
        public Guid UserId { get; set; }
        public string GroupName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string TotalData { get; set; }
        public string FileName { get; set; }
    }
}
