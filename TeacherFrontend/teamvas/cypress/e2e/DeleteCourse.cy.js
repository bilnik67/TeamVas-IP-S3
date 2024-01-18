import { getCourses } from "../interceptions";


describe('Delete courses', () => {
  let originalCoursesData = [];

  before(() => {
    cy.readFile("cypress/fixtures/courses.json").then((courses) => {
      originalCoursesData = [...courses]; 
    });
  });

  beforeEach(() => {
    cy.kcLogout();
    cy.kcLogin("user");
    
    getCourses();
  });

  it('Deletes course', () => {
    cy.visit('http://localhost:8081/')

    cy.wait(500);

    cy.get(':nth-child(2) > .nav-button').click();
    cy.wait(1500);

    cy.get('.delete-course-btn').click();
    cy.intercept(
      "DELETE",
      "https://localhost:7232/Courses/0",
      {
        statusCode: 200,
      }
    ).as("addCourses");
    
    cy.writeFile('cypress/fixtures/courses.json', []).then(() => {
    });
    getCourses();
    cy.get('.swal2-confirm').click();
    cy.wait(500);
  })
  afterEach(() => {
    cy.writeFile("cypress/fixtures/courses.json", originalCoursesData);
  });

})