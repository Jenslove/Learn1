using System;
using System.Collections.Generic;

namespace Learn1.Models
{
    public partial class Version
    {
        public decimal Id { get; set; }
        public decimal Thing { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public string Desc { get; set; }
        public string Comment { get; set; }
        public byte[] Item { get; set; }

        public Thing ThingNavigation { get; set; }
    }
}
