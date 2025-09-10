using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipDriver
{
    public class COprParam
    {
        /*
         oprtype  oprinfo
         OPEN      IO          打开IO
         CLOSE     IO          关闭IO
         DIR       IO          反相IO
         AOPEN                 打开所有
         ACLOSE                关闭所有
         ADIR                  反相所有
         CLOSEONE  IO_timer1          闪闭一次
         OPENONE   IO_timer1          闪开一次
         OPERMORE  IO_timer1_timer2   闪烁
         WAO       regstart_ao1_ao2_ao3_ao4         
         WDO       regstart_reginfo  012  0关闭 1打开 2忽略
         RAO       regstart_regnum
         RAI       regstart_regnum
         RDO       regstart_regnum
         RDI       regstart_regnum
         */
        public string OprType;
        public string UNID;
        public string Oprinfo;

        public DateTime createtime = DateTime.Now;


        public COprParam(string oprtype)
        {
            OprType = oprtype;
        }
        //设备组操作
        public COprParam(string oprtype, string unid, string oprinfo)
        {
            OprType = oprtype;
            UNID = unid;
            Oprinfo = oprinfo;
        }
    }
}
