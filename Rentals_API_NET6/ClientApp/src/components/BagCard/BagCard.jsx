import styled from "styled-components";

const StyledBagCard = styled.div`
  margin-right: auto;
  margin: 1rem;
  width: calc(100% - 2rem);
  height: 8rem;
  display: flex;
  justify-content: space-between;
  background-color: #fff;
  border-radius: 1.5rem;

  -webkit-box-shadow: 0px 5px 50px -30px #191919;
  box-shadow: 0px 5px 50px -30px #191919;

  @media (max-width: 700px) {
    height: 6rem;
    width: 97.5%;
    margin: auto;
  }

  @media (max-width: 600px) {
    height: 5rem;
  }

  .delete {
    height: 100%;
    position: relative;
    display: grid;
    place-items: center;
    width: auto;
    margin-right: 2rem;

    .proomka-badge {
      cursor: pointer;
      position: unset;
      background-color: inherit;
    }
  }

  .time {
    opacity: 0;

    width: 12rem;
    height: 2rem;
    border-radius: 1rem;

    right: unset;
    left: calc(50% - 6rem);
    top: calc(-2.5rem);

    background-color: #1a1a1a;

    color: white;
    font-size: 1rem;
  }

  &:hover {
    .time {
      opacity: 1;
    }
  }
`;

const BagCard = (props) => {
  return <StyledBagCard>{props.children}</StyledBagCard>;
};

export default BagCard;
