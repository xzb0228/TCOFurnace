using ModBusRTU;
using System.Linq;

namespace EquipDriver
{
    public class sys_EquipUNIDModel
    {
        public int mbaddr = 0;
        public int PRODUCTID = 0;
        public int[] regdi = new int[4];
        public int[] regdo = new int[4];

        public string sn = "";
        public int ainum = 0;
        public int dinum = 0;
        public int donum = 0;
        public int aonum = 0;


        public bool IsInverse = true;
        public bool IsTiming = false;
        public int aidonum = 0;
        public int didonum = 0;
        public int rtcdonum = 0;

        public int aimode = 0;//0 int16类型    地址在0
                              //1 int32类型 inverse    地址在0  
                              //2 float类型  inverse  地址在0  
                              //3 float类型   inverse 地址在50
        public int AIRegRqNum = 0;
        public sys_EquipUNIDModel()
        {
            regdi = new int[4];
            regdo = new int[4];

            ainum = 0;
            dinum = 0;
            donum = 0;
            aonum = 0;
            mbaddr = 0;
        }

        public sys_EquipUNIDModel(int mbaddr, int ainum, int dinum, int donum)
        {
            this.mbaddr = mbaddr;
            this.ainum = ainum;
            this.dinum = dinum;
            this.donum = donum;

            regdi = new int[4];
            regdo = new int[4];

            aonum = 0;
            AIRegRqNum = ainum;
        }

        public string GetSN(byte[] src, int startindex, int length)
        {
            if (length != 16) return "";
            byte[] tm = new byte[length];
            for (int i = 0; i < length; i++)
            {
                tm[i] = src[startindex + i];
            }

            bool isstr = true;
            for (int i = 0; i < length; i++)
            {
                if ((tm[i] >= '0') && (tm[i] <= '9')) { }
                else if ((tm[i] >= 'a') && (tm[i] <= 'z')) { }
                else if ((tm[i] >= 'A') && (tm[i] <= 'Z')) { }
                else isstr = false;
            }

            if (isstr == true)
                return CMethord.bytetostring(tm, 0, 16);

            byte[] sn = new byte[16];
            int add = 1;
            for (int i = 0; i < length; i++)
            {
                add += tm[i];
            }
            const string asciitable = "VWiRY6MkgcwEZLabOghPqXxBA01D34CqQmF2NUKp789GnoryzdeflGHIsSt5Tuva";

            sn[0] = (byte)'J';
            sn[1] = (byte)'Z';
            for (int i = 2; i < 16; i++)
            {
                sn[i] = (byte)asciitable.ElementAt((add + tm[i - 1] + tm[i]) & 0x3f);
                add += 0xabcd;
            }
            return CMethord.bytetostring(sn, 0, 16);
        }

        public bool UpdateByte(byte[] src)
        {
            if (src == null) return false;
            if (src.Length != 40) return false;
            mbaddr = CMethord.bytetos16(src, 0);
            PRODUCTID = CMethord.bytetos16(src, 2);
            for (int i = 0; i < 4; i++)
            {
                regdo[i] = src[4 + i];
                regdi[i] = src[8 + i];
            }

            sn = GetSN(src, 12, 16);


            byte temp = src[28];

            ainum = temp & 0x3f;
            aimode = temp >> 6;

            dinum = src[29] & 0x3f;
            donum = src[30] & 0x3f;
            aonum = src[31] & 0x3f;


            if ((src[31] & 0x80) == 0x00) IsInverse = true;
            else IsInverse = false;

            if ((src[31] & 0x40) == 0x40)
            {
                IsTiming = true;
                aidonum = src[33];
                didonum = src[34];
                rtcdonum = src[35];
            }


            switch (aimode & 0x07)
            {
                case 0://
                    AIRegRqNum = ainum;
                    break;
                case 1:
                    AIRegRqNum = ainum * 2;
                    break;
                case 2:
                    AIRegRqNum = ainum * 2;
                    break;
                case 3:
                    AIRegRqNum = ainum * 2;
                    break;
            }
            return true;
        }

        public static string GetAIMode(int aimode)
        {
            if (aimode == 0)
            {
                return "int16类型    地址在0";
            }
            else if (aimode == 1)
            {
                return "1 int32类型   地址在0";
            }
            else if (aimode == 2)
            {
                return "2 float类型   地址在0";
            }
            else if (aimode == 3)
            {
                return "3 float类型   地址在50";
            }

            return "未知类型";
        }
    }
}
