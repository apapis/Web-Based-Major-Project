import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../Redux/configureStore";
import { setProductsList } from "../../Redux/productsListSlice";
import agent from "../../api/agent";
import {
  Line,
  Wrapper,
  Heading,
  SectionHeading,
  SaveBtn,
  Form,
  InputForm,
} from "./MealManager.style";
import { useForm, SubmitHandler, FieldValues } from "react-hook-form";
import { useLocation, useNavigate } from "react-router-dom";
import { useProductSelection, useCostManagement } from "./MealManager.hooks";
import ProductSelector from "./ProductSelector/ProductSelector";
import CostManager from "./CostManager/CostManager";
import MealSummary from "./MealSummary/MealSummary";
import ExistingImages from "./ExistingImages/ExistingImages";
import NewImages from "./NewImages/NewImages";
import MealImageInput from "../FormInput/MealImageInput";

interface MealManagerProps {
  edit?: boolean;
}

export interface MealFormValues extends FieldValues {
  name: string;
  numberOfPeople: number;
  description: string;
  price: string; // Changed to string to ensure proper parsing
  products: { productId: number; quantity: number }[];
  costs: { name: string; value: number }[];
  existingImageUrls: string;
}

export default function MealManager({ edit }: MealManagerProps) {
  const {
    register,
    handleSubmit,
    reset,
    control,
    watch,
    setValue,
    formState: { errors },
  } = useForm<MealFormValues>({
    defaultValues: {
      costs: [{ name: "", value: 0 }],
    },
  });

  const [formData, setFormData] = useState(new FormData());
  const [selectedImages, setSelectedImages] = useState<File[]>([]);
  const dispatch = useAppDispatch();
  const location = useLocation();
  const mealId = new URLSearchParams(location.search).get("id_meal");
  const {
    selectedProducts,
    setSelectedProducts,
    handleProductSelect,
    handleProductQuantityChange,
    resetSelectedProducts,
  } = useProductSelection({});

  const { setCostsFromMeal } = useCostManagement(
    [],
    setValue,
    control,
    register
  );

  useEffect(() => {
    agent.Products.list().then((products) =>
      dispatch(setProductsList(products))
    );

    if (edit && mealId) {
      agent.Meals.details(parseInt(mealId)).then((meal) => {
        setValue("name", meal.name);
        setValue("numberOfPeople", meal.numberOfPeople);
        setValue("description", meal.description);
        setValue("price", meal.pricing.price.toFixed(2)); // Ensure price is string with two decimal places

        const initialSelectedProducts = {};
        meal.ingredients.products.forEach((mealProduct) => {
          initialSelectedProducts[mealProduct.productId] = mealProduct.quantity;
        });
        setSelectedProducts(initialSelectedProducts);

        const mealCosts = Array.isArray(meal.pricing.costs)
          ? meal.pricing.costs
          : [];
        setCostsFromMeal(mealCosts);

        const newFormData = new FormData();
        meal.imageUrls.forEach((imageUrl) => {
          newFormData.append("existingImages", imageUrl);
        });
        setValue("existingImageUrls", JSON.stringify(meal.imageUrls));
        setFormData(newFormData);
      });
    }
  }, [dispatch, edit, mealId, setValue, setSelectedProducts]);

  const onSubmit: SubmitHandler<MealFormValues> = async (data) => {
    const productsData = Object.entries(selectedProducts).map(
      ([productId, quantity]) => ({
        productId: parseInt(productId),
        quantity,
      })
    );

    const parsedPrice = parseFloat(data.price); // Parse price as float

    if (isNaN(parsedPrice)) {
      console.error("Invalid price value");
      return;
    }

    const updatedData = {
      ...data,
      numberOfPeople: data.numberOfPeople.toString(),
      products: JSON.stringify(productsData),
      description: data.description ? data.description.toString() : "",
      costs: JSON.stringify(data.costs),
      existingImageUrls: data.existingImageUrls,
      price: parsedPrice, // Ensure price is sent as a number
    };

    const updatedFormData = new FormData();
    Object.entries(updatedData).forEach(([key, value]) => {
      updatedFormData.append(key, value?.toString() || "");
    });

    selectedImages.forEach((file) => {
      updatedFormData.append("images", file);
    });

    try {
      if (edit && mealId) {
        await agent.Meals.edit(parseInt(mealId), updatedFormData);
        console.log("Meal updated successfully");
      } else {
        await agent.Meals.add(updatedFormData);
        console.log("Meal created successfully");
      }
      reset();
      resetSelectedProducts();
      setFormData(new FormData());
      navigate("/admin/meals/mealsList");
    } catch (error) {
      console.error("Error creating/updating meal:", error);
    }
  };

  const handleFileChange = (files: FileList | null) => {
    if (files) {
      const filesArray = Array.from(files);
      setSelectedImages(filesArray);
      filesArray.forEach((file) => {
        formData.append("images", file);
      });
      setFormData(formData);
    }
  };

  const handleRemoveImage = (index: number) => {
    const updatedImages = selectedImages.filter((_, i) => i !== index);
    setSelectedImages(updatedImages);
    const updatedFormData = new FormData();
    updatedImages.forEach((file) => {
      updatedFormData.append("images", file);
    });
    setFormData(updatedFormData);
  };

  const products = useAppSelector((state) => state.productList.products);
  const navigate = useNavigate();
  const costsWatch = watch("costs", [{ name: "", value: 0 }]);

  return (
    <div>
      <Wrapper>
        <Form onSubmit={handleSubmit(onSubmit)}>
          <Heading>{edit ? "Edit Meal" : "Create Meal"}</Heading>
          <SectionHeading>Meal information</SectionHeading>
          <Line />
          <InputForm
            label="Name"
            register={register}
            name="name"
            errors={errors}
            validation={{ required: "Name is required" }}
          />

          <InputForm
            label="For how many people"
            register={register}
            type="number"
            name="numberOfPeople"
            errors={errors}
            validation={{
              required: "For how many people is required",
              min: {
                value: 1,
                message: "For how many people must be greater than 0",
              },
            }}
          />

          <InputForm
            label="Price"
            register={register}
            type="number"
            name="price"
            errors={errors}
            step="0.01"
            validation={{
              required: "Price is required",
              min: {
                value: 0,
                message: "Price must be greater than or equal to 0",
              },
              validate: (value) => {
                const price = parseFloat(value);
                return !isNaN(price) || "Price must be a valid number";
              },
            }}
          />

          <InputForm
            label="Description"
            register={register}
            type="textarea"
            name="description"
            errors={errors}
            validation={{ required: "Description is required" }}
            placeholder="Enter description"
          />

          <SectionHeading>Products</SectionHeading>
          <Line />
          <ProductSelector
            products={products}
            selectedProducts={selectedProducts}
            handleProductSelect={handleProductSelect}
            handleProductQuantityChange={handleProductQuantityChange}
          />
          <input
            type="hidden"
            {...register("products")}
            value={JSON.stringify(Object.entries(selectedProducts))}
          />

          <div>
            <SectionHeading>Costs</SectionHeading>
            <Line />
            <CostManager
              control={control}
              errors={errors}
              register={register}
            />
          </div>

          {edit && (
            <>
              <SectionHeading>Existing Images</SectionHeading>
              <Line />
              <ExistingImages
                imageUrls={JSON.parse(watch("existingImageUrls") || "[]")}
                onRemoveImage={(imageUrl) => {
                  const existingImageUrls = JSON.parse(
                    watch("existingImageUrls") || "[]"
                  );
                  const updatedImageUrls = existingImageUrls.filter(
                    (url) => url !== imageUrl
                  );
                  setValue(
                    "existingImageUrls",
                    JSON.stringify(updatedImageUrls)
                  );
                }}
              />

              <input
                type="hidden"
                {...register("existingImageUrls")}
                defaultValue={JSON.stringify(watch("existingImageUrls") || [])}
              />
            </>
          )}

          <SectionHeading>Images</SectionHeading>
          <Line />
          <MealImageInput
            register={register}
            name="images"
            errors={errors}
            onFileChange={handleFileChange}
          />

          <NewImages
            images={selectedImages}
            onRemoveImage={handleRemoveImage}
          />

          <SaveBtn type="submit" data-testid="Submit button for meal manager">
            {edit ? "Update Meal" : "Create Meal"}
          </SaveBtn>
        </Form>
        <SaveBtn onClick={() => navigate("/admin/meals/mealsList")}>
          Go back
        </SaveBtn>
      </Wrapper>

      <MealSummary
        products={products}
        selectedProducts={selectedProducts}
        costs={costsWatch}
        numberOfPeople={watch("numberOfPeople")}
      />
    </div>
  );
}
