using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace baitaptailopbuoi5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Bật tính năng nhận sự kiện phím trên form
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lấy danh sách font hệ thống
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                cmbFonts.Items.Add(font.Name); // cmbFonts là ComboBox chứa Font
            }
            cmbFonts.SelectedItem = "Tahoma"; // Giá trị mặc định

            // Tạo danh sách Size
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cmbSize.Items.Add(size); // cmbSize là ComboBox chứa kích thước
            }
            cmbSize.SelectedItem = 14; // Giá trị mặc định

            // Cài đặt font mặc định cho RichTextBox
            richTextBox1.Font = new Font("Tahoma", 14);
        }

        // Phương thức xử lý sự kiện phím
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu nhấn Ctrl + N
            if (e.Control && e.KeyCode == Keys.N)
            {
                NewFile_Click(sender, e); // Gọi sự kiện tạo file mới
                e.Handled = true; // Đánh dấu sự kiện đã xử lý
            }

            // Kiểm tra nếu nhấn Ctrl + S
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveFile_Click(sender, e); // Gọi sự kiện lưu file
                e.Handled = true; // Đánh dấu sự kiện đã xử lý
            }
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Tahoma", 14); // Reset về mặc định
            cmbFonts.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = 14;
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.FileName.EndsWith(".rtf"))
                    {
                        richTextBox1.LoadFile(openFileDialog.FileName); // Mở file RTF
                    }
                    else
                    {
                        richTextBox1.Text = System.IO.File.ReadAllText(openFileDialog.FileName); // Mở file TXT
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveFileDialog.FileName.EndsWith(".rtf"))
                    {
                        richTextBox1.SaveFile(saveFileDialog.FileName); // Lưu file RTF
                    }
                    else
                    {
                        System.IO.File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text); // Lưu file TXT
                    }
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Bold ? FontStyle.Regular : FontStyle.Bold;
                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Italic ? FontStyle.Regular : FontStyle.Italic;
                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Underline ? FontStyle.Regular : FontStyle.Underline;
                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFonts.SelectedItem != null && cmbSize.SelectedItem != null)
            {
                // Lấy font và kích thước đã chọn
                string selectedFont = cmbFonts.SelectedItem.ToString();
                float size = float.Parse(cmbSize.SelectedItem.ToString());

                // Kiểm tra nếu SelectionFont không null để áp dụng Font mới
                if (richTextBox1.SelectionFont != null)
                {
                    FontStyle currentStyle = richTextBox1.SelectionFont.Style;
                    richTextBox1.SelectionFont = new Font(selectedFont, size, currentStyle);
                }
                else
                {
                    // Nếu không có vùng văn bản được chọn, đặt Font mặc định
                    richTextBox1.Font = new Font(selectedFont, size);
                }
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFonts.SelectedItem != null && cmbSize.SelectedItem != null)
            {
                // Lấy font và kích thước đã chọn
                string selectedFont = cmbFonts.SelectedItem.ToString();
                float size = float.Parse(cmbSize.SelectedItem.ToString());

                // Kiểm tra nếu SelectionFont không null để áp dụng kích thước mới
                if (richTextBox1.SelectionFont != null)
                {
                    FontStyle currentStyle = richTextBox1.SelectionFont.Style;
                    richTextBox1.SelectionFont = new Font(selectedFont, size, currentStyle);
                }
                else
                {
                    // Nếu không có vùng văn bản được chọn, đặt Font mặc định
                    richTextBox1.Font = new Font(selectedFont, size);
                }
            }
        }

        private void tạoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Tahoma", 14); // Reset về mặc định
            cmbFonts.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = 14;
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile_Click(sender, e); // Sử dụng phương thức chung
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile_Click(sender, e); // Sử dụng phương thức chung
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {

        }
    }
}
