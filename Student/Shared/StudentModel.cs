﻿using LazurdIT.FluentOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Shared
{
    [FluentTable("Students")]
    public class StudentModel : IFluentModel
    {
        [FluentField(name: "StudentId", autoGenerated: true, isPrimary: true, allowNull: false)]
        public int Id { get; set; }

        [FluentField(name: "StudentName", allowNull: false)]
        public string Name { get; set; } = string.Empty;

        [FluentField(name: "StudentAddress", allowNull: true)]
        public string? Address { get; set; }

        public StudentModel()
        {
            Id = 0; // Explicitly set Id to 0 for new entries
        }

    }
}