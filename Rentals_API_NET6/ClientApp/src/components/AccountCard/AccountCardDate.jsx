import styled from "styled-components";

const StyledAccountCardDate = styled.div`
  height: 2rem;
  margin: 3rem 0 3rem 0;

  .green {
    color: #00ae7c;
  }

  .orange {
    color: #d0b055;
  }

  .red {
    color: #d05555;
  }

  p {
    text-transform: uppercase;
    padding-top: 0.25rem;
    font-size: 1rem;

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
    margin: 2.25rem 0 2.25rem 0;
  }
`;

const AccountCardDate = (props) => {
  if (props.state === 3) {
    return (
      <StyledAccountCardDate isAvailable={props.isAvailable}>
        <p className="red">Zrušeno</p>
      </StyledAccountCardDate>
    );
  }
  if (props.state === 2) {
    return (
      <StyledAccountCardDate isAvailable={props.isAvailable}>
        <p className="green">Vráceno</p>
      </StyledAccountCardDate>
    );
  } else if (props.state === 1) {
    return (
      <StyledAccountCardDate isAvailable={props.isAvailable}>
        <p className="orange">Probíhá</p>
      </StyledAccountCardDate>
    );
  } else if (props.state === 0) {
    return (
      <StyledAccountCardDate isAvailable={props.isAvailable}>
        <p className="orange">Čeká potvrzení</p>
      </StyledAccountCardDate>
    );
  }
};

export default AccountCardDate;
