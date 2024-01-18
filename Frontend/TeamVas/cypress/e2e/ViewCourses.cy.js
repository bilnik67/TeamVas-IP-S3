import { getCourses } from "../interceptions";

describe('View courses', () => {
  beforeEach(() => {
    cy.kcLogout();
    cy.kcLogin("user");
    getCourses();
  });

  it('passes', () => {
    cy.visit('http://localhost:3000/');



    /* ==== Generated with Cypress Studio ==== */
    cy.request('/Courses');
    /* ==== End Cypress Studio ==== */
  });
});