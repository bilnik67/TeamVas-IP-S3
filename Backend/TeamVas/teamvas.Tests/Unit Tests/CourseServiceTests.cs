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
            _mockRepo.Setup(repo => repo.GetAllCoursesAsync())
                .ReturnsAsync(new List<Course>
                {
                    new Course { Id = 1, Name = "Course 1", Description = "Description 1" },
                    new Course { Id = 2, Name = "Course 2", Description = "Description 2" }
                });

            _mockRepo.Setup(repo => repo.GetCourseByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new Course { Id = id, Name = $"Course {id}", Description = $"Description {id}" });

            _courseService = new CourseService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllCoursesAsync_ReturnsAllCourses()
        {
            // Act
            var courses = await _courseService.GetAllCoursesAsync();
            

            // Assert
            Assert.AreEqual(2, courses.Count());
            _mockRepo.Verify(repo => repo.GetAllCoursesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseByIdAsync_ReturnsCorrectCourse()
        {
            // Arrange
            int courseId = 1;

            // Act
            var course = await _courseService.GetCourseByIdAsync(courseId);

            // Assert
            Assert.IsNotNull(course);
            Assert.AreEqual(courseId, course.Id);
            _mockRepo.Verify(repo => repo.GetCourseByIdAsync(courseId), Times.Once);
        }
    }
}
