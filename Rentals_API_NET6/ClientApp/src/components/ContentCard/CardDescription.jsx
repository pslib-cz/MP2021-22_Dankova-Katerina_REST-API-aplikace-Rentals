import styled from "styled-components";

const StyledCardDescription = styled.div`
  margin: 0.75rem;
  font-size: 1rem;
  color: grey;
  height: 25px;

  p {
    max-width: 250px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
`;

const CardDescription = (props) => {
  return (
    <StyledCardDescription>
      <p>{props.text}</p>
    </StyledCardDescription>
  );
};

export default CardDescription;
