using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace NginxCmdGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.textBox1.Text.ToLower();
            if (this.textBox1.Text.IndexOf(".") != -1)
            {
                this.textBox1.Text = this.textBox1.Text.Split('.')[0];
            }
            this.textBox2.Text = this.textBox1.Text.Replace(".exe", "")+"-service";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private String getAssertFile(string name)
        {
            Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("NginxCmdGen."+name);
            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            UTF8Encoding con = new UTF8Encoding();
            string str = con.GetString(bs);
            return str;
        }

        private void saveAssertFile(string name,string saveName, string path)
        {
            if (saveName == null || saveName.Length == 0)
            {
                saveName = name;
            }
            Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("NginxCmdGen." + name);
            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllBytes(path + saveName, bs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string exeName = this.textBox1.Text.ToLower().Trim();
            string srvName = this.textBox2.Text.ToLower().Trim();
            string exeDir = this.textBox3.Text.Trim().Replace("\\","/");
            string binDir = this.textBox5.Text.Trim().Replace("\\","/");
            string args = this.textBox4.Text;

            if (!exeDir.EndsWith("/"))
            {
                exeDir += "/";
            }

            if (binDir.Length>0 && binDir.StartsWith("/"))
            {
                binDir = "." + binDir;
            }

            if (binDir.Length > 0 && !binDir.EndsWith("/"))
            {
                binDir += "/";
            }

            binDir = exeDir + binDir;

            if (!Directory.Exists(exeDir))
            {
                MessageBox.Show("启动目录不存在！","错误提示",MessageBoxButtons.OK);
                return;
            }

            if (!File.Exists(binDir+exeName+".exe"))
            {
                MessageBox.Show("镜像文件不存在！", "错误提示", MessageBoxButtons.OK);
                return;
            }

            if (srvName.Length == 0)
            {
                MessageBox.Show("服务名称必须填写！", "错误提示", MessageBoxButtons.OK);
                return;
            }

            DialogResult dr = MessageBox.Show("确认镜像名称为"+exeName+"，服务名为"+srvName+"吗？", "请确认", 
                MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                string startBatFile = "1.1-startup.bat";
                string stopBatFile = "1.2-stop.bat";
                if (this.checkBox1.Checked)
                {
                    startBatFile = "1.1-start-nohup.bat";
                    stopBatFile = "1.2-stop-nohup.bat";
                }
                string[] batFiles = { 
                                        startBatFile ,
                                        stopBatFile ,
                                        "1.3-check-state.bat" ,
                                        "1.4-service-install.bat" ,
                                        "1.5-service-remove.bat" ,
                                        "1.6-reload-cfg.bat" ,
                                        "1.7-reopen-log.bat" ,
                                    };
                if (this.checkBox2.Checked)
                {
                    for (int i = 5; i < batFiles.Length; i++)
                    {
                        batFiles[i]="";
                    }
                }
                for (int i = 0; i < batFiles.Length; i++)
                {
                    string filename = batFiles[i];
                    if (filename.Length == 0)
                    {
                        continue;
                    }
                    string content = getAssertFile(filename);
                    content = content.Replace("{exefile}",exeName+".exe");
                    content = content.Replace("{exedir}", exeDir);
                    content = content.Replace("{srvname}", srvName);
                    content = content.Replace("{args}", args);
                    content = content.Replace("{startfile}", startBatFile);
                    content = content.Replace("{stopfile}", stopBatFile);
                    content = content.Replace("{bindir}", binDir);
                    string path = exeDir + filename;
                    File.WriteAllText(path, content, new UTF8Encoding(false));
                }

                saveAssertFile("srv.exe",srvName+".exe",exeDir);
                saveAssertFile("WinSrvD.exe", null, exeDir+"WinSrvD/");
                saveAssertFile("ReadMe.md", null, exeDir + "WinSrvD/");

                MessageBox.Show("生成成功！","恭喜！");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox3.Text = Application.ExecutablePath.Replace("\\","/");
            this.textBox3.Text = this.textBox3.Text.Substring(0, this.textBox3.Text.LastIndexOf("/")+1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                MessageBox.Show("选择此选项，程序崩溃时会重新启动，请谨慎选择。","温馨提示");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                MessageBox.Show("选择此选项，将只生成通用命令，不包含Nginx特有命令。", "温馨提示");
            }
        }
    }
}
