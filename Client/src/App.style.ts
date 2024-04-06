import styled from 'styled-components';

export const Wrapper = styled.div`
  background-color: ${({ theme }) => theme.colors.brandColorTwo};
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  min-height: 100vh;
`;