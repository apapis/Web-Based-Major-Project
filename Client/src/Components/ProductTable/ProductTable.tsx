import { useState } from "react";
import Modal from "../Modal/Modal";
import { Container, TableCell, Table } from "./ProductTable.style";
import AddProduct from "../AddProduct/ProductManager";
import DeletICon from "../../assets/icons/close.svg?react";
import EditIcon from "../../assets/icons/edit.svg?react";
import agent from "../../api/agent";
import { ButtonsWithSvg } from "../../assets/styles/Button.style";
import { Product } from "../../Models/product";
import { useAppDispatch, useAppSelector } from "../../Redux/configureStore";
import { deleteProduct } from "../../Redux/productsListSlice";

export default function ProductTable() {
  const [isOpen, setIsOpen] = useState(false);
  const [editProduct, setEditProduct] = useState<Product | null>(null);
  const productsList = useAppSelector((state) => state.productList);
  const dispatch = useAppDispatch();

  const handelDeletProduct = (id: number) => {
    agent.Products.delete(id)
      .then((deletProductID) => {
        dispatch(deleteProduct(deletProductID));
      })
      .catch((error) => {
        // Handle any errors, such as showing an error message to the user
        console.error("Error deleting product:", error);
      });
  };

  const handelCloseModal = () => {
    setIsOpen(false);
    setEditProduct(null);
  };

  const handelEditProduct = (product: Product) => {
    setEditProduct(product);
    setIsOpen(true);
  };
  return (
    <Container>
      <h1>Products List</h1>
      <Table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Store</th>
            <th>Price</th>
            <th>Price per Gram</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {productsList &&
          productsList.products &&
          productsList.products.length > 0 ? (
            productsList.products.map((product) => (
              <tr key={product.id}>
                <TableCell>{product.name}</TableCell>
                <TableCell>{product.store}</TableCell>
                <TableCell>{product.price}</TableCell>
                <TableCell>{product.pricePerGram}</TableCell>
                <TableCell>
                  <ButtonsWithSvg>
                    <EditIcon
                      onClick={() => {
                        handelEditProduct(product);
                      }}
                    />
                  </ButtonsWithSvg>
                  <ButtonsWithSvg>
                    <DeletICon
                      onClick={() => {
                        handelDeletProduct(product.id);
                      }}
                    />
                  </ButtonsWithSvg>
                </TableCell>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={5}>No products available</td>
            </tr>
          )}
        </tbody>
      </Table>
      <Modal open={isOpen} onClose={() => handelCloseModal()}>
        <AddProduct product={editProduct} onClose={handelCloseModal} />
      </Modal>
      <button onClick={() => setIsOpen(true)}>Add new Product</button>
    </Container>
  );
}
