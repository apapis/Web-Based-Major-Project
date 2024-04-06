/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import svgr from "vite-plugin-svgr";

const vitestConfig = {
  test: {
    globals: true,
    environment: "jsdom",
    include: ["**/*.{test,spec}.{js,ts,jsx,tsx}"],
  },
};

export default defineConfig({
  plugins: [react(), svgr()],
  server: {
    host: "0.0.0.0",
  },
  ...vitestConfig, // Merge the vitestConfig into the main config
});
