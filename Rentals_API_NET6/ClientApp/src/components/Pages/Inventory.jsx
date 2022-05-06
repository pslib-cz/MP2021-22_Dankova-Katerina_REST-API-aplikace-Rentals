import Axios from "axios";
import { Alert, Badge, Card } from "proomkatest";
import { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";
import AdminListItem from "../Admin/AdminListItem";
import AdminListItemDate from "../Admin/AdminListItemDate";
import AdminListItemName from "../Admin/AdminListItemName";
import AdminListItemUser from "../Admin/AdminListItemUser";
import { StyledContentGrid } from "../Content/Content";
import CardImage from "../ContentCard/CardImage";
import useLongPress from "../helpers/UseLongPress";
import ReactDOM from "react-dom";
import { StyledDetail } from "./Detail";

const Modal = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  z-index: 6;
  width: 100vw;
  height: 100vh;
  background-color: #000000c3;
  backdrop-filter: blur(10px);
  transition: 250ms;
  display: grid;
  place-items: center;
  cursor: pointer;
`;
const StyledModal = styled.div`
  .modal-card {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    margin: auto;
    z-index: 7;
    cursor: default;

    width: 80vw;
    height: 80vh;

    display: grid;
    place-items: start;

    text-align: center;

    overflow: scroll;

    & > div {
      width: 100%;

      & > h3 {
        margin-bottom: 3rem;
      }
    }
  }
`;

const Inventory = (props) => {
  let history = useHistory();
  const [{ accessToken }] = useAppContext();
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState({});
  const [data, setData] = useState([]);
  const [updater, Update] = useState(1); // eslint-disable-next-line

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }
  var options = {
    month: "short",
    day: "numeric",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  };

  useEffect(() => {
    const config = {
      headers: {
        Authorization: "Bearer " + accessToken,
      },
    };

    const fetchUser = async () => {
      const { data } = await Axios.get("/api/User/Inventory/" + id, config);
      setUser(data);
      setLoading(false);
    };
    fetchUser().then((document.title = `Rentals | Inventář ${user.name}`));

    const fetchData = async () => {
      const { data } = await Axios.get("/api/Renting/All/?id=" + id, config);
      setData(data);
    };
    fetchData();

    // eslint-disable-next-line
  }, [id, accessToken]);

  const Button = (props) => {
    let history = useHistory();
    const onLongPress = () => {
      navigator.vibrate(65);
      Axios({
        method: "put",
        url: "api/Renting/Activate/" + props.setId,
        headers: { Authorization: "Bearer " + accessToken },
      }).then(history.push("/admin/renting/" + props.setId));
      props.Update(updater + 1);
    };

    const open = () => {
      navigator.vibrate(65);
      Axios({
        method: "put",
        url: "api/Renting/Activate/" + props.setId,
        headers: { Authorization: "Bearer " + accessToken },
      }).then(history.push("/admin/renting/" + props.setId));
      props.Update(updater + 1);
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p>Aktivovat</p>
      </button>
    );
  };

  const Button2 = (props) => {
    let history = useHistory();
    const onLongPress = () => {
      navigator.vibrate(65);
      Axios({
        method: "delete",
        url: "api/Renting/" + props.setId,
        headers: { Authorization: "Bearer " + accessToken },
      }).then(history.push("/admin/renting/" + props.setId));
      props.Update(updater + 1);

      ReactDOM.render(
        <Alert
          textColor="white"
          width="16rem"
          height="4rem"
          color="#d05555"
          delay="2000"
        >
          <i className="far fa-check-circle icon" /> Výpůjčka zrušena
        </Alert>,

        document.getElementById("ok")
      );
    };

    const open = () => {
      navigator.vibrate(65);
      Axios({
        method: "delete",
        url: "api/Renting/" + props.setId,
        headers: { Authorization: "Bearer " + accessToken },
      }).then(history.push("/admin/renting/" + props.setId));
      props.Update(updater + 1);

      ReactDOM.render(
        <Alert
          textColor="white"
          width="16rem"
          height="4rem"
          color="#d05555"
          delay="2000"
        >
          <i className="far fa-check-circle icon" /> Výpůjčka zrušena
        </Alert>,

        document.getElementById("ok")
      );
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p className="red">Zrušit</p>
      </button>
    );
  };

  const Button3 = (props) => {
    let history = useHistory();

    const onLongPress = () => {
      navigator.vibrate(65);
      props.Update(updater + 1);
      return history.push("/return/" + props.setId);
    };

    const open = () => {
      return history.push("/return/" + props.setId);
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p className="green">Vrátit</p>
      </button>
    );
  };

  const ButtonBlank = (props) => {
    return (
      <button className="blank">
        <p className="green">Bez akce</p>
      </button>
    );
  };

  return (
    <StyledModal>
      <Modal onClick={() => history.push("/admin/users")}></Modal>
      {!loading ? (
        <Card className="modal-card">
          <div>
            <h3>Předměty</h3>

            <StyledContentGrid>
              {user.length ? (
                <>
                  {user.map((i, index) => {
                    return (
                      <>
                        <Card key={i}>
                          <CardImage src={i.id}></CardImage>
                          <p className="card-header">{i.name}</p>
                          <p className="card-desc">{i.description}</p>
                          <Badge
                            color="#007784"
                            colorHover="#009fb1"
                            textColor="white"
                          >
                            <i className="fas fa-pen"></i>
                          </Badge>
                        </Card>
                      </>
                    );
                  })}
                </>
              ) : (
                <h3>Žádné předměty</h3>
              )}
            </StyledContentGrid>
          </div>
          <div>
            <h3>Výpůjčky</h3>
            {data.length ? (
              <>
                {data.map((i, index) => {
                  return (
                    <AdminListItem key={index} kid={i.items} detail>
                      <AdminListItemName
                        id={i.id}
                        items={i.items}
                        name={"Výpůjčka #" + i.id}
                        note={i.note ? i.note : "Bez poznámky"}
                      ></AdminListItemName>
                      <AdminListItemUser
                        name={i.owner?.fullName}
                      ></AdminListItemUser>
                      <AdminListItemDate
                        from={handleTime(i.start)}
                        to={handleTime(i.end)}
                        stateType="soon"
                        state={i.state}
                      ></AdminListItemDate>
                      {i.state === 0 ? (
                        <div className="center-me">
                          <Button
                            setId={i.id}
                            Update={Update}
                            updater={updater}
                          />
                          <Button2
                            setId={i.id}
                            Update={Update}
                            updater={updater}
                          />
                        </div>
                      ) : i.state === 1 ? (
                        <div className="center-me1">
                          <Button3
                            setId={i.id}
                            Update={Update}
                            updater={updater}
                          />
                        </div>
                      ) : (
                        <div className="center-me1">
                          <ButtonBlank />
                        </div>
                      )}
                    </AdminListItem>
                  );
                })}
              </>
            ) : (
              <h3>Žádné výpůjčky</h3>
            )}
          </div>
        </Card>
      ) : (
        <Card className="modal-card">
          <StyledDetail id="spinner">
            <ImpulseSpinner
              className="spinner"
              frontColor="#007784"
              size="64"
            />
          </StyledDetail>
        </Card>
      )}
    </StyledModal>
  );
};

export default Inventory;
