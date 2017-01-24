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

namespace Converte_PDF_Texto
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Selecione o Diretorio";
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtCaminoNomePDF.Text = folderBrowserDialog1.SelectedPath;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtArquivoTexto.Text = "";

            DirectoryInfo Dir = new DirectoryInfo(@txtCaminoNomePDF.Text);
            FileInfo[] files = Dir.GetFiles("*.pdf", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
            {
                try
                {
                    ConvertePDF pdftxt = new ConvertePDF();
                    if (pdftxt.ExtrairTexto_PDF(file.FullName).Contains(txtCPF.Text))
                        txtArquivoTexto.Text += file.FullName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(@txtCaminoNomePDF.Text + "\\" + txtCPF.Text))
            {
                Directory.CreateDirectory(@txtCaminoNomePDF.Text + "\\" + txtCPF.Text);

                DirectoryInfo Dir = new DirectoryInfo(@txtCaminoNomePDF.Text);
                FileInfo[] files = Dir.GetFiles("*.pdf", SearchOption.AllDirectories);

                foreach (FileInfo file in files)
                {
                    ConvertePDF pdftxt = new ConvertePDF();
                    if (pdftxt.ExtrairTexto_PDF(file.FullName).Contains(txtCPF.Text))
                        File.Copy(txtArquivoTexto.Text, txtCaminoNomePDF.Text + @"\" + txtCPF.Text + @"\" + file.Name, true);
                }  
            }
            else
            {
                DirectoryInfo Dir = new DirectoryInfo(@txtCaminoNomePDF.Text);
                FileInfo[] files = Dir.GetFiles("*.pdf", SearchOption.AllDirectories);

                foreach (FileInfo file in files)
                {
                    ConvertePDF pdftxt = new ConvertePDF();
                    if (pdftxt.ExtrairTexto_PDF(file.FullName).Contains(txtCPF.Text))
                        File.Copy(txtArquivoTexto.Text, txtCaminoNomePDF.Text + @"\" + txtCPF.Text + @"\" + file.Name, true);
                }               
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Aplicativo Minimizado para Bandeja";
            notifyIcon1.BalloonTipText = "Você minimizou com êxito sua aplicação.";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
