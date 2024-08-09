import { useEffect } from "react";
import agent from "../../../api/agent";
import ProductTable from "../../../Components/ProductTable/ProductTable";
import { useAppDispatch } from "../../../Redux/configureStore";
import { setProductsList } from "../../../Redux/productsListSlice";
export default function ProductPage() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    agent.Products.list().then((products) => {
      dispatch(setProductsList(products));
      console.log(products);
    });
  }, [dispatch]);

  return (
    <>
      <ProductTable />
    </>
  );
}
