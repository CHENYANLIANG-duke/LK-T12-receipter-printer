using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCodeTest
{
    public partial class QRCodeTest : Form
    {
        public QRCodeTest()
        {
            InitializeComponent();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            String QRStr1 = "123456789012345678901234567890";
            String QRStr2 = "**1234567890";


            LKPrint.OpenPort("USB", 0);

            LKPrint.PrintStart();

            //列印一個code 39條碼，並且在列印後多帶一行Enter
            LKPrint.PrintBarCode("1234567890123456789", LKPrint.LK_BCS_Code39, 40, 472, LKPrint.LK_ALIGNMENT_CENTER, LKPrint.LK_HRI_TEXT_NONE);
            LKPrint.PrintText("\n", LKPrint.LK_ALIGNMENT_LEFT, LKPrint.LK_FNT_DEFAULT, LKPrint.LK_TXT_1WIDTH);

            //設定 Page mode大小，高最少要180個dot才不會有問題
            LKPrint.SetLabelSize(360, 480);      // width, height 

            LKPrint.PrintQRCodeAlign(LKPrint.LK_ALIGNMENT_LEFT, 0, QRStr1, 0, 3, LKPrint.LK_QRCODE_EC_LEVEL_L, LKPrint.LK_QRCODE_VERSION_06, LKPrint.LK_QRCODE_MASK_AUTO);

            LKPrint.PrintQRCodeAlign(LKPrint.LK_ALIGNMENT_RIGHT, 0, QRStr2, 0, 3, LKPrint.LK_QRCODE_EC_LEVEL_L, LKPrint.LK_QRCODE_VERSION_06, LKPrint.LK_QRCODE_MASK_AUTO); 

            LKPrint.PrintTTFAlign(LKPrint.LK_ALIGNMENT_LEFT, 130, "Arial", 1, 30, "測試列印12345678", 0);

            LKPrint.PrintLabel();  //列印剛剛Page mode的內容

            // 字串、對齊位置、字型?、字體大小
            LKPrint.PrintText("DUKE TRY IT  ", LKPrint.LK_ALIGNMENT_LEFT, LKPrint.LK_FNT_DEFAULT, LKPrint.LK_TXT_2WIDTH);

            

            LKPrint.CutPaper();


            LKPrint.PrintStop();
        }


    }
}
