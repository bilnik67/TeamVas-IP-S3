using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TeamVas.BLogic.Services;
using TeamVas.DAL.Entities;
using TeamVas.DAL.Repositories;
using System.Threading.Tasks;

namespace teamvas.Tests.Unit_Tests
{
    [TestClass]
    public class CourseServiceTests
    {
        private Mock<ICourseRepository> _mockRepo;
        private CourseService _courseService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _mockRepo.Setup(repo => repo.GetAllCourses())
                .Returns(new List<Course>
                {
                    new Course(1, "Course 1", "Description 1"),
                    new Course(2, "Course 2", "Description 2")
                });;

            _mockRepo.Setup(repo => repo.GetCourseById(It.IsAny<int>()))
                .Returns((int id) => new Course(id, "Course {id}", "Description {id}"));

            _courseService = new CourseService(_mockRepo.Object);
        }

        [TestMethod]
        public void GetAllCoursesAsync_ReturnsAllCourses()
        {
            // Actt
            var courses =  _courseService.GetAllCourses();


            // Assert
            Assert.AreEqual(2, courses.Count());
            _mockRepo.Verify(repo => repo.GetAllCourses(), Times.Once);
        }

        [TestMethod]
        public void GetCourseByIdAsync_ReturnsCorrectCourse()
        {
            // Arrange
            int courseId = 1;

            // Act
            var course =  _courseService.GetCourseById(courseId);

            // Assert
            Assert.IsNotNull(course);
            Assert.AreEqual(courseId, course.Id);
            _mockRepo.Verify(repo => repo.GetCourseById(courseId), Times.Once);
        }
    }
}
