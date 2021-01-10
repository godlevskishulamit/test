using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.IO;
using WordToPDF;
using Spire.Doc;

namespace word2PdfNet
{
    public class Program

    {



        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }

        static void Main(string[] args)
        {

            string folderFile = "C:\\data";

            ////החלפת שמות
            Word2Pdf objWorPdf = new Word2Pdf();
            string backfolder1 = @"C:\\data\\WOrdToPDF";
            string strFileName = "past2.docx";
            object FromLocation = folderFile + "\\" + strFileName;
            string FileExtension = Path.GetExtension(strFileName);
            string ChangeExtension = strFileName.Replace(FileExtension, ".pdf");
            string ToLocation = "";
            if (FileExtension == ".doc" || FileExtension == ".docx")
            {
                 ToLocation = folderFile + "\\" + ChangeExtension;
            }    
            ////קוד ראשוןני מורד מהאינטרמט לא עובד במחשב שלי 
                  objWorPdf.InputLocation = FromLocation;
            objWorPdf.OutputLocation = ToLocation;
            //  string aa= objWorPdf.Word2PdfCOnversion();
            //Console.WriteLine(aa);

            ////////////////////////////////////////
            ///spire antil 3 document
           Spire.Doc.Document document = new Spire.Doc.Document();
            document.LoadFromFile(FromLocation.ToString());

            //Convert Word to PDF  
            document.SaveToFile (ToLocation, FileFormat.PDF);
            ///תיקית הקבציפ

            ////////////////////////////////////////////////



            /////////////////////////////////////////////////////////
            ///קוד של WORD
            ///חסר לי משהו לא ברור מה

            Microsoft.Office.Interop.Word.Document wordDocument;

                Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                wordDocument = appWord.Documents.Open(FromLocation);
                wordDocument.SaveAs2(ToLocation, WdExportFormat.wdExportFormatPDF);
            appWord.Quit(false);
            /////////////////////////////////////////////////
         


            //            string printerName = "Microsoft Print to PDF";
            //           // = @"C:\data\past2.pdf";
            //            string flagPrintFileToPrinter = string.Format("\"{0}\"", printerName);
            //            /// const string flagNoSplashScreen = "/s";
            //            ///const string flagOpenMinimized = "/h";


            //            Process.
            //                Start(
            //                new ProcessStartInfo(fileName)
            //                { Verb = "PrintTo",
            //                    UseShellExecute = true,
            //                    WindowStyle = ProcessWindowStyle.Hidden,
            //                    CreateNoWindow = true,
            //                    Arguments = "\"" + printerName + "\"" 
            //                });

            //            ///string argss = string.Format("{0} {1} {2}", flagNoSplashScreen, flagOpenMinimized, flagPrintFileToPrinter);
            //            ///string arguments = String.Format(@"-t ""{0}"" ""{1}""", printFileName, printerName);

            ////            Process p = new Process();
            ////            p.StartInfo = new ProcessStartInfo()
            ////            {
            ////                CreateNoWindow = true,
            ////                Verb = "PrintTo",
            ////                FileName = 
            ////                //\"{1}\""
            ////                Arguments = flagPrintFileToPrinter,fileName,
            ////WindowStyle = ProcessWindowStyle.Hidden 

            //////optional, if you can't hide the adobe window properly with CreateNoWindow
            ////            };
            ////            p.Start();            //See if any

            ////     //       if (PrinterSettings.InstalledPrinters.Count <= 0)
            ////     //       {
            ////     //           //  MessageBox.Show("Printer not found!");
            ////     //           return;
            ////     //       }

            ////     //       //Get all available printers and add them to the combo box  
            ////     //       foreach (String printer in PrinterSettings.InstalledPrinters)
            ////     //                       // printersList.Itmes.Add(printer.ToString());

            ////     //           Console.WriteLine(printer.ToString());
            ////     //           // printersList.Itmes.Add(printer.ToString());

            ////     //      // string printerName = "Microsoft Print to PDF";

            ////     //      PrintDocument pd = new PrintDocument();

            ////     //       //Set PrinterName as the selected printer in the printers list  
            ////     //      // pd.PrinterSetting.PrinterName = printerName;


            ////     ////   pd.p

            ////     //       //Add PrintPage event handler  
            ////     //     //  pd.PrintPage + = new PrintPageEventHandler(pd_PrintPage);

            //     //       //Print the document  
            //     //       pd.Print();

        }

    }
}
//}



