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

namespace Super_Bloco_de_Notas
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void novoDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilha f = new FrmFilha();
            f.Text = String.Format("Novo docoumento - {0}", this.MdiChildren.Length + 1);
            f.MdiParent = this;
            f.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "deseja realmente sair?",
                "sair",
                   MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilha filhaAtiva = (FrmFilha)this.ActiveMdiChild;
            if (filhaAtiva != null)
            {
                try
                {


                    RichTextBox rtTexte = filhaAtiva.rtTextoUsuario;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Arquivo de Texto | *.txt";
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile());
                        for (int i = 0; i < rtTexte.Lines.Length; i++)
                        {
                            writer.WriteLine(rtTexte.Lines[i]);
                        }
                        filhaAtiva.Text = saveFileDialog.FileName;

                        writer.Close();
                        writer.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilha frmFilha = new FrmFilha();
            frmFilha.MdiParent = this;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivo de Texto | *.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog.OpenFile());

                frmFilha.rtTextoUsuario.Text = reader.ReadToEnd();

                reader.Dispose();
                reader.Close();

                frmFilha.Text = openFileDialog.FileName;
                frmFilha.Show();
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copiar_recortar(false);
        }
        private void copiar_recortar(bool recortando)
        {
            FrmFilha frmFilha = (FrmFilha)this.ActiveMdiChild;
            if (frmFilha != null)
            {
                try
                {
                    RichTextBox textBox = frmFilha.rtTextoUsuario;
                    if (textBox != null)
                    {
                        Clipboard.SetText(textBox.SelectedText, TextDataFormat.UnicodeText);
                        if (recortando)
                        {
                            textBox.SelectedText = String.Empty;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Erro ao copiar...");
                }
            }
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copiar_recortar(true);
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilha frmFilha = (FrmFilha)this.ActiveMdiChild;
            if (frmFilha != null)
            {
                try
                {
                    RichTextBox textBox = frmFilha.rtTextoUsuario;
                    if (textBox != null)
                    {
                        IDataObject data = Clipboard.GetDataObject();
                        if (data.GetDataPresent(DataFormats.StringFormat))
                        {
                            textBox.SelectedText = data.GetData(DataFormats.StringFormat).ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Erro ao colar...");
                }

            }
        }
    }

 }

