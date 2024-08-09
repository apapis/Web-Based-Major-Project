describe("All operation with products", () => {
  it("Go to product list", () => {
    cy.visit("http://localhost:5174/admin");

    // Enter your login details
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");

    // Click the login button
    cy.get('button[type="submit"]').click();

    // Check if you were redirected to the page after logging in
    cy.url().should("eq", "http://localhost:5174/admin");

    // Check if the expected element is present on the page after login
    cy.contains("Admin Panel").should("be.visible");

    // Click the "Meals" button
    cy.get('[data-testid="Meals panel"]').click();

    // Check if you have been redirected to the Meals page
    cy.url().should("eq", "http://localhost:5174/admin/meals");

    cy.contains("Product List").click();

    // Check if the expected item is present on the Meals page
    cy.contains("Products List").should("be.visible");
  });

  it("Check adding and editing product", () => {
    cy.visit("http://localhost:5174/admin");

    // Enter your login details
    cy.get('input[type="email"]').type("user@example.com");
    cy.get('input[type="password"]').type("Password123!");
    cy.get('button[type="submit"]').click();

    // Check if you were redirected to the page after logging in
    cy.url().should("eq", "http://localhost:5174/admin");
    cy.contains("Admin Panel").should("be.visible");

    cy.get('[data-testid="Meals panel"]').click();
    cy.url().should("eq", "http://localhost:5174/admin/meals");
    cy.contains("Product List").click();

    // Check if the expected item is present on the Meals page
    cy.contains("Products List").should("be.visible");
    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");

    // Click the "Add product" button without filling out the form fields
    cy.contains("Add product").click();
    // Check for validation error messages
    cy.contains("Name is required").should("be.visible");
    cy.contains("Store is required").should("be.visible");
    cy.contains("Weight must be greater than 0").should("be.visible");
    cy.contains("Price must be greater than 0").should("be.visible");

    // Fill the form fields with invalid values
    cy.get('input[name="name"]').type("Test Product cypress");
    cy.get('input[name="store"]').type("Test Store");
    cy.get('input[name="weight"]').type("0");
    cy.get('input[name="price"]').type("-10");

    // Click the "Add product" button
    cy.contains("Add product").click();

    // Check for validation error messages for the values
    cy.contains("Weight must be greater than 0").should("be.visible");
    cy.contains("Price must be greater than 0").should("be.visible");

    cy.get('input[name="weight"]').type("100");
    cy.get('input[name="price"]').type("100");
    cy.contains("Add product").click();

    cy.contains("Test Product cypress").should("be.visible");

    // Click the edit icon for "Test Product cypress"
    cy.get('[data-testid="Edit Test Product cypress"]').click();
    cy.get('input[name="name"]').clear().type("Updated Test Product cypress");
    cy.contains("Save product").click();
    cy.contains("Updated Test Product cypress").should("be.visible");
    cy.get('[data-testid="Delet Updated Test Product cypress"]').click();
    cy.contains("Updated Test Product cypress").should("not.exist");

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Mushrooms and nuts");
    cy.get('input[name="store"]').type("Tesco");
    cy.get('input[name="weight"]').type("500");
    cy.get('input[name="price"]').type("7");
    cy.get('input[name="nuts"]').check();
    cy.contains("Add product").click();

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Cheddar Cheese");
    cy.get('input[name="store"]').type("Lidl");
    cy.get('input[name="weight"]').type("400");
    cy.get('input[name="price"]').type("12");
    cy.get('input[name="dairy"]').check();
    cy.contains("Add product").click();

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Pasta");
    cy.get('input[name="store"]').type("Lidl");
    cy.get('input[name="weight"]').type("1000");
    cy.get('input[name="price"]').type("5");
    cy.get('input[name="gluten"]').check();
    cy.contains("Add product").click();

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Bag Salad");
    cy.get('input[name="store"]').type("Tesco");
    cy.get('input[name="weight"]').type("90");
    cy.get('input[name="price"]').type("2");
    cy.contains("Add product").click();

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Beef");
    cy.get('input[name="store"]').type("Tesco");
    cy.get('input[name="weight"]').type("500");
    cy.get('input[name="price"]').type("25");
    cy.contains("Add product").click();

    cy.contains("Add new Product").click();
    cy.contains("Add new product").should("be.visible");
    cy.get('input[name="name"]').type("Potatoes");
    cy.get('input[name="store"]').type("Tesco");
    cy.get('input[name="weight"]').type("1000");
    cy.get('input[name="price"]').type("2");
    cy.contains("Add product").click();
  });
});
