using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GlobalDataBase
    {
        public int GlobalDataBaseId { get; set; }
        public long TotalConfirmed { get; set; } 
        public DateTime DateDataBase { get; set; }      
    }
    public class DataBase : DbContext
    {
        public virtual DbSet<GlobalDataBase> GDB { get; set; }
    }
}
