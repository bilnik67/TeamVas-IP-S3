export function getCourses() {
    cy.intercept(
      "GET",
      "https://localhost:7232/Courses",
      {
        statusCode: 200,
        fixture: "../fixtures/courses.json",
      }
    ).as("getCourses");
  }

  