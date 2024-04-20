//https://www.c-sharpcorner.com/UploadFile/f2e803/basic-pdf-creation-using-itextsharp-part-i/

using DNTPersianUtils.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;
class Program
{
    static void Main(string[] args)
    {
        // set encoding
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        // configure doc
        FileStream fileStream = new FileStream("2024_1402_DailyPlanner.pdf", FileMode.Create);
        Document document = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

        // fonts
        //Create a base font object making sure to specify IDENTITY-H
        var fontTahoma = BaseFont.CreateFont("C:/Windows/Fonts/tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        var fontSamim = BaseFont.CreateFont("Samim.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        var fontShabnam = BaseFont.CreateFont("Shabnam.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        // get bg image
        Image backgroundImage = Image.GetInstance("template_yellow.png");
        backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
        backgroundImage.Alignment = Image.UNDERLYING;
        backgroundImage.SetAbsolutePosition(0, 0);

        // Start date: January 1st, 2024
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);

        //Open document for writing
        document.Open();

        var pageNumber = 0;
        // Iterate through each day from start date to end date
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            //Add a page
            document.NewPage();
            pageNumber++;

            // add bg
            document.Add(backgroundImage);

            // Day Of Month
            var dayOfMonthContent = $"{date.ToString("dd")}";
            var dayOfMonthFont = new Font(fontSamim, 45, Font.BOLD, new BaseColor(77, 73, 65));
            ColumnText.ShowTextAligned(writer.DirectContent,
                                        Element.ALIGN_RIGHT, // text align
                                        new Phrase(dayOfMonthContent, dayOfMonthFont), // content
                                        document.PageSize.Width - 492, // x
                                        753, // y
                                        0,// rotation
                                        PdfWriter.RUN_DIRECTION_LTR, // direction
                                        0); // arabic options

            // DAY OF WEEK
            var dayOfWeekContent = $"{date.DayOfWeek.ToString().ToUpper()}";
            var dayOfWeekFont = new Font(fontSamim, 12, Font.BOLD, new BaseColor(77, 73, 65));
            ColumnText.ShowTextAligned(writer.DirectContent,
                                        Element.ALIGN_LEFT, // text align
                                        new Phrase(dayOfWeekContent, dayOfWeekFont), // content
                                        document.PageSize.Width - 469, // x
                                        779, // y
                                        0,// rotation
                                         PdfWriter.RUN_DIRECTION_LTR, // direction
                                        0); // arabic options

            // MONTH and YEAR
            var monthYearContent = $"{date.ToString("MMM")} {date.Year}";
            var monthYearFont = new Font(fontSamim, 16, Font.BOLD, new BaseColor(77, 73, 65));
            ColumnText.ShowTextAligned(writer.DirectContent,
                                        Element.ALIGN_LEFT, // text align
                                        new Phrase(monthYearContent, monthYearFont), // content
                                        document.PageSize.Width - 469, // x
                                        763, // y
                                        0,// rotation
                                         PdfWriter.RUN_DIRECTION_LTR, // direction
                                        0); // arabic options

            // Shamsi Date
            var shamsiContent = date.ToPersianDateTextify();
            var shamsiFont = new Font(fontShabnam, 10, Font.BOLD, new BaseColor(77, 73, 65));
            ColumnText.ShowTextAligned(writer.DirectContent,
                                        Element.ALIGN_LEFT, // text align
                                        new Phrase(shamsiContent, shamsiFont), // content
                                        document.PageSize.Width - 469, // x
                                        750, // y
                                        0,// rotation
                                         PdfWriter.RUN_DIRECTION_RTL, // direction
                                        0); // arabic options
        }

        //Close the PDF
        document.Close();
        writer.Close();
        Console.WriteLine("PDF created successfully!");
    }
}