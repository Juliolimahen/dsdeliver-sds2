import jwt from 'jsonwebtoken';

// Função para verificar se o token está expirado
export function isTokenExpired(token: string): boolean {
  const decodedToken: any = jwt.decode(token);

  if (decodedToken && decodedToken.exp) {
    const expirationTimestamp = decodedToken.exp * 1000; // Convertendo para milissegundos
    const currentTimestamp = Date.now();

    return currentTimestamp > expirationTimestamp;
  }

  return true; // Retorna true se não conseguir decodificar o token
}
