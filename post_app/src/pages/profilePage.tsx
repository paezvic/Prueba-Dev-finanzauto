import { useEffect, useState } from "react";
import { Container, Row, Col, Form, Button, Navbar, Nav, Card } from "react-bootstrap";
import { usePostAppContext } from "../context/postAppContext";
import { Link, useNavigate } from "react-router-dom";

const ProfilePage = () => {

    const navigate = useNavigate();
    const { user } = usePostAppContext();


    const [formData, setFormData] = useState({
        userId: "",
        email: "",
        password: "",
        firstName: "",
        secondName: "",
        creationDate: "",
        updateDate: "",
        active: true,
    });

    const [errors, setErrors] = useState({
        userId: "",
        email: "",
        password: "",
        firstName: "",
        secondName: "",
        creationDate: "",
        updateDate: "",
        active: "",
    });

    const validateForm = () => {
        let newErrors = {
            userId: "",
            email: "",
            password: "",
            firstName: "",
            secondName: "",
            creationDate: "",
            updateDate: "",
            active: "",
        };

        if (!formData.email.trim() || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
            newErrors.email = "Correo inválido.";
        }
        if (!formData.password.trim()) newErrors.password = "La contraseña es obligatoria.";
        if (!formData.firstName.trim()) newErrors.firstName = "El primer nombre es obligatorio.";
        if (!formData.creationDate.trim() || isNaN(Date.parse(formData.creationDate))) {
            newErrors.creationDate = "Fecha de creación inválida.";
        }
        if (!formData.updateDate.trim() || isNaN(Date.parse(formData.updateDate))) {
            newErrors.updateDate = "Fecha de actualización inválida.";
        }

        setErrors(newErrors);
        return Object.values(newErrors).every((error) => error === "");
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (validateForm()) {
            const dta = {
                usuarioId: 6,
                correo: formData.email,
                contrasenaHash: formData.password,
                primerNombre: formData.firstName,
                segundoNombre: formData.secondName,
                fechaCreacion: formData.creationDate,
                fechaActualizacion: formData.updateDate,
            }
            try {
                const response = await fetch(`https://localhost:44342/api/Usuario/${user?.idUser}`, {
                    method: "PUT",
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(dta)
                });
                if (!response.ok) {
                    throw new Error(`Error ${response.status}: ${response.statusText}`);
                }

                const data = await response.json();

                setFormData({
                    userId: data?.usuarioId.toString() || "",
                    email: data?.correo || "",
                    password: data?.contrasenaHash || "",
                    firstName: data?.primerNombre || "",
                    secondName: data?.segundoNombre || "",
                    creationDate: data?.fechaCreacion ? new Date(data.fechaCreacion).toISOString().split("T")[0] : "",
                    updateDate: data?.fechaActualizacion ? new Date(data.fechaActualizacion).toISOString().split("T")[0] : "",
                    active: data?.activo || false,
                });

                alert("Datos guardados exitosamente")

                setTimeout(() => {
                    navigate('/post');
                }, 2000);


            } catch (error) {
                console.error("Error fetching profile data:", error);
            }

        }
    };

    useEffect(() => {
        const fetchData = async () => {

            try {
                const response = await fetch(`https://localhost:44342/api/Usuario/${user?.idUser}`, {
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                    },
                });
                if (!response.ok) {
                    throw new Error(`Error ${response.status}: ${response.statusText}`);
                }

                const data = await response.json();
                console.log(data);

                setFormData({
                    userId: data?.usuarioId.toString() || "",
                    email: data?.correo || "",
                    password: data?.contrasenaHash || "",
                    firstName: data?.primerNombre || "",
                    secondName: data?.segundoNombre || "",
                    creationDate: data?.fechaCreacion ? new Date(data.fechaCreacion).toISOString().split("T")[0] : new Date().toISOString().split("T")[0],
                    updateDate: new Date().toISOString().split("T")[0],
                    active: data?.activo?.toString() || "false",
                });


            } catch (error) {
                console.error("Error fetching profile data:", error);
            }
        };

        fetchData();

    }, []);

    return (
        <Container fluid >

            <Navbar bg="light" expand="lg" className="mb-4 px-3">
                <Navbar.Brand href="#home"><img loading='lazy' src="https://www.finanzauto.com.co/portal/assets/finanzauto-logo-4d8a7b8a.png" width={220} height={30} alt="logo" /></Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="ml-auto">
                        <Link className="btn btn-link" to={'/post'}>Volver</Link>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>

            <Container>

                <Card className="p-3">

                    <Card.Body>
                        <Card.Title><h1>Perfil de Usuario</h1></Card.Title>


                        <Form onSubmit={handleSubmit}>
                            <Row>

                                <Col md="6">
                                    <Form.Group controlId="formEmail">
                                        <Form.Label>Correo</Form.Label>
                                        <Form.Control
                                            type="email"
                                            name="email"
                                            value={formData.email}
                                            onChange={handleChange}
                                            isInvalid={!!errors.email}
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.email}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formPasswordHash">
                                        <Form.Label>Contraseña</Form.Label>
                                        <Form.Control
                                            type="password"
                                            name="password"
                                            value={formData.password}
                                            onChange={handleChange}
                                            isInvalid={!!errors.password}
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.password}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formFirstName">
                                        <Form.Label>Primer Nombre</Form.Label>
                                        <Form.Control
                                            type="text"
                                            name="firstName"
                                            value={formData.firstName}
                                            onChange={handleChange}
                                            isInvalid={!!errors.firstName}
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.firstName}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formSecondName">
                                        <Form.Label>Segundo Nombre</Form.Label>
                                        <Form.Control
                                            type="text"
                                            name="secondName"
                                            value={formData.secondName}
                                            onChange={handleChange}
                                        />
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formCreationDate">
                                        <Form.Label>Fecha Creación</Form.Label>
                                        <Form.Control
                                            type="date"
                                            name="creationDate"
                                            value={formData.creationDate}
                                            onChange={handleChange}
                                            isInvalid={!!errors.creationDate}
                                            disabled
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.creationDate}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formUpdateDate">
                                        <Form.Label>Fecha Actualización</Form.Label>
                                        <Form.Control
                                            type="date"
                                            name="updateDate"
                                            value={formData.updateDate}
                                            onChange={handleChange}
                                            isInvalid={!!errors.updateDate}
                                            disabled
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.updateDate}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                                <Col md="6">
                                    <Form.Group controlId="formActive">
                                        <Form.Label>Activo</Form.Label>
                                        <Form.Control
                                            type="text"
                                            name="active"
                                            value={formData.active.toString()}
                                            onChange={handleChange}
                                            isInvalid={!!errors.active}
                                            disabled
                                        />
                                        <Form.Control.Feedback type="invalid">{errors.active}</Form.Control.Feedback>
                                    </Form.Group>
                                </Col>

                            </Row>

                            <Row>
                                <Col md="12" className="mt-3">
                                    <Button variant="primary" type="submit">
                                        Guardar Cambios
                                    </Button>
                                </Col>
                            </Row>
                        </Form>

                    </Card.Body>
                </Card>
            </Container>

        </Container>
    );
};

export default ProfilePage;
