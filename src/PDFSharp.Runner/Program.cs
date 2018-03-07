using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.BarCodes;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PDFSharp.Runner
{
    /// <summary>
    /// Testing the code128 barcode
    /// </summary>
    static class Program
    {
        private static double _xAxis = 15;
        private static string _fontType = "Helvetica";
        private static XFont _h1Font = new XFont(_fontType, 16);
        private static XFont _h2Font = new XFont(_fontType, 13, XFontStyle.Bold);
        private static XFont _font = new XFont(_fontType, 12);
        private static XFont _smallFont = new XFont(_fontType, 8);
        private static XFont _th1Font = new XFont(_fontType, 10);
        private static XFont _tb1Font = new XFont(_fontType, 9);
        private static XFont _barcodeFont = new XFont(_fontType, 14, XFontStyle.Regular);

        static void Main(string[] args)
        {
            // Create a new PDF document
            var document = new PdfDocument();

            // Create an empty page
            var page = document.AddPage();

            page.Size = PageSize.A4;

            // Get an XGraphics object for drawing
            var gfx = XGraphics.FromPdfPage(page);

            gfx.DrawLine(XPens.Black, 1, 1, page.Width - 1, 1);
            gfx.DrawLine(XPens.Black, 1, 1, 1, page.Height - 1);
            gfx.DrawLine(XPens.Black, page.Width - 1, 1, page.Width - 1, page.Height - 1);
            gfx.DrawLine(XPens.Black, 1, page.Height - 1, page.Width - 1, page.Height - 1);

            gfx.DrawH1Text("Vehicle Manifest", page.Width / 2, 25, XStringFormats.Center);

            gfx.DrawLine(XPens.Black, 1, 50, page.Width - 1, 50);

            gfx.DrawH2Text("Shipper Load Reference", _xAxis, 80);

            //code128 barcode
            var code128Barcode = new Code128("(9001)2325/8841/1234")
            {
                Size = new XSize(180, 90)
            };
            gfx.DrawBarCode(code128Barcode, XBrushes.Black, _barcodeFont, new XPoint(_xAxis, 90));

            gfx.DrawH2Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), (page.Width / 2) + (page.Width / 4), 80);

            gfx.DrawText("Despatch Location ID", _xAxis, 200);
            gfx.DrawText("(414)5060/0123/4999/8", _xAxis, 220);

            gfx.DrawText("Load Reference No.", _xAxis + 170, 200);
            gfx.DrawText("(9001)2325/8841/1234", _xAxis + 170, 220);

            gfx.DrawH2Text("Despatch Location", _xAxis, 270);
            gfx.DrawText("Supplier: 6539", _xAxis, 285);
            gfx.DrawText("Blue Earth Foods", _xAxis, 300);
            gfx.DrawText("B98 0RE", _xAxis, 315);

            gfx.DrawH2Text("Deliver To / Haulier", (page.Width / 2), 270);
            gfx.DrawText("Gist Portbury", (page.Width / 2), 285);
            gfx.DrawText("BS20 7XR", (page.Width / 2), 300);

            gfx.DrawText("Total Pallets", _xAxis, 360);
            gfx.DrawText("Total Dollies", _xAxis, 375);
            gfx.DrawText("Total Other", _xAxis, 390);
            gfx.DrawText("Vehicle Registration / Box No", _xAxis, 405);
            gfx.DrawText("Security Seal Number", _xAxis, 420);

            gfx.DrawText("0", _xAxis + 210, 360);
            gfx.DrawText("87", _xAxis + 210, 375);
            gfx.DrawText("0", _xAxis + 210, 390);
            gfx.DrawText("CU1547", _xAxis + 210, 405);
            gfx.DrawText("123456789012", _xAxis + 210, 420);


            var ponos = new List<string>() { "2055703012", "2055703012", "2055703012", "2055703012", "2055703012", "2055703012", };

            var rowHeight = 35;

            // Initial table construct
            // Table head (TH)
            gfx.DrawLine(XPens.Black, 1, 450, page.Width - 1, 450);
            gfx.DrawLine(XPens.Black, 1, 470, page.Width - 1, 470);
            gfx.DrawLine(XPens.Black, 130, 450, 130, 470);
            gfx.DrawLine(XPens.Black, 230, 450, 230, 470);
            gfx.DrawLine(XPens.Black, 310, 450, 310, 470);
            gfx.DrawLine(XPens.Black, 370, 450, 370, 470);
            gfx.DrawLine(XPens.Black, 480, 450, 480, 470);

            gfx.DrawTH1Text("RDC Destination", _xAxis, 465);
            gfx.DrawTH1Text("Delivery Method", _xAxis + 125, 465);
            gfx.DrawTH1Text("Equipment", _xAxis + 230, 465);
            gfx.DrawTH1Text("Quantity", _xAxis + 305, 465);
            gfx.DrawTH1Text("Depot Date/Cycle", _xAxis + 365, 465);
            gfx.DrawTH1Text("Purchase Orders", _xAxis + 475, 465);

            // Table data
            // Table body (TB)
            gfx.DrawTB1Text("M&S Bristol", _xAxis, 485);
            gfx.DrawTB1Text("Chilled", _xAxis + 125, 485);
            gfx.DrawTB1Text("Supplier", _xAxis + 230, 485);
            gfx.DrawTB1Text("Dolly", _xAxis + 235, 500);
            gfx.DrawTB1Text("17", _xAxis + 305, 485);
            gfx.DrawTB1Text(DateTime.Now.ToString("dd/MM/yyyy tt"), _xAxis + 365, 485);

            var currentPos = 470;
            var currentCount = 0;

            ponos.ForEach(x => 
            {
                currentPos += 15;
                gfx.DrawTB1Text(x, _xAxis + 475, currentPos);
            });

            if (ponos.Count > 2)
            {
                rowHeight += (15 * (ponos.Count - 2));
            }

            gfx.DrawLine(XPens.Black, 130, 470, 130, 470 + rowHeight);
            gfx.DrawLine(XPens.Black, 230, 470, 230, 470 + rowHeight);
            gfx.DrawLine(XPens.Black, 310, 470, 310, 470 + rowHeight);
            gfx.DrawLine(XPens.Black, 370, 470, 370, 470 + rowHeight);
            gfx.DrawLine(XPens.Black, 480, 470, 480, 470 + rowHeight);
            gfx.DrawLine(XPens.Black, 1, 470 + rowHeight, page.Width - 1, 470 + rowHeight);


            gfx.DrawLine(XPens.Black, 1, 680, page.Width - 1, 680);
            gfx.DrawLine(XPens.Black, 100, 700, page.Width - 1, 700);
            gfx.DrawLine(XPens.Black, 100, 680, 100, 770);
            gfx.DrawLine(XPens.Black, page.Width / 2 + 50, 680, page.Width / 2 + 50, 770);
            gfx.DrawLine(XPens.Black, 1, 770, page.Width - 1, 770);


            gfx.DrawTB1Text("Quantities", _xAxis, 695);
            gfx.DrawTB1Text("Received As", _xAxis, 710);
            gfx.DrawTB1Text("Indicated", _xAxis, 725);
            gfx.DrawTH1Text("Driver Signature:", _xAxis + 90, 695);
            gfx.DrawTH1Text("Date:", _xAxis + 90, 710);
            gfx.DrawTH1Text("Receiving Site Signature:", _xAxis + 340, 695);
            gfx.DrawTH1Text("Date:", _xAxis + 340, 710);
            gfx.DrawSmallText("* Provide for any delivery differences, damage, quantity variances etc.", _xAxis, 780);

            // Save the document...
            string filename = $"HelloWorld{Guid.NewGuid()}.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        public static void DrawH1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _h1Font, XBrushes.Black, xAxis, yAxis);
        public static void DrawH1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _h1Font, XBrushes.Black, xAxis, yAxis, format);
        public static void DrawH2Text(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _h2Font, XBrushes.Black, xAxis, yAxis);
        public static void DrawH2Text(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _h2Font, XBrushes.Black, xAxis, yAxis, format);
        public static void DrawTH1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _th1Font, XBrushes.Black, xAxis, yAxis);
        public static void DrawTH1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _th1Font, XBrushes.Black, xAxis, yAxis, format);
        public static void DrawTB1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _tb1Font, XBrushes.Black, xAxis, yAxis);
        public static void DrawTB1Text(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _tb1Font, XBrushes.Black, xAxis, yAxis, format);
        public static void DrawText(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _font, XBrushes.Black, xAxis, yAxis);
        public static void DrawText(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _font, XBrushes.Black, xAxis, yAxis, format);
        public static void DrawSmallText(this XGraphics xGraphics, string text, double xAxis, double yAxis) => xGraphics.DrawString(text, _smallFont, XBrushes.Black, xAxis, yAxis);
        public static void DrawSmallText(this XGraphics xGraphics, string text, double xAxis, double yAxis, XStringFormat format) => xGraphics.DrawString(text, _smallFont, XBrushes.Black, xAxis, yAxis, format);
    }
}
