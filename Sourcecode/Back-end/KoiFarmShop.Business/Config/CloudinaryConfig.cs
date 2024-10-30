using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Config
{
    public class CloudinaryConfig
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public FolderSettings Folders { get; set; }
    }
    public class FolderSettings
    {
        public string Images { get; set; }
        public string Videos { get; set; }
        public string Files { get; set; }
    }
}
