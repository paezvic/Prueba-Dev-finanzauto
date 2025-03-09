import React, { useState, ReactNode, useEffect } from 'react';
import PostAppContext from './postAppContext'; 
import { User } from '../models/UserModel'; 
import { fetchUserFromCookie, logoutUser } from '../config/services/authService';

const PostProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<User | null>({
        user: '',
        idUser: 0,
        name: '',
        message: ''
    });

    const handleLogout = () => {
        logoutUser();
        setUser(null);
    };

    useEffect(() => {
        const checkAuth = async () => {
            try {
                debugger;
                const userData = await fetchUserFromCookie();
                setUser({user: userData.email, idUser: userData.userId});
            } catch (error) {
                setUser(null);
            } 
        };

        checkAuth();
    }, []);

    return (
        // Proveedor del contexto, pasamos el estado y setState como valor
        <PostAppContext.Provider value={{ user, setUser, handleLogout }}>
            {children}
        </PostAppContext.Provider>
    );
};

export { PostProvider };
