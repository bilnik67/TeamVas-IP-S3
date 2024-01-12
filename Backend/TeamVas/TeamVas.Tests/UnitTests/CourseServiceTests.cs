using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TeamVas.BLogic.Services;
using TeamVas.DAL.Entities;
using TeamVas.DAL.Repositories;
using System.Threading.Tasks;
using Exceptions.Courses;
using TeamVas.BLogic.Models;

namespace teamvas.Tests.Unit_Tests
{
    [TestClass]
    public class CourseServiceTests
    {
        private Mock<ICourseRepository> _mockRepo;
        private CourseService _courseService;

        public CourseServiceTests()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _courseService = new CourseService(_mockRepo.Object);
        }
        

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
        [TestMethod]
        public void AddCourse_ReturnsAddedCourse()
        {
            // Arrange
            var courseModel = new CourseModel(3, "Course 3", "Description 3");
            var addedCourse = new Course(3, "Course 3", "Description 3");
            _mockRepo.Setup(repo => repo.AddCourse(It.IsAny<Course>()))
                .Returns(addedCourse);

            // Act
            var result = _courseService.AddCourse(courseModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Id);
        }

        [TestMethod]
        public void UpdateCourse_UpdatesExistingCourse()
        {
            // Arrange
            var courseModel = new CourseModel(1, "Updated Course", "Updated Description");
            var existingCourse = new Course(1, "Course 1", "Description 1");
            _mockRepo.Setup(repo => repo.GetCourseById(1))
                .Returns(existingCourse);

            // Act
            _courseService.UpdateCourse(courseModel);

            // Assert
            Assert.AreEqual("Updated Course", existingCourse.Name);
            Assert.AreEqual("Updated Description", existingCourse.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public void UpdateCourse_ThrowsCourseNotFoundExceptionWhenCourseNotFound()
        {
            // Arrange
            var courseModel = new CourseModel(3, "Updated Course", "Updated Description");
            _mockRepo.Setup(repo => repo.GetCourseById(3))
                     .Returns((Course)null!);

            // Act and Assert
            _courseService.UpdateCourse(courseModel);
        }

        [TestMethod]
        public void DeleteCourse_DeletesExistingCourse()
        {
            // Arrange
            int courseId = 2;
            _mockRepo.Setup(repo => repo.GetCourseById(courseId))
                .Returns(new Course(courseId, "Course 2", "Description 2"));

            // Act
            _courseService.DeleteCourse(courseId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteCourse(courseId), Times.Once);
        }
    }
}
