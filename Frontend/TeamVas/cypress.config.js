import { defineConfig } from "cypress";

export default defineConfig({
  e2e: {
    experimentalStudio: true,
    baseUrl: "http://localhost:3000/",
    viewportWidth: 1920,
    viewportHeight: 1080,
  },
  env: {
    auth_base_url: "http://localhost:8080/",
    auth_realm: "TeamVas",
    auth_client_id: "TeamVasClient",
  },
});
