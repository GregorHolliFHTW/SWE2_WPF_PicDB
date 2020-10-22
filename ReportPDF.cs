using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using WpfPicDB.Models;

namespace WpfPicDB
{
    public class ReportPDF
    {
        public static void ReportPicture(PictureModel picture)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PICTURE Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Arial", 20);

            StringBuilder photographer = new StringBuilder();
            photographer.Append("Photographer: ");
            photographer.Append(picture.Photographer.FirstName);
            photographer.Append(" " + picture.Photographer.LastName);
            XRect photographerRect = new XRect(0, 10, page.Width, page.Height);
            gfx.DrawString(photographer.ToString(), fontHeader, XBrushes.Black, photographerRect, XStringFormats.TopCenter);
            double imgHeight;
            using (Stream ms = new MemoryStream(picture.Bytes))
            {
                XImage img = XImage.FromStream(ms);
                img.Interpolate = false;
                imgHeight = (double)img.PixelHeight / img.PixelWidth * page.Width / 2;

                XRect imageRect = new XRect((page.Width / 2) / 2, 50, page.Width / 2, imgHeight);
                gfx.DrawImage(img, imageRect);
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Title", picture.IPTC.Title);
            data.Add("Author", picture.EXIF.Author);
            data.Add("Description", picture.IPTC.Description);
            data.Add("Tags", picture.IPTC.Keywords);
            AddList(gfx, data, (page.Width / 2) / 2, imgHeight + 60, page.Width / 2, 0);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Picture Report.pdf";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF documents (.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                document.Save(dlg.FileName);
            }
        }

        public static void ReportTags(List<PictureModel> pictures)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Tags Report";
            StringBuilder header = new StringBuilder("The following Tags where found: ");
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Arial", 20);
            XRect headerRect = new XRect(10, 10, page.Width, page.Height);
            gfx.DrawString(header.ToString(), fontHeader, XBrushes.Black, headerRect, XStringFormats.TopLeft);
            Dictionary<string, int> totalTagsRatio = new Dictionary<string, int>();
            foreach (var picture in pictures)
            {
                List<string> totalTags = new List<string>(picture.IPTC.Keywords.Split(",").Select(p => p.Trim()).ToList());
                foreach (var tag in totalTags)
                {
                    if (totalTagsRatio.ContainsKey(tag))
                    {
                        totalTagsRatio[tag] += 1;
                    }
                    else if (!string.IsNullOrEmpty(tag))
                    {
                        totalTagsRatio.Add(tag, 1);
                    }
                }
            }
            AddList(gfx, totalTagsRatio, 10, 35, page.Width, 0);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "TagsReport.pdf";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF documents (.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                document.Save(dlg.FileName);
            }
        }

        public static void ReportPictureList(ObservableCollection<PictureModel> pictures)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Picture Extraction Report";
            StringBuilder header = new StringBuilder("The following Pictures were selected: ");
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Arial", 20);
            XRect headerRect = new XRect(0, 10, page.Width, page.Height);
            gfx.DrawString(header.ToString(), fontHeader, XBrushes.Black, headerRect, XStringFormats.TopLeft);
            int counter = 0;
            foreach (var picture in pictures)
            {
                double imgHeight;
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                using (Stream ms = new MemoryStream(picture.Bytes))
                {
                    XImage img = XImage.FromStream(ms);
                    img.Interpolate = false;
                    imgHeight = (double)img.PixelHeight / img.PixelWidth * page.Width / 2;

                    XRect imageRect = new XRect((page.Width / 2) / 2, 50, page.Width / 2, imgHeight);
                    gfx.DrawImage(img, imageRect);
                }
                counter++;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "PictureExtractionReport.pdf";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF documents (.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                document.Save(dlg.FileName);
            }
        }

        private static void AddList(XGraphics gfx, Dictionary<string, string> data, double x, double y, double width, double height)
        {
            XFont fontData = new XFont("Arial", 12);
            foreach (var item in data)
            {
                XRect photoDataRect = new XRect(x, y, width, height);
                gfx.DrawString(item.Key + ": " + item.Value, fontData, XBrushes.Black, photoDataRect, XStringFormats.TopLeft);
                y += 15;
            }
        }

        private static void AddList(XGraphics gfx, Dictionary<string, int> data, double x, double y, double width, double height)
        {
            XFont fontData = new XFont("Arial", 12);
            foreach (var item in data)
            {
                XRect photoDataRect = new XRect(x, y, width, height);
                gfx.DrawString(item.Key + ": " + item.Value, fontData, XBrushes.Black, photoDataRect, XStringFormats.TopLeft);
                y += 15;
            }
        }
    }
}
