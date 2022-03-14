import styled from "styled-components";
import { Link, NavLink } from "react-router-dom";

const StyledError = styled.div`
  width: 100%;
  height: 65vh;
  border-radius: 2rem;
  box-shadow: rgb(0 0 0 / 10%) 0px 8px 20px 0px;

  background: url("/Wave.svg");
  background-color: #cccccc;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  text-align: center;
  color: white !important;

  h2 {
    font-size: 4rem;
    padding-top: 5%;
  }

  p {
    font-size: 7rem;
    font-weight: 700;
  }

  .safer {
    transform: scale(1);
    text-decoration: none;
    p {
      box-shadow: rgb(0 0 0 / 10%) 0px 8px 20px 0px;

      border-radius: 6rem;
      background-color: #fff;
      margin: auto;
      width: 30rem;
      padding: 2rem;
      font-size: 2rem;
      transform: scale(1);
      color: black;
      text-decoration: none;
      transition: 150ms;

      &:hover {
        transform: scale(1.05);
        transition: 150ms;
      }

      @media (max-width: 700px) {
        width: 12.5rem;
        font-size: 1rem;
      }
    }

    &:after {
      display: none;
    }
  }
`;

const Error = (props) => {
  return (
    <StyledError>
      <h2>Whoops</h2>
      <p>404</p>
      <NavLink className="safer" tag={Link} to={"/"}>
        <p>Do bezpečnějších vod</p>
      </NavLink>
    </StyledError>
  );
};

export default Error;
