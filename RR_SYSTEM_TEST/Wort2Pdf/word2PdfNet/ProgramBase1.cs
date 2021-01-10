//namespace word2PdfNet
//{
//    internal class test
//    {
//        #region old
//        /* static void Main(string[] args)
//         {


//             // generate a file name as the current date/time in unix timestamp format
//             string file = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();

//             // the directory to store the output.
//             string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

//             // initialize PrintDocument object
//             PrintDocument doc = new PrintDocument()
//             {
//                 PrinterSettings = new PrinterSettings()
//                 {
//                     // set the printer to 'Microsoft Print to PDF'
//                     PrinterName = "Microsoft Print to PDF",

//                     // tell the object this document will print to file
//                     PrintToFile = true,

//                     // set the filename to whatever you like (full path)
//                     PrintFileName = Path.Combine(directory, file + ".pdf"),
//                 }
//             };

//             doc.Print();




//             }
//        */
//        #endregion

//        static void Main(string[] args)
//        {

//            //See if any printers are installed  
//            if (PrinterSettings.InstalledPrinters.Count <= 0)
//            {
//                //  MessageBox.Show("Printer not found!");
//                return;
//            }

//            //Get all available printers and add them to the combo box  
//            foreach (String printer in PrinterSettings.InstalledPrinters)
//            {
//                Console.WriteLine(printer.ToString());
//                // printersList.Itmes.Add(printer.ToString());
//            }
//        }
//    }
//}