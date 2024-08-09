import React from "react";
import { Box, Product, SearchBar } from "./ProductSelector.style";

function ProductSelector({
  products,
  selectedProducts,
  handleProductSelect,
  handleProductQuantityChange,
}) {
  const [searchQuery, setSearchQuery] = React.useState("");

  const filteredProducts = products.filter((product) =>
    product.name.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <div>
      <SearchBar
        type="text"
        placeholder="Search products..."
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        style={{ marginBottom: "10px" }}
      />
      <Box>
        {filteredProducts.map((product) => (
          <Product
            data-testid={`product-select ${product.name}`}
            key={product.id}
            style={{ display: "inline-block", margin: "5px" }}
            onClick={() => handleProductSelect(product)}
            isSelected={!!selectedProducts[product.id]}
          >
            <div>
              <span>{product.name}</span>
              <span>{product.store}</span>
              <span>Price per gram: {product.pricePerGram}</span>
            </div>
            {selectedProducts[product.id] !== undefined && (
              <input
                data-testid={`product-select ${product.name} add value`}
                type="number"
                value={selectedProducts[product.id] || ""}
                onChange={(e) =>
                  handleProductQuantityChange(
                    product.id,
                    e.target.value ? parseInt(e.target.value) : 0
                  )
                }
                onClick={(e) => e.stopPropagation()}
                min="1"
              />
            )}
          </Product>
        ))}
      </Box>
    </div>
  );
}

export default ProductSelector;
