using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_transver.DesignPattern
{
    /// <summary>
    /// Dùng khi mong muốn chỉ có 1 instance được tạo ra và xử lý trong chương trình (như instance calendrier)
    /// </summary>
    public class SingletonDP
    {
        private SingletonDP()
        {
        }

        public static SingletonDP getInstance()
        {
            return SingletonHelper.INSTANCE;
        }

        private static class SingletonHelper
        {
            public static readonly SingletonDP INSTANCE = new SingletonDP();
        }
    }
}
