import { defineConfig } from "cypress";

export default defineConfig({
  component: {
    devServer: {
      framework: "react",
      bundler: "vite",
    },
  },
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
    specPattern: [
      "cypress/e2e/authorization.cy.ts",
      "cypress/e2e/product.cy.ts",
      "cypress/e2e/meals.cy.ts",
      "cypress/e2e/menuAndHomePage.cy.ts",
    ],
    reporter: "cypress-mochawesome-reporter",
    reporterOptions: {
      reportDir: "cypress/reports",
      overwrite: false,
      html: true,
      json: false,
    },
  },
});
