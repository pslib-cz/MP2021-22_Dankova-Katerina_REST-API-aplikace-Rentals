import AdminList from "../Admin/AdminList";
import AdminListItem from "../Admin/AdminListItem";
import AdminListItemDate from "../Admin/AdminListItemDate";
import AdminListItemName from "../Admin/AdminListItemName";
import AdminListItemUser from "../Admin/AdminListItemUser";
import { StyledMainGrid } from "../Content/Content";
import ContentMenu, {
  StyledSearchBox,
  StyledSearchBoxWithin,
} from "../Content/ContentMenu";
import Axios from "axios";
import { useEffect, useState } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import useLongPress from "../helpers/UseLongPress";
import ReactDOM from "react-dom";
import { Alert, Badge, Card } from "proomkatest";
import { useHistory } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import { StyledDetail } from "../Pages/Detail";
import { useQuery, useQueryClient } from "react-query";

const AdminLanding = (props) => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);
  const [updater, Update] = useState(1);
  const queryClient = useQueryClient();

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };
  var options = {
    month: "short",
    day: "numeric",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  };

  document.title = "Rentals | Admin";

  const FetchItems = () => {
    return useQuery(
      "admin-when",
      async () => {
        const { data } = await Axios.get("/api/Renting", config);
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

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

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
    console.log(props.setId);
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
    console.log(props.setId);
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
    console.log(props.setId);
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

  const [inputText, setInputText] = useState("");

  if (status === "success") {
    let inputHandler = (e) => {
      //convert input text to lower case
      var lowerCase = e.target.value.toLowerCase();
      setInputText(lowerCase);
    };

    setTimeout(() => {
      ReactDOM.render(
        <StyledSearchBox>
          <StyledSearchBoxWithin>
            <input
              id="searchField"
              type="text"
              placeholder="Jméno uživatele..."
              onChange={inputHandler}
            />
            <span>
              <i className="fas fa-search"></i>
            </span>
          </StyledSearchBoxWithin>
        </StyledSearchBox>,
        document.getElementById("searchme")
      );
    }, 1000);

    return (
      <StyledMainGrid>
        <ContentMenu></ContentMenu>
        <AdminList isSmall>
          {storedFiles
            .filter(
              (obj) =>
                obj.owner?.fullName
                  .toLowerCase()
                  .indexOf(inputText.toLowerCase()) >= 0
            )
            .map((i, index) => {
              return (
                <AdminListItem key={index}>
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
                      <Button setId={i.id} Update={Update} updater={updater} />
                      <Button2 setId={i.id} Update={Update} updater={updater} />
                    </div>
                  ) : i.state === 1 ? (
                    <div className="center-me1">
                      <Button3 setId={i.id} Update={Update} updater={updater} />
                    </div>
                  ) : (
                    <div className="center-me1">
                      <ButtonBlank />
                    </div>
                  )}
                </AdminListItem>
              );
            })}
        </AdminList>
      </StyledMainGrid>
    );
  }

  if (status === "loading") {
    return (
      <StyledDetail id="spinner">
        <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
      </StyledDetail>
    );
  }
  return (
    <Card
      color="#d0af5529"
      width="100%"
      height="6rem"
      className="proomka-card empty"
    >
      <Badge
        top="1rem"
        right="1rem"
        colorHover="white"
        color="#ffc21c"
        textColor="white"
        textColorHover="#ffc21c"
      >
        <i className="fas fa-exclamation"></i>
      </Badge>
      Při načítání položek došlo k chybě
    </Card>
  );
};

export default AdminLanding;
