export function getCourses() {
    cy.intercept(
        "GET", 
        "http://localhost:3000/Courses", 
        {
            statusCode: 200,
            fixture: "courses.json", 
        }
    ).as("getCourses");   
    cy.intercept('GET', '/favicon.ico', (req) => {
        req.reply({
            statusCode: 200,
            body: '',
            headers: {
                'content-type': 'image/x-icon'
            }
        });
    }).as("favicon");
}