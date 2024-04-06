import ReactDom from "react-dom";
import { ModalWindow, Overlay } from "./Modal.style";
import { ModalProps } from "../../Models/modal";

export default function Modal({ open, children, onClose }: ModalProps) {
  if (!open) return null;

  const portalElement = document.getElementById("portal");

  if (!portalElement) {
    // Handle the case where the portal element is not found in the document
    console.error("The element #portal was not found in the document.");
    return null;
  }

  return ReactDom.createPortal(
    <>
      <Overlay />
      <ModalWindow>
        <button onClick={onClose}>Close Modal</button>
        {children}
      </ModalWindow>
    </>,
    portalElement
  );
}
