import styled from "styled-components";
import { useState } from "react";
import AdminCalendar from "../Admin/AdminCalendar";

const StyledCardDate = styled.span`
  margin: ${(props) => (props.open === true ? "0" : "0.75")};
  font-size: 0.75rem;
  color: #00ae7c;
  min-height: 25px;

  p {
    max-width: 275px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;

    &.notAvailable {
      color: #d05555;
      cursor: pointer;
    }

    margin: 0.75rem;

    i.fa-times {
      margin-left: 6.5rem;
      display: ${(props) => (props.open === true ? "" : "none")};
      color: black;
    }
  }

  div {
    display: ${(props) => (props.open === true ? "" : "none")};
  }
`;

const CardDate = (props) => {

  const [open, setOpen] = useState(false);
  const buttonHandler = () => {
    setOpen(current => !current)
    console.log(open);
  }


  if (props.dateFrom === undefined) {
    return (
      <StyledCardDate open={open}>
        <p onClick={buttonHandler} className="notAvailable">Momentálně nedostupné <i className="fas fa-times"></i></p>
        <AdminCalendar className="cal" isSmall isHeight></AdminCalendar>
      </StyledCardDate>
    );
  } else {
    return (
      <StyledCardDate>
        <p>
          Dostupnost: {props.dateFrom} - {props.dateTo}
        </p>
      </StyledCardDate>
    );
  }
};

export default CardDate;
