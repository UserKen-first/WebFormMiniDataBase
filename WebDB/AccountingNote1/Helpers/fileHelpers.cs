using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AccountingNote1.Helpers
{
    public class fileHelpers
    {
        private static string[] allowFileExt = { ".bmp", ".jpg", ".png" };
        private const int mbs = 1;
        public const int maxLength = mbs * 1024 * 1024;

        private static string GetNewFileName(FileUpload fileUpload)
        {
            // 重名的解一
            System.Threading.Thread.Sleep(10);

            // 重名的解二
            string seqText = new Random((int)DateTime.Now.Ticks).Next(0, 100).ToString().PadLeft(3, '0');

            string orgFileName = fileUpload.FileName;
            string ext = System.IO.Path.GetExtension(orgFileName);
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssFFFFFF") + seqText + ext;
            return newFileName;
        }

        private static bool VadifFileUpload(FileUpload fileUpload, out List<string> msgList)
        {
            msgList = new List<string>();

            if (!ValidFileExt(fileUpload.FileName))
            {
                msgList.Add("Only allow .bmp, .jpg, .png");
            }

            if (!ValidFileLength(fileUpload.FileBytes))
            {
                msgList.Add("Only allow Length:" + mbs + "MB");
            }

            if (msgList.Any())
                return false;
            else
                return true;
        }

        private static bool ValidFileExt(string fileName)
        {
            // => 驗證副檔名
            // 含有.的檔名 
            string ext = System.IO.Path.GetExtension(fileName);

            if (!allowFileExt.Contains(ext.ToLower()))
                return false;
            else
                return true;
        }

         private static bool ValidFileLength(byte[] fileContent)
        {
            if (fileContent.Length > maxLength)
                return false;
            else
                return true;
        }
    }
}