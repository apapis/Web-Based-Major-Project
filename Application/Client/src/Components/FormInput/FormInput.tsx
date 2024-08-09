import React from "react";
import {
  FieldValues,
  UseFormRegister,
  Path,
  FieldErrors,
  RegisterOptions,
} from "react-hook-form";

interface FormInputProps<TFieldValues extends FieldValues> {
  label?: string;
  register: UseFormRegister<TFieldValues>;
  name: Path<TFieldValues>;
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

function FormInput<TFieldValues extends FieldValues>({
  label,
  register,
  name,
  type = "text",
  errors,
  validation,
  className,
  placeholder,
  multiple,
  accept,
  step,
  onFileChange,
}: FormInputProps<TFieldValues>) {
  return (
    <div className={className}>
      <label htmlFor={name}>{label}</label>
      {type === "file" ? (
        <input
          {...register(name)}
          type={type}
          id={name}
          multiple={multiple}
          accept={accept}
          onChange={(e) => {
            if (onFileChange) {
              onFileChange(e.target.files);
            }
          }}
        />
      ) : type === "textarea" ? (
        <textarea
          {...register(name, { ...validation })}
          id={name}
          placeholder={placeholder}
        />
      ) : (
        <input
          {...register(name, { ...validation })}
          type={type}
          id={name}
          placeholder={placeholder}
          step={step}
        />
      )}
      {errors[name]?.message && (
        <p data-testid="error-message">{errors[name]?.message as string}</p>
      )}
    </div>
  );
}

export default FormInput;
