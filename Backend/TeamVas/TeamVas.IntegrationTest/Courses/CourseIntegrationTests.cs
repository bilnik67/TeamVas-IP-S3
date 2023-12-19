using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.IntegrationTest;
using TeamVas;
using System.Text.Json;
using TeamVas.DAL;
using Microsoft.Extensions.DependencyInjection;
using TeamVas.DAL.Entities;
using System.Net;
using TeamVas.API.DTOs;

namespace TeamVas.IntegrationTest.Courses
{
    public class CourseRepositoryTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly EducationalContext _context;

        public CourseRepositoryTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var scope = _factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<EducationalContext>();

            SeedSampleCourses();
        }

        private void SeedSampleCourses()
        {
            if (!_context.Database.EnsureDeleted())
            {

                var courses = new List<Course>
                {
                    new Course(1, "Course 1", "Desc course 1"),
                    new Course(2, "Course 2", "Desc course 2"),
                };

                _context.Courses.AddRange(courses);
                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAllCourses_ShouldReturnOk_WhenCalled()
        {
            // Act
            var response = await _client.GetAsync("/courses");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
            }
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(stringResponse);
        }
        

        [Fact]
        public async Task GetCourse_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            var invalidId = 99;

            // Act
            var response = await _client.GetAsync($"/course/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetCourse_ShouldReturnBadRequest_WhenInvalidRoute()
        {
            // Act
            var response = await _client.GetAsync("/courses/invalid");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
