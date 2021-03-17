using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_transver.DesignPattern
{
    /// <summary>
    /// Dùng khi chi phí tạo ra 1 instance mới là quá tốn kém => xài clone
    /// </summary>
    public class PrototypeDP : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
