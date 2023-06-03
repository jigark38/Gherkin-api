using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AttendanceDetail
{
    public class BiometricLogDTO
    { 
            [Key]
            [Column("ID")]
            public int ID { get; set; }
            [Column("MachineNumber")]
            public int? MachineNumber { get; set; }
            [Column("IndRegID")]
            public int? IndRegID { get; set; }
            [Column("DateTimeRecord")]
            public DateTime? DateTimeRecord { get; set; }
            [Column("DateOnlyRecord")]
            public DateTime? DateOnlyRecord { get; set; }
            [Column("TimeOnlyRecord")]
            public DateTime? TimeOnlyRecord { get; set; }
            [Column("dwInOutMode")]
            public int DwInOutMode { get; set; }
            [Column("IsBackedUp")]
            public int IsBackedUp { get; set; }
    }
}