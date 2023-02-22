/*
 * 由SharpDevelop创建。
 * 用户： ifwz
 * 日期: 2023/2/22
 * 时间: 23:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using LitJson;
using Sunny.UI;

namespace bilist
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : UIForm
	{
		public MainForm()
		{
			//https://api.bilibili.com/x/space/wbi/arc/search?mid=1278081874   获取up空间视频
			//
			//https://api.bilibili.com/x/v3/fav/resource/list?media_id=87317442&pn=1&ps=20 收藏夹
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	 
		void UiButton1Click(object sender, EventArgs e)
		{
			string rawjson=Clipboard.GetText();
			JsonData json=JsonMapper.ToObject(rawjson);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json["code"]==0) {
				JsonData data=json["data"]["list"]["vlist"];
				
				for (int i = 0; i < data.Count; i++) {
					checkedListBox1.Items.Add(data[i]["title"].ToString()+"#"+data[i]["bvid"].ToString());
				}
				
				
			}
		}
		void UiButton2Click(object sender, EventArgs e)
		{
			string tt="@echo off\n";
				for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
			  if (checkedListBox1.GetItemChecked(i))
			  {
			  	string t=checkedListBox1.GetItemText(checkedListBox1.Items[i]);
			  	string title=t.Split('#')[0];
			  	string bvid=t.Split('#')[1];
			  	tt+="mkdir \""+title+"\""+"\n"+"lux -c cookies.txt -C -o \""+title+"\""+" -O \""+t+"\" \""+"https://www.bilibili.com/video/"+bvid+"\""+"\n";
			  }
			}
				 
				Clipboard.SetText(tt);
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox1.Checked) 
			{
			     for (int j = 0; j < checkedListBox1.Items.Count; j++) 
			        checkedListBox1.SetItemChecked(j, true); 
			}
			else
			{
			for (int j =0; j < checkedListBox1.Items.Count; j++) 
			   checkedListBox1.SetItemChecked(j, false);
			}
		}
		void UiButton4Click(object sender, EventArgs e)
		{
			Process.Start("https://api.bilibili.com/x/space/wbi/arc/search?mid="+Clipboard.GetText());
		}
		void UiButton5Click(object sender, EventArgs e)
		{
			Process.Start("https://api.bilibili.com/x/v3/fav/resource/list?media_id="+Clipboard.GetText()+"&pn=1&ps=20");
		}
		void UiButton3Click(object sender, EventArgs e)
		{
			string rawjson=Clipboard.GetText();
			JsonData json=JsonMapper.ToObject(rawjson);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json["code"]==0) {
				JsonData data=json["data"]["medias"];
				
				for (int i = 0; i < data.Count; i++) {
					checkedListBox1.Items.Add(data[i]["title"].ToString()+"#"+data[i]["bvid"].ToString());
				}
				
				
			}
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void UiButton8Click(object sender, EventArgs e)
		{
			Process.Start("https://api.bilibili.com/x/web-interface/history/cursor");
		}
		void UiButton9Click(object sender, EventArgs e)
		{
			Process.Start("https://api.bilibili.com/x/v2/history/toview/web?jsonp=jsonp");
		}
		void UiButton6Click(object sender, EventArgs e)
		{
			string rawjson=Clipboard.GetText();
			JsonData json=JsonMapper.ToObject(rawjson);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json["code"]==0) {
				JsonData data=json["data"]["list"];
				
				for (int i = 0; i < data.Count; i++) {
					checkedListBox1.Items.Add(data[i]["title"].ToString()+"#"+data[i]["history"]["bvid"].ToString());
				}
				
				
			}
		}
		void UiButton7Click(object sender, EventArgs e)
		{
			string rawjson=Clipboard.GetText();
			JsonData json=JsonMapper.ToObject(rawjson);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json["code"]==0) {
				JsonData data=json["data"]["list"];
				
				for (int i = 0; i < data.Count; i++) {
					checkedListBox1.Items.Add(data[i]["title"].ToString()+"#"+data[i]["bvid"].ToString());
				}
				
				
			}
		}
	}
}
