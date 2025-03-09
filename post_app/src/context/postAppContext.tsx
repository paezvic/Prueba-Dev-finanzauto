// src/context/PostAppContext.tsx
import React, { createContext, useContext } from 'react';
import { User } from '../models/UserModel'; // Asegúrate de tener este modelo

// Definición de la interfaz para el contexto
interface PostAppContextProps {
    user: User | null;
    setUser: React.Dispatch<React.SetStateAction<User | null>>;
    handleLogout: () => void;
}

// Creación del contexto con un valor por defecto undefined
const PostAppContext = createContext<PostAppContextProps | undefined>(undefined);

// Hook personalizado para consumir el contexto
export const usePostAppContext = () => {
    const context = useContext(PostAppContext);
    if (context === undefined) {
        throw new Error('usePostAppContext must be used within a PostAppProvider');
    }
    return context;
};

export default PostAppContext;
