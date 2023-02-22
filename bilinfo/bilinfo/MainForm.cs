/*
 * 由SharpDevelop创建。
 * 用户： ifwz
 * 日期: 2023/2/20
 * 时间: 19:33
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LitJson;
using Sunny.UI;

namespace bilinfo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : UIForm
	{
		public MainForm()
		{
			//https://api.bilibili.com/x/space/wbi/arc/search?mid=1278081874   获取up空间视频
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private string sec_to_hms(string duration)
		{
		TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(duration));
		string str = "";
		if (ts.Hours > 0)
		{
		str = String.Format("{0:00}", ts.Hours)+":"+ String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
		}
		if (ts.Hours == 0 && ts.Minutes > 0)
		{
		str = "00:"+ String.Format("{0:00}", ts.Minutes)+":" + String.Format("{0:00}", ts.Seconds);
		}
		if (ts.Hours == 0 && ts.Minutes == 0)
		{
		str = "00:00:" + String.Format("{0:00}",ts.Seconds);
		}
		return str;
		}
		public	 string timestamp(string ts){
		  System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//当地时区
		  return startTime.AddSeconds(double.Parse(ts)).ToString();
		}
		public	 string timestampYear(string ts){
		  System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//当地时区
		  return startTime.AddSeconds(double.Parse(ts)).ToString("yyyy").ToString();
		}
		
		private string GetWebClient(string url)
		{
		    string strHTML = "";
		    WebClient myWebClient = new WebClient();
		    Stream myStream = myWebClient.OpenRead(url);
		    StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
		    strHTML = sr.ReadToEnd();
		    myStream.Close();
		    return strHTML;
		}
		void UiButton1Click(object sender, EventArgs e)
		{
			string bvid=uiEdit1.Text.Trim();
			string tag=GetWebClient("https://api.bilibili.com/x/tag/archive/tags?bvid="+bvid);
			string info=GetWebClient("https://api.bilibili.com/x/web-interface/view?bvid="+bvid);
			
			string nfo=File.ReadAllText("temp//tvshow.nfo");
			
			
			JsonData json=JsonMapper.ToObject(info);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json["code"]==0) {
				JsonData data=json["data"];
				string title=data["title"].ToString();
				string desc=data["desc"].ToString();
				string aid=data["aid"].ToString();
				string pubdate=timestamp(data["pubdate"].ToString());
				string year=timestampYear(data["pubdate"].ToString());
				string duration=sec_to_hms(data["duration"].ToString());
				string up=data["owner"]["name"].ToString();
				nfo=nfo.Replace("$desc$",desc).Replace("$title$",title).Replace("$duration$",duration).Replace("$year$",year).Replace("$pubdate$",pubdate).Replace("$BV$",bvid).Replace("$up$",up);
			}
			JsonData json2=JsonMapper.ToObject(tag);  //https://blog.csdn.net/DoyoFish/article/details/81976181
			if ((int)json2["code"]==0) {
				JsonData data=json2["data"];
				string tt="";
				for (int i = 0; i < data.Count; i++) {
					tt+="<genre>"+data[i]["tag_name"].ToString()+"</genre>"+"\n";
				}
				for (int j = 0; j < data.Count; j++) {
					tt+="<tag>"+data[j]["tag_name"].ToString()+"</tag>"+"\n";
				} 
				nfo=nfo.Replace("$tag$",tt);
			}
			UIMessageBox.Show("刮削完毕！输出nfo文件...");
			File.WriteAllText("tvshow.nfo",nfo);
		}
		 

	}
}
