﻿using System;

namespace DataImporter.Logic.BusinessObjects
{
    public class ExcelFile 
    {
        public Guid Id { get ; set ; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string FileName { get; set; }
    }
}
