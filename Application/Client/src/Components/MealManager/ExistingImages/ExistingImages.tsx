import React from "react";

import {
  ImageWrapper,
  ImagesContainer,
  Image,
  DeleteButton,
} from "../Images.styles";

interface ExistingImagesProps {
  imageUrls: string[];
  onRemoveImage: (imageUrl: string) => void;
}

const ExistingImages: React.FC<ExistingImagesProps> = ({
  imageUrls,
  onRemoveImage,
}) => {
  if (!imageUrls) {
    return null;
  }

  return (
    <ImagesContainer>
      {imageUrls.map((imageUrl) => (
        <ImageWrapper key={imageUrl}>
          <Image src={imageUrl} alt="Meal Image" />
          <DeleteButton onClick={() => onRemoveImage(imageUrl)}>
            &times;
          </DeleteButton>
        </ImageWrapper>
      ))}
    </ImagesContainer>
  );
};

export default ExistingImages;
