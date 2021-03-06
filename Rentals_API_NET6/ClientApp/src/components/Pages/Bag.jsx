import { StyledMainGrid, StyledIntroImage } from "../Content/Content";
import BagImage from "../BagCard/BagImage";
import BagCard from "../BagCard/BagCard";
import BagText from "../BagCard/BagText";
import Button from "../Button/Button";
import wave from "../media/Wave.svg";
import { Badge, Alert } from "proomkatest";
import { useEffect, useState } from "react";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import ReactDOM from "react-dom";
import { useQuery } from "react-query";
import moment from "moment";

const Bag = () => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  document.title = "Rentals | Košík";

  const FetchItems = () => {
    return useQuery(
      "bag",
      async () => {
        const { data } = await Axios.get("/api/User/Cart", config);
        return data;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };
  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setStoredFiles(data);
    }
  }, [status, data, accessToken]);

  const handleClick = (id, i) => {
    Axios({
      method: "delete",
      url: "api/User/Cart/" + id,
      headers: { Authorization: "Bearer " + accessToken },
    })
      .then(
        setStoredFiles(
          storedFiles.filter(function (v, index) {
            return index !== i;
          })
        )
      )
      .then(
        ReactDOM.unmountComponentAtNode(document.getElementById("ok")),
        ReactDOM.render(
          <Alert
            textColor="white"
            width="16rem"
            height="4rem"
            color="#00ae7c"
            delay="2000"
          >
            <i className="far fa-check-circle icon" /> Odebráno z košíku
          </Alert>,
          document.getElementById("ok")
        )
      );
  };

  const [start, setStart] = useState();
  const [end, setEnd] = useState();

  useEffect(() => {
    console.log(start + " - " + end);
  }, [start, end]);

  function handleChange(event) {
    setStart(event.target.value);
  }
  function handleChange2(event) {
    setEnd(event.target.value);
  }

  return (
    <StyledMainGrid>
      <StyledIntroImage bgImage={wave}>
        <h1>Váš košík</h1>
      </StyledIntroImage>
      {storedFiles.map((file, i) => (
        <BagCard key={i}>
          <BagImage src={file.id}></BagImage>
          <BagText text={file.name} description={file.desc}></BagText>
          <div className="delete">
            <Badge>
              <i
                className="fas fa-times"
                onClick={() => handleClick(file.id, i)}
              ></i>
            </Badge>
          </div>
        </BagCard>
      ))}

      <div className="total">
        <p>
          Předmětů: <span>{storedFiles.length}</span>
        </p>
        <p>
          Doba výpůjčky:{" "}
          <span>
            <br />
            Od:
            <input
              type="datetime-local"
              id="meeting-time"
              name="meeting-time"
              onChange={handleChange}
              min={moment().format("YYYY-MM-DDThh:mm")}
            />
            <br /> Do:{" "}
            <input
              type="datetime-local"
              id="meeting-time"
              name="meeting-time"
              onChange={handleChange2}
              min={moment().format("YYYY-MM-DDThh:mm")}
            />
          </span>
        </p>
      </div>
      <div className="but">
        <Button
          type="green"
          text="Vypůjčit"
          onClick={() => {
            storedFiles.length && start && end
              ? Axios({
                  method: "post",
                  url: "/api/Renting",
                  data: {
                    items: storedFiles.map((a) => a.id),
                    start: start,
                    end: end,
                    note: "",
                  },
                  headers: { Authorization: "Bearer " + accessToken },
                })
                  .catch((res) =>
                    res?.response?.status === 400
                      ? (ReactDOM.unmountComponentAtNode(
                          document.getElementById("ok") // eslint-disable-next-line
                        ),
                        ReactDOM.render(
                          <Alert
                            textColor="white"
                            width="32rem"
                            height="4rem"
                            color="#d05555"
                            delay="2000"
                          >
                            <i className="fas fa-exclamation icon" />
                            {"  "}
                            {JSON.stringify(res.response.data)}
                          </Alert>,
                          document.getElementById("ok")
                        ))
                      : res?.response?.status === 200
                      ? (ReactDOM.unmountComponentAtNode(
                          document.getElementById("ok") // eslint-disable-next-line
                        ),
                        ReactDOM.render(
                          <Alert
                            textColor="white"
                            width="26rem"
                            height="4rem"
                            color="#00ae7c"
                            delay="2000"
                          >
                            <i className="far fa-check-circle icon" />
                            Rezervace proběhla úspěšně
                          </Alert>,
                          document.getElementById("ok")
                        ))
                      : (ReactDOM.unmountComponentAtNode(
                          document.getElementById("ok")
                        ),
                        ReactDOM.render(
                          <Alert
                            textColor="white"
                            width="26rem"
                            height="4rem"
                            color="#00ae7c"
                            delay="2000"
                          >
                            <i className="far fa-check-circle icon" />
                            Rezervace proběhla úspěšně
                          </Alert>,
                          document.getElementById("ok")
                        ))
                  )
                  .then(
                    setTimeout(() => {
                      window.location.reload(false);
                    }, 2500)
                  )
              : ReactDOM.unmountComponentAtNode(
                  document.getElementById("ok")
                ).then(
                  ReactDOM.render(
                    <Alert
                      textColor="white"
                      width="26rem"
                      height="4rem"
                      color="#d05555"
                      delay="2000"
                    >
                      <i className="fas fa-exclamation icon" />
                      {"  "}Některá data nejsou správně vyplněna
                    </Alert>,
                    document.getElementById("ok")
                  )
                );
          }}
        ></Button>
      </div>
    </StyledMainGrid>
  );
};

export default Bag;
