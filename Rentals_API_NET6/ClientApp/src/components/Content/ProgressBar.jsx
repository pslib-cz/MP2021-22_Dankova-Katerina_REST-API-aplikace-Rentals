import { useEffect } from "react";
import { useState } from "react";
import styled from "styled-components";

const StyledProgress = styled.div`
  &.progress {
    position: relative;

    width: calc(100% - 4rem);
    height: 0.5rem;
    border-radius: 0.5rem;

    margin: 2rem 2rem;

    background-color: #c3c3c3;

    .bar {
      height: 100%;
      transition: width 1000ms linear;
      background-color: #007784;
      border-radius: 0.5rem;
      width: ${(props) => (props.width ? props.width + "%" : "0")};
      max-width: 100%;
    }

    .fifty {
      width: 2rem;
      height: 2rem;

      right: unset;
      left: calc(50% - 1rem);
      top: calc(-0.75rem - 4px);
      background-color: white;
      border: ${(props) =>
        props.fifty ? "4px solid #007784" : "4px solid #c3c3c3"};
      transition: all 1000ms;
      color: ${(props) => (props.fifty ? "#007784" : "#c3c3c3")};
    }

    .hundred {
      width: 2rem;
      height: 2rem;

      right: unset;
      left: calc(100% - 2rem);
      top: calc(-0.75rem - 4px);
      background-color: white;
      border: ${(props) =>
        props.hundred ? "4px solid #007784" : "4px solid #c3c3c3"};
      transition: all 1000ms;
      color: ${(props) => (props.hundred ? "#007784" : "#c3c3c3")};
    }

    .proomka-badge {
      border-radius: 50%;

      .proomka-badge {
        opacity: 0;

        width: 12rem;
        height: 2rem;
        border-radius: 1rem;

        right: unset;
        left: calc(50% - 6rem);
        top: calc(-2.5rem);

        background-color: #1a1a1a;

        color: white;
        font-size: 1rem;
      }

      &:hover {
        .proomka-badge {
          opacity: 1;
        }
      }
    }
  }
`;

const ProgressBar = (props) => {
  const [fifty, setFifty] = useState(false);
  const [hundred, setHundred] = useState(false);

  useEffect(() => {
    if (parseInt(props.width) >= 50) {
      setFifty(true);
    } else {
      setFifty(false);
    }
    if (parseInt(props.width) >= 100) {
      setHundred(true);
    } else {
      setHundred(false);
    }
  }, [props.width]);

  return (
    <StyledProgress
      className="progress"
      {...props}
      fifty={fifty}
      hundred={hundred}
      width={props.width}
    >
      <div className="bar"></div>
      {props.children}
    </StyledProgress>
  );
};

export default ProgressBar;
