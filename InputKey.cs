using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    [DataContract]
    public class InputKey
    {
        [DataMember]
        public int KeyId { get; set; }
        [DataMember]
        public string KeyString { get; set; } = "";

        public InputKey Clone()
        {
            return new InputKey()
            {
                KeyId = KeyId,
                KeyString = KeyString
            };
        }
    }
}
