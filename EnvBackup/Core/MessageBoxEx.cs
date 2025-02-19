using System.Windows.Forms;

namespace EnvBackup
{
    public class MessageBoxEx
    {
        public static DialogResult Info(string message, string caption = "信息")
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Warn(string message, string caption = "警告")
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Error(string message, string caption = "错误")
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult Ask(string msg, string caption = "询问")
        {
            return MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
    }
}
