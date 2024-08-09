import { useState } from "react";
import { useFieldArray } from "react-hook-form";
// @ts-ignore
export function useProductSelection(initialProducts) {
  const [selectedProducts, setSelectedProducts] = useState(initialProducts);
  // @ts-ignore
  const handleProductSelect = (product) => {
    // @ts-ignore
    setSelectedProducts((prev) => {
      if (prev[product.id]) {
        const newProducts = { ...prev };
        delete newProducts[product.id];
        return newProducts;
      } else {
        return { ...prev, [product.id]: 1 };
      }
    });
  };
  // @ts-ignore
  const handleProductQuantityChange = (productId, quantity) => {
    // @ts-ignore
    setSelectedProducts((prev) => ({ ...prev, [productId]: quantity }));
  };

  const resetSelectedProducts = () => {
    setSelectedProducts({});
  };

  return {
    selectedProducts,
    setSelectedProducts,
    handleProductSelect,
    handleProductQuantityChange,
    resetSelectedProducts,
  };
}
// @ts-ignore
export function useCostManagement(
  /* @ts-ignore */
  initialCosts = [],
  setValue,
  control,
  register
) {
  const { fields, append, update, remove } = useFieldArray({
    control,
    name: "costs",
  });

  const addCost = () => {
    append({ name: "", value: 0 });
  };

  // @ts-ignore
  const updateCost = (index, updatedCost) => {
    update(index, updatedCost);
  };

  // @ts-ignore
  const removeCost = (index) => {
    remove(index);
  };

  // @ts-ignore
  const setCostsFromMeal = (mealCosts) => {
    if (Array.isArray(mealCosts)) {
      const costsWithId = mealCosts.map((cost) => ({
        ...cost,
        id: Math.random(),
      }));
      // @ts-ignore
      setValue("costs", costsWithId);
    } else {
      setValue("costs", []);
    }
  };

  return {
    fields,
    addCost,
    updateCost,
    removeCost,
    setCostsFromMeal,
    register,
  };
}
