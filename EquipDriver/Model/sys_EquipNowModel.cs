using System;

namespace EquipDriver
{
    public class sys_EquipNowModel
    {
        public int ID = 0;
        public int Addr = 0;//起始地址
        public string SN = "";//序列号
        public int[] regdi = new int[32];
        public int[] regdo = new int[32];
        public double[] regai = new double[32];
        public DateTime AITime = DateTime.Now;
        public DateTime DOTime = DateTime.Now;
        public DateTime DITime = DateTime.Now;
        public DateTime AOTime = DateTime.Now;

        public string ip = "";
        public string mbInfo = "";

        public int ainum = 0;//输入寄存器路数
        public int aonum = 0;//保持寄存器路数 
        public int dinum = 0;//光耦路数
        public int donum = 0;//线圈路数
        public int aimode = 0;
        public int IsInverse = 0;
        public string equiptype = "";

        public int IsTiming = 0;
        public int aidonum = 0;
        public int didonum = 0;
        public int rtcdonum = 0;

        public sys_EquipNowModel()
        {
            regdi = new int[4];
            regdo = new int[4];
            regai = new double[32];

            AITime = DateTime.Now;
            DITime = DateTime.Now;
            DOTime = DateTime.Now;
            AOTime = DateTime.Now;
        }

        public void Clear()
        {
            for (int i = 0; i < 4; i++)
            {
                this.regdi[i] = 0;
                this.regdo[i] = 0;
            }

            for (int i = 0; i < 32; i++)
                this.regai[i] = 0;

            SN = "";
            ip = "";
            mbInfo = "";
            IsTiming = 0;
            IsInverse = 0;
            aidonum = 0;
            didonum = 0;
            rtcdonum = 0;
        }

        public void Copy(sys_EquipNowModel model)
        {
            this.ID = model.ID;
            this.SN = model.SN;
            for (int i = 0; i < 4; i++)
            {
                this.regdi[i] = model.regdi[i];
                this.regdo[i] = model.regdo[i];
            }

            for (int i = 0; i < 32; i++)
                this.regai[i] = model.regai[i];

            this.AITime = model.AITime;
            this.DITime = model.DITime;
            this.DOTime = model.DOTime;
            this.AOTime = model.AOTime;

            this.ip = model.ip;
            this.mbInfo = model.mbInfo;

            this.ainum = model.ainum;
            this.aonum = model.aonum;
            this.dinum = model.dinum;
            this.donum = model.donum;
            this.aimode = model.aimode;
            this.IsInverse = model.IsInverse;
            this.equiptype = model.equiptype;
            this.Addr = model.Addr;

            this.IsTiming = model.IsTiming;
            this.aidonum = model.aidonum;
            this.didonum = model.didonum;
            this.rtcdonum = model.rtcdonum;
        }

        public void Update(sys_EquipUNIDModel model)
        {
            this.SN = model.sn;
            for (int i = 0; i < 4; i++)
            {
                this.regdi[i] = model.regdi[i];
                this.regdo[i] = model.regdo[i];
            }

            this.DITime = DateTime.Now;
            this.DOTime = DateTime.Now;
            this.ainum = model.ainum;
            this.aonum = model.aonum;
            this.dinum = model.dinum;
            this.donum = model.donum;
            this.aimode = model.aimode;
            this.IsInverse = model.IsInverse ? 1 : 0;
            this.equiptype = model.PRODUCTID.ToString();
            this.Addr = model.mbaddr;

            this.IsTiming = model.IsTiming ? 1 : 0;
            this.aidonum = model.aidonum;
            this.didonum = model.didonum;
            this.rtcdonum = model.rtcdonum;

        }
    }
}

