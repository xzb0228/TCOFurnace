using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipDriver
{
    interface IEquipDriver
    {
        void Init(string param,string info);
        void Close(string param);
        bool IsOnline(string param);
        void SendString(string param, string info);
        void SendByte(string param, byte[] buffer, int offset, int count);
        void dealDriver();
    }
}
