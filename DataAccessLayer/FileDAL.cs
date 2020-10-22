using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WpfPicDB.Models;

namespace WpfPicDB.DataAccessLayer
{
    public class FileDAL
    {
        private static readonly List<string> _extensions = new List<string> { ".BMP", ".JPG", ".JPE", ".JPEG", ".PNG" };

        public List<PictureModel> LoadFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            List<PictureModel> pictures = new List<PictureModel>();
            foreach (string file in files)
            {
                if (_extensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                {
                    pictures.Add(LoadPicture(new FileInfo(file)));
                    pictures.Last().Index = pictures.IndexOf(pictures.Last());
                }
            }
            return pictures;
        }

        private PictureModel LoadPicture(FileInfo fileInfo)
        {
            byte[] picbytes;
            using (Stream filestream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                filestream.Seek(0, SeekOrigin.Begin);
                using BinaryReader reader = new BinaryReader(filestream);
                picbytes = reader.ReadBytes((int)filestream.Length);
            }
            PictureModel picture = new PictureModel(0, picbytes, new EXIFModel(), new IPTCModel(), new PhotographerModel());
            return picture;
        }

        //public static void ZipPictures(List<PictureModel> pictureModels) { }

    }
}
