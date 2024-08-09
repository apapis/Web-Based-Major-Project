import React from "react";
import { UseFormRegister, FieldErrors, RegisterOptions } from "react-hook-form";
import { MealFormValues } from "../MealManager/MealManager";
import FormInput from "./FormInput";

interface MealInputFormProps {
  label?: string;
  register: UseFormRegister<MealFormValues>;
  name: Extract<keyof MealFormValues, string>;
  type?: React.HTMLInputTypeAttribute | "textarea";
  errors: FieldErrors;
  validation?: RegisterOptions;
  className?: string;
  placeholder?: string;
  multiple?: boolean;
  accept?: string;
  step?: string;
  onFileChange?: (files: FileList | null) => void;
}

const MealInputForm: React.FC<MealInputFormProps> = ({
  label,
  register,
  name,
  type,
  errors,
  validation,
  className,
  placeholder,
  multiple,
  accept,
  step,
  onFileChange,
}) => {
  return (
    <FormInput
      step={step}
      label={label}
      register={register}
      name={name}
      type={type}
      errors={errors}
      validation={validation}
      className={className}
      placeholder={placeholder}
      multiple={multiple}
      accept={accept}
      onFileChange={onFileChange}
    />
  );
};

export default MealInputForm;
