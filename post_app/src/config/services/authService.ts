import { RegisterData } from "../../models/RegisterModel";
import { User } from "../../models/UserModel";
import { API_ROUTES_AUTH } from "../apiRoutes/API_ROUTES_AUTH";
import { BASE_URL } from "../apiRoutes/API_ROUTES_REGISTER";

export const fetchUserFromCookie = async (): Promise<any> => {
    const response = await fetch('https://localhost:44342/api/Auth', {
        credentials: 'include',
    });

    if (!response.ok) {
        throw new Error('No hay sesión activa');
    }

    return response.json();
};

export const loginUser = async (loginData: { correo: string; contrasenaHash: string }): Promise<User> => {
    const response = await fetch(`${API_ROUTES_AUTH.LOGIN}`, {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
        },
        body: JSON.stringify(loginData),
    });

    if (!response.ok) {
        throw new Error('Error en el inicio de sesión');
    }

    return response.json();
};



export const registerUser = async (userData: RegisterData): Promise<any> => {
    try {
        const response = await fetch(`${BASE_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                correo: userData.email,
                contrasenaHash: userData.password,
                primerNombre: userData.username,
                segundoNombre: userData.lastName,
                fechaCreacion: new Date().toISOString(),
                fechaActualizacion: new Date().toISOString(),
                activo: true
            }),
        });

        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.statusText}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error en registerUser:', error);
        throw error;
    }
};

export const logoutUser = async (): Promise<void> => {
    const response = await fetch(`${API_ROUTES_AUTH.LOGOUT}`, {
        method: 'POST',
        credentials: 'include',
    });

    if (!response.ok) {
        throw new Error('Error al cerrar sesión');
    }
};

