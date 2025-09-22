using Common;
using EquipDriver;
using ModBusRTU;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TCOFurnace.Common;
using TCOFurnace.Models;
using TCOFurnace.UserControls;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TCOFurnace.Forms
{
    public partial class FormParaSet : BaseForm
    {
        private string _labModel = "自定义";
        TCOFRunningMode tCOFRunningMode;
        public string LabModel
        {
            get { return _labModel; }
            set
            {
                if (value.IndexOf("自定义") > -1)
                {
                    _labModel = "自定义";
                }
                else
                {
                    _labModel = value;
                }
            }
        }
        public FormParaSet()
        {
            InitializeComponent();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            //模式数据是否新增加
            bool isAdd = false;
            object modeCode = SqliteHelper.ExecuteScalar($" select max(ModeCode) from TCOF_RunningMode ");
            float intTextFlow = 0;
            //新增
            if (modeCode == null || modeCode ==DBNull.Value || tCOFRunningMode.ModeCode == 0)
            {

                isAdd = true;
                tCOFRunningMode.ModeCode = (modeCode == DBNull.Value || modeCode == null) ? 1 : Convert.ToInt32(modeCode)+1; 
                tCOFRunningMode.ModeName = _labModel;
                tCOFRunningMode.StepCount = 9;
            }
            else
            {
                tCOFRunningMode.ModeName = _labModel;
            }

            if (float.TryParse(this.textFlow.Text, out intTextFlow))
            {
                tCOFRunningMode.Flow = intTextFlow;
            }
            else
            {
                tCOFRunningMode.Flow = 0;
            }

            String strSql = "";
            if (isAdd)
            {
                strSql = $"INSERT INTO TCOF_RunningMode (ModeCode, ModeName, StepCount, Flow) VALUES ({tCOFRunningMode.ModeCode}, '{tCOFRunningMode.ModeName}',{tCOFRunningMode.StepCount},{tCOFRunningMode.Flow}); ";
            }
            else
            {
                strSql += $" UPDATE TCOF_RunningMode SET  Flow = {tCOFRunningMode.Flow} where ModeCode = {tCOFRunningMode.ModeCode} ; ";
            }

            //保存数据
            for (int i = 1; i < 10; i++)
            {
                bool isAddStep = false;
                TCOFRunningStep tCOFRunningStep;
                if (isAdd)
                {
                    isAddStep = true;
                    tCOFRunningStep = new TCOFRunningStep() { ModeCode = tCOFRunningMode.ModeCode, StepNum = i };
                    tCOFRunningMode.RunningSteps.Add(tCOFRunningStep);
                }
                else
                {
                    tCOFRunningStep = tCOFRunningMode.RunningSteps.FirstOrDefault(c => c.StepNum == i);
                    if (tCOFRunningStep == null)
                    {
                        isAddStep = true;
                        tCOFRunningStep = new TCOFRunningStep() { ModeCode = tCOFRunningMode.ModeCode, StepNum = i };
                    }
                }

                tCOFRunningStep.Flow = tCOFRunningMode.Flow;

                #region 给温度时间赋值
                Control[] contrsO = this.Controls.Find($"textTStep{i}O", true);
                if (contrsO.Length > 0 && contrsO[0] is TextBox tb)
                {
                    int intPar;
                    if (int.TryParse(tb.Text, out intPar))
                    {
                        tCOFRunningStep.Otemp = intPar;

                    }
                    else
                    {
                        tCOFRunningStep.Otemp = 0;
                    }
                }


                Control[] contrsC = this.Controls.Find($"textTStep{i}C", true);
                if (contrsC.Length > 0 && contrsC[0] is TextBox tbc)
                {
                    int intPar;
                    if (int.TryParse(tbc.Text, out intPar))
                    {
                        tCOFRunningStep.CTemp = intPar;
                    }
                    else
                    {
                        tCOFRunningStep.CTemp = 0;
                    }
                }

                Control[] contrsT = this.Controls.Find($"textTStep{i}T", true);
                if (contrsT.Length > 0 && contrsT[0] is TextBox tbt)
                {
                    int intPar;
                    if (int.TryParse(tbt.Text, out intPar))
                    {
                        tCOFRunningStep.Times = intPar;
                    }
                    else
                    {
                        tCOFRunningStep.Times = 0;
                    }
                }
                #endregion

                #region 给电压赋值  textVStep1O
                Control[] contrsVO = this.Controls.Find($"textVStep{i}O", true);
                if (contrsVO.Length > 0 && contrsVO[0] is TextBox tbvo)
                {
                    int intPar;
                    if (int.TryParse(tbvo.Text, out intPar))
                    {
                        tCOFRunningStep.OVol = intPar;
                    }
                    else
                    {
                        tCOFRunningStep.OVol = 0;
                    }
                }

                Control[] contrsVC = this.Controls.Find($"textVStep{i}C", true);
                if (contrsVC.Length > 0 && contrsVC[0] is TextBox tbvc)
                {
                    int intPar;
                    if (int.TryParse(tbvc.Text, out intPar))
                    {
                        tCOFRunningStep.CVol = intPar;
                    }
                    else
                    {
                        tCOFRunningStep.CVol = 0;
                    }
                }
                #endregion

                #region 组件sql 语句
                if (isAddStep)
                {
                    strSql += $"INSERT INTO TCOF_RunningStep (ModeCode, StepNum, Times, K1, K2, Otemp, CTemp, OVol, CVol, Flow) VALUES  ({tCOFRunningStep.ModeCode}, {tCOFRunningStep.StepNum}, {tCOFRunningStep.Times},{tCOFRunningStep.K1},{tCOFRunningStep.K2},{tCOFRunningStep.Otemp},{tCOFRunningStep.CTemp},{tCOFRunningStep.OVol},{tCOFRunningStep.CVol},{tCOFRunningStep.Flow}) ; ";
                }
                else
                {
                    strSql += $" UPDATE TCOF_RunningStep  SET Times = {tCOFRunningStep.Times},  Otemp = {tCOFRunningStep.Otemp}, CTemp = {tCOFRunningStep.CTemp}, OVol = {tCOFRunningStep.OVol},     CVol = {tCOFRunningStep.CVol}, Flow = {tCOFRunningStep.Flow} WHERE ModeCode = {tCOFRunningStep.ModeCode} AND StepNum = {tCOFRunningStep.StepNum} ; ";
                }
                #endregion
            }

            int Result = SqliteHelper.ExecuteNonQuery(strSql);
            if (Result > 0)
            {
                MessageBox.Show(LanguageManager.GetMsg("10009"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                MessageBox.Show(LanguageManager.GetMsg("10010"), LanguageManager.GetMsg("10000"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void FormParaSet_Load(object sender, EventArgs e)
        {
            tCOFRunningMode = SqliteHelper.Query<TCOFRunningMode>(
               $" select * From TCOF_RunningMode where modename='自定义' ").FirstOrDefault();

            if (tCOFRunningMode != null)
            {
                tCOFRunningMode.RunningSteps = SqliteHelper.Query<TCOFRunningStep>(
                $"select * from TCOF_RunningStep where  modecode='{tCOFRunningMode.ModeCode}' order by stepnum asc; ").ToList();
            }
            else
            {
                tCOFRunningMode = new TCOFRunningMode();
            }

            //给控件赋值
            if (tCOFRunningMode != null && tCOFRunningMode.RunningSteps != null && tCOFRunningMode.RunningSteps.Count > 0)
            {
                //给流量赋值
                this.textFlow.Text = tCOFRunningMode.Flow.ToString();

                foreach (var item in tCOFRunningMode.RunningSteps)
                {
                    #region 给温度时间赋值
                    Control[] contrsO = this.Controls.Find($"textTStep{item.StepNum}O", true);
                    if (contrsO.Length > 0 && contrsO[0] is TextBox tb)
                    {
                        tb.Text = item.Otemp.ToString();
                    }

                    Control[] contrsC = this.Controls.Find($"textTStep{item.StepNum}C", true);
                    if (contrsC.Length > 0 && contrsC[0] is TextBox tbc)
                    {
                        tbc.Text = item.CTemp.ToString();
                    }

                    Control[] contrsT = this.Controls.Find($"textTStep{item.StepNum}T", true);
                    if (contrsT.Length > 0 && contrsT[0] is TextBox tbt)
                    {
                        tbt.Text = item.Times.ToString();
                    }
                    #endregion

                    #region 给电压赋值  textVStep1O
                    Control[] contrsVO = this.Controls.Find($"textVStep{item.StepNum}O", true);
                    if (contrsVO.Length > 0 && contrsVO[0] is TextBox tbvo)
                    {
                        tbvo.Text = item.OVol.ToString();
                    }

                    Control[] contrsVC = this.Controls.Find($"textVStep{item.StepNum}C", true);
                    if (contrsVC.Length > 0 && contrsVC[0] is TextBox tbvc)
                    {
                        tbvc.Text = item.CVol.ToString();
                    }
                    #endregion
                }
            }
        }
    }
}
