import { useEffect } from "react";
import { Container, Form } from "./ProductManager.style";
import agent from "../../api/agent";
import { ProductManagerProps, Product, FormValues } from "../../Models/product";
import { useAppDispatch } from "../../Redux/configureStore";
import { addProduct, editProduct } from "../../Redux/productsListSlice";
import { useForm, SubmitHandler } from "react-hook-form";
import FormInput from "../FormInput/FormInput";

export default function ProductManager({
  product,
  onClose,
}: ProductManagerProps & { onClose: () => void }) {
  const dispatch = useAppDispatch();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<FormValues>();

  useEffect(() => {
    const initialFormState: FormValues = {
      id: product?.id ?? undefined,
      name: product?.name ?? "",
      store: product?.store ?? "",
      weight: product?.weight ?? 0,
      price: product?.price ?? 0,
    };

    reset(initialFormState);
  }, [product, reset]);

  const onSubmit: SubmitHandler<FormValues> = (data) => {
    if (product && data.id) {
      agent.Products.edit(data.id, data).then((updatedProduct) => {
        dispatch(editProduct(updatedProduct));
      });
    } else {
      agent.Products.add(data as Product).then((newProduct) => {
        dispatch(addProduct(newProduct));
      });
    }
    onClose();
  };

  return (
    <Container>
      <h1>{product ? "Edit product" : "Add new product"}</h1>
      <Form onSubmit={handleSubmit(onSubmit)}>
        <FormInput
          label="Name"
          register={register}
          name="name"
          errors={errors}
          validation={{ required: "Name is required" }}
        />

        <FormInput
          label="Store"
          register={register}
          name="store"
          errors={errors}
          validation={{ required: "Store is required" }}
        />

        <FormInput
          label="Weight"
          register={register}
          name="weight"
          type="number"
          errors={errors}
          validation={{
            required: "Weight is required",
            valueAsNumber: true,
            validate: (value) => value > 0 || "Weight must be greater than 0",
          }}
        />

        <FormInput
          label="Price"
          register={register}
          name="price"
          type="number"
          errors={errors}
          validation={{
            required: "Price is required",
            valueAsNumber: true,
            validate: (value) => value > 0 || "Price must be greater than 0",
          }}
        />

        <button type="submit">
          {product ? "Save product" : "Add product"}
        </button>
      </Form>
    </Container>
  );
}
