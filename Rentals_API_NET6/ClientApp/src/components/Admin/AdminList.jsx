import styled from "styled-components";

const StyledAdminList = styled.div`
  width: ${(props) => (props.isSmall ? "100%" : "80%")};
  padding: ${(props) => (props.isSmall ? "0 " : "10%")};
  border-radius: 1.5rem;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;

  background-color: white;
  text-align: center;
  height: auto;

  overflow-y: scroll;
  &::-webkit-scrollbar {
    display: none;
  }

  & .item-list:nth-child(even) {
    background-color: #f7f7f7;
  }
`;

const AdminList = (props) => {
  return (
    <StyledAdminList isSmall={props.isSmall}>{props.children}</StyledAdminList>
  );
};

export default AdminList;
