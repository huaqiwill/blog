using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvBackup
{
    public partial class EnvBackupForm : Form
    {
        public EnvBackupForm()
        {
            InitializeComponent();
        }

        private void EnvBak_Load(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "v1";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            string filename = saveFileDialog.FileName;
            File.WriteAllText(filename, WindowsEnvironment.Get());
            MessageBoxEx.Info("备份成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 询问用户是否继续还原
            if (string.IsNullOrWhiteSpace(previewFileText))
            {
                MessageBoxEx.Error("请先进行预览操作");
                return;
            }

            // 进行还原操作
            Task.Run(() =>
            {
                string title = Text;
                BeginInvoke((MethodInvoker)delegate
                {
                    button3.Text = "还原中";
                    button3.Enabled = false;
                    Text = "还原中，请勿关闭对话框！";
                });
                WindowsEnvironment.Reset(previewFileText);
                BeginInvoke((MethodInvoker)delegate
                {
                    button3.Text = "还原";
                    button3.Enabled = true;
                    Text = title;
                    MessageBoxEx.Info("还原成功");
                });
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] pathArr = WindowsEnvironment.Get().Split(';');
            listBox_view.Items.Clear();
            foreach (var path in pathArr)
            {
                listBox_view.Items.Add(path);
            }
        }

        string previewFileText;

        private void button5_Click(object sender, EventArgs e)
        {
            // 打开文件对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            // 读取文件内容并且加入到列表框
            string text = File.ReadAllText(openFileDialog.FileName);
            string[] pathArr = text.Split(';');
            listBox_view.Items.Clear();
            foreach (var path in pathArr)
            {
                listBox_view.Items.Add(path);
            }

            previewFileText = text;
        }
    }
}
