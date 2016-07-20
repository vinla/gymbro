using System;
using SQLite;

namespace GymBro.Models
{
    public class EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }
    }
}