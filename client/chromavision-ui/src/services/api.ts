// src/services/api.ts
import axios from 'axios';

const API_URL = 'http://localhost:5141/api/v1';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  }
});

export interface GenerateFromTextRequest {
  description: string;
  colorCount?: number;
  name?: string;
}

export interface ColorPalette {
  id?: string;
  name: string;
  description: string;
  colors: string[];
  userId?: string;
  createdAt?: string;
  updatedAt?: string;
}

export interface ApiResponse<T> {
  succeeded: boolean;
  data: T;
  errors: string[];
}

export const colorService = {
  generateFromText: async (description: string, colorCount: number = 5, name: string = ''): Promise<ApiResponse<ColorPalette>> => {
    const response = await api.post('/ColorPalette/generate-from-text', {
      description,
      colorCount,
      name
    });
    return response.data;
  }
};

export default api;