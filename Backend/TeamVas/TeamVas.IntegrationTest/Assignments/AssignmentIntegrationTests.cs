using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TeamVas.DAL;
using Microsoft.Extensions.DependencyInjection;
using TeamVas.DAL.Entities;
using System.Net;
using TeamVas.API.DTOs;
using TeamVas.BLogic.Models;
using System.Net.Http.Json;

namespace TeamVas.IntegrationTest.Assignments
{
    public class AssignmentRepositoryTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly EducationalContext _context;

        public AssignmentRepositoryTests(CustomWebApplicationFactory factory)
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
        public async Task GetAllAssignments_ShouldReturnOk_WhenCalled()
        {
            // Act
            var response = await _client.GetAsync("/assignments");
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
        public async Task GetAssignment_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            var invalidId = 99;

            // Act
            var response = await _client.GetAsync($"/assignments/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAssignment_ShouldReturnBadRequest_WhenInvalidRoute()
        {
            // Act
            var response = await _client.GetAsync("/assignments/invalid");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task GetAssignment_ShouldReturnOk_WhenCalledValidId()
        {
            // Arrange
            var validId = 1;

            // Act
            var response = await _client.GetAsync($"/assignments/{validId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task AddAssignment_ShouldReturnCreated_WhenAssignmentIsValid()
        {
            // Arrange
            var newAssignment = new AssignmentDto(3, "newcAssignment", "New Assignment Description");

            // Act
            var response = await _client.PostAsJsonAsync("/assignments", newAssignment);
            var createdAssignment = await response.Content.ReadFromJsonAsync<AssignmentDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdAssignment);
            Assert.Equal("newcAssignment", createdAssignment?.Name);
        }
        [Fact]
        public async Task UpdateAssignment_ShouldReturnNoContent_WhenAssignmentExists()
        {
            // Arrange
            var existingAssignment = new Assignment(1, "Updated Assignment", "Updated Assignment Description");

            var content = new StringContent(JsonSerializer.Serialize(existingAssignment), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/assignments/{existingAssignment.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAssignment_ShouldUpdateAssignment_WhenAssignmentExists()
        {
            // Arrange
            var existingAssignmentId = 1;
            var updatedAssignment = new AssignmentDto(1, "Updated Assignment", "Updated Assignment Description");

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/assignments/{existingAssignmentId}", updatedAssignment);
            updateResponse.EnsureSuccessStatusCode();

            var retrievedAssignment = await _client.GetFromJsonAsync<AssignmentDto>($"/assignments/{existingAssignmentId}");

            // Assert
            Assert.NotNull(retrievedAssignment);
            Assert.Equal("Updated Assignment", retrievedAssignment.Name);
            Assert.Equal("Updated Assignment Description", retrievedAssignment.Description);
        }

        [Fact]
        public async Task DeleteAssignment_ShouldReturnNoContent_WhenAssignmentExists()
        {
            // Arrange
            var existingAssignmentId = 1;

            // Act
            var response = await _client.DeleteAsync($"/assignments/{existingAssignmentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        private async Task ClearDatabase()
        {
            _context.Assignments.RemoveRange(_context.Assignments);
            await _context.SaveChangesAsync();
        }

        private async Task ReseedDatabase()
        {
            var assignments = new List<Assignment>
            {
                new Assignment(1, "Assignment 1", "Desc assignment 1"),
                new Assignment(2, "Assignment 2", "Desc assignment 2"),
            };

            _context.Assignments.AddRange(assignments);
            await _context.SaveChangesAsync();
        }


    }
}
