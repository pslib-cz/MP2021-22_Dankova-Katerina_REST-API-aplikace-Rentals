import styled from "styled-components";
import { Link } from "react-router-dom";
import { WavyLink } from "react-wavy-transitions";

const StyledList = styled.ul`
  display: flex;
  flex-direction: row;
  list-style-type: none;
`;

const StyledListItem = styled.li`
  flex-grow: 1;
  display: flex;
  justify-content: center;
  transition: 250ms;

  i {
    font-size: 24px;
  }
`;

const StyledNavbarMenu = styled.nav`
  width: 25%;

  @media (max-width: 700px) {
    margin-top: 0.25rem;
    width: 100%;
  }
`;

const NavbarMenu = (props) => {
  var w = window.innerWidth;

  if (w < 700) {
    if (
      window.localStorage.getItem("isAdmin") === "true" &&
      window.localStorage.getItem("isLogged") === "true"
    ) {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled header"
                tag={Link}
                to={"/"}
                activeClassName={""}
              >
                <i className="fas fa-home"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/favorite"}
                activeClassName={"active"}
              >
                <i className="fas fa-heart"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/bag"}
                activeClassName={"active"}
              >
                <i className="fas fa-shopping-bag"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/admin"}
                activeClassName={"active"}
              >
                <i className="fas fa-cog"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    } else if (window.localStorage.getItem("isLogged") === "false") {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled header"
                tag={Link}
                to={"/"}
                activeClassName={""}
              >
                <i className="fas fa-home"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    } else {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled header"
                tag={Link}
                to={"/"}
                activeClassName={""}
              >
                <i className="fas fa-home"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/favorite"}
                activeClassName={"active"}
              >
                <i className="fas fa-heart"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/bag"}
                activeClassName={"active"}
              >
                <i className="fas fa-shopping-bag"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    }
  } else {
    if (
      window.localStorage.getItem("isAdmin") === "true" &&
      window.localStorage.getItem("isLogged") === "true"
    ) {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/favorite"}
                activeClassName={"active"}
              >
                <i className="fas fa-heart"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/bag"}
                activeClassName={"active"}
              >
                <i className="fas fa-shopping-bag"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/admin"}
                activeClassName={"active"}
              >
                <i className="fas fa-cog"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    } else if (window.localStorage.getItem("isLogged") === "false") {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    } else {
      return (
        <StyledNavbarMenu>
          <StyledList>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/favorite"}
                activeClassName={"active"}
              >
                <i className="fas fa-heart"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/bag"}
                activeClassName={"active"}
              >
                <i className="fas fa-shopping-bag"></i>
              </WavyLink>
            </StyledListItem>
            <StyledListItem>
              <WavyLink
                waveColor="#007784"
                className="unstyled"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                <i className="fas fa-user"></i>
              </WavyLink>
            </StyledListItem>
          </StyledList>
        </StyledNavbarMenu>
      );
    }
  }
};

export default NavbarMenu;
