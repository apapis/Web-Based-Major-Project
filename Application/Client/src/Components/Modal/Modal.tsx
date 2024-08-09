// Modal.tsx
import ReactDom from "react-dom";
import { ModalWindow, Overlay, CloseButton, GlobalStyle } from "./Modal.style";
import { ModalProps } from "../../Models/modal";

export default function Modal({ open, children, onClose }: ModalProps) {
  if (!open) return null;

  const portalElement = document.getElementById("portal");
  if (!portalElement) {
    console.error("The element #portal was not found in the document.");
    return null;
  }

  return ReactDom.createPortal(
    <>
      <GlobalStyle hidden={open} />
      <Overlay />
      <ModalWindow>
        <CloseButton onClick={onClose}>&times;</CloseButton>
        {children}
      </ModalWindow>
    </>,
    portalElement
  );
}
