using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace bookmarkCreator4jPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;
        public MainWindow()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Title = "Select File to open",
                Filter = "Text documents(.txt) | *.txt|All Files|*.*",
            };
            saveFileDialog = new SaveFileDialog
            {
                OverwritePrompt = true,
                Title = "Save bookmark file",
                DefaultExt = ".txt",
                Filter = "Text documents(.txt) | *.txt",
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int st;
            try
            {
                st = int.Parse(textbox1.Text.Split(Environment.NewLine).FirstOrDefault());
            }
            catch (FormatException)
            {

                st = 0;
            }

            string txt = textbox1.Text;
            Regex rx = new Regex(@"(.*?)[\W]+(\d+)",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //  Regex rx = new Regex(@"(.*?)[\W]+(\d+)(?=\n|$)",
            //RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(txt);
            String finalBookmarkText = "";
            try
            {
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    finalBookmarkText += groups[1].Value + "\\" + (int.Parse(groups[2].Value) + st) + "\n";
                    Trace.WriteLine(
                                      groups[1].Value + "\\" +
                                      groups[2].Value);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("An unhandled exception just occurred: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                //MessageBox.Show("An error occured,\n" +
                //    "Make sure you enter the text in correct format");
            }
            textbox1.Text = finalBookmarkText;
            MessageBox.Show(finalBookmarkText);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                textbox1.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            saveFileDialog.ShowDialog();
        }
    }
}
