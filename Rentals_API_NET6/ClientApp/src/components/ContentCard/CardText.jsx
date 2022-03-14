import styled from "styled-components";

const StyledCardText = styled.div`
  max-width: 100%;
  margin: 1rem;
  font-size: 1.5rem;
  height: 35px;

  p {
    max-width: 250px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
`;

const CardText = (props) => {
  return (
    <StyledCardText>
      <p>{props.text}</p>
    </StyledCardText>
  );
};

export default CardText;
