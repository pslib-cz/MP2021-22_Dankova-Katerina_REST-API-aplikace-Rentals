import styled from "styled-components";

const StyledCardActionButton = styled.div`
  text-align: center;
  background-color: #007784;
  padding: 1rem;
  border-radius: 0 0 2.5rem 2.5rem;
  color: #fff;
  cursor: pointer;
  transition: 100ms;

  &:hover {
    background-color: #198490;
    transition: 100ms;
  }
`;

const CardActionButton = (props) => {
  if (props.text) {
    return (
      <StyledCardActionButton>
        <p>{props.text}</p>
      </StyledCardActionButton>
    );
  } else {
    return (
      <StyledCardActionButton>
        <p>
          Přidat do košíku <i className="fas fa-shopping-bag"></i>
        </p>
      </StyledCardActionButton>
    );
  }
};

export default CardActionButton;
