describe("Try to access the admin panel without logging in", () => {
  const links = [
    "/admin",
    "/admin/menu",
    "/admin/shoppingList",
    "/admin/meals",
    "/admin/meals/productsList",
    "/admin/meals/mealsList",
    "/admin/meals/mealsList/meal",
  ];

  links.forEach((link) => {
    it(`displays login form when visiting ${link}`, () => {
      cy.visit(`http://localhost:5174${link}`);
      // Check if the login form is visible
      cy.get("form").should("be.visible");
      // Check if the form fields are available
      cy.get('input[type="email"]').should("be.visible");
      cy.get('input[type="password"]').should("be.visible");
      cy.get('button[type="submit"]').should("be.visible");
    });
  });

  it("Go to the home page without logging in", () => {
    cy.visit("http://localhost:5174/");

    // Check if the page says "Today's Menu" or "Sorry we are closed today"
    cy.contains(/Today's Menu|Sorry we are closed today/).should("exist");
  });
});
