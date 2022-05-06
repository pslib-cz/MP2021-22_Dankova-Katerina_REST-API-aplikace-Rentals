import styled from "styled-components";

const StyledAdminListItemDate = styled.div`
  height: auto;

  margin: 0 1rem;
  display: flex;
  flex-direction: column;
  justify-content: center;

  text-align: center;

  p {
    display: grid;
    justify-items: center;
    align-items: center;
    text-align: center;

    &.abbr {
      margin: auto;
    }
  }
  .green {
    color: #00ae7c;
  }

  .orange {
    color: #d0b055;
  }

  .red {
    color: #d05555;
  }
`;

const AdminListItemDate = (props) => {
  if (props.state === 3) {
    return (
      <StyledAdminListItemDate>
        <p>
          {props.from} - {props.to}
        </p>
        <p className="red">Zamítnuto</p>
      </StyledAdminListItemDate>
    );
  }
  if (props.state === 2) {
    return (
      <StyledAdminListItemDate>
        <p>
          {props.from} - {props.to}
        </p>
        <p className="green">Vráceno</p>
      </StyledAdminListItemDate>
    );
  } else if (props.state === 1) {
    return (
      <StyledAdminListItemDate>
        <p>
          {props.from} - {props.to}
        </p>
        <p className="orange">Vypůjčeno</p>
      </StyledAdminListItemDate>
    );
  } else if (props.state === 0) {
    return (
      <StyledAdminListItemDate>
        <p>
          {props.from} - {props.to}
        </p>
        <p className="orange">Rezervované</p>
      </StyledAdminListItemDate>
    );
  }
};

export default AdminListItemDate;
