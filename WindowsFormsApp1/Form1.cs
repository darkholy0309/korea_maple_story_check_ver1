using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Size = new Size(610, 110);
            //http://blog.naver.com/totaes2/221248783837
            //http://gamebulletin.nexon.com/maplestory/inspection3.html
            //http://gamebulletin.nexon.com/maplestory/game3.html
            label1.Location = new Point(10, 10);
            label2.Location = new Point(10, 40);
            label1.Font = new Font("dotum", 14);
            label2.Font = new Font("dotum", 14);
            webBrowser1.Visible = false;
            webBrowser2.Visible = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser2.ScriptErrorsSuppressed = true;
            main();
        }

        void main()
        {
            if (Convert.ToInt32(Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default).OpenSubKey("software\\microsoft\\windows nt\\currentversion").GetValue("currentbuild")) < 22000)
            {
                MessageBox.Show("windows 11 update 21H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                webBrowser1.Navigate("https://maplestory.nexon.com/news/notice/inspection");
                webBrowser2.Navigate("https://maplestory.nexon.com/testworld/main");
            }
            else
            {
                MessageBox.Show("인터넷확인");
                Environment.Exit(0);
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser2.Url.AbsoluteUri == e.Url.AbsoluteUri)
            {
                foreach (HtmlElement htmlelement in webBrowser2.Document.GetElementsByTagName("div"))
                {
                    if (htmlelement.GetAttribute("className") == "test_wrold_side")
                    {
                        string text = null;
                        if (htmlelement.InnerText.Trim().Substring(0, 14) == "테스트월드 운영기간입니다.")
                        {
                            foreach (HtmlElement htmlelement2 in webBrowser2.Document.GetElementsByTagName("div"))
                            {
                                if (htmlelement2.GetAttribute("className") == "contents_wrap")
                                {
                                    if (htmlelement2.Children[2].Children[1].Children[0].Children[1].InnerText.Trim().Substring(0, 4) == "[오픈]")
                                    {
                                        text = htmlelement2.Children[2].Children[1].Children[0].Children[1].InnerText.Trim();
                                    }
                                    if (htmlelement2.Children[2].Children[1].Children[1].Children[1].InnerText.Trim().Substring(0, 4) == "[오픈]")
                                    {
                                        text = htmlelement2.Children[2].Children[1].Children[1].Children[1].InnerText.Trim();
                                    }
                                    if (htmlelement2.Children[2].Children[1].Children[2].Children[1].InnerText.Trim().Substring(0, 4) == "[오픈]")
                                    {
                                        text = htmlelement2.Children[2].Children[1].Children[2].Children[1].InnerText.Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        label2.Text = text;
                        break;
                    }
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri == e.Url.AbsoluteUri)
            {
                foreach (HtmlElement htmlelement in webBrowser1.Document.GetElementsByTagName("div"))
                {
                    if (htmlelement.GetAttribute("className") == "news_board")
                    {
                        string text = null;
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 9) == "(연장)[패치중]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 6) == "[패치예정]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 5) == "[패치중]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 9) == "(연장)[점검중]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 6) == "[점검예정]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        if (htmlelement.Children[0].Children[0].Children[0].InnerText.Trim().Substring(0, 5) == "[점검중]")
                        {
                            text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                        }
                        label1.Text = text;
                        if (text == null)
                        {
                            label1.Text = "공지없음";
                        }
                        label1.Text = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();//test view
                        break;
                    }
                    if (htmlelement.GetAttribute("className") == "information")
                    {
                        label1.Text = htmlelement.Children[0].InnerText.Trim();
                        break;
                    }
                }
            }
        }
    }
}
