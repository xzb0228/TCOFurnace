using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EquipDriver
{
    public class EquipInfoLog
    {
        public string prevLogFile = "";
        public string selunid = "";
        string fName = "";
        List<string> adcLine = null;
        long index = 0;
        public void InitFile(bool isNew = true)
        {
            try
            {
                if (selunid == "" || selunid == null) return;
                if (isNew) fName = string.Format("logs//{0}_{1}.csv", DateTime.Now.ToString("yyyyMMddHHmmss"), selunid);

                StringBuilder str = new StringBuilder();
                str.Append("序号,");
                str.Append("时间,"); 
                str.Append("毫秒,");
                str.Append("日期");

                if (Directory.Exists("logs") == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory("logs");
                }
                List<string> lindex = new List<string>();
                lindex.Add(str.ToString());
                File.AppendAllLines(fName, lindex);
                prevWtime = DateTime.Now;
                adcLine = new List<string>();

                prevLogFile = fName;
                index = 0;
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
        }

        private DateTime prevWtime = DateTime.Now;
        public void WriteFile()
        {
            try
            {
                if (selunid != CEquipServer.equipinfo.m_model.SN)
                {                    
                    selunid = CEquipServer.equipinfo.m_model.SN;
                    InitFile();
                }
                index++;
                StringBuilder str = new StringBuilder();
                str.Append(index.ToString() + ",");
                str.Append(DateTime.Now.ToString("HH:mm:ss") + ",");
                for (int i = 0; i < CEquipServer.equipinfo.m_model.ainum; i++)
                {
                    double info = CEquipServer.equipinfo.regai[i];
                    str.Append(info.ToString() + ",");
                }
                str.Append(DateTime.Now.ToString("fff") + ",");
                str.Append(DateTime.Now.ToLongDateString());

                if(adcLine==null) adcLine = new List<string>();
                adcLine.Add(str.ToString());

                if (prevWtime.AddSeconds(10) < DateTime.Now)
                {
                    File.AppendAllLines(fName, adcLine);
                    prevWtime = DateTime.Now;
                    adcLine.Clear();
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
        }
    }
}
