using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{

    public class mdlzip : result
    {
        public int documentIdentifier { get; set; }
        public string zipDestinationPath { get; set; }
        public string documentPaths { get; set; }

    }

}