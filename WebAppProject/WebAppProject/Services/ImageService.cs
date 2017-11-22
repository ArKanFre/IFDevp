using System.IO;
using System.Web;

namespace WebAppProject.Services
{
    /*
     * A CLASSE ESTUDADA A PARTIR DAS VIDEOS AULAS DO HUGO VASCONCELOS
     * LINK: https://www.youtube.com/user/tutoriais01/videos
     */
    public class ImageService
    {
        /*
         * MÉTODO QUE FAZ O CARREGAMENTO DAS IMAGENS
         */
        public static string UploadPicture(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            return pic;
        }
    }
}