using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSX.Shared.Entities.Common
{
    public class Localization : EntityBase
    {
        public required string EnContent { get; set; }
        public required string ArContent { get; set; }
    }
}
