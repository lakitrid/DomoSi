using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fr.Lakitrid.DomoDb.Entities
{
    [Table("Sample")]
    public class Sample
    {
        [Column("Id")]
        public decimal Id { get; set; }

        [Column("SerieId")]
        [ForeignKey("Serie")]
        public decimal SerieId { get; set; }

        public DataSerie Serie { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Value")]
        public decimal Value { get; set; }
    }
}
