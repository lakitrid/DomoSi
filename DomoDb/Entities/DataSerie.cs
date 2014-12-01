using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fr.Lakitrid.DomoDb.Entities
{
    [Table("Serie")]
    public class DataSerie
    {
        [Column("Id")]
        public decimal Id { get; set; }

        public List<Parameter> Parameters { get; private set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ModuleName")]
        public string ModuleName { get; set; }
    }
}
