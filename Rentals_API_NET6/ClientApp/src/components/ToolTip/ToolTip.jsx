import styled from "styled-components";

const ToolTipText = styled("span")({
  visibility: "hidden",
  opacity: "0",
  transition: "visibility 0.3s linear,opacity 0.3s linear",
  width: "120px",
  backgroundColor: "#000",
  color: "#fff",
  textAlign: "center",
  borderRadius: "6px",
  padding: "5px 0",
  position: "absolute",
  zIndex: 1,
  bottom: "100%",
  left: "50%",
  marginLeft: "-60px",
  ":after": {
    content: '""',
    position: "absolute",
    top: "100%",
    left: "50%",
    marginLeft: "-5px",
    borderWidth: "5px",
    borderStyle: "solid",
    borderColor: "black transparent transparent transparent"
  }
});

const StyledToolTip = styled("div")({
  position: "relative",
  ":hover span": {
    visibility: "visible",
    opacity: "1"
  }
});

const ToolTip = ({ children, toolTipText }) => (
  <StyledToolTip>
    {children}
    <ToolTipText>{toolTipText}</ToolTipText>
  </StyledToolTip>
);

export default ToolTip;