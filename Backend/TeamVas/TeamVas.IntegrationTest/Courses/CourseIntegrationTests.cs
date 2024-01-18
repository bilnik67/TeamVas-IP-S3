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
using TeamVas.BLogic.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using Azure;
using Newtonsoft.Json;

namespace TeamVas.IntegrationTest.Courses
{
    public class CourseRepositoryTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
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

        }

        public async Task InitializeAsync()
        {
            await ClearDatabase();
            await ReseedDatabase();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
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
            var courses = JsonConvert.DeserializeObject<List<Course>>(stringResponse);

            // Assert
            Assert.NotNull(stringResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); 

            Assert.NotNull(courses); 
            Assert.NotEmpty(courses);
        }
        

        [Fact]
        public async Task GetCourse_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            var invalidId = 99;

            // Act
            var response = await _client.GetAsync($"/courses/{invalidId}");

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
        [Fact]
        public async Task GetCourse_ShouldReturnOk_WhenCalledValidId()
        {
            // Arrange
            var validId = 1;

            // Act
            var response = await _client.GetAsync($"/courses/{validId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task AddCourse_ShouldReturnCreated_WhenCourseIsValid()
        {
            // Arrange
            var newCourse = new CourseDto(3, "newcCourse", "New Course Description");

            // Act
            var response = await _client.PostAsJsonAsync("/courses", newCourse);
            var createdCourse = await response.Content.ReadFromJsonAsync<CourseDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdCourse);
            Assert.Equal("newcCourse", createdCourse?.Name);
        }
        [Fact]
        public async Task UpdateCourse_ShouldReturnNoContent_WhenCourseExists()
        {
            // Arrange
            var existingCourse = new Course(1, "Updated Course", "Updated Course Description");

            // Use Newtonsoft.Json.JsonConvert for serialization
            var content = new StringContent(JsonConvert.SerializeObject(existingCourse), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/courses/{existingCourse.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateCourse_ShouldUpdateCourse_WhenCourseExists()
        {
            // Arrange
            var existingCourseId = 1;
            var updatedCourse = new CourseDto(1, "Updated Course", "Updated Course Description");

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/courses/{existingCourseId}", updatedCourse);
            updateResponse.EnsureSuccessStatusCode();

            var retrievedCourse = await _client.GetFromJsonAsync<CourseDto>($"/courses/{existingCourseId}");

            // Assert
            Assert.NotNull(retrievedCourse);
            Assert.Equal("Updated Course", retrievedCourse.Name);
            Assert.Equal("Updated Course Description", retrievedCourse.Description);
        }

        [Fact]
        public async Task DeleteCourse_ShouldReturnNoContent_WhenCourseExists()
        {
            // Arrange
            var existingCourseId = 1; 

            // Act
            var response = await _client.DeleteAsync($"/courses/{existingCourseId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        private async Task ClearDatabase()
        {
            _context.Courses.RemoveRange(_context.Courses);
            await _context.SaveChangesAsync();
        } 

        private async Task ReseedDatabase()
        {
            var courses = new List<Course>
            {
                new Course(1, "Course 1", "Desc course 1"),
                new Course(2, "Course 2", "Desc course 2"),
            };

            _context.Courses.AddRange(courses);
            await _context.SaveChangesAsync();
        }

    }
    
}
