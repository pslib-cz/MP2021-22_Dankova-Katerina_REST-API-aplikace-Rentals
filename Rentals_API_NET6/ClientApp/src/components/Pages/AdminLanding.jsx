import AdminList from "../Admin/AdminList";
import AdminListItem from "../Admin/AdminListItem";
import AdminListItemDate from "../Admin/AdminListItemDate";
import AdminListItemName from "../Admin/AdminListItemName";
import AdminListItemUser from "../Admin/AdminListItemUser";
import { StyledcategoryWrapper, StyledMainGrid } from "../Content/Content";
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
import { useQuery } from "react-query";
import styled from "styled-components";

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

const AdminLanding = (props) => {
  const [{ accessToken }] = useAppContext(); // eslint-disable-next-line
  const [storedFiles, setStoredFiles] = useState([]);
  const [filteredFiles, setFilteredFiles] = useState([]);
  const [page, setPage] = useState(0);
  const [updater, Update] = useState(1); // eslint-disable-next-line
  const [Categories, setCategories] = useState([
    { id: 0, name: "Rezervované" },
    { id: 1, name: "Vypůjčené" },
    { id: 2, name: "Vrácené" },
    { id: 3, name: "Zamítnuté" },
    { id: 4, name: "Historie" },
  ]);
  const [loaded, setLoaded] = useState(true);

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
        const { data } = await Axios.get("/api/Renting/AllByState", config);
        return data;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };

  const [current, setCurent] = useState();
  const EditSelectedCategories = (newItem) => {
    setCurent(newItem);
  };

  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setStoredFiles(data);
      setLoaded(true);
    }
  }, [status, data, accessToken]);

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

  useEffect(() => {
    Axios.get("/api/Renting/All?page=" + page, config).then((res) =>
      setFilteredFiles(res?.data)
    ); // eslint-disable-next-line
  }, [page]);

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
        <p>Vypůjčit</p>
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
        <p className="red">Zamítnout</p>
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

  const CategoryButton = (props) => {
    return (
      <StyledCategoryButton
        onClick={() => {
          setFilteredFiles([]);
          if (props.i === 4) {
            Axios.get("/api/Renting/All", config).then((res) =>
              setFilteredFiles(res?.data)
            );
          } else {
            setLoaded(false);
            Axios.get("/api/Renting/AllByState?state=" + props.i, config).then(
              (res) => {
                setLoaded(true);
                setFilteredFiles(res?.data);
              }
            );
          }
        }}
        clicked={props.clicked}
      >
        {props.children}
      </StyledCategoryButton>
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
        <div className="shadow-wrap">
          <StyledcategoryWrapper>
            <div className="shadow"></div>
            <div className="shadow"></div>
            {Categories.map((category, i) => (
              <div
                key={i}
                onClick={() => {
                  EditSelectedCategories(i);
                }}
              >
                <CategoryButton
                  key={i}
                  onClick={() => console.log("New call")}
                  clicked={i === current ? true : false}
                  i={i}
                >
                  <p>{category.name}</p>
                </CategoryButton>
              </div>
            ))}
          </StyledcategoryWrapper>
        </div>
        {loaded ? (
          <AdminList isSmall>
            {current === 4 && page > 0 ? (
              <AdminListItem>
                <p
                  style={{ display: "grid", placeItems: "center" }}
                  onClick={() => setPage(page - 1)}
                >
                  {page}
                  <i className="fas fa-chevron-circle-down"></i>
                </p>
              </AdminListItem>
            ) : null}
            {filteredFiles
              .filter(
                (obj) =>
                  obj.owner?.fullName
                    .toLowerCase()
                    .indexOf(inputText.toLowerCase()) >= 0
              )
              .map((i, index) => {
                return (
                  <AdminListItem
                    key={index}
                    kid={i.items}
                    detail
                    edit={i.state === 0 ? true : false}
                    setId={i.id}
                  >
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
            {current === 4 && filteredFiles.length === 10 ? (
              <AdminListItem>
                <p
                  onClick={() => setPage(page + 1)}
                  style={{ display: "grid", placeItems: "center" }}
                >
                  {page + 2}
                  <i className="fas fa-chevron-circle-up"></i>
                </p>
              </AdminListItem>
            ) : null}
          </AdminList>
        ) : (
          <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
        )}
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
