import {
  Wrapper,
  SummaryItem,
  MealSummaryContainer,
} from "./MealSummary.style";

function MealSummary({ products, selectedProducts, costs, numberOfPeople }) {
  // Calculate the total cost of all products
  const costOfAllProducts = products.reduce((total, product) => {
    const quantity = selectedProducts[product.id] || 0;
    return total + product.pricePerUnit * quantity;
  }, 0);

  // Calculate total production cost, make sure value is a number
  const costOfMakeIt = costs.reduce((total, cost) => {
    return total + parseFloat(cost.value || 0); // convert string to number if needed
  }, costOfAllProducts);

  // Obliczanie ceny za osobę
  const pricePerPerson =
    numberOfPeople > 0 ? (costOfMakeIt / numberOfPeople).toFixed(2) : 0;

  return (
    <MealSummaryContainer>
      <Wrapper>
        <SummaryItem>
          <span>Cost of all products:</span>
          <span>£{costOfAllProducts.toFixed(2)}</span>
        </SummaryItem>
        <SummaryItem>
          <span>Cost of make it:</span>
          <span>£{costOfMakeIt.toFixed(2)}</span>
        </SummaryItem>
        <SummaryItem>
          <span>Price per person:</span>
          <span>£{pricePerPerson}</span>
        </SummaryItem>
      </Wrapper>
    </MealSummaryContainer>
  );
}

export default MealSummary;
