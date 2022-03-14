import { useEffect, useState } from "react";
import styled from "styled-components";
import BagCard from "../BagCard/BagCard";
import BagImage from "../BagCard/BagImage";
import BagText from "../BagCard/BagText";
import AccountCardButton from "./AccountCardButton";

const StyledAccountCard = styled.div`
  margin: auto;
  width: ${(props) => (props.isBordered ? "100%" : "90%")};
  min-height: 8rem;
  display: grid;
  background-color: #fff;
  border-radius: 1.5rem;
  grid-template-columns: repeat(1, 1fr);
  transition: 250ms;
  margin-bottom: 2.25rem;
  box-shadow: 0px 5px 50px -30px #191919;

  @media (max-width: 700px) {
    min-height: 6rem;
    width: 100%;
  }

  @media (max-width: 600px) {
    min-height: 5rem;
  }
`;

const StyledAccountCardWrapper = styled.div`
  display: flex;
  justify-content: space-between;
`;

const StyledAccountCardDetail = styled.div`
  max-height: 0;
  display: flex;
  justify-content: space-between;
  flex-direction: column;
  transition: 250ms;
  height: auto;

  div {
    height: 0;
    transition: 150ms;
  }

  &.visible {
    max-height: 50rem;
    height: auto;
    padding-bottom: 2rem;
    transition: 750ms;

    div {
      box-shadow: none;
      height: 8rem;
      transition: 500ms;

      @media (max-width: 700px) {
        height: 6rem;
        width: 100%;
      }

      @media (max-width: 600px) {
        height: 5rem;
      }

      .image-container {
        aspect-ratio: 1/1;
        width: auto;
        margin-left: 1rem;
      }
    }
  }

  div {
    height: 0;
    box-shadow: none;
  }
`;

const AccountCard = (props) => {
  const [items, setItems] = useState([]);
  useEffect(() => {
    setItems(props.items);
  }, [props.items]);

  return (
    <>
      <StyledAccountCard id={"order-" + props.id} isBordered={props.isBordered}>
        <StyledAccountCardWrapper>
          {props.children}
          <AccountCardButton
            isAdmin={props.isAdmin}
            id={"order-" + props.id}
            isSendable={props.isSendable}
          ></AccountCardButton>
        </StyledAccountCardWrapper>
        <StyledAccountCardDetail id={"detail-order-" + props.id}>
          {items.map((file, i) => (
            <BagCard key={i}>
              <BagImage src={file.itemId}></BagImage>
              <BagText
                file={file.itemId}
                text="Nazev produktu"
                description="Popisek produktu"
              ></BagText>
            </BagCard>
          ))}
        </StyledAccountCardDetail>
      </StyledAccountCard>
    </>
  );
};

export default AccountCard;
