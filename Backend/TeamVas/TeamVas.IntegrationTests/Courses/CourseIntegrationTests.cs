//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TeamVas.BLogic;
//using TeamVas.DAL;
//using TeamVas.API;
//using TeamVas.DAL.Repositories;
//using TeamVas.DAL.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;
//using Exceptions.Courses;

//namespace TeamVas.IntegrationTests.Courses
//{
//    [TestClass]
//    public class CourseRepositoryTests 
//    {
//        private EducationalContext _context;
//        private CourseRepository _repository;
//        private static CustomWebApplicationFactory _factory;
//        private HttpClient _client;

//        [ClassInitialize]
//        public static void ClassInitialize(TestContext testContext)
//        {
//            _factory = new CustomWebApplicationFactory();
//        }

//        [TestInitialize]
//        public void TestInitialize()
//        {   
//            var options = new DbContextOptionsBuilder<EducationalContext>()
//                .UseInMemoryDatabase(databaseName: "EducationalDatabase") 
//                .Options;

//            _context = new EducationalContext(options);
//            _repository = new CourseRepository(_context);
//            _client = _factory.CreateClient();
//        }

        

//        [TestMethod]
//        [ExpectedException(typeof(InvalidOperationException))]
//        public void GetAllCourses_ShouldThrowException_WhenNoCoursesExist()
//        {
//            // Act
//            var courses = _repository.GetAllCourses();
//        }

//        [TestMethod]
//        public async Task GetAllCourses_ShouldReturnOk_WhenCalled()
//        {
//            // Act
//            var response = await _client.GetAsync("/courses");
//            response.EnsureSuccessStatusCode();
//            var stringResponse = await response.Content.ReadAsStringAsync();

//            // Assert
//            Assert.IsNotNull(stringResponse);
//        }

//        [TestMethod]
//        public void GetCourseById_ShouldReturnCourse_WhenCourseExists()
//        {
//            // Arrange
//            var testCourse = new Course(2,"Course 2","Description for Course 2");
//            _context.SaveChanges();

//            // Act
//            var course = _repository.GetCourseById(testCourse.Id);

//            // Assert
//            Assert.IsNotNull(course);
//            Assert.AreEqual(testCourse.Id, course.Id);
//            Assert.AreEqual(testCourse.Name, course.Name);
//            Assert.AreEqual(testCourse.Description, course.Description);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(CourseNotFoundException))]
//        public void GetCourseById_ShouldThrowException_WhenCourseDoesNotExist()
//        {
//            // Act
//            var course = _repository.GetCourseById(-1);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(CourseNotFoundException))]
//        public void UpdateCourse_ShouldThrowException_WhenCourseDoesNotExist()
//        {
//            // Act
//            var course = _repository.GetCourseById(-1);
//            _context.Course.Add(course);
//            _context.SaveChanges();

//            // Assert (should throw exception)
//            _repository.UpdateCourse(course);

//        }

//        [TestMethod]
//        public void AddCourse_ShouldAddCourse()
//        {
//            // Arrange
//            var course = new Course( 3,"Course 3", "Description for Course 3");

//            // Act
//            _repository.AddCourse(course);

//            // Assert
//            var addedCourse = _context.Course.FirstOrDefault(c => c.Id == course.Id);
//            Assert.IsNotNull(addedCourse);
//            Assert.AreEqual(course.Name, addedCourse.Name);
//            Assert.AreEqual(course.Description, addedCourse.Description);
//        }
//        [TestMethod]
//        public void UpdateCourse_ShouldUpdateCourse_WhenCourseExists()
//        {
//            // Arrange
//            var originalCourse = new Course(4,"Original Course","Original Description");
//            _context.Course.Add(originalCourse);
//            _context.SaveChanges();

//            var updatedCourse = new Course (4, "Updated Course", "Updated Description");

//            // Act
//            _repository.UpdateCourse(updatedCourse);
//            var result = _context.Course.Find(originalCourse.Id);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(updatedCourse.Name, result.Name);
//            Assert.AreEqual(updatedCourse.Description, result.Description);
//        }
//        [TestMethod]
//        public void DeleteCourse_ShouldRemoveCourse_WhenCourseExists()
//        {
//            // Arrange
//            var course = new Course(5, "Course to Delete", "Description for deletion");
//            _context.Course.Add(course);
//            _context.SaveChanges();

//            // Act
//            _repository.DeleteCourse(course.Id);
//            var deletedCourse = _context.Course.Find(course.Id);

//            // Assert
//            Assert.IsNull(deletedCourse);
//        }

//        [TestCleanup]
//        public void TestCleanup()
//        {
//            _context.Database.EnsureDeleted();
//            _context.Dispose();
//        }
//    }
//}
