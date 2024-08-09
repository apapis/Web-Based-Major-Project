/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import svgr from "vite-plugin-svgr";

export default defineConfig({
  plugins: [react(), svgr()],
  build: {
    outDir:
      "../Web-Based Major Project - API/Web-Based Major Project - API/wwwroot",
  },
  server: {
    host: "0.0.0.0",
    port: 3000,
  },
});
