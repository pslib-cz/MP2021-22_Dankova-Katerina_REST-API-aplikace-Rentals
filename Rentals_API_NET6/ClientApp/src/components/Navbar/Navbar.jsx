import styled from "styled-components";
import { Link } from "react-router-dom";
import NavbarMenu from "./NavbarMenu";
import { WavyLink } from "react-wavy-transitions";

const StyledNavbar = styled.div`
  display: flex;
  width: 100%;
  z-index: 999;
  font-family: "Ubuntu", sans-serif;
  transition: all 0.3s ease;

  h1 {
    font-family: "Playfair Display", serif;
  }

  @media (max-width: 700px) {
    position: fixed;
    bottom: 0;
  }
`;

const StyledNavbarContent = styled.nav`
  width: 70%;
  padding: 2rem;
  margin: 1rem auto 1rem auto;
  display: flex;
  justify-content: space-between;
  align-items: center;

  background-color: #fff;

  border-radius: 1.5rem;

  -webkit-box-shadow: 0px 5px 50px -30px #191919;
  box-shadow: 0px 5px 50px -30px #191919;

  @media (max-width: 700px) {
    width: 80%;
    flex-direction: column;

    background-color: #007784;
    color: white;

    margin-bottom: 0.5rem;

    *::after {
      color: white;
      border-bottom: white solid 2px !important;
    }
  }
`;

const Navbar = (props) => {
  var w = window.innerWidth;

  if (w < 700) {
    return (
      <StyledNavbar className="small-nav">
        <StyledNavbarContent className="navbar">
          <NavbarMenu></NavbarMenu>
        </StyledNavbarContent>
      </StyledNavbar>
    );
  } else {
    return (
      <StyledNavbar>
        <StyledNavbarContent className="navbar">
          <WavyLink
            waveColor="#007784"
            className="unstyled header"
            tag={Link}
            to={"/"}
            activeClassName={""}
          >
            <h1>Rentals</h1>
          </WavyLink>
          <NavbarMenu></NavbarMenu>
        </StyledNavbarContent>
      </StyledNavbar>
    );
  }
};

export default Navbar;
