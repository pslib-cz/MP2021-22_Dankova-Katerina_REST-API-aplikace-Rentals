import styled from "styled-components";
import { useState } from "react";

const StyledCategoryButton = styled.div`
  height: 4rem;
  width: auto;
  background-color: ${(props) => (props.clicked ? "#007784" : "#fff")};
  border-radius: 2.5rem;
  color: ${(props) => (props.clicked ? "white" : "unset")};

  box-shadow: rgb(0 0 0 / 23%) 0px 8px 20px 0px;

  display: grid;
  place-items: center;

  margin: 0 1rem;

  padding: 0 2rem;

  font-size: 1.25rem;
  font-weight: 500;

  cursor: pointer;

  transition: 250ms;

  &:hover {
    background-color: ${(props) => (props.clicked ? "#007784" : "#00a7b9")};
    color: white;
  }
`;

const CategoryButton = (props) => {
  const [clicked, setClicked] = useState(false);
  const handleClick = () => {
    setClicked(!clicked);
    console.log(clicked);
  };

  return (
    <StyledCategoryButton
      className="category-button"
      onClick={() => handleClick()}
      clicked={clicked}
    >
      <div>{props.children}</div>
    </StyledCategoryButton>
  );
};

export default CategoryButton;
