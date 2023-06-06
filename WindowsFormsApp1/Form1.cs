using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
            Size = new Size(650, 110);
            webBrowser1.Visible = false;
            webBrowser2.Visible = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser2.ScriptErrorsSuppressed = true;
            label1.Location = new Point(10, 10);
            label2.Location = new Point(10, 40);
            label1.Font = new Font("dotum", 14);
            label2.Font = new Font("dotum", 14);
            main_();
            /*
            2023.06
            net framework 4.8 / visual studio 2022 ver 17.6
            http://blog.naver.com/totaes2/221248783837
            http://gamebulletin.nexon.com/maplestory/inspection3.html
            http://gamebulletin.nexon.com/maplestory/game3.html
            */
        }

        void main_()
        {
            RegistryKey localmachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            var opensubkey = localmachine.OpenSubKey("software\\microsoft\\windows nt\\currentversion");
            int currentbuild = Convert.ToInt32(opensubkey.GetValue("currentbuild"));
            if (currentbuild < 19045)
            {
                MessageBox.Show("windows 10 update 22H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                webBrowser1.Navigate("https://maplestory.nexon.com/news/notice/inspection");
                webBrowser2.Navigate("https://maplestory.nexon.com/testworld/main");
            }
            else
            {
                MessageBox.Show("인터넷확인");
                Environment.Exit(0);
            }
            localmachine.Dispose();
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser2.Url.AbsoluteUri == e.Url.AbsoluteUri)
            {
                webbrowser2();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri == e.Url.AbsoluteUri)
            {
                webbrowser1();
            }
        }

        void webbrowser1()
        {
            foreach (HtmlElement htmlelement in webBrowser1.Document.GetElementsByTagName("div"))
            {
                if (htmlelement.GetAttribute("className") == "news_board")
                {
                    string text = null;
                    string innertext = htmlelement.Children[0].Children[0].Children[0].InnerText.Trim();
                    if (innertext.Substring(0, 9) == "(연장)[패치중]")
                    {
                        text = innertext;
                    }
                    if (innertext.Substring(0, 6) == "[패치예정]")
                    {
                        text = innertext;
                    }
                    if (innertext.Substring(0, 5) == "[패치중]")
                    {
                        text = innertext;
                    }
                    if (innertext.Substring(0, 9) == "(연장)[점검중]")
                    {
                        text = innertext;
                    }
                    if (innertext.Substring(0, 6) == "[점검예정]")
                    {
                        text = innertext;
                    }
                    if (innertext.Substring(0, 5) == "[점검중]")
                    {
                        text = innertext;
                    }
                    label1.Text = text;
                    if (text == null)
                    {
                        label1.Text = "공지없음";
                    }
                    label1.Text = innertext;//테스트 보기
                    break;
                }
                if (htmlelement.GetAttribute("className") == "information")
                {
                    label1.Text = htmlelement.Children[0].InnerText.Trim();
                    break;
                }
            }
        }

        void webbrowser2()
        {
            foreach (HtmlElement htmlelement2 in webBrowser2.Document.GetElementsByTagName("div"))
            {
                if (htmlelement2.GetAttribute("className") == "test_wrold_side")
                {
                    string text2 = null;
                    string innertext2 = htmlelement2.InnerText.Trim();
                    if (innertext2.Substring(0, 14) == "테스트월드 운영기간입니다.")
                    {
                        foreach (HtmlElement htmlelement3 in webBrowser2.Document.GetElementsByTagName("div"))
                        {
                            if (htmlelement3.GetAttribute("className") == "contents_wrap")
                            {
                                string innertext_2101 = htmlelement3.Children[2].Children[1].Children[0].Children[1].InnerText.Trim();
                                string innertext_2111 = htmlelement3.Children[2].Children[1].Children[1].Children[1].InnerText.Trim();
                                string innertext_2121 = htmlelement3.Children[2].Children[1].Children[2].Children[1].InnerText.Trim();
                                if (innertext_2101.Substring(0, 4) == "[오픈]")
                                {
                                    text2 = innertext_2101;
                                }
                                if (innertext_2111.Substring(0, 4) == "[오픈]")
                                {
                                    text2 = innertext_2111;
                                }
                                if (innertext_2121.Substring(0, 4) == "[오픈]")
                                {
                                    text2 = innertext_2121;
                                }
                                label2.Text = text2;
                                break;
                            }
                        }
                    }
                    if (text2 == null)
                    {
                        label2.Text = "테섭 공지없음";
                    }
                    break;
                }
            }
        }
    }
}
