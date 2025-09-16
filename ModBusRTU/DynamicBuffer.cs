using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusRTU
{
    public class DynamicBuffer
    {
        public byte[] Buffer { get; set; } //存放内存的数组
        public int DataCount { get; set; } //写入数据大小

        public DynamicBuffer(int bufferSize)
        {
            DataCount = 0;
            Buffer = new byte[bufferSize];
        }

        public int GetDataCount() //获得当前写入的字节数
        {
            return DataCount;
        }

        public int GetReserveCount() //获得剩余的字节数
        {
            return Buffer.Length - DataCount;
        }

        public void Clear()
        {
            DataCount = 0;
        }

        public void Clear(int count) //清理指定大小的数据
        {
            if ((count >= DataCount) || (count == -1) || (count == 0)) //如果需要清理的数据大于现有数据大小，则全部清理
            {
                DataCount = 0;
            }
            else
            {
                Array.Copy(Buffer, count, Buffer, 0, DataCount - count);
                DataCount = DataCount - count;
            }
        }

        public void WriteBuffer(byte[] buffer, int offset, int count)
        {
            if (GetReserveCount() > count) //缓冲区空间够，不需要申请
            {
                Array.Copy(buffer, offset, Buffer, DataCount, count);
                DataCount = DataCount + count;
            }
        }

        public byte[] GetBytes()
        {
            byte[] dst = new byte[DataCount];
            Array.Copy(Buffer, dst, DataCount);

            return dst;
        }
    }
}
