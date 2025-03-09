import React from 'react';
import ReactDOM from 'react-dom';
import { Modal, Button } from 'react-bootstrap';

interface ModalPortalProps {
  show: boolean;
  title: string;
  handleClose: () => void;
  onSave: () => void;  // Nueva prop
  children: React.ReactNode;
}

const ModalPortal: React.FC<ModalPortalProps> = ({ show, title, handleClose, onSave, children }) => {
  return ReactDOM.createPortal(
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {children}
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Cerrar
        </Button>
        <Button variant="primary" onClick={onSave}>
          Guardar
        </Button>
      </Modal.Footer>
    </Modal>,
    document.getElementById('modal-root') as HTMLElement
  );
};
export default ModalPortal;
