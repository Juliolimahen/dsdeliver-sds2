import axios from 'axios';
import jwt_decode, { JwtPayload } from 'jwt-decode';

const TOKEN_KEY = 'jwtToken';

const API_URL = process.env.REACT_APP_API_URL ?? 'https://localhost:44369';

const authService = {
  login: async (login: string, password: string): Promise<{ success: boolean }> => {
    try {
      const response = await axios.post<{ token: string }>(`${API_URL}/api/user/login`, { login, password });
      const token = response.data.token;

      authService.setToken(token);

      return { success: true };
    } catch (error) {
      return { success: false };
    }
  },

  setToken: (token: string) => {
    localStorage.setItem(TOKEN_KEY, token);
  },

  getToken: (): string | null => {
    return localStorage.getItem(TOKEN_KEY);
  },

  isAuthenticated: (): boolean => {
    const token = authService.getToken();

    if (token) {
      const decodedToken = jwt_decode<JwtPayload>(token);

      if (decodedToken && typeof decodedToken.exp === 'number' && decodedToken.exp * 1000 > Date.now()) {
        return true;
      }
    }

    return false;
  },

  logout: (): void => {
    localStorage.removeItem(TOKEN_KEY);
  },
};

export default authService;
