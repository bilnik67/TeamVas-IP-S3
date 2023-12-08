using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.BLogic;
using TeamVas.DAL;
using TeamVas.API;
using TeamVas.DAL.Repositories;
using TeamVas.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace TeamVas.IntegrationTests.Courses
{
    [TestClass]
    public class CourseRepositoryTests
    {
        private EducationalContext _context;
        private CourseRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {   
            var options = new DbContextOptionsBuilder<EducationalContext>()
                .UseInMemoryDatabase(databaseName: "EducationalDatabase") 
                .Options;

            _context = new EducationalContext(options);
            _repository = new CourseRepository(_context);
        }

        [TestMethod]
        public void GetAllCourses_ShouldReturnCourses_WhenCoursesExist()
        {
            // Arrange
            _context.Course.Add(new Course(1,"Course 1","Description for Course 1"));
            _context.SaveChanges();

            // Act
            var courses = _repository.GetAllCourses();

            // Assert
            Assert.IsTrue(courses.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAllCourses_ShouldThrowException_WhenNoCoursesExist()
        {
            // Act
            var courses = _repository.GetAllCourses();
        }

        [TestMethod]
        public void GetCourseById_ShouldReturnCourse_WhenCourseExists()
        {
            // Arrange
            var testCourse = new Course(2,"Course 2","Description for Course 2");
            _context.Course.Add(testCourse);
            _context.SaveChanges();

            // Act
            var course = _repository.GetCourseById(testCourse.Id);

            // Assert
            Assert.IsNotNull(course);
            Assert.AreEqual(testCourse.Id, course.Id);
            Assert.AreEqual(testCourse.Name, course.Name);
            Assert.AreEqual(testCourse.Description, course.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetCourseById_ShouldThrowException_WhenCourseDoesNotExist()
        {
            // Act
            var course = _repository.GetCourseById(-1);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void AddCourse_ShouldAddCourse()
        {
            // Arrange
            var course = new Course( 3,"Course 3", "Description for Course 3");

            // Act
            _repository.AddCourse(course);

            // Assert
            var addedCourse = _context.Course.FirstOrDefault(c => c.Id == course.Id);
            Assert.IsNotNull(addedCourse);
            Assert.AreEqual(course.Name, addedCourse.Name);
            Assert.AreEqual(course.Description, addedCourse.Description);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
