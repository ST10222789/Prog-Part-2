using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Part_2
{
    [TestClass]
    public class UnitTest1
    {
        [TestFixture]
        public class ClaimServiceTests
        {
            private ClaimService _claimService;

            [SetUp]
            public void Setup()
            {
                _claimService = new ClaimService();
            }

            [Test]
            public void SubmitClaim_ValidInput_ReturnsTrue()
            {
                // Arrange
                string lecturerName = "John Doe";
                string description = "Travel Reimbursement";
                double amount = 150.50;
                string document = "receipt.pdf";

                // Act
                bool result = _claimService.SubmitClaim(lecturerName, description, amount, document);

                // Assert
                Assert.IsTrue(result);
                Assert.AreEqual(1, _claimService.Claims.Count);
            }

            [Test]
            public void SubmitClaim_InvalidInput_ReturnsFalse()
            {
                // Arrange
                string lecturerName = "";
                string description = "";
                double amount = -100;
                string document = "invalid.pdf";

                // Act
                bool result = _claimService.SubmitClaim(lecturerName, description, amount, document);

                // Assert
                Assert.IsFalse(result);
                Assert.AreEqual(0, _claimService.Claims.Count); // Ensure no claim was added
            }

            [Test]
            public void ApproveClaim_ValidClaim_ChangesStatusToApproved()
            {
                // Arrange
                var claim = new ClaimService.ClaimInfo
                {
                    LecturerName = "John Doe",
                    Description = "Travel Reimbursement",
                    Amount = 150.50,
                    SubmissionDate = DateTime.Now,
                    Status = "Submitted"
                };

                _claimService.Claims.Add(claim);

                // Act
                bool result = _claimService.ApproveClaim(claim);

                // Assert
                Assert.IsTrue(result);
                Assert.AreEqual("Approved", claim.Status);
            }

            [Test]
            public void ApproveClaim_AlreadyApproved_ReturnsFalse()
            {
                // Arrange
                var claim = new ClaimService.ClaimInfo
                {
                    LecturerName = "John Doe",
                    Description = "Travel Reimbursement",
                    Amount = 150.50,
                    SubmissionDate = DateTime.Now,
                    Status = "Approved"
                };

                _claimService.Claims.Add(claim);

                // Act
                bool result = _claimService.ApproveClaim(claim);

                // Assert
                Assert.IsFalse(result); // Should return false since the claim is already approved
            }

            [Test]
            public void RejectClaim_ValidClaim_ChangesStatusToRejected()
            {
                // Arrange
                var claim = new ClaimService.ClaimInfo
                {
                    LecturerName = "John Doe",
                    Description = "Travel Reimbursement",
                    Amount = 150.50,
                    SubmissionDate = DateTime.Now,
                    Status = "Submitted"
                };

                _claimService.Claims.Add(claim);

                // Act
                bool result = _claimService.RejectClaim(claim);

                // Assert
                Assert.IsTrue(result);
                Assert.AreEqual("Rejected", claim.Status);
            }

            [Test]
            public void RejectClaim_AlreadyRejected_ReturnsFalse()
            {
                // Arrange
                var claim = new ClaimService.ClaimInfo
                {
                    LecturerName = "John Doe",
                    Description = "Travel Reimbursement",
                    Amount = 150.50,
                    SubmissionDate = DateTime.Now,
                    Status = "Rejected"
                };

                _claimService.Claims.Add(claim);

                // Act
                bool result = _claimService.RejectClaim(claim);

                // Assert
                Assert.IsFalse(result); // Should return false since the claim is already rejected
            }
        }
    }

}
