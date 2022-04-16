import styled from "styled-components";
import Button from "../Button/Button";
import CategoryButton from "../Button/CategoryButton";
import { Badge, Card, Modal } from "proomkatest";
import { useState } from "react";
import ProgressBar from "./ProgressBar";
import { useEffect } from "react";

const JustStyledDiv = styled.div`
  .no {
    overflow: hidden;
  }

  .create-item-form {
    width: 75vw;
    height: 75vh;
    position: fixed;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    z-index: 10;

    display: flex;
    flex-direction: column;
    justify-content: space-between;
  }
`;

const StyledContentMenu = styled.div`
  justify-content: space-between;
  align-items: center;
  align-items: flex-start;
  margin: 0 1%;
  @media (max-width: 700px) {
    flex-direction: row;
    display: flex;

    &.marginTop {
      margin-top: 2rem;
    }
  }
`;

export const StyledSearchBox = styled.div`
  height: 2rem;
  background-color: #fff;
  width: 20rem;
  padding: 1rem;
  border-radius: 2.5rem;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;
  float: left;

  #searchField {
    height: 2rem;
    width: 80%;
    margin-left: 5%;
    margin: 0;
    padding: 0;
    border: none;
    color: black;
    background-color: inherit;
    font-family: "Poppins";
    font-size: 0.9rem;

    &:focus-visible {
      outline: none;
    }
  }

  i {
    font-size: 1rem;
    padding: 0.5rem;
  }

  @media (max-width: 700px) {
    width: 95%;
  }
  @media (max-width: 400px) {
    width: 90%;
  }
`;

export const StyledSearchBoxWithin = styled.div`
  width: 90%;
  margin-left: 5%;
  float: left;
  display: flex;
  justify-content: space-between;
`;

export const StyledFilterButton = styled.div`
  height: 2rem;
  min-width: 2rem;
  max-width: 2rem;
  padding: 1rem;
  background-color: #fff;
  border-radius: 2.5rem;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;
  margin-left: auto;

  @media (max-width: 700px) {
    margin-left: unset;
  }

  i {
    padding: 0.125rem;
    font-size: 1.75rem;
    cursor: pointer;
    transition: 200ms ease-out;
    transform: rotate(${(props) => (props.isFiltered ? "0deg" : "180deg")});
  }
`;

export const StyledDiv = styled.div`
  @media (max-width: 700px) {
    width: auto;
    margin-left: 2rem;
    display: flex;
    justify-content: right;
  }
`;

const ContentMenu = (props) => {
  const [showModal, setShowModal] = useState(false);
  const showModalFn = (e) => {
    showModal === false
      ? setShowModal(true)
      : setTimeout(() => {
          setShowModal(false);
        }, 500);
  };
  function prevent(fn, defaultOnly) {
    return (e, ...params) => {
      e && e.preventDefault();
      !defaultOnly && e && e.stopPropagation();
      fn(e, ...params);
    };
  }

  const [counter, setCounter] = useState(0);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setCounter(counter + 5);
    }, 1000);
    //console.log(counter);
    return () => {
      clearTimeout(timeout);
    };
  }, [counter]);

  if (props.isAdmin) {
    return (
      <StyledContentMenu>
        <div id="searchme"></div>
        <div>
          <div id="filter"></div>
          <div id="buttonme"></div>
        </div>
      </StyledContentMenu>
    );
  } else if (props.isView) {
    return (
      <JustStyledDiv>
        <StyledContentMenu className="marginTop">
          <StyledSearchBox>
            <StyledSearchBoxWithin>
              <input id="searchField" type="text" placeholder="Hledat..." />
              <span>
                <i className="fas fa-search"></i>
              </span>
            </StyledSearchBoxWithin>
          </StyledSearchBox>
          <StyledDiv>
            <CategoryButton
              isUnfiltered={props.isUnfiltered}
              isView
            ></CategoryButton>
            <StyledFilterButton isUnfiltered={props.isUnfiltered}>
              <i className="fas fa-sort-amount-up-alt"></i>
            </StyledFilterButton>
          </StyledDiv>
        </StyledContentMenu>
        <Button text="Přidat" type="green" onClick={showModalFn}></Button>
        <div className="no" onClick={showModalFn}>
          {showModal ? (
            <Modal>
              <Card
                className="proomka-card create-item-form"
                onClick={prevent(() => void 0)}
              >
                <div></div>
                <ProgressBar width={counter.toString()}>
                  <Badge className="fifty proomka-badge">
                    <i className="fas fa-check"></i>
                    <Badge>Vytvoření položky</Badge>
                  </Badge>
                  <Badge className="hundred proomka-badge">
                    <i className="fas fa-check"></i>
                    <Badge>Nahrání obrázku</Badge>
                  </Badge>
                </ProgressBar>
              </Card>
            </Modal>
          ) : null}
        </div>
      </JustStyledDiv>
    );
  } else {
    return (
      <StyledContentMenu>
        <div id="searchme"></div>
        <div id="filter"></div>
      </StyledContentMenu>
    );
  }
};

export default ContentMenu;
