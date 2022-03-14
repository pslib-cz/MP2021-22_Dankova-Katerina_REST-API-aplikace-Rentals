import styled from "styled-components";

const StyledBagDate = styled.div`
  height: 2rem;
  margin: 3rem 0 3rem 0;
  position: relative;

  p {
    text-transform: uppercase;
    padding-top: 0.25rem;
    font-size: 1rem;
    color: ${(props) => (props.isAvailable ? "#00ae7c" : "#d05555")};

    @media (max-width: 700px) {
      font-size: 0.8rem;
      height: 1rem;
      padding-top: 0;
    }
  }

  @media (max-width: 700px) {
    height: 1rem;
    margin: 2.5rem 0 2.5rem 0;
  }

  @media (max-width: 600px) {
    height: 1rem;
    margin: 2rem 0 2rem 0;
  }
`;

const BagDate = (props) => {
  var w = window.innerWidth;

  if (props.isAvailable) {
    if (w < 700) {
      return (
        <StyledBagDate isAvailable={props.isAvailable}>
          <p>
            <i className="far fa-check-circle"></i>
          </p>
          {props.children}
        </StyledBagDate>
      );
    } else {
      return (
        <StyledBagDate isAvailable={props.isAvailable}>
          <p>Dostupné</p>
          {props.children}
        </StyledBagDate>
      );
    }
  } else {
    if (w < 700) {
      return (
        <StyledBagDate isAvailable={props.isAvailable}>
          <p>
            <i className="far fa-times-circle"></i>
          </p>
          {props.children}
        </StyledBagDate>
      );
    } else {
      return (
        <StyledBagDate isAvailable={props.isAvailable}>
          <p>Nedostupné</p>
          {props.children}
        </StyledBagDate>
      );
    }
  }
};

export default BagDate;
