import { useState } from 'react';
import { Form, Button, Row, Col, Alert, Container } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { RegisterData } from '../models/RegisterModel';
import { registerUser } from '../config/services/authService';



interface Errors {
    username: string;
    lastName: string;
    email: string;
    password: string;
}

function RegisterPage() {

    const navigate = useNavigate(); 

    const [formData, setFormData] = useState<RegisterData>({
        username: '',
        lastName: '',
        email: '',
        password: '',
    });

    const [errors, setErrors] = useState<Errors>({
        username: '',
        lastName: '',
        email: '',
        password: '',
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setFormData({
            ...formData,
            [id]: value,
        });
    };

    const validateForm = () => {
        let valid = true;
        const newErrors = { username: '', lastName: '', email: '', password: '' };

        if (!formData.username) {
            newErrors.username = 'Nombre es requerido';
            valid = false;
        }

        if (!formData.lastName) {
            newErrors.lastName = 'Apellido es requerido';
            valid = false;
        }

        if (!formData.email) {
            newErrors.email = 'Email es requerido';
            valid = false;
        } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = 'Email is invalid';
            valid = false;
        }

        if (!formData.password) {
            newErrors.password = 'Password es requerido';
            valid = false;
        } else if (formData.password.length < 6) {
            newErrors.password = 'La contraseña debe terner al menos 6 caracteres';
            valid = false;
        }

        setErrors(newErrors);
        return valid;
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (validateForm()) {
            try {
                const data = await registerUser(formData);
                console.log('Success:', data);
    
                setFormData({
                    username: '',
                    lastName: '',
                    email: '',
                    password: '',
                });
    
                navigate('/');
            } catch (error) {
                console.error('Error al registrar usuario:', error);
            }
        }
    };

    return (
        <>
            <Container>
                <div className="d-flex justify-content-center my-4">
                    <img loading='lazy' src="https://www.finanzauto.com.co/portal/assets/finanzauto-logo-4d8a7b8a.png" width={220} height={30} alt="logo" />
                </div>

                <Form onSubmit={handleSubmit}>
                    <Row>
                        <Col md={6}>
                            <Form.Group controlId="username">
                                <Form.Label>Nombre</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Nombres"
                                    value={formData.username}
                                    onChange={handleChange}
                                />
                                {errors.username && <Alert variant="danger">{errors.username}</Alert>}
                            </Form.Group>
                        </Col>

                        <Col md={6}>
                            <Form.Group controlId="lastName">
                                <Form.Label>Apellido</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Apellido"
                                    value={formData.lastName}
                                    onChange={handleChange}
                                />
                                {errors.lastName && <Alert variant="danger">{errors.lastName}</Alert>}
                            </Form.Group>
                        </Col>
                    </Row>

                    <Row>
                        <Col>
                            <Form.Group controlId="email">
                                <Form.Label>Email</Form.Label>
                                <Form.Control
                                    type="email"
                                    placeholder="Enter email"
                                    value={formData.email}
                                    onChange={handleChange}
                                />
                                {errors.email && <Alert variant="danger">{errors.email}</Alert>}
                            </Form.Group>
                        </Col>
                    </Row>

                    <Row>
                        <Col>
                            <Form.Group controlId="password">
                                <Form.Label>Contraseña</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Password"
                                    value={formData.password}
                                    onChange={handleChange}
                                />
                                {errors.password && <Alert variant="danger">{errors.password}</Alert>}
                            </Form.Group>
                        </Col>
                    </Row>

                    <div className="my-3">
                        <Button variant="primary" type="submit">
                            Registrarme
                        </Button>
                    </div>

                    <Row>
                        <Col>
                            <Link to={'/'}>Ya tengo una cuenta</Link>

                        </Col>
                    </Row>
                </Form>
            </Container>

        </>
    );
}

export default RegisterPage;
