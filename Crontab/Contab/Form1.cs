using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;
using System.Security.Principal;


namespace Crontab
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer = new System.Timers.Timer(1000);
        private static string BaseDir = System.AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");

        private struct cronTask
        {
            public string mins;
            public string hours;
            public string days;
            public string months;
            public string weeks;
            public string command;
        }

        private List<cronTask> cronTasks = new List<cronTask>(); 

        public Form1()
        {
            InitializeComponent();

            this.Closing += new CancelEventHandler(Form1_Closing);

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init timer
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = false;
        }

        //拦截标题栏的关闭事件
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 取消关闭窗体
            e.Cancel = true;

            // 将窗体变为最小化
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; //不显示在系统任务栏 
            this.notifyIcon_cron.Visible = true;
        }

        public void log(string log)
        {
            Console.WriteLine(DateTime.Now.ToLocalTime().ToString() + "---------------------" + log);
        }

        private void list_log(string log)
        {
            string content = DateTime.Now.ToLocalTime().ToString() + " --- " + log;

            cron_logs.Items.Insert(0, content);

            if (cron_logs.Items.Count > 14) {
               cron_logs.Items.RemoveAt(14);
            }

            string path = BaseDir + "cron_" + DateTime.Now.ToShortDateString().ToString().Replace("/", "_") + ".log";
            _WriteContent(path, true, content + "\r\n");
        }

        private static bool checkTimeIsOk(string task_item, int value, string type) {
            if (task_item.IndexOf("/") > 0)
            {
                string[] task_sep = task_item.Split('/');
                string task_i = task_sep[0];
                int sep = Convert.ToInt32(task_sep[1]);

                if (task_i.Trim() == "*")
                {
                    if (value % sep == 0)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (task_i.IndexOf("-") > 0)
                {
                    string[] task_list = task_i.Split('-');
                    int task_start = Convert.ToInt32(task_list[0]);
                    int task_end = Convert.ToInt32(task_list[1]);
                    if (task_start > value || task_end < value)
                    {
                        //Console.WriteLine("3 -- " + type + " -- pass fail -- " + task_item + ":" + value);
                        return false;
                    }

                    if (value % sep == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (task_item.IndexOf("-") > 0)
            {
                string[] task_list = task_item.Split('-');
                int task_start = Convert.ToInt32(task_list[0]);
                int task_end = Convert.ToInt32(task_list[1]);
                if (task_start > value || task_end < value)
                {
                    //Console.WriteLine("1 -- " + type + " -- pass fail -- " + task_item + ":" + value);
                    return false;
                }
            }
            else if (task_item.Trim() != "*" && task_item != value.ToString())
            {
               // Console.WriteLine("2 -- " + type + " -- pass fail -- " + task_item + ":" + value);
                return false;
            }
            //Console.WriteLine(type + " -- pass ok");
            return true;
        }

        private bool checkTaskIsRun(cronTask c) {
            //分　时　日　月　周
            if (c.mins.Trim() == "*" && c.hours.Trim() == "*" &&
                c.days.Trim() == "*" && c.months.Trim() == "*" &&
                c.weeks.Trim() == "*")
            {
                //【开启|关闭】秒监控
                int sec2 = Convert.ToInt32(DateTime.Now.Second);
                if (this.checkBoxMin.Checked)
                {
                    return true;
                }
                else
                {
                    if (sec2 == 0)
                    {
                        return true;
                    }
                    return false;
                }
            }

            //周
            int week = Convert.ToInt32(DateTime.Now.DayOfWeek);
            if (!checkTimeIsOk(c.weeks, week, "week")){
                return false;
            }

            //月
            int month = Convert.ToInt32(DateTime.Now.Month);
            //Console.WriteLine(month.ToString());
            if (!checkTimeIsOk(c.months, month, "month"))
            {
                return false;
            }

            //天
            int day = Convert.ToInt32(DateTime.Now.Day);
            //Console.WriteLine(day.ToString());
            if (!checkTimeIsOk(c.days, day, "day"))
            {
                return false;
            }

            //小时
            int hour = Convert.ToInt32(DateTime.Now.Hour);
            //Console.WriteLine(hour.ToString());
            if (!checkTimeIsOk(c.hours, hour, "hour"))
            {
                return false;
            }

            //分钟
            int min = Convert.ToInt32(DateTime.Now.Minute);
            //Console.WriteLine(min.ToString());
            if (!checkTimeIsOk(c.mins, min, "min"))
            {
                return false;
            }

            //【开启|关闭】秒监控
            int sec = Convert.ToInt32(DateTime.Now.Second);
            //this.log(this.checkBoxMin.Checked.ToString() + ":" + sec.ToString());
            if (this.checkBoxMin.Checked)
            {
                //this.log("开启秒控制");
                return true;
            }
            else 
            {
                //this.log("关闭秒控制");
                if (sec == 0)
                {
                    return true;
                }
                return false;
            }
        }

        //定时显示 监听状态
        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.cron_start.Text == "停止")
            {
                foreach (cronTask c in cronTasks)
                {
                    if (this.checkTaskIsRun(c))
                    {
                        this.list_log(c.command);
                        Wcmd(c.command);
                    }
                }
            }
        }

        private void cron_start_Click(object sender, EventArgs e)
        {
            if (this.cron_start.Text == "启动")
            {
                this.cron_start_start();
                this.cron_start.Text = "停止";     
            }
            else 
            {
                this.cron_start_stop();
                this.cron_start.Text = "启动";
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        private void cron_start_start() {
            log("启动");

            string cron_path = BaseDir + "crontab.txt";

            if (File.Exists(cron_path))
            {
                string content = _ReadContent(cron_path);
                if (content == "")
                {
                    this.list_log("start no cron task");
                }
                else {
                    this.cron_parse(content);
                }
            }
            else {
                this._WriteContent(cron_path, false,"");
            }

            timer.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        private void cron_start_stop() {
            log("停止");

            timer.Stop();
        }

        /// <summary>
        /// 解析cron task
        /// </summary>
        /// <param name="content"></param>
        private void cron_parse(string content) {
            //this.log(content);
            this.cronTasks.Clear();
            string[] cron = content.Split('\r');
            foreach (string cron_i in cron)
            {
                string cron_ii = cron_i.ToString().Trim();
                if (cron_ii != "")
                {
                    this.cron_line_parse(cron_ii);
                }                
            }
        }


        /// <summary>
        /// 对每一行进行解析
        /// </summary>
        /// <param name="cron_line"></param>
        private void cron_line_parse(string cron_line)
        {
            string[] cron_real = cron_line.Split('#');
            if (cron_real[0].Trim() != "")
            {
                //Console.WriteLine(cron_real[0].Trim());
                this.cron_task_parse(cron_real[0].Trim());
            }
        }

        /// <summary>
        /// 任务的拆分
        /// </summary>
        /// <param name="cron_task"></param>
        private void cron_task_parse(string cron_task) {
            string[] cron_parse = cron_task.Split(' ');

            if (cron_parse.Length < 6)
            {
                this.list_log(cron_task + " -- 无法解析");
                return;
            }

            //分　时　日　月　周　命令 解析
            cronTask c = new cronTask();

            c.mins = cron_parse[0].Trim();
            c.hours = cron_parse[1].Trim();
            c.days = cron_parse[2].Trim();
            c.months = cron_parse[3].Trim();
            c.weeks = cron_parse[4].Trim();

            List<string> cron_command = new List<string>();
            for (int i = 5; i < cron_parse.Length; i++)
            {
                string args_t = cron_parse[i].Trim();
                cron_command.Add(args_t);
            }
            c.command = string.Join(" ", cron_command);

            //if (File.Exists(command_s))
            //{
            //    c.command = command_s;

            //    List<string> cron_args = new List<string>();
            //    for (int i = 6; i < cron_parse.Length; i++)
            //    {
            //        string args_t = cron_parse[i].Trim();
            //        cron_args.Add(args_t);
            //    }
            //    c.args = string.Join(" ", cron_args);
            //    c.type = "file";
            //}
            //else 
            //{
                
            //    List<string> cron_command = new List<string>();
            //    for (int i = 5; i < cron_parse.Length; i++)
            //    {
            //        string args_t = cron_parse[i].Trim();
            //        cron_command.Add(args_t);
            //    }
            //    c.command = string.Join(" ", cron_command);
            //    c.type = "cmd";
            //}

            this.log( c.command );
            cronTasks.Add(c);
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.notifyIcon_cron.Visible = false;
            Application.Exit();
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_cron_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }


        /**
         * 常用方法
         */

        /// <summary>
        /// 写入内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool _WriteContent(string path, bool append,string content)
        {
            bool ok = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, append, System.Text.Encoding.UTF8);
            try
            {
                sw.Write(content);
                ok = true;
                sw.Close();
            }
            catch { }
            return ok;
        }
        
        /// <summary>
        ///  读取内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string _ReadContent(string path)
        {
            //string content = "";
            //System.IO.StreamReader sw = new System.IO.StreamReader(path);
            //try
            //{
            //    content = sw.ReadToEnd();
            //}
            //catch {
            //    return "";
            //}
            //sw.Close();
            //return content;
            string str = File.ReadAllText(path);
            return str;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        private void __Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 字符串转md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToMD5(string str)
        {
             byte[] data = Encoding.GetEncoding("GB2312").GetBytes(str);
             MD5 md5 = new MD5CryptoServiceProvider();
             byte[] OutBytes = md5.ComputeHash(data);
 
             string OutString = "";
             for (int i = 0; i < OutBytes.Length; i++)
             {
                 OutString += OutBytes[i].ToString("x2");
             }
             //return OutString.ToUpper();
             return OutString.ToLower();
         }

        //获取超级权限
        private void Wcmd(string cmdtext)
        {
            ProcessStartInfo info = new ProcessStartInfo();

            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            info.Arguments = "/c " + cmdtext;
            info.Verb = "runas";
            Process.Start(info);
        }

        

       
        
    }
}
