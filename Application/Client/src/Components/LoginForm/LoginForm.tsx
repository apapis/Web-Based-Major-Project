import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import agent from "../../api/agent";
import { setCredentials } from "../../Redux/AuthenticationSlice";
import { useDispatch } from "react-redux";
import {
  StyledForm,
  ErrorMessage,
  InputForm,
  SubmitButton,
} from "./LoginForm.style";

interface LoginFormValues {
  email: string;
  password: string;
}

const LoginForm: React.FC = () => {
  const {
    // @ts-ignore
    register,
    handleSubmit,
    // @ts-ignore
    formState: { errors },
    setError,
  } = useForm<LoginFormValues>();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [loginError, setLoginError] = useState("");

  const onSubmit = async (data: LoginFormValues) => {
    try {
      const response = await agent.auth.login(data.email, data.password);
      agent.setJwt(response.token);
      dispatch(setCredentials({ token: response.token }));
      navigate("/admin");
    } catch (error) {
      console.error("Login error:", error);
      setLoginError("Failed to log in. Check your credentials.");
      setError("email", { type: "manual", message: "Check this field" });
      setError("password", { type: "manual", message: "Check this field" });
    }
  };

  return (
    <StyledForm onSubmit={handleSubmit(onSubmit)}>
      {loginError && <ErrorMessage>{loginError}</ErrorMessage>}
      {/* @ts-ignore */}
      <InputForm<LoginFormValues>
        label="Email"
        name="email"
        type="email"
        register={register}
        errors={errors}
        className="form-input"
        validation={{
          required: "Email is required",
          pattern: {
            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
            message: "Invalid email address",
          },
        }}
      />
      {/* @ts-ignore */}
      <InputForm<LoginFormValues>
        label="Password"
        name="password"
        type="password"
        register={register}
        errors={errors}
        className="form-input"
        validation={{
          required: "Password is required",
        }}
      />
      <SubmitButton type="submit">Login</SubmitButton>
    </StyledForm>
  );
};

export default LoginForm;
