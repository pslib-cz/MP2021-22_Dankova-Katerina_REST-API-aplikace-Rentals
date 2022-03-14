import styled from "styled-components";

const StyledBagMenu = styled.div`
  height: 100%;
  width: auto;
  display: flex;
  justify-content: center;

  i.trash {
    font-size: 2rem;
    margin: 3rem 2rem 3rem 0;
    cursor: pointer;

    @media (max-width: 700px) {
      margin: 2.125rem 1rem 2.125rem 0;
      font-size: 1.75rem;
    }

    @media (max-width: 600px) {
      margin: 2rem 1rem 2rem 0;
      font-size: 1rem;
    }
  }

  div {
    display: flex;
    margin: 3rem 1.5rem 3rem 1.5rem;
    text-align: center;

    i {
      font-size: 1rem;
      color: gray;
      transition: 250ms;
      margin: 0.75rem;

      &:hover {
        transition: 250ms;
        color: black;
        cursor: pointer;
      }
    }

    p {
      font-size: 1.75rem;
      font-weight: 700;

      @media (max-width: 600px) {
        font-size: 1.5rem;
      }
    }

    @media (max-width: 700px) {
      flex-direction: column-reverse;
      margin: 0.66rem 1.25rem 0.66rem 1.25rem;

      i {
        font-size: 0.75rem;
        margin: 0.15rem;
      }
    }

    @media (max-width: 600px) {
      margin: 0.33rem 1rem;
    }
  }
`;

const BagMenu = (props) => {
  return (
    <StyledBagMenu amount={props.amount}>
      <i className="fas fa-trash trash"></i>
    </StyledBagMenu>
  );
}

export default BagMenu;
