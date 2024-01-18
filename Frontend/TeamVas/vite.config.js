import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],

  server:{
    port: 3000,
    proxy: {
      '/Courses': {
        target: 'https://localhost:7232/',
        ws: true,
        changeOrigin: true,
        secure: false,
        },
      '/Assignments': {
        target: 'https://localhost:7232/',
        ws: true,
        changeOrigin: true,
        secure: false,
        }
      }
      
  }
})