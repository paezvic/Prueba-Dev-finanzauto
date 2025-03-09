import { Post } from "../../models/PostModel";
import { API_ROUTES_POSTS } from "../apiRoutes/API_ROUTES_POSTS";

export const savePost = async (postData: Post, isUpdate: boolean): Promise<void> => {
    const url = isUpdate ? `${API_ROUTES_POSTS.POSTS}/${postData.publicacionId}` : API_ROUTES_POSTS.POSTS;

    try {
        const response = await fetch(url, {
            method: isUpdate ? 'PUT' : 'POST',
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(postData)
        });

        if (!response.ok) {
            throw new Error('Error al guardar la publicación');
        }
    } catch (error) {
        console.error('Error en savePost:', error);
        throw error;
    }
};


export const deletePost = async (postId: string): Promise<void> => {
    const url = `${API_ROUTES_POSTS.POSTS}/${postId}`;

    try {
        const response = await fetch(url, {
            method: 'DELETE',
            credentials: "include",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error('Error al eliminar la publicación');
        }
    } catch (error) {
        console.error('Error en deletePost:', error);
        throw error;
    }
};