using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TeamVas.BLogic.Services;
using TeamVas.DAL.Entities;
using TeamVas.DAL.Repositories;
using System.Threading.Tasks;
using Exceptions.Assignments;
using TeamVas.BLogic.Models;

namespace teamvas.Tests.Unit_Tests
{
    [TestClass]
    public class AssignmentServiceTests
    {
        private Mock<IAssignmentRepository> _mockRepo;
        private AssignmentService _assignmentService;

        public AssignmentServiceTests()
        {
            _mockRepo = new Mock<IAssignmentRepository>();
            _assignmentService = new AssignmentService(_mockRepo.Object);
        }


        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IAssignmentRepository>();
            _mockRepo.Setup(repo => repo.GetAllAssignments())
                .Returns(new List<Assignment>
                {
                    new Assignment(1, "Assignment 1", "Description 1"),
                    new Assignment(2, "Assignment 2", "Description 2")
                }); ;

            _mockRepo.Setup(repo => repo.GetAssignmentById(It.IsAny<int>()))
                .Returns((int id) => new Assignment(id, "Assignment {id}", "Description {id}"));

            _assignmentService = new AssignmentService(_mockRepo.Object);
        }

        [TestMethod]
        public void GetAllAssignmentsAsync_ReturnsAllAssignments()
        {
            // Actt
            var assignments = _assignmentService.GetAllAssignments();


            // Assert
            Assert.AreEqual(2, assignments.Count());
            _mockRepo.Verify(repo => repo.GetAllAssignments(), Times.Once);
        }

        [TestMethod]
        public void GetAssignmentByIdAsync_ReturnsCorrectAssignment()
        {
            // Arrange
            int assignmentId = 1;

            // Act
            var assignment = _assignmentService.GetAssignmentById(assignmentId);

            // Assert
            Assert.IsNotNull(assignment);
            Assert.AreEqual(assignmentId, assignment.Id);
            _mockRepo.Verify(repo => repo.GetAssignmentById(assignmentId), Times.Once);
        }
        [TestMethod]
        public void AddAssignment_ReturnsAddedAssignment()
        {
            // Arrange
            var assignmentModel = new AssignmentModel(3, "Assignment 3", "Description 3");
            var addedAssignment = new Assignment(3, "Assignment 3", "Description 3");
            _mockRepo.Setup(repo => repo.AddAssignment(It.IsAny<Assignment>()))
                .Returns(addedAssignment);

            // Act
            var result = _assignmentService.AddAssignment(assignmentModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Id);
        }

        [TestMethod]
        public void UpdateAssignment_UpdatesExistingAssignment()
        {
            // Arrange
            var assignmentModel = new AssignmentModel(1, "Updated Assignment", "Updated Description");
            var existingAssignment = new Assignment(1, "Assignment 1", "Description 1");
            _mockRepo.Setup(repo => repo.GetAssignmentById(1))
                .Returns(existingAssignment);

            // Act
            _assignmentService.UpdateAssignment(assignmentModel);

            // Assert
            Assert.AreEqual("Updated Assignment", existingAssignment.Name);
            Assert.AreEqual("Updated Description", existingAssignment.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(AssignmentNotFoundException))]
        public void UpdateAssignment_ThrowsAssignmentNotFoundExceptionWhenAssignmentNotFound()
        {
            // Arrange
            var assignmentModel = new AssignmentModel(3, "Updated Assignment", "Updated Description");
            _mockRepo.Setup(repo => repo.GetAssignmentById(3))
                     .Returns((Assignment)null!);

            // Act and Assert
            _assignmentService.UpdateAssignment(assignmentModel);
        }

        [TestMethod]
        public void DeleteAssignment_DeletesExistingAssignment()
        {
            // Arrange
            int assignmentId = 2;
            _mockRepo.Setup(repo => repo.GetAssignmentById(assignmentId))
                .Returns(new Assignment(assignmentId, "Assignment 2", "Description 2"));

            // Act
            _assignmentService.DeleteAssignment(assignmentId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAssignment(assignmentId), Times.Once);
        }
    }
}
