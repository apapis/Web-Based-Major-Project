import styled from "styled-components";

export const ImagesContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 30px;
  margin-bottom: 20px;
`;

export const ImageWrapper = styled.div`
  position: relative;
  width: 200px;
  height: 200px;
`;

export const Image = styled.img`
  width: 100%;
  height: 100%;
  object-fit: cover;
  border-radius: 8px;
`;

export const DeleteButton = styled.button`
  position: absolute;
  top: -15px;
  right: -15px;
  background-color: ${({ theme }) => theme.colors.brandColorFour};
  color: ${({ theme }) => theme.colors.white};
  border: none;
  border-radius: 50%;
  width: 45px;
  height: 45px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  font-size: 18px;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: ${({ theme }) => theme.colors.brandColorFive};
  }
`;
