using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API.Configuration
{
    [DataContract]
    public class PathsConfiguration
    {
        [DataMember]
        public string DtwoDataBasePath { get; set; } = "Data";

        [DataMember]
        public string JpexsPath { get; set; } = "";

        public PathsConfiguration Clone()
        {
            return new PathsConfiguration
            {
                DtwoDataBasePath = DtwoDataBasePath,
                JpexsPath = JpexsPath
            };
        }
    }
}
