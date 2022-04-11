import { Link, NavLink } from "react-router-dom";
import styled from "styled-components";

const StyledAdminMenu = styled.div`
  position: fixed;
  display: flex;
  flex-direction: column;
  left: 0;
  top: 50%;
  transform: translateY(-50%);
  width: 6rem;
  height: auto;
  min-height: 6rem;
  transition: 250ms;
  z-index: 5;
  text-align: left;

  &:hover {
    transition: 250ms;
    width: auto;

    .selector p {
      display: unset;
    }
  }

  .react-wavy-link--active {
    color: black !important;
  }

  @media (max-width: 700px) {
    width: 90%;
    margin: 0 5%;
    min-height: unset;
    align-items: center;
    justify-content: center;

    border-radius: 1.5rem 1.5rem 0 0;

    bottom: 0;
    left: 0;
    top: unset;

    flex-direction: row;
    transform: unset;

    .selector {
      width: 20% !important;
    }

    &:hover {
      button p {
        display: none !important;
      }

      width: 90%;
    }
  }

  background-color: white;
  border-radius: 0 1.5rem 1.5rem 0;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;

  .active-link i {
    color: black;
  }

  .selector {
    height: 4rem;
    width: auto;
    min-width: 4rem;
    margin: 1rem;
    display: flex;
    flex-direction: row;
    border-radius: 1.5rem 1.5rem;
    transition: 250ms;
    color: gray;
    text-decoration: none;

    &.active {
      transform: none;

      &:hover {
        transform: none !important;
      }

      &:after {
        display: none;
      }
    }

    i {
      font-size: 3rem;
      height: 3rem;
      width: 3rem;
      padding: 0.5rem;

      @media (max-width: 700px) {
        font-size: 2rem;
        height: auto;
      }
    }

    p {
      display: none;
      font-size: 1.25rem;
      height: 2.25rem;
      width: 100%;
      padding: 1rem;
      text-align: left;
      margin-left: 0.5rem;

      @media (max-width: 700px) {
        font-size: 1rem;
        height: 1.75rem;
        padding: 0.75rem;
      }
    }

    &:hover {
      background-color: #f7f7f7;
      cursor: pointer;
      transition: 250ms;
      color: black;
    }

    @media (max-width: 700px) {
      margin: 0.5rem 0;
      height: 3rem;
    }
  }

  a {
    &:after {
      display: none;
    }
  }
`;

const AdminMenu = (props) => {
  return (
    <StyledAdminMenu>
      <NavLink waveColor="#007784" tag={Link} to="/admin" className="selector">
        <i className="fas fa-globe-europe"></i>
        <p>Přehled</p>
      </NavLink>
      <NavLink
        activeClassName="active-link"
        tag={Link}
        to="/admin/calendar"
        className="selector"
      >
        <i className="far fa-calendar"></i>
        <p>Kalendář</p>
      </NavLink>
      <NavLink
        activeClassName="active-link"
        tag={Link}
        to="/admin/list"
        className="selector"
      >
        <i className="fas fa-clipboard-list"></i>
        <p>Položky</p>
      </NavLink>
      <NavLink
        activeClassName="active-link"
        tag={Link}
        to="/admin/users"
        className="selector"
      >
        <i className="fas fa-user-shield"></i>
        <p>Uživatelé</p>
      </NavLink>
    </StyledAdminMenu>
  );
};

export default AdminMenu;
