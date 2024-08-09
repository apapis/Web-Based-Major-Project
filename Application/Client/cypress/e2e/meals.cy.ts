describe("All operation with products", () => {
  it("Go to product list", () => {
    cy.visit("http://localhost:5174/admin");

    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");
    cy.contains("Meals List").click();
    cy.contains("Meals list page").should("be.visible");
  });

  it("Add new meal", () => {
    cy.visit("http://localhost:5174/admin");

    //Login and go to meal list page
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");
    cy.contains("Meals List").click();
    cy.contains("Meals list page").should("be.visible");
    cy.get('[data-testid="Add new meal"]').click();

    // Validation of form fields
    cy.get('[data-testid="Submit button for meal manager"]').click();

    // Check for validation error messages
    cy.contains("Name is required").should("be.visible");
    cy.contains("For how many people is required").should("be.visible");
    cy.contains("Description is required").should("be.visible");

    // Filling form fields with invalid values
    cy.get('input[name="name"]').type("Test Meal Cypress");
    cy.get('input[name="numberOfPeople"]').type("0");
    cy.get('textarea[name="description"]').type("Test Description");

    cy.get('button[type="submit"]').click();

    // Check if error message appears for field "numberOfPeople"
    cy.contains("For how many people must be greater than 0").should(
      "be.visible"
    );

    // Fill the "numberOfPeople" field with the correct value
    cy.get('input[name="numberOfPeople"]').clear().type("2");

    // Adding a product to a meal
    cy.get('[data-testid="product-select Beef"]').first().click();
    cy.get('[data-testid="product-select Beef add value"]').type("100");

    // Adding a cost to the meal
    cy.get('[data-testid="cost-add-new"]').click();
    cy.get('input[name="costs.0.name"]').first().type("Cost 1");
    cy.get('input[name="costs.0.value"]').type("10");
    // Przesłanie formularza
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the new meal is on the list
    cy.contains("Test Meal Cypress").should("be.visible");
  });

  it("Edit meal", () => {
    cy.visit("http://localhost:5174/admin");

    // Login and go to meal list page
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();

    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");

    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");

    cy.contains("Meals List").click();
    cy.contains("Meals list page").should("be.visible");

    // Click the edit button for "Test Meal Cypress"
    cy.get('[data-testid="Edit meal Test Meal Cypress"]').first().click();

    // Check if we are on the meal edit page
    cy.contains("Edit Meal").should("be.visible");

    // Update form fields
    cy.get('input[name="name"]').clear().type("Updated Test Meal Cypress");
    cy.get('input[name="numberOfPeople"]').clear().type("4");
    cy.get('textarea[name="description"]')
      .clear()
      .type("Updated Test Description");

    // Update product quantity
    cy.get('[data-testid="product-select Beef add value"]').clear().type("200");

    // Add a new cost
    cy.get('[data-testid="cost-add-new"]').click();
    cy.get('input[name="costs.1.name"]').type("Cost 2");
    cy.get('input[name="costs.1.value"]').type("20");

    // Remove the first cost
    cy.get('[data-testid="cost-remove"]').first().click();

    // Submit the form
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the updated meal is in the list
    cy.contains("Updated Test Meal Cypress").should("be.visible");
  });

  it("Add 3 more meals for testing Allergies", () => {
    cy.visit("http://localhost:5174/admin");

    //Login and go to meal list page
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");
    cy.contains("Meals List").click();
    cy.contains("Meals list page").should("be.visible");

    // Add first meal with nut Allergies
    cy.get('[data-testid="Add new meal"]').click();

    // Validation of form fields
    cy.get('[data-testid="Submit button for meal manager"]').click();

    cy.get('input[name="name"]').type("Test Meal Cypress - Allergies(Nuts)");
    cy.get('input[name="numberOfPeople"]').type("2");
    cy.get('textarea[name="description"]').type("Test Description");

    // Adding a product to a meal
    cy.get('[data-testid="product-select Beef"]').first().click();
    cy.get('[data-testid="product-select Beef add value"]').type("100");
    cy.get('[data-testid="product-select Mushrooms and nuts"]').first().click();
    cy.get('[data-testid="product-select Mushrooms and nuts add value"]').type(
      "50"
    );

    // Adding a cost to the meal
    cy.get('[data-testid="cost-add-new"]').click();
    cy.get('input[name="costs.0.name"]').first().type("Cost 1");
    cy.get('input[name="costs.0.value"]').type("10");
    // Submit the form
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the new meal is on the list
    cy.contains("Test Meal Cypress - Allergies(Nuts)").should("be.visible");
    cy.get('[data-testid="Allergies Test Meal Cypress - Allergies(Nuts)"]')
      .should("be.visible")
      .and("contain.text", "nuts");

    //Add second meal with dairy allergy
    cy.get('[data-testid="Add new meal"]').click();

    cy.get('input[name="name"]').type("Test Meal Cypress - Allergies(Dairy)");
    cy.get('input[name="numberOfPeople"]').type("4");
    cy.get('textarea[name="description"]').type("Test Description");

    // Adding a product to a meal
    cy.get('[data-testid="product-select Beef"]').first().click();
    cy.get('[data-testid="product-select Beef add value"]').type("50");
    cy.get('[data-testid="product-select Cheddar Cheese"]').first().click();
    cy.get('[data-testid="product-select Cheddar Cheese add value"]').type(
      "70"
    );

    // Adding a cost to the meal
    cy.get('[data-testid="cost-add-new"]').click();
    cy.get('input[name="costs.0.name"]').first().type("Cost 1");
    cy.get('input[name="costs.0.value"]').type("10");
    // Submit the form
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the new meal is on the list
    cy.contains("Test Meal Cypress").should("be.visible");
    cy.get('[data-testid="Allergies Test Meal Cypress - Allergies(Dairy)"]')
      .should("be.visible")
      .and("contain.text", "dairy");

    //Add third meal with glutne allergy
    cy.get('[data-testid="Add new meal"]').click();

    cy.get('input[name="name"]').type("Test Meal Cypress - Allergies(Gluten)");
    cy.get('input[name="numberOfPeople"]').type("4");
    cy.get('textarea[name="description"]').type("Test Description");

    // Adding a product to a meal
    cy.get('[data-testid="product-select Beef"]').first().click();
    cy.get('[data-testid="product-select Beef add value"]').type("90");
    cy.get('[data-testid="product-select Pasta"]').first().click();
    cy.get('[data-testid="product-select Pasta add value"]').type("100");

    // Adding a cost to the meal
    cy.get('[data-testid="cost-add-new"]').click();
    cy.get('input[name="costs.0.name"]').first().type("Cost 1");
    cy.get('input[name="costs.0.value"]').type("30");
    // Przesłanie formularza
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the new meal is on the list
    cy.contains("Test Meal Cypress - Allergies(Gluten)").should("be.visible");
    cy.get('[data-testid="Allergies Test Meal Cypress - Allergies(Gluten)"]')
      .should("be.visible")
      .and("contain.text", "gluten");
  });

  it("Check if allergies are updated correctly", () => {
    cy.visit("http://localhost:5174/admin");

    //Login and go to meal list page
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");
    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");
    cy.contains("Meals List").click();
    cy.contains("Meals list page").should("be.visible");

    cy.get('[data-testid="Edit meal Test Meal Cypress - Allergies(Gluten)"]')
      .first()
      .click();

    // Check if we are on the meal edit page
    cy.contains("Edit Meal").should("be.visible");

    // Update form fields
    cy.get('input[name="name"]')
      .clear()
      .type("Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)");
    cy.get('input[name="numberOfPeople"]').clear().type("4");

    // Adding a product to a meal
    cy.get('[data-testid="product-select Beef add value"]').type("20");
    cy.get('[data-testid="product-select Pasta"]').first().click();
    cy.get('[data-testid="product-select Cheddar Cheese"]').first().click();
    cy.get('[data-testid="product-select Cheddar Cheese add value"]').type(
      "20"
    );
    cy.get('[data-testid="product-select Mushrooms and nuts"]').first().click();
    cy.get('[data-testid="product-select Mushrooms and nuts add value"]').type(
      "20"
    );

    // Submit the form
    cy.get('button[type="submit"]').click();

    // Check if you have been redirected to the food list page
    cy.url().should("eq", "http://localhost:5174/admin/meals/mealsList");

    // Check if the new meal is on the list
    cy.contains(
      "Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"
    ).should("be.visible");
    cy.get(
      '[data-testid="Allergies Updated Test Meal Cypress - Allergies(Gluten) to (Nuts & Diary)"]'
    )
      .should("be.visible")
      .and("contain.text", "nuts, dairy");
  });
});
