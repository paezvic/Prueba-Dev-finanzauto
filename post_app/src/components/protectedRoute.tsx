// src/components/ProtectedRoute.tsx
import { usePostAppContext } from '../context/postAppContext';
import { Navigate } from 'react-router-dom';
import { ReactElement } from 'react';

// Definimos las props del componente
interface ProtectedRouteProps {
  children: ReactElement;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  // Obtenemos el usuario del contexto
  const { user } = usePostAppContext();

  // Si no hay usuario, redirigimos al login
  if (!user) {
    return <Navigate to="/" replace />;
  }

  // Si está autenticado, renderizamos el componente hijo
  return children;
};

export default ProtectedRoute;