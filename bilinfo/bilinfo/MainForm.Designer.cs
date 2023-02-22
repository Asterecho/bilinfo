/*
 * 由SharpDevelop创建。
 * 用户： ifwz
 * 日期: 2023/2/20
 * 时间: 19:33
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace bilinfo
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private Sunny.UI.UIButton uiButton1;
		private Sunny.UI.UIEdit uiEdit1;
		private Sunny.UI.UILabel uiLabel1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.uiButton1 = new Sunny.UI.UIButton();
			this.uiEdit1 = new Sunny.UI.UIEdit();
			this.uiLabel1 = new Sunny.UI.UILabel();
			this.SuspendLayout();
			// 
			// uiButton1
			// 
			this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiButton1.Location = new System.Drawing.Point(194, 55);
			this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
			this.uiButton1.Name = "uiButton1";
			this.uiButton1.Size = new System.Drawing.Size(110, 48);
			this.uiButton1.TabIndex = 0;
			this.uiButton1.Text = "刮削";
			this.uiButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			this.uiButton1.Click += new System.EventHandler(this.UiButton1Click);
			// 
			// uiEdit1
			// 
			this.uiEdit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uiEdit1.DoubleValue = 0D;
			this.uiEdit1.EnterAsTab = false;
			this.uiEdit1.Font = new System.Drawing.Font("微软雅黑", 15F);
			this.uiEdit1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.uiEdit1.Location = new System.Drawing.Point(16, 69);
			this.uiEdit1.MaxValue = 2147483647D;
			this.uiEdit1.MinValue = 0D;
			this.uiEdit1.Name = "uiEdit1";
			this.uiEdit1.Size = new System.Drawing.Size(162, 34);
			this.uiEdit1.TabIndex = 1;
			this.uiEdit1.Text = " ";
			this.uiEdit1.Watermark = "";
			this.uiEdit1.WaterMarkActiveForeColor = System.Drawing.Color.Gray;
			this.uiEdit1.WaterMarkColor = System.Drawing.Color.Gray;
			// 
			// uiLabel1
			// 
			this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiLabel1.Location = new System.Drawing.Point(15, 43);
			this.uiLabel1.Name = "uiLabel1";
			this.uiLabel1.Size = new System.Drawing.Size(121, 19);
			this.uiLabel1.TabIndex = 2;
			this.uiLabel1.Text = "输入BV号";
			this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(318, 126);
			this.Controls.Add(this.uiLabel1);
			this.Controls.Add(this.uiEdit1);
			this.Controls.Add(this.uiButton1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "bilinfo";
			this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 480);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
