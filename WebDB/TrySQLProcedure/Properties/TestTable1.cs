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
        // �p�GDB�����]�w�w�]�ȡA�ѳo�ӫ��O��C#�i�DDB�۰ʥͦ���ƭ�
        public Guid ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime? CreateDate { get; set; }
    }
}
