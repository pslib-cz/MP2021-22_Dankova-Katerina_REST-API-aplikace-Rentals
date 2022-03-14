import styled from "styled-components";

const StyledCard = styled.div`
  background-color: #fff;
  border-radius: 2.5rem;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;
  transition: 350ms;
  width: 100%;
  position: relative;
  max-width: 300px;
  height: auto;
  display: grid;
  grid-template-rows: auto 0.33fr 0.33fr auto;
  margin: 0 auto auto auto;

  &:hover {
    transform: scale(1.05);
    transition: 350ms;
  }
`;

const ContentCard = (props) => {
  return (
    <>
      <StyledCard isLeft={props.isLeft} className="card">
        {props.children}
      </StyledCard>
    </>
  );
};

export default ContentCard;
