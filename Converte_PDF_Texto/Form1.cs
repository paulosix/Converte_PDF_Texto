﻿using System;
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

        private void btnConvertePDF_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ConvertePDF pdftxt = new ConvertePDF();
            //    txtArquivoTexto.Text = pdftxt.ExtrairTexto_PDF(txtCaminoNomePDF.Text);
            //    if (pdftxt.ExtrairTexto_PDF(txtCaminoNomePDF.Text).Contains("041.253.069-40"))
            //        MessageBox.Show("Achou informação");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Selecione o Diretorio";
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtCaminoNomePDF.Text = folderBrowserDialog1.SelectedPath;
            
            //define as propriedades do controle 
            //OpenFileDialog
            //this.ofd1.Multiselect = false;
            //this.ofd1.Title = "Selecionar PDF";
            //ofd1.InitialDirectory = @"C:\dados";
            ////filtra para exibir somente arquivos de imagens
            //ofd1.Filter = "Files (*.PDF)|*.PDF|" + "All files (*.*)|*.*";
            //ofd1.CheckFileExists = true;
            //ofd1.CheckPathExists = true;
            //ofd1.FilterIndex = 2;
            //ofd1.RestoreDirectory = true;
            //ofd1.ReadOnlyChecked = true;
            //ofd1.ShowReadOnly = false;
            
            //DialogResult dr = this.ofd1.ShowDialog();

            //if (dr == System.Windows.Forms.DialogResult.OK)
            //{
            //    txtCaminoNomePDF.Text = ofd1.FileName;
            //}
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
                    //txtArquivoTexto.Text = pdftxt.ExtrairTexto_PDF(txtCaminoNomePDF.Text);
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