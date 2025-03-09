import { useState } from "react";
import { Container, Form, Button, Alert } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { usePostAppContext } from "../context/postAppContext";
import { loginUser } from "../config/services/authService";

const HomePage = () => {
    const { setUser } = usePostAppContext();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState(false);

    const navigate = useNavigate(); // Para redirigir a otras páginas, si es necesario

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        // Validaciones básicas de los campos
        if (email === '' || password === '') {
            setError('Por favor, ingresa todos los campos');
            setSuccess(false);
            return;
        }

        // Estructura de datos para la API
        const loginData = {
            correo: email,
            contrasenaHash: password,  // Asegúrate de que la contraseña esté correctamente "hasheada"
        };

        try {
            const response = await loginUser(loginData);
            if (response) {
                debugger;
                // Si la respuesta es exitosa, mostramos un mensaje de éxito
                setSuccess(true);
                setError('');
                setUser({
                    idUser: response.idUser,
                    user: response.user,
                });
                // Redirigir a otra página si es necesario (ejemplo: al dashboard)
                navigate('/post');
            } else {
                // Si la respuesta no es exitosa, mostramos un mensaje de error
                setError('Credenciales incorrectas');
                setSuccess(false);
            }
        }
        catch (err) {
            // Manejo de errores en caso de que el fetch falle
            console.error('Error:', err);
            setError('Hubo un error al intentar iniciar sesión');
            setSuccess(false);
        }
    };

    return (
        <>
            <Container className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
                <Form onSubmit={handleSubmit} style={{ width: '300px' }}>
                    <div className="d-flex justify-content-center mb-4">
                        <img loading='lazy' src="https://www.finanzauto.com.co/portal/assets/finanzauto-logo-4d8a7b8a.png" width={220} height={30} alt="logo" />
                    </div>
                    <h3 className="text-center mb-4">Iniciar Sesión</h3>

                    {/* Mostrar alertas de error y éxito */}
                    {error && <Alert variant="danger">{error}</Alert>}
                    {success && <Alert variant="success">¡Inicio de sesión exitoso!</Alert>}

                    <Form.Group className="mb-3" controlId="formBasicEmail">
                        <Form.Label>Correo Electrónico</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder="Ingresa tu correo"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">
                        <Form.Label>Contraseña</Form.Label>
                        <Form.Control
                            type="password"
                            placeholder="Ingresa tu contraseña"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </Form.Group>

                    <Button variant="primary" type="submit" className="w-100">
                        Iniciar Sesión
                    </Button>

                    <Link to={'/register'} className="d-block text-center mt-2">
                        ¿No tienes una cuenta? ¡Regístrate aquí!
                    </Link>
                </Form>
            </Container>
        </>
    );
};

export default HomePage;



