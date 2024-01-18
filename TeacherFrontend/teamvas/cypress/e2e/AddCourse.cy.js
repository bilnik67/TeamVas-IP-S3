import { getCourses } from "../interceptions";

describe('Adding a course', () => {
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
  
  it('adds a course', () => {
    cy.visit('http://localhost:8081/')

    cy.wait(500);

    cy.get(':nth-child(2) > .nav-button').click();
    cy.wait(1500);
    cy.get('.add-course-btn').should('be.visible');
    cy.get('.add-course-btn').click();
    cy.get('#courseName').clear('N');
    cy.get('#courseName').type('New Course Test');
    cy.get('#courseDescription').click();
    cy.get('#courseDescription').type('New Course Test Description');
    cy.wait(500);
    cy.intercept(
      "POST",
      "https://localhost:7232/Courses",
      {
        statusCode: 200,
      }
    ).as("addCourses");

    const newCourseData = { id:1, name: 'New Course Test', description: 'New Course Test Description' };
    cy.readFile("cypress/fixtures/courses.json").then((existingData) => {
      if (Array.isArray(existingData)) {
        existingData[0] = { ...existingData[0],};
        existingData.push(newCourseData);
    
        cy.writeFile("cypress/fixtures/courses.json", existingData).then(() => {

          getCourses();
          cy.get('.submit-button').click();
    
        });
      }
    });
  })
  afterEach(() => {
    cy.writeFile("cypress/fixtures/courses.json", originalCoursesData);
  });
})