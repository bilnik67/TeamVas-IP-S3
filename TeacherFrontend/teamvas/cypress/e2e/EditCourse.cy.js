import { getCourses } from "../interceptions";

describe('Editing a course', () => {
  let courseId = -1;
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
  
  it('edits a course', () => {
    cy.visit('http://localhost:8081/')
    cy.wait(500);

    cy.get(':nth-child(2) > .nav-button').click();
    cy.wait(1500);

    cy.readFile("cypress/fixtures/courses.json").then((courses) => {
      if (courses.length > 0) {
        const firstCourse = courses[0]; 
        courseId = firstCourse.id;

        cy.intercept(
          "GET",
          `https://localhost:7232/Courses/${courseId}`,
          {
            statusCode: 200,
            body: firstCourse, 
          }
        ).as("editCourses");
      }
    });

    cy.get('.edit-course-link').click();
    cy.wait(500);
    cy.get('#courseName').clear('Course about Animal');
    cy.get('#courseName').type('Course about software');
    cy.get('#courseDescription').click();
    cy.get('#courseDescription').type('Stackoverflow');
    cy.wait(500);
    cy.intercept(
      "PUT",
      `https://localhost:7232/Courses/0`,
      {
        statusCode: 200,
      }
    ).as("editCourse");
    cy.readFile("cypress/fixtures/courses.json").then((courses) => {
      if (courses.length > 0) {
        courses[0].name = 'Course about software'; 
        courses[0].description = 'Stackoverflow'; 
      }

      cy.writeFile("cypress/fixtures/courses.json", courses);
    });
    getCourses();
    cy.get('.submit-button').click();
  })
  afterEach(() => {
    cy.writeFile("cypress/fixtures/courses.json", originalCoursesData);
  });
})