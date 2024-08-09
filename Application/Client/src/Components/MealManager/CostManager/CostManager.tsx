// CostManager.tsx
import { useFieldArray } from "react-hook-form";
import { Box, Button, Cost, InputForm } from "./CostManager.style";
import DeletICon from "../../../assets/icons/close.svg?react";
import PlusICon from "../../../assets/icons/plus.svg?react";

function CostManager({ control, errors, register }) {
  const { fields, append, remove } = useFieldArray({
    control,
    name: "costs",
  });

  return (
    <Box>
      {fields.map((field, index) => (
        <Cost key={field.id}>
          <InputForm
            data-testid="cost-select-name"
            label="Cost Name"
            register={register}
            name={`costs.${index}.name`}
            errors={errors}
            validation={{ required: "Cost name is required" }}
          />
          <InputForm
            data-testid={`cost-add-value`}
            label="Cost Value"
            register={register}
            type="number"
            name={`costs.${index}.value`}
            errors={errors}
            validation={{ required: "Cost value is required" }}
            step="0.01"
          />
          <button
            data-testid="cost-remove"
            type="button"
            onClick={() => remove(index)}
          >
            <DeletICon />
          </button>
        </Cost>
      ))}
      <Button
        type="button"
        onClick={() => append({ name: "", value: 0 })}
        data-testid={`cost-add-new`}
      >
        <PlusICon />
      </Button>
    </Box>
  );
}

export default CostManager;
