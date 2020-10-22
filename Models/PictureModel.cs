using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace WpfPicDB.Models
{
    public class PictureModel
    {
        private readonly int _id;
        private byte[] _bytes;
        private int _index;
        private readonly BitmapImage _bitmap;
        private EXIFModel _exif;
        private IPTCModel _iptc;
        private PhotographerModel _photographer;
        private bool IsSelected;

        public byte[] Bytes { get => _bytes; set => _bytes = value; }
        public int Index { get => _index; set => _index = value; }
        public BitmapImage Bitmap => _bitmap;
        public EXIFModel EXIF { get => _exif; set => _exif = value; }
        public IPTCModel IPTC { get => _iptc; set => _iptc = value; }
        public PhotographerModel Photographer { get => _photographer; set => _photographer = value; }
        public int Id => _id;
        public bool IsSelected1 { get => IsSelected; set => IsSelected = value; }

        public PictureModel(int id, byte[] bytes, EXIFModel exif, IPTCModel iptc, PhotographerModel photographer)
        {
            _id = id;
            _bytes = bytes;
            _exif = exif;
            _iptc = iptc;
            _photographer = photographer;
            _bitmap = CreateBitmapImage(_bytes);
            
        }

        private BitmapImage CreateBitmapImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            BitmapImage image = new BitmapImage();
            using (MemoryStream memorystream = new MemoryStream(bytes))
            {
                memorystream.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = memorystream;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

    }
}
