import styled from "styled-components";

const StyledBagImage = styled.div`
  height: 100%;
  aspect-ratio: 1/1;
  border-radius: 1.5rem 0 0 1.5rem;

  img {
    object-fit: contain;
    width: 100%;
    height: 100%;
    border-radius: 1.5rem 0 0 1.5rem;
  }
`;

const BagImage = (props) => {
  return (
    <StyledBagImage className="image-container" {...props}>
      <img
        src={"/api/Item/Img/" + props.src}
        alt="alt"
        onError={(e) => {
          e.currentTarget.src = "/image.svg";
        }}
      />
    </StyledBagImage>
  );
};

export default BagImage;
