namespace TCOFurnace
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labTCO = new System.Windows.Forms.Label();
            this.butModel = new System.Windows.Forms.Button();
            this.butFish = new System.Windows.Forms.Button();
            this.butMeat = new System.Windows.Forms.Button();
            this.butSoil = new System.Windows.Forms.Button();
            this.butPlant = new System.Windows.Forms.Button();
            this.butCustomize = new System.Windows.Forms.Button();
            this.butManualMode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labTCO
            // 
            this.labTCO.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTCO.Location = new System.Drawing.Point(116, 67);
            this.labTCO.Name = "labTCO";
            this.labTCO.Size = new System.Drawing.Size(554, 29);
            this.labTCO.TabIndex = 0;
            this.labTCO.Text = "有机氚碳氧化系统";
            this.labTCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // butModel
            // 
            this.butModel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butModel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butModel.Location = new System.Drawing.Point(116, 150);
            this.butModel.Name = "butModel";
            this.butModel.Size = new System.Drawing.Size(250, 70);
            this.butModel.TabIndex = 1;
            this.butModel.Text = "标准模式";
            this.butModel.UseVisualStyleBackColor = false;
            this.butModel.Click += new System.EventHandler(this.butModel_Click);
            // 
            // butFish
            // 
            this.butFish.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butFish.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butFish.Location = new System.Drawing.Point(414, 150);
            this.butFish.Name = "butFish";
            this.butFish.Size = new System.Drawing.Size(120, 70);
            this.butFish.TabIndex = 2;
            this.butFish.Text = "鱼类";
            this.butFish.UseVisualStyleBackColor = false;
            this.butFish.Click += new System.EventHandler(this.butFish_Click);
            // 
            // butMeat
            // 
            this.butMeat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butMeat.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butMeat.Location = new System.Drawing.Point(550, 150);
            this.butMeat.Name = "butMeat";
            this.butMeat.Size = new System.Drawing.Size(120, 70);
            this.butMeat.TabIndex = 3;
            this.butMeat.Text = "肉类";
            this.butMeat.UseVisualStyleBackColor = false;
            this.butMeat.Click += new System.EventHandler(this.butMeat_Click);
            // 
            // butSoil
            // 
            this.butSoil.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butSoil.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butSoil.Location = new System.Drawing.Point(116, 277);
            this.butSoil.Name = "butSoil";
            this.butSoil.Size = new System.Drawing.Size(120, 70);
            this.butSoil.TabIndex = 4;
            this.butSoil.Text = "土壤";
            this.butSoil.UseVisualStyleBackColor = false;
            this.butSoil.Click += new System.EventHandler(this.butSoil_Click);
            // 
            // butPlant
            // 
            this.butPlant.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butPlant.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butPlant.Location = new System.Drawing.Point(246, 277);
            this.butPlant.Name = "butPlant";
            this.butPlant.Size = new System.Drawing.Size(120, 70);
            this.butPlant.TabIndex = 5;
            this.butPlant.Text = "植物";
            this.butPlant.UseVisualStyleBackColor = false;
            this.butPlant.Click += new System.EventHandler(this.butPlant_Click);
            // 
            // butCustomize
            // 
            this.butCustomize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butCustomize.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butCustomize.Location = new System.Drawing.Point(414, 277);
            this.butCustomize.Name = "butCustomize";
            this.butCustomize.Size = new System.Drawing.Size(120, 70);
            this.butCustomize.TabIndex = 6;
            this.butCustomize.Text = "自定义";
            this.butCustomize.UseVisualStyleBackColor = false;
            this.butCustomize.Click += new System.EventHandler(this.butCustomize_Click);
            // 
            // butManualMode
            // 
            this.butManualMode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butManualMode.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butManualMode.Location = new System.Drawing.Point(550, 277);
            this.butManualMode.Name = "butManualMode";
            this.butManualMode.Size = new System.Drawing.Size(120, 70);
            this.butManualMode.TabIndex = 7;
            this.butManualMode.Text = "手动模式";
            this.butManualMode.UseVisualStyleBackColor = false;
            this.butManualMode.Click += new System.EventHandler(this.butManualMode_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.butManualMode);
            this.Controls.Add(this.butCustomize);
            this.Controls.Add(this.butPlant);
            this.Controls.Add(this.butSoil);
            this.Controls.Add(this.butMeat);
            this.Controls.Add(this.butFish);
            this.Controls.Add(this.butModel);
            this.Controls.Add(this.labTCO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.Text = "有机氚碳氧化系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labTCO;
        private System.Windows.Forms.Button butModel;
        private System.Windows.Forms.Button butFish;
        private System.Windows.Forms.Button butMeat;
        private System.Windows.Forms.Button butSoil;
        private System.Windows.Forms.Button butPlant;
        private System.Windows.Forms.Button butCustomize;
        private System.Windows.Forms.Button butManualMode;
    }
}

