using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
namespace ST10222789_Part1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Collection to store claims submitted by lecturers
        public ObservableCollection<ClaimInfo> Claims { get; set; }

        private string UploadedDocumentPath = string.Empty; // Store the uploaded document path

        public MainWindow()
        {
            InitializeComponent();
            Claims = new ObservableCollection<ClaimInfo>();
            listBox.ItemsSource = Claims;
        }

        // Button click event for submitting a claim
        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {

            string claimDescription = ClaimDescription.Text;
            string lecturerName = LecturerName.Text;
            double claimAmount;

            // Validate claim amount
            if (!double.TryParse(ClaimAmount.Text, out claimAmount))
            {
                MessageBox.Show("Please enter a valid claim amount.");
                return;
            }

            // Validate file attachment
            if (!string.IsNullOrEmpty(UploadedDocumentPath) && !IsValidDocument(UploadedDocumentPath))
            {
                MessageBox.Show("Please upload a valid document (e.g., PDF, DOCX).");
                return;
            }

            // Create a new claim and add it to the collection
            Claims.Add(new ClaimInfo
            {

                Description = claimDescription,
                LecturerName = lecturerName,
                Amount = claimAmount,
                SubmissionDate = DateTime.Now,
                Status = "Submitted",  // Initial status
                SupportingDocument = UploadedDocumentPath  // File name or path
            });

            // Clear input fields

            ClaimDescription.Clear();
            ClaimAmount.Clear();
            LecturerName.Clear();
            UploadedDocumentPath = string.Empty; // Clear the uploaded document path
        }

        // Approve claim
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem is ClaimInfo selectedClaim)
            {
                selectedClaim.Status = "Approved";
                MessageBox.Show("Claim approved.");
                listBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a claim to approve.");
            }
        }

        // Reject claim
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem is ClaimInfo selectedClaim)
            {
                selectedClaim.Status = "Rejected";
                MessageBox.Show("Claim rejected.");
                listBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a claim to reject.");
            }
        }


        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Word documents (*.docx)|*.docx";

            if (openFileDialog.ShowDialog() == true)
            {
                UploadedDocumentPath = openFileDialog.FileName;
                MessageBox.Show($"Document '{UploadedDocumentPath}' uploaded successfully.");
            }
        }


        private bool IsValidDocument(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            return extension == ".pdf" || extension == ".docx";
        }


        public class ClaimInfo
        {
            public string LecturerName { get; set; }
            public string Description { get; set; }
            public double Amount { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string Status { get; set; }
            public string SupportingDocument { get; set; }

            public override string ToString()
            {
                return $"{LecturerName} - {Description} - {Amount:C} - Status: {Status} - Submitted: {SubmissionDate:d}";
            }