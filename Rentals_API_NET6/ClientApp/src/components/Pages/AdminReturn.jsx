import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import BagCard from "../BagCard/BagCard";
import BagImage from "../BagCard/BagImage";
import BagText from "../BagCard/BagText";
import { Badge } from "proomkatest";
import styled from "styled-components";
import Button from "../Button/Button";
import { createBrowserHistory } from "history";

const StyledReturn = styled.div`
  input {
    position: absolute;
    transform: scale(0);
  }

  .checkbox {
    position: relative;
    width: 50px;
    height: 50px;
    border: 3px solid #36383e;
    border-radius: 6px;
    cursor: pointer;
    transition: background 0.1s ease;
  }

  .checkbox::after {
    content: "";
    position: absolute;
    top: 50%;
    left: 50%;
    width: 10px;
    height: 20px;
    margin-left: -5px;
    margin-top: -13px;
    opacity: 0;
    transform: rotate(45deg) scale(0);
    border-right: 3px solid #fff;
    border-bottom: 3px solid #fff;
    transition: all 0.3s ease;
    transition-delay: 0.15s;
  }

  input:checked ~ .checkbox {
    border-color: transparent;
    background: #1ac0a2;
    animation: jelly 0.6s ease;
  }

  input:checked ~ .checkbox:after {
    opacity: 1;
    transform: rotate(45deg) scale(1);
  }

  @keyframes jelly {
    0%,
    100% {
      transform: scale(1, 1);
    }
    30% {
      transform: scale(1.1, 0.9);
    }
    40% {
      transform: scale(0.8, 1.2);
    }
    50% {
      transform: scale(1.1, 0.8);
    }
    65% {
      transform: scale(0.9, 1);
    }
    75% {
      transform: scale(1, 0.9);
    }
  }
`;

const history = createBrowserHistory();

const Return = (props) => {
  const { id } = useParams();
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);
  // eslint-disable-next-line
  const [selectedFiles, setSelectedFiles] = useState([]);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  document.title = `Rentals | Vrácení ${id}`;

  const fetchStoredFiles = async () => {
    const { data } = await Axios.get("/api/Renting/Items/" + id, config);
    setStoredFiles(data);
    console.log("data", data);
  };

  useEffect(() => {
    fetchStoredFiles();

    // eslint-disable-next-line
  }, [accessToken]);

  const EditSelected = (newItem) => {
    selectedFiles.indexOf(newItem) === -1
      ? selectedFiles.push(newItem)
      : selectedFiles.splice(selectedFiles.indexOf(newItem), 1);
    console.log(selectedFiles);
  };

  const handleClick = () => {
    Axios({
      method: "put",
      url: "/api/Renting/",
      data: { id: id, returnedItems: selectedFiles },
      headers: { Authorization: "Bearer " + accessToken },
    }).then(
      setTimeout(() => {
        fetchStoredFiles();
        history.push("/admin");
      }, 1500)
    );
  };

  return (
    <StyledReturn>
      {storedFiles.length ? (
        <>
          {storedFiles.map((file, i) => (
            <>
              <BagCard key={i}>
                <BagImage src={file.id}></BagImage>
                <BagText text={file.name} description={file.desc}></BagText>
                <div className="delete">
                  <Badge>
                    <input
                      id={"checkbox" + file.id}
                      type="checkbox"
                      onClick={() => EditSelected(file.id)}
                    />
                    <label for={"checkbox" + file.id} class="checkbox"></label>
                  </Badge>
                </div>
              </BagCard>
            </>
          ))}
          <Button text="Vrátit" type="green" onClick={() => handleClick()} />
        </>
      ) : (
        <p>V této výpůjčce nelze provést žádnou akci</p>
      )}
    </StyledReturn>
  );
};

export default Return;
