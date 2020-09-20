using System;
using System.Collections.Generic;
using System.Drawing;
namespace FMS.Data.Models.Request
{
    public class LoadToLUPoint
    {
        public int LoadID { get; set; }

        public Load Load { get; set; }

        public int LoadingUnloadingPointID { get; set; }

        public LoadingUnloadingPoint LoadingUnloadingPoint { get; set; }

    }
}
