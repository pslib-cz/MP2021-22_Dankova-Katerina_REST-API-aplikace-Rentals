import styled from "styled-components";

const StyledButton = styled.div`
  height: 5rem;
  width: 14rem;
  border-radius: 3rem;
  background-color: ${(props) => props.color};
  color: ${(props) => (props.darkText ? "black" : "white")};
  cursor: pointer;
  transition: 250ms;
  margin: 1rem auto;
  -webkit-box-shadow: 0 8px 20px 0px #8d8d8d;
  box-shadow: 0 8px 20px 0px #8d8d8d;

  p {
    text-align: center;
    font-size: 1.5rem;
    padding: 1.45rem;
    margin: 0 1rem;
  }

  &:hover {
    transition: 250ms;
    transform: scale(1.1);
  }
`;

const Button = (props) => {
  switch (props.type) {
    case "white":
      return (
        <StyledButton
          color="white"
          darkText
          text={props.text}
          onClick={props.onClick}
        >
          <p>{props.text}</p>
        </StyledButton>
      );
    case "green":
      return (
        <StyledButton color="#007784" text={props.text} onClick={props.onClick}>
          <p>{props.text}</p>
        </StyledButton>
      );
    case "red":
      return (
        <StyledButton color="#d05555" text={props.text} onClick={props.onClick}>
          <p>{props.text}</p>
        </StyledButton>
      );
    default:
      return (
        <StyledButton
          color="white"
          darkText
          text={props.text}
          onClick={props.onClick}
        >
          <p>{props.text}</p>
        </StyledButton>
      );
  }
};

export default Button;
