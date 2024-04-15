using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.ENTITY
{
    public class PreferenceEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }
        public int ResumeId { get; set; } // Foreign key referencing Resume

        public string PreferredLocation { get; set; }
        public double CurrentCTC { get; set; }
        public double PreferredCTC { get; set; }
        public int NoticePeriod { get; set; }


        [ForeignKey("ResumeId")]
        public ResumeEntity Resume { get; set; } // Navigation property

    }
}
