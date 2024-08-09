import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import agent from "../../api/agent";
import { InputForm } from "../LoginForm/LoginForm.style";
import {
  ErrorMessage,
  StyledForm,
  SubmitButton,
  SuccessMessage,
} from "./RegisterForm.style";

interface RegisterFormValues {
  email: string;
  password: string;
  confirmPassword: string;
}

const RegisterForm: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
  } = useForm<RegisterFormValues>();
  const navigate = useNavigate();
  const [registerError, setRegisterError] = useState("");
  const [registerSuccess, setRegisterSuccess] = useState(false);

  const onSubmit = async (data: RegisterFormValues) => {
    if (data.password !== data.confirmPassword) {
      setError("confirmPassword", {
        type: "manual",
        message: "Passwords do not match",
      });
      return;
    }

    try {
      await agent.auth.register(
        data.email,
        data.password,
        data.confirmPassword
      );
      setRegisterSuccess(true);
      setTimeout(() => navigate("/admin"), 5000);
    } catch (error) {
      console.error("Registration error:", error);
      setRegisterError(
        "Registration failed. Please check your inputs and try again."
      );
      setRegisterSuccess(false);
    }
  };

  return (
    <>
      <h1>Add new user</h1>
      <StyledForm onSubmit={handleSubmit(onSubmit)}>
        {registerError && <ErrorMessage>{registerError}</ErrorMessage>}
        {registerSuccess && (
          <SuccessMessage>
            Registration successful. Redirecting to login...
          </SuccessMessage>
        )}
        <InputForm
          label="Email"
          name="email"
          type="email"
          // @ts-ignore
          register={register}
          errors={errors}
          validation={{ required: "Email is required" }}
        />
        <InputForm
          label="Password"
          name="password"
          type="password"
          // @ts-ignore
          register={register}
          errors={errors}
          validation={{ required: "Password is required" }}
        />
        <InputForm
          label="Confirm Password"
          name="confirmPassword"
          type="password"
          // @ts-ignore
          register={register}
          errors={errors}
          validation={{ required: "Confirm password is required" }}
        />
        <SubmitButton type="submit">Register</SubmitButton>
      </StyledForm>
    </>
  );
};

export default RegisterForm;
