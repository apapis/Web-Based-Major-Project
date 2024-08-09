import React from "react";
import {
  ImageWrapper,
  ImagesContainer,
  Image,
  DeleteButton,
} from "../Images.styles";

interface NewImagesProps {
  images: File[];
  onRemoveImage: (index: number) => void;
}

const NewImages: React.FC<NewImagesProps> = ({ images, onRemoveImage }) => {
  return (
    <ImagesContainer>
      {images.map((image, index) => (
        <ImageWrapper key={index}>
          <Image src={URL.createObjectURL(image)} alt="New Image" />
          <DeleteButton onClick={() => onRemoveImage(index)}>
            &times;
          </DeleteButton>
        </ImageWrapper>
      ))}
    </ImagesContainer>
  );
};

export default NewImages;
