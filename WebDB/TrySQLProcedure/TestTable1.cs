namespace TrySQLProcedure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TestTable1
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
