using System;
using System.Collections.ObjectModel;

namespace ST10222789_Part1
{
    public class ClaimService
    {
        // Collection to store claims submitted by lecturers
        public ObservableCollection<ClaimInfo> Claims { get; private set; }

        public ClaimService()
        {
            Claims = new ObservableCollection<ClaimInfo>();
        }

        // Method to submit a claim
        public bool SubmitClaim(string lecturerName, string claimDescription, double claimAmount, string supportingDocument)
        {
            if (string.IsNullOrWhiteSpace(lecturerName) || string.IsNullOrWhiteSpace(claimDescription) || claimAmount <= 0)
            {
                return false; // Invalid claim submission
            }

            Claims.Add(new ClaimInfo
            {
                LecturerName = lecturerName,
                Description = claimDescription,
                Amount = claimAmount,
                SubmissionDate = DateTime.Now,
                Status = "Submitted",  // Initial status
                SupportingDocument = supportingDocument
            });

            return true;
        }

        // Method to approve a claim
        public bool ApproveClaim(ClaimInfo claim)
        {
            if (claim == null || claim.Status == "Approved")
            {
                return false; // Claim is null or already approved
            }

            claim.Status = "Approved";
            return true;
        }

        // Method to reject a claim
        public bool RejectClaim(ClaimInfo claim)
        {
            if (claim == null || claim.Status == "Rejected")
            {
                return false; // Claim is null or already rejected
            }

            claim.Status = "Rejected";
            return true;
        }

        // Class to represent a claim
        public class ClaimInfo
        {
            public string LecturerName { get; set; }
            public string Description { get; set; }
            public double Amount { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string Status { get; set; } // Claim status (Submitted, Approved, Rejected)
            public string SupportingDocument { get; set; }

            public override string ToString()
            {
                return $"{LecturerName} - {Description} - {Amount:C} - Status: {Status} - Submitted: {SubmissionDate:d}";
            }
        }
    }
}
