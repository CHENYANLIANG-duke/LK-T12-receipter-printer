
using QRCodeTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK_T12
{
    public partial class Form1 : Form
    {

        System.DateTime dtNow = System.DateTime.Now;
        Random rnd = new Random();
       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Add extra initialization here
            for (int i = 1; i < 5; i++)
            {
                portNameCBox.Items.Add("COM" + i);
            }

            for (int i = 1; i < 4; i++)
            {
                portNameCBox.Items.Add("LPT" + i);
            }

            portNameCBox.Items.Add("USB");
            portNameCBox.Items.Add("TCP/IP");
            portNameCBox.SelectedIndex = 0;
            // BaudRate
            baudRateCBox.Items.Add("115200");
            baudRateCBox.Items.Add("57600");
            baudRateCBox.Items.Add("38400");
            baudRateCBox.Items.Add("19200");
            baudRateCBox.Items.Add("9600");
            baudRateCBox.Items.Add("4800");
            baudRateCBox.SelectedIndex = 2;

            closeButton.Enabled = false;
            printSampleButton.Enabled = false;
            txtIP.Enabled = false;
            

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            LKPrint.ClosePort();
            this.Close();
        }

        private void portNameCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sIndex = portNameCBox.SelectedIndex;
            if (sIndex == 8)
            {
                baudRateCBox.Enabled = false;
                txtIP.Enabled = true;
            }
            else
            {
                baudRateCBox.Enabled = true;
                txtIP.Enabled = false;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            string port;
            long lResult = 0;
            Int32 baudRate = 0;

            int sIndex = portNameCBox.SelectedIndex;
            if (sIndex == 8)
            {
                lResult = LKPrint.OpenPort(txtIP.Text, 9100);
            }
            else
            {
                port = portNameCBox.SelectedItem.ToString();
                baudRate = Int32.Parse(baudRateCBox.SelectedItem.ToString());
                lResult = LKPrint.OpenPort(port, baudRate);
            }
            if (lResult != 0)
            {
                MessageBox.Show("Open Port Failed", "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                portNameCBox.Enabled = false;
                baudRateCBox.Enabled = false;
                openButton.Enabled = false;
                closeButton.Enabled = true;
                printSampleButton.Enabled = true;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            long lResult = LKPrint.ClosePort();
            if (lResult != 0)
            {
                MessageBox.Show("Close Port Failed", "Error", MessageBoxButtons.OK);
            }
            else
            {
                int sIndex = portNameCBox.SelectedIndex;
                if (sIndex > 8)
                {
                    baudRateCBox.Enabled = false;
                }
                else
                {
                    baudRateCBox.Enabled = true;
                    portNameCBox.Enabled = true;
                    openButton.Enabled = true;
                    closeButton.Enabled = false;
                    printSampleButton.Enabled = false;
                }
            }
        }

        private void printSampleButton_Click(object sender, EventArgs e)
        {

            string Datetime = dtNow.ToString("yyyy-MM-dd     HH:MM:ss");
            string StoreName = textBox1.Text;
            string ReceiptDate = textBox2.Text + "年" + textBox3.Text + "-" + textBox4.Text + "月";
            string StoreNo = textBox5.Text;
            string TotalAcount = textBox6.Text;
            string ReceiptNo = textBox7.Text;
            string RndNo = rnd.Next(0, 9999).ToString();
            string QR1 = textBox7.Text+textBox2.Text+textBox3.Text+textBox4.Text+RndNo + "000000000000" + StoreNo+ "kEMSFSEB63U0rjm6abfhjw==.*********:1:1:1:鴨肉飯PET590:1:25";
            string QR2 = "**經典鴨肉飯:1:45";

            LKPrint.PrintStart();

            LKPrint.SetLabelSize(400, 325);

            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_CENTER, 10, "Arial", 1, 65, StoreName, 0);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_CENTER, 70, "Arial", 1, 55, "電子發票證明聯", 0);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_CENTER, 130, "Arial", 1, 55,ReceiptDate, 0);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_CENTER, 175, "Arial", 1, 55, ReceiptNo, 0);
          
            LKPrint.PrintTTFXY(30, 240, "新細明體", 1, 23, Datetime, 0);
            LKPrint.PrintTTFXY(30, 265, "新細明體", 1, 23, "隨機碼:" + RndNo + "      總計:   " + TotalAcount, 0);
            LKPrint.PrintTTFXY(30, 290, "新細明體", 1, 23, "賣方:  " + StoreNo, 0);

            LKPrint.PrintLabel();

            LKPrint.PrintBarCode(textBox2.Text.ToString()+textBox4.Text.ToString()+ ReceiptNo+RndNo, LKPrint.LK_BCS_Code39, 50, 256, LKPrint.LK_ALIGNMENT_CENTER, LKPrint.LK_HRI_TEXT_NONE);

            LKPrint.SetLabelSize(400, 130);
            LKPrint.PrintQRCodeXY(40, 0, QR1, 0, 3, LKPrint.LK_QRCODE_EC_LEVEL_L, LKPrint.LK_QRCODE_VERSION_06, LKPrint.LK_QRCODE_MASK_AUTO);
            LKPrint.PrintQRCodeXY(230, 0, QR2, 0, 3, LKPrint.LK_QRCODE_EC_LEVEL_L, LKPrint.LK_QRCODE_VERSION_06, LKPrint.LK_QRCODE_MASK_AUTO);
            LKPrint.PrintLabel();

            LKPrint.PrintText("\n", LKPrint.LK_ALIGNMENT_CENTER, LKPrint.LK_FNT_DEFAULT, LKPrint.LK_TXT_2WIDTH);
            LKPrint.PrintText("七賢   131478  序749160  機3\n", LKPrint.LK_ALIGNMENT_CENTER, LKPrint.LK_FNT_DEFAULT, LKPrint.LK_TXT_1WIDTH);
            LKPrint.PrintText("退貨憑電子發票證明聯正本辦理\n", LKPrint.LK_ALIGNMENT_CENTER, LKPrint.LK_FNT_DEFAULT, LKPrint.LK_TXT_1WIDTH);

            LKPrint.CutPaper();

            LKPrint.SetLabelSize(400, 200);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_LEFT, 10, "Arial", 1, 45, StoreName, 0);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_LEFT, 70, "Arial", 1, 45, "等候第 045號", 0);
            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_LEFT, 130, "Arial", 1, 30, "時間:" + Datetime+" $"+TotalAcount, 0);
            LKPrint.PrintLabel();
            LKPrint.CutPaper();

            LKPrint.PrintStop();

        }
    }
}
