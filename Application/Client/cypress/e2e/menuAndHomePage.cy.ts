describe("Tests for menu editor and home page", () => {
  const today = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ][new Date().getDay()];

  it("Go to the home page without logging in", () => {
    cy.visit("http://localhost:5174/");

    // Check if the page says "Today's Menu" or "Sorry we are closed today"
    cy.contains(/Today's Menu|Sorry we are closed today/).should("exist");
  });
  it("Go to the menu editor page after login in", () => {
    cy.visit("http://localhost:5174/admin");

    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Menu panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/menu");
  });

  it("Add a meal to today's menu and check if it is available on the home page and if they display Allergies.", () => {
    cy.visit("http://localhost:5174/admin");

    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Menu panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/menu");
    cy.get(`[data-testid="Add meal for ${today}"]`).click();
    cy.get('[data-testid="Add meal Test Meal Cypress - Allergies(Dairy)"]')
      .first()
      .click();
    cy.get('[data-testid="Add meal Test Meal Cypress - Allergies(Nuts)"]')
      .first()
      .click();
    cy.get(
      '[data-testid="Add meal Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"]'
    )
      .first()
      .click();
    cy.get('[data-testid="Save meal to menu"]').click();
    cy.visit("http://localhost:5174/");
    cy.contains(
      "Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"
    ).should("be.visible");
    cy.get(
      '[data-testid="Menu item Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"]'
    )
      .should("be.visible")
      .and("contain.text", "nuts, dairy");
  });
  it("Check if deleting meals from the menu is working and updating the home page.", () => {
    cy.visit("http://localhost:5174/admin");

    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Menu panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/menu");
    cy.get(
      `[data-testid="Remove menu meal Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"]`
    ).click();
    cy.visit("http://localhost:5174/");
    cy.get(
      '[data-testid="Menu item Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"]'
    ).should("not.exist");
  });

  it("Check if changing is restaurant open change something on home page.", () => {
    cy.visit("http://localhost:5174/admin");

    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Menu panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/menu");
    cy.get(`[data-testid="Menu for ${today}"]`) // Find the OneMenu item for a specific day of the week
      .find('input[type="checkbox"]') // Find the checkbox inside OneMenu
      .click({ force: true });
    cy.visit("http://localhost:5174/");

    // Check if the page says "Today's Menu" or "Sorry we are closed today"
    cy.contains(
      "Sorry, we are closed today. Please come back another day."
    ).should("exist");
  });
});
