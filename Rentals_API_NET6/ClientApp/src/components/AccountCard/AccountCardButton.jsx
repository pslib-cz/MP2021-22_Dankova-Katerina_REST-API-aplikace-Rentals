import styled from "styled-components";
import { useState } from "react";

const StyledAccountCardButton = styled.div`
  width: auto;
  display: flex;
  justify-content: center;
  transition: 250ms;
  margin: 3rem 2rem 3rem 0;
  color: grey;

  @media (max-width: 700px) {
    margin: 2.125rem 1rem 2.125rem 0;
  }

  @media (max-width: 600px) {
    margin: 2rem 1rem 2rem 0;
  }

  i.trash {
    font-size: 2rem;
    cursor: pointer;
    margin: 0 0.5rem;

    @media (max-width: 700px) {
      font-size: 1.75rem;
    }

    @media (max-width: 600px) {
      font-size: 1rem;
    }
  }

  &.isOpened .chevron {
    transition: 250ms;
    transform: rotate(180deg);
    color: black;
  }

  .trash:hover {
    transition: 250ms;
    color: black;
  }
`;

const AccountCardButton = (props) => {
  const [isActive, setActive] = useState(false);

  const toggleClass = () => {
    setActive(!isActive);

    document.getElementById("detail-" + props.id).classList.toggle("visible");
    document.getElementById("detail-" + props.id).focus();
  };

  if (props.isAdmin) {
    if (props.isSendable) {
      return (
        <StyledAccountCardButton
          id={props.id}
          className={(isActive ? "isOpened" : "") + " "}
        >
          <div>
            <i className="fas fa-bell trash"></i>
            <i className="fas fa-edit trash"></i>
            <i className="fas fa-trash trash"></i>
            <i
              className="fas fa-chevron-down trash chevron"
              onClick={toggleClass}
            ></i>
          </div>
        </StyledAccountCardButton>
      );
    } else {
      return (
        <StyledAccountCardButton className={(isActive ? "isOpened" : "") + " "}>
          <div>
            <i className="fas fa-edit trash"></i>
            <i className="fas fa-trash trash"></i>
            <i
              className="fas fa-chevron-down trash chevron"
              onClick={toggleClass}
            ></i>
          </div>
        </StyledAccountCardButton>
      );
    }
  } else {
    return (
      <StyledAccountCardButton className={(isActive ? "isOpened" : "") + " "}>
        <div>
          <i
            className="fas fa-chevron-down trash chevron"
            onClick={toggleClass}
          ></i>
        </div>
      </StyledAccountCardButton>
    );
  }
};

export default AccountCardButton;
