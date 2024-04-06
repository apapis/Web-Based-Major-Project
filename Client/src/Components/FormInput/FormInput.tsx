import React from "react";
import {
  FieldValues,
  UseFormRegister,
  Path,
  FieldErrors,
  RegisterOptions,
} from "react-hook-form";

interface FormInputProps<TFieldValues extends FieldValues> {
  label: string;
  register: UseFormRegister<TFieldValues>;
  name: Path<TFieldValues>;
  type?: React.HTMLInputTypeAttribute;
  errors: FieldErrors;
  validation?: RegisterOptions;
}

function FormInput<TFieldValues extends FieldValues>({
  label,
  register,
  name,
  type = "text",
  errors,
  validation,
}: FormInputProps<TFieldValues>) {
  return (
    <div>
      <label htmlFor={name}>{label}</label>
      <input {...register(name, { ...validation })} type={type} id={name} />
      {errors[name]?.message && (
        <p data-testid="error-message">{errors[name]?.message as string}</p>
      )}
    </div>
  );
}

export default FormInput;
