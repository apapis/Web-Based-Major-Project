import React from "react";
import { UseFormRegister, FieldErrors } from "react-hook-form";
import { MealFormValues } from "../MealManager/MealManager";
import {
  Wrapper,
  HiddenInput,
  Label,
  ErrorMessage,
} from "./MealImageInput.style";

interface MealImageInputProps {
  register: UseFormRegister<MealFormValues>;
  name: "images";
  errors: FieldErrors;
  onFileChange: (files: FileList | null) => void;
}

const MealImageInput: React.FC<MealImageInputProps> = ({
  register,
  name,
  errors,
  onFileChange,
}) => {
  return (
    <Wrapper>
      <Label htmlFor={name}>
        Choose Images
        <HiddenInput
          type="file"
          id={name}
          multiple
          accept="image/*"
          {...register(name)}
          onChange={(e) => onFileChange(e.target.files)}
        />
      </Label>
      {errors[name]?.message && (
        <ErrorMessage data-testid="error-message">
          {errors[name]?.message as string}
        </ErrorMessage>
      )}
    </Wrapper>
  );
};

export default MealImageInput;
