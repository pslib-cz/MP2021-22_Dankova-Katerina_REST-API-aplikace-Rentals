import styled from "styled-components";
import { Link } from "react-router-dom";
import { WavyLink } from "react-wavy-transitions";
import { useState } from "react";

const StyledCardImage = styled.div`
  img {
    aspect-ratio: 1/1;
    border-radius: 1rem;
    width: 85%;
    margin: 7.5%;
    object-fit: contain;
  }

  .loading-img {
    border-radius: 1rem;
    width: 85%;
    aspect-ratio: 1/1;
    margin: 7.5%;
    cursor: progress;
    background: linear-gradient(0.25turn, transparent, #fff, transparent),
      linear-gradient(#eee, #eee);
    background-repeat: no-repeat;
    background-position: -315px 0, 0 0, 0px 190px, 50px 195px;
    animation: loading 1.5s infinite;
  }
  @keyframes loading {
    to {
      background-position: 315px 0, 0 0, 0 190px, 50px 195px;
    }
  }
`;

const CardImage = (props) => {
  //props -> path to image
  const [loaded, setLoaded] = useState(false);

  return (
    <StyledCardImage {...props}>
      {loaded ? null : <div className="loading-img"></div>}
      <WavyLink
        waveColor="#007784"
        className="unstyled header"
        tag={Link}
        to={"/detail/" + props.src}
        activeClassName={""}
      >
        <img
          src={"/api/Item/Img/" + props.src}
          alt=""
          style={loaded ? {} : { display: "none" }}
          onLoad={() => setLoaded(true)}
          onError={(e) => {
            e.currentTarget.src = "/image.svg";
          }}
        />
      </WavyLink>
    </StyledCardImage>
  );
};

export default CardImage;
