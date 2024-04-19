using DNTPersianUtils.Core;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

// Start date: January 1st, 2024
DateTime startDate = new DateTime(2024, 1, 1);

// End date: December 31st, 2024
//DateTime endDate = new DateTime(2024, 12, 31);
DateTime endDate = new DateTime(2024, 1, 2);

// Set the desired page size
PageSize pageSize = PageSize.A4; // You can use other standard sizes or define custom sizes

// Create a new PDF document
PdfDocument pdfDoc = new PdfDocument(new PdfWriter("365Days.pdf"));

// Create a document
Document document = new Document(pdfDoc, pageSize);

// get bg image
Image backgroundImage = new Image(ImageDataFactory.Create("template_yellow.png"));

var pageNumber = 0;
// Iterate through each day from start date to end date
for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
{
    // Create a new page
    var outputPage = pdfDoc.AddNewPage();
    pageNumber++;

    backgroundImage.SetFixedPosition(outputPage.GetPageSize().GetLeft(), outputPage.GetPageSize().GetBottom());
    backgroundImage.ScaleToFit(outputPage.GetPageSize().GetWidth(), outputPage.GetPageSize().GetHeight());
    document.Add(backgroundImage);

    // Specify the path to the font file that supports Persian characters
    PdfFont fontSamim = PdfFontFactory.CreateFont("samim.ttf", PdfEncodings.IDENTITY_H);
    PdfFont fontSamimFD = PdfFontFactory.CreateFont("Samim-FD.ttf", PdfEncodings.IDENTITY_H);
    PdfFont fontSamimFDWol = PdfFontFactory.CreateFont("Samim-FD-WOL.ttf", PdfEncodings.IDENTITY_H);
    //document.SetFont(fontSamim);

    // set the paragraph
    //var paragraph = new Paragraph($"Date: {date:dddd, MMMM d, yyyy}");
    //var paragraph = new Paragraph($"{date.Day}");

    // Day Of Month
    var paragraphDay = new Paragraph($"{date.ToString("dd")}");
    paragraphDay.SetFont(fontSamim).SetFontSize(45); // Set the font size to 12 points
    paragraphDay.SetBold();
    paragraphDay.SetCharacterSpacing(-1);
    paragraphDay.SetFontColor(new DeviceRgb(34, 34, 34));
    document.ShowTextAligned(paragraphDay,
        106, 810,
        pageNumber,
        TextAlignment.RIGHT,
        VerticalAlignment.TOP,
        0);

    // DAY OF WEEK
    var paragraphDayOfWeek = new Paragraph($"{date.DayOfWeek.ToString().ToUpper()}");
    paragraphDayOfWeek.SetFont(fontSamim).SetFontSize(12); // Set the font size to 12 points
    paragraphDayOfWeek.SetBold();
    paragraphDayOfWeek.SetCharacterSpacing(0);
    paragraphDayOfWeek.SetFontColor(new DeviceRgb(34, 34, 34));
    document.ShowTextAligned(paragraphDayOfWeek,
        120, 794,
        pageNumber,
        TextAlignment.LEFT,
        VerticalAlignment.TOP,
        0);

    // MONTH
    var paragraphMonth = new Paragraph($"{date.ToString("MMM")} {date.Year}");
    paragraphMonth.SetFont(fontSamim).SetFontSize(16); // Set the font size to 12 points
    paragraphMonth.SetBold();
    paragraphMonth.SetCharacterSpacing(-1);
    paragraphMonth.SetFontColor(new DeviceRgb(34, 34, 34));
    document.ShowTextAligned(paragraphMonth,
        120, 783,
        pageNumber,
        TextAlignment.LEFT,
        VerticalAlignment.TOP,
        0);

    // PERSIAN
    string hassan = date.ToPersianDateTextify();

    var paragraphPersian = new Paragraph(hassan);
    paragraphPersian.SetFont(fontSamim).SetFontSize(10); // Set the font size to 12 points
    paragraphPersian.SetBold();
    paragraphPersian.SetCharacterSpacing(0);
    paragraphPersian.SetFontColor(new DeviceRgb(34, 34, 34));
    paragraphPersian.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);
    paragraphPersian.SetTextAlignment(TextAlignment.RIGHT);
    document.Add(paragraphPersian);

    //Table table = new Table(1);
    //table.SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);
    //table.AddCell(paragraphPersian);
    //document.Add(table);

    //document.ShowTextAligned(paragraphPersian,
    //    120, 762,
    //    pageNumber,
    //    TextAlignment.RIGHT,
    //    VerticalAlignment.TOP,
    //    0);

    // Add a new page if it's not the last day of the year
    if (date != endDate)
        document.Add(new AreaBreak());
}

// Close the document
document.Close();

Console.WriteLine("PDF created successfully!");