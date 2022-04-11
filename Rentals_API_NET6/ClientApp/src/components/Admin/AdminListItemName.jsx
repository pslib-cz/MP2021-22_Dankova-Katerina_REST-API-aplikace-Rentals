import styled from "styled-components";

const StyledAdminListItemName = styled.div`
  height: auto;

  margin: 0 2rem;
  display: flex;
  flex-direction: column;

  text-align: center;

  text-align: left;

  p {
    font-size: 0.75rem;
    color: grey;
  }

  h3 {
    margin-top: 0 !important;
  }

  img {
    height: 2rem;
    border-radius: 0.25rem;
    margin: 0 0.25rem;
  }
`;

const AdminListItemName = (props) => {
  if (props.items) {
    return (
      <StyledAdminListItemName>
        <h3>{props.name}</h3>
        <div>
          {props.items.slice(0, 3).map((i, index) => {
            return (
              <img
                src={"/api/Item/Img/" + i.itemId}
                alt=""
                onError={(e) => {
                  e.currentTarget.src = "/image.svg";
                }}
              />
            );
          })}
        </div>
      </StyledAdminListItemName>
    );
  } else {
    return (
      <StyledAdminListItemName>
        <h3>{props.name}</h3>
      </StyledAdminListItemName>
    );
  }
};

export default AdminListItemName;
