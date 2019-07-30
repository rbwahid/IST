using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace IST.Common
{
    public class FileExtension
    {
        public static String GetFileExtensionThumbnail(string ext)
        {
        
            var extLower = ext.ToLower();
            var extThumb = "";
            switch (extLower)
            {
                case ".pdf":
                    extThumb = "/Content/images/File_Extension/file_extension_pdf.png";
                    break;
                case ".xls":
                    extThumb = "/Content/images/File_Extension/file_extension_excel.png";
                    break;
                case ".xlsx":
                    extThumb = "/Content/images/File_Extension/file_extension_excel.png";
                    break;
                case ".csv":
                    extThumb = "/Content/images/File_Extension/file_extension_csv.png";
                    break;
                case ".doc":
                    extThumb = "/Content/images/File_Extension/file_extension_word.png";
                    break;
                case ".docx":
                    extThumb = "/Content/images/File_Extension/file_extension_word.png";
                    break;
                case ".ppt":
                    extThumb = "/Content/images/File_Extension/file_extension_ppt.png";
                    break;
                case ".pptx":
                    extThumb = "/Content/images/File_Extension/file_extension_ppt.png";
                    break;
                case ".jpg":
                    extThumb = "/Content/images/File_Extension/file_extension_jpeg.png";
                    break;
                case ".jpeg":
                    extThumb = "/Content/images/File_Extension/file_extension_jpeg.png";
                    break;
                case ".png":
                    extThumb = "/Content/images/File_Extension/file_extension_png.png";
                    break;
                case ".gif":
                    extThumb = "/Content/images/File_Extension/file_extension_gif.png";
                    break;
                case ".psd":
                    extThumb = "/Content/images/File_Extension/file_extension_psd.png";
                    break;
                case ".txt":
                    extThumb = "/Content/images/File_Extension/file_extension_txt.png";
                    break;
                default:
                    extThumb = "/Content/images/File_Extension/file_extension_unknown.png";
                    break;

            }
            return extThumb;
        }
   
    }
}
