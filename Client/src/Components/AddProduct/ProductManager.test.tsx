// ProductManager.test.tsx
import { describe, it, expect } from "vitest";
import { render, fireEvent, screen } from "@testing-library/react";
import { Provider } from "react-redux";
import { store } from "../../Redux/configureStore";
import ProductManager from "./ProductManager";

describe("ProductManager Component", () => {
  it("renders correctly", () => {
    render(
      <Provider store={store}>
        <ProductManager onClose={() => {}} product={null} />
      </Provider>
    );
  });
  it("displays error messages when trying to submit with empty fields", async () => {
    render(
      <Provider store={store}>
        <ProductManager onClose={() => {}} product={null} />
      </Provider>
    );

    // Symulacja kliknięcia bez wypełniania formularza
    fireEvent.click(screen.getByText("Add product"));

    // Sprawdzenie, czy komunikaty o błędzie są wyświetlane dla wszystkich pól
    expect(await screen.findByText("Name is required")).toBeDefined();
    expect(await screen.findByText("Store is required")).toBeDefined();
    expect(
      await screen.findByText("Weight must be greater than 0")
    ).toBeDefined();
    expect(
      await screen.findByText("Price must be greater than 0")
    ).toBeDefined();
  });

  it("displays only one error message for the name when all other fields are filled", async () => {
    render(
      <Provider store={store}>
        <ProductManager onClose={() => {}} product={null} />
      </Provider>
    );

    // Wypełniamy wszystkie pola oprócz "name"
    fireEvent.change(screen.getByLabelText("Store"), {
      target: { value: "Test Store" },
    });
    fireEvent.change(screen.getByLabelText("Weight"), {
      target: { value: "10" },
    });
    fireEvent.change(screen.getByLabelText("Price"), {
      target: { value: "20" },
    });

    // Symulacja kliknięcia przycisku "Add product"
    fireEvent.click(screen.getByText("Add product"));

    // Pobieramy wszystkie komunikaty o błędach i sprawdzamy, czy jest ich dokładnie jeden

    const errorMessages = await screen.findAllByTestId("error-message");
    expect(errorMessages).toHaveLength(1);
    expect(errorMessages[0].textContent).toBe("Name is required");
  });
});
