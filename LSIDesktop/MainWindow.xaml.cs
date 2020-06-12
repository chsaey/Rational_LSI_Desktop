using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;


namespace LSIDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
   
    
    public partial class MainWindow : Window
    {
      

     
        public MainWindow()
        {      
            
            InitializeComponent();
        }

        IncidentJSON JsonLsi = new IncidentJSON();
        Microsoft.Win32.OpenFileDialog LoadDlg = new Microsoft.Win32.OpenFileDialog();
        Microsoft.Win32.SaveFileDialog SaveDlg = new Microsoft.Win32.SaveFileDialog();
      


        private void Save(object sender, RoutedEventArgs e)
        {            
            SaveDlg.FileName = LSI.Text; // Default file name
            SaveDlg.DefaultExt = ".json"; // Default file extension
            SaveDlg.Filter = "Json documents (.json)|*.json"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = SaveDlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
               
                JsonLsi.LSINumber = LSI.Text;
                JsonLsi.Cloud = Cloud.Text;
                JsonLsi.Region = Region.Text;
                JsonLsi.Services = Service.Text;
                JsonLsi.Notes = Notes.Text;
                JsonLsi.CurrentTitle = CurrentTitle.Text;
                JsonLsi.Current = CurrentText.Text;
                JsonLsi.IridiasTitle = IridiasTitle.Text;
                JsonLsi.Iridias = IridiasText.Text;
                JsonLsi.DRSTitle = DRSTitle.Text;
                JsonLsi.DRS = DRSText.Text;
                JsonLsi.SubList = SubList.Text;


                using (StreamWriter file = File.CreateText(SaveDlg.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, JsonLsi);
                }
            }          

        }
        private void Load(object sender, RoutedEventArgs e)
        {

            LoadDlg.FileName = ""; // Default file name
            LoadDlg.DefaultExt = ".json"; // Default file extension
            LoadDlg.Filter = "Json documents (.json)|*.json"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = LoadDlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                using (StreamReader file = File.OpenText(LoadDlg.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    JsonLsi = (IncidentJSON)serializer.Deserialize(file, typeof(IncidentJSON));
                    LSI.Text = JsonLsi.LSINumber;
                    Cloud.Text = JsonLsi.Cloud;
                    Region.Text = JsonLsi.Region;
                    Service.Text = JsonLsi.Services;
                    Notes.Text = JsonLsi.Notes;
                    CurrentTitle.Text = JsonLsi.CurrentTitle;
                    CurrentText.Text = JsonLsi.Current;
                    IridiasText.Text = JsonLsi.Iridias;
                    IridiasTitle.Text = JsonLsi.IridiasTitle;                  
                    DRSTitle.Text = JsonLsi.DRSTitle;
                    DRSText.Text = JsonLsi.DRS;
                    SubList.Text = JsonLsi.SubList;

                    if(JsonLsi.Severity == 1) { OneRadio.IsChecked =  true; }
                    else { TwoRadio.IsChecked = true; }
                    if (JsonLsi.IncidentType == "Availability") { AvailabilityRadio.IsChecked = true; }
                    else { ManagementRadio.IsChecked = true; }
                }
            }

        }
        private void One(object sender, RoutedEventArgs e)
        {
            JsonLsi.Severity = 1;
           
        }
        private void Two(object sender, RoutedEventArgs e)
        {
            JsonLsi.Severity = 2;           

        }
        private void Management(object sender, RoutedEventArgs e)
        {
            JsonLsi.IncidentType = "Management";

        }
        private void Availability(object sender, RoutedEventArgs e)
        {
            JsonLsi.IncidentType = "Availability";

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
