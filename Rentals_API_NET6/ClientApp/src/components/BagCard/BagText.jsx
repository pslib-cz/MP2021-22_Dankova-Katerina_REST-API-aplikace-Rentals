import { useEffect, useState } from "react";
import styled from "styled-components";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";

const StyledBagText = styled.div`
  display: grid;
  grid-template-columns: repeat(1, auto);
  grid-template-rows: repeat(2, auto);
  max-width: 50%;
  max-height: 4rem;
  margin: 2rem;

  h3 {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    margin-top: unset !important;

    @media (max-width: 700px) {
      font-size: 1rem;
    }
  }

  p {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    color: grey;

    @media (max-width: 700px) {
      font-size: 0.8rem;
    }
  }

  @media (max-width: 700px) {
    max-height: 4rem;
    margin: 1rem;
  }
`;

const BagText = (props) => {
  const [header, setHeader] = useState(props.text);
  const [desc, setDesc] = useState(props.description);
  const [{ accessToken }] = useAppContext();

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };
  useEffect(() => {
    const fetchStoredFiles = async () => {
      const { data } = await Axios.get("/api/Item/" + props.file, config);
      setHeader(data.name);
      setDesc(data.description);
    };
    if (props.file) {
      fetchStoredFiles();
    }

    // eslint-disable-next-line
  }, [props.file, accessToken]);

  return (
    <StyledBagText>
      <h3>{header}</h3>
      <p>{desc}</p>
    </StyledBagText>
  );
};

export default BagText;
