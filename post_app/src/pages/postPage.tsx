// PostPage.tsx
import React, { useEffect, useState } from 'react';
import { Card, Button, Container, Row, Col, Navbar, Nav, Form } from 'react-bootstrap';
import { usePostAppContext } from '../context/postAppContext';
import { Link } from 'react-router-dom';
import { Post } from '../models/PostModel';
import ModalPortal from '../components/modalPortal';
import { deletePost, savePost } from '../config/services/postService';

const PostPage: React.FC = () => {
    const { user, handleLogout } = usePostAppContext();
    const [posts, setPosts] = useState<Post[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [currentPost, setCurrentPost] = useState<Post | null>(null);
    const [formData, setFormData] = useState({
        usuarioId: user?.idUser ?? 0,
        titulo: '',
        contenido: '',
        fechaCreacion: new Date().toISOString(),
        fechaActualizacion: new Date().toISOString()
    });

    const fetchPosts = () => {
        fetch('https://localhost:44344/api/Publicaciones', {
            method: 'GET',
            credentials: "include",
        })
            .then(response => response.json())
            .then(data => setPosts(data))
            .catch(error => console.error('Error:', error));
    };



    // Manejar apertura de modal
    const handleShowModal = (post: Post | null) => {
        setCurrentPost(post);
        setFormData(post ? {
            usuarioId: post.usuarioId ?? 0,
            titulo: post.titulo,
            contenido: post.contenido,
            fechaCreacion: post.fechaCreacion,
            fechaActualizacion: post.fechaActualizacion
        } : {
            usuarioId: user?.idUser ?? 0,
            titulo: '',
            contenido: '',
            fechaCreacion: new Date().toISOString(),
            fechaActualizacion: new Date().toISOString()
        });
        setShowModal(true);
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSave = async () => {
        const postData = {
            ...formData,
            publicacionId: currentPost?.publicacionId || 0,
            fechaCreacion: currentPost?.fechaCreacion || new Date().toISOString()
        };

        try {
            await savePost(postData, !!currentPost);
            fetchPosts();
            setShowModal(false);
        } catch (error) {
            console.error('Error al guardar la publicación:', error);
        }
    };

    const handleDelete = (postId: number) => {
        if (window.confirm('¿Estás seguro de eliminar esta publicación?')) {
            deletePost(postId.toString());
            fetchPosts();
        }
    };

    useEffect(() => { fetchPosts(); }, []);


    return (
        <Container fluid className='px-3'>
            <Navbar bg="light" expand="lg" className="mb-4 px-3">
                <Navbar.Brand> <img loading='lazy' src="https://www.finanzauto.com.co/portal/assets/finanzauto-logo-4d8a7b8a.png" width={220} height={30} alt="logo" /> </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse className="justify-content-end">
                    <Nav>
                        <Link to="/profile" className="nav-link">Editar {user?.user}</Link>
                        <Button variant="success" onClick={() => handleShowModal(null)}>
                            Nueva Publicación
                        </Button>
                        <Button onClick={handleLogout} className="btn btn-danger mx-2">Cerrar sesion</Button>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>

            <ModalPortal
                show={showModal}
                title={currentPost ? 'Editar Publicación' : 'Nueva Publicación'}
                handleClose={() => setShowModal(false)}
                onSave={handleSave}
            >
                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label>ID Usuario</Form.Label>
                        <Form.Control
                            type="number"
                            name="usuarioId"
                            value={formData.usuarioId}
                            onChange={handleInputChange}
                            disabled
                        />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Título</Form.Label>
                        <Form.Control
                            type="text"
                            name="titulo"
                            maxLength={50}
                            value={formData.titulo}
                            onChange={handleInputChange}
                            required
                        />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Contenido</Form.Label>
                        <Form.Control
                            as="textarea"
                            rows={5}
                            name="contenido"
                            maxLength={50}
                            value={formData.contenido}
                            onChange={handleInputChange}
                            required
                        />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Fecha Creación</Form.Label>
                        <Form.Control
                            type="datetime-local"
                            name="fechaCreacion"
                            value={new Date(formData.fechaCreacion).toISOString().slice(0, 16)}
                            onChange={handleInputChange}
                            disabled={true}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Fecha Actualización</Form.Label>
                        <Form.Control
                            type="datetime-local"
                            name="fechaActualizacion"
                            value={new Date(formData.fechaActualizacion).toISOString().slice(0, 16)}
                            onChange={handleInputChange}
                            disabled
                        />
                    </Form.Group>
                </Form>
            </ModalPortal>

            <Row>
                {posts.map(post => (
                    <Col key={post.publicacionId} sm={12} md={6} lg={4} className="mb-4">
                        <Card>
                            <Card.Body>
                                <Card.Title>{post.titulo}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">
                                    Creado el: {new Date(post.fechaCreacion).toLocaleDateString('es-ES')}
                                </Card.Subtitle>
                                <Card.Text>{post.contenido}</Card.Text>
                                <div className="d-flex gap-2">

                                    {user?.idUser == post.usuarioId
                                        && (
                                            <Button variant="primary" onClick={() => handleShowModal(post)}>
                                                Editar
                                            </Button>
                                        ) || null}

                                    {user?.idUser == post.usuarioId
                                        && (
                                            <Button variant="danger" onClick={() => handleDelete(post.publicacionId)}>
                                                Eliminar
                                            </Button>
                                        ) || null}
                                </div>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
};

export default PostPage;