import styled from "styled-components";

const StyledAdminListItem = styled.div`
  min-height: 2rem;
  height: auto;
  width: 100%;
  padding: 1rem 0;

  border-bottom: 1px grey solid;

  display: flex;
  justify-content: space-between;

  & > * {
    width: 100%;
  }

  @media (max-width: 1000px) {
    flex-direction: column;

    * {
      text-align: center !important;
      margin: 0;
      place-items: center;
    }
  }
`;

const AdminListItem = (props) => {
  return (
    <StyledAdminListItem className="item-list">
      {props.children}
    </StyledAdminListItem>
  );
};

export default AdminListItem;
