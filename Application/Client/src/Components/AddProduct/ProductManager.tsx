import { useEffect, useState } from "react";
import {
  Container,
  Form,
  FormInputProduct,
  SubmitButton,
} from "./ProductManager.style";
import agent from "../../api/agent";
import {
  ProductManagerProps,
  Product,
  FormValues,
  ProductData,
} from "../../Models/product";
import { useAppDispatch } from "../../Redux/configureStore";
import { addProduct, editProduct } from "../../Redux/productsListSlice";
import { useForm, SubmitHandler } from "react-hook-form";
import { Allergen } from "../../Models/allergen";
import AllergenSection from "../AllergenSection/AllergenSection";

export default function ProductManager({
  product,
  onClose,
}: ProductManagerProps & { onClose: () => void }) {
  const dispatch = useAppDispatch();
  const [allergens, setAllergens] = useState<Allergen[]>([]);
  const [selectedAllergens, setSelectedAllergens] = useState<Allergen[]>([]);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<FormValues>();

  useEffect(() => {
    agent.Allergens.list().then((data) => setAllergens(data));
  }, []);

  useEffect(() => {
    const initialFormState: FormValues = {
      id: product?.id ?? undefined,
      name: product?.name ?? "",
      store: product?.store ?? "",
      quantity: product?.quantity ?? 0, // Zmieniona nazwa
      price: product?.price ?? 0,
      unit: product?.unit ?? "", // Nowe pole
    };
    reset(initialFormState);

    if (product?.allergens) {
      setSelectedAllergens(product.allergens);
    } else {
      setSelectedAllergens([]);
    }
  }, [product, reset]);

  const onSubmit: SubmitHandler<FormValues> = (data) => {
    let pricePerUnit = data.price / data.quantity; // Zmieniona nazwa
    pricePerUnit =
      pricePerUnit > 0.02 ? parseFloat(pricePerUnit.toFixed(2)) : 0.02;

    const productData: ProductData = {
      ...data,
      pricePerUnit, // Zmieniona nazwa
      allergenIds: selectedAllergens.map((allergen) => allergen.id),
    };

    if (product && productData.id) {
      agent.Products.edit(productData.id, productData).then(
        (updatedProduct) => {
          const updatedProductWithAllergens: Product = {
            ...updatedProduct,
            allergens: selectedAllergens,
          };
          dispatch(editProduct(updatedProductWithAllergens));
        }
      );
    } else {
      agent.Products.add(productData).then((newProduct) => {
        const newProductWithAllergens: Product = {
          ...newProduct,
          allergens: selectedAllergens,
        };
        dispatch(addProduct(newProductWithAllergens));
      });
    }
    onClose();
  };

  const handleAllergenChange = (allergen: Allergen) => {
    const allergenIndex = selectedAllergens.findIndex(
      (a) => a.id === allergen.id
    );
    if (allergenIndex === -1) {
      setSelectedAllergens([...selectedAllergens, allergen]);
    } else {
      setSelectedAllergens(
        selectedAllergens.filter((a) => a.id !== allergen.id)
      );
    }
  };

  const handleAddAllergen = async (allergenName: string) => {
    try {
      const newAllergen: Allergen = { name: allergenName };
      const addedAllergen = await agent.Allergens.add(newAllergen);
      setAllergens([...allergens, addedAllergen]);
    } catch (error) {
      console.error("Failed to add allergen:", error);
    }
  };

  return (
    <Container>
      <h1>{product ? "Edit product" : "Add new product"}</h1>
      <Form onSubmit={handleSubmit(onSubmit)}>
        <FormInputProduct
          label="Name"
          register={register}
          name="name"
          errors={errors}
          validation={{ required: "Name is required" }}
        />
        <FormInputProduct
          label="Store"
          register={register}
          name="store"
          errors={errors}
          validation={{ required: "Store is required" }}
        />
        <FormInputProduct
          label="Quantity"
          register={register}
          name="quantity" // Zmieniona nazwa
          type="number"
          step="0.01"
          errors={errors}
          validation={{
            required: "Quantity is required",
            valueAsNumber: true,
            validate: (value) => value > 0 || "Quantity must be greater than 0",
          }}
        />
        <FormInputProduct
          label="Price(£)"
          register={register}
          name="price"
          type="number"
          step="0.01"
          errors={errors}
          validation={{
            required: "Price is required",
            valueAsNumber: true,
            validate: (value) => value > 0 || "Price must be greater than 0",
          }}
        />
        <FormInputProduct
          label="Unit"
          register={register}
          name="unit" // Nowe pole
          errors={errors}
          validation={{ required: "Unit is required" }}
        />
        <AllergenSection
          allergens={allergens}
          selectedAllergens={selectedAllergens}
          handleAllergenChange={handleAllergenChange}
          onAddAllergen={handleAddAllergen}
        />
        <SubmitButton type="submit">
          {product ? "Save product" : "Add product"}
        </SubmitButton>
      </Form>
    </Container>
  );
}
