import styled from "styled-components";
import ToolTip from "../ToolTip/ToolTip";

const StyledCardInfo = styled.span`
  position: absolute;
  top: 2rem;
  left: 2rem;

  i {
    font-size: 2rem;
    color: #d05555;
    transition: 150ms;
  }
`;

const CardInfo = (props) => {
  return (
    <StyledCardInfo>
      <ToolTip toolTipText={props.text}>
        <i className="fas fa-exclamation"></i>
      </ToolTip>
    </StyledCardInfo>
  );
};

export default CardInfo;
