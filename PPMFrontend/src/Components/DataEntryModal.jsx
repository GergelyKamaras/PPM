import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { PropertyForm } from './Forms/PropertyForm';
import { FinancialObjectForm } from './Forms/FinancialObjectForm';

export function DataEntryModal({type, url}) {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <>
        <Button onClick={handleShow}>
            Add {type}
        </Button>

        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
            <Modal.Title>Property form</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {type == "Property" &&
                    <PropertyForm url={url} handleClose={handleClose}/>
                }
                {type == "FinancialObject" &&
                    <FinancialObjectForm url={url} handleClose={handleClose}/>
                }
            </Modal.Body>
            <Modal.Footer>
            </Modal.Footer>
        </Modal>
        </>
    );
}
