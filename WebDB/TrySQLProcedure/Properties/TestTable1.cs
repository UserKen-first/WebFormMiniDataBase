namespace TrySQLProcedure.Properties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TestTable1
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // 如果DB內有設定預設值，由這個指令讓C#告訴DB自動生成其數值
        public Guid ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime? CreateDate { get; set; }
    }
}
