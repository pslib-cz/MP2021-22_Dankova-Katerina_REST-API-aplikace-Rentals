import AdminList from "../Admin/AdminList";
import AdminListItem from "../Admin/AdminListItem";
import { default as BoldName } from "../Admin/AdminListItemName";
import { default as NormalName } from "../Admin/AdminListItemUser";
import { StyledMainGrid } from "../Content/Content";
import ContentMenu from "../Content/ContentMenu";
import Axios from "axios";
import { useEffect, useState } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import { Badge, Card } from "proomkatest";
import { Link, useParams } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import { StyledDetail } from "../Pages/Detail";
import { useQuery } from "react-query";

const Renting = (props) => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);
  const [items, setItems] = useState([]);

  const { id } = useParams();

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
        const { data } = await Axios.get(
          `/api/Renting/History?id=${id}`,
          config
        );
        return data;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

  function actionType(type) {
    switch (type) {
      case 0:
        return " Vytvoření ";
      case 1:
        return " Aktivování ";
      case 2:
        return " Změna ";
      case 3:
        return " Zamítnutí ";
      case 4:
        return " Vrácení ";
      case 5:
        return " Editace ";
      default:
        return "";
    }
  }

  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setStoredFiles(data);
      console.log(data);
    }
  }, [status, data, accessToken]);

  useEffect(() => {
    Axios.get("/api/Renting/" + id, config)
      .then((res) => {
        setItems(res.data.items);
      })
      .catch((err) => console.log(err)); // eslint-disable-next-line
  }, []);

  if (status === "success") {
    return (
      <StyledMainGrid>
        <ContentMenu></ContentMenu>
        <AdminList isSmall>
          <AdminListItem>
            <BoldName
              name={
                "Výpůjčka #" +
                id +
                " " +
                items.map((item) => {
                  return item.name + ", ";
                })
              }
            ></BoldName>
            <Link to={"/return/" + id}>
              <button className="upgrade">
                <p>Vrátit předměty</p>
              </button>
            </Link>
          </AdminListItem>
          <AdminListItem>
            <BoldName name={"Akce"}></BoldName>
            <BoldName name={"Kdy"}></BoldName>
            <BoldName name={"Kdo"}></BoldName>
          </AdminListItem>
          {storedFiles &&
            storedFiles.map((i, index) => {
              return (
                <AdminListItem key={index}>
                  <NormalName
                    name={
                      "#" +
                      index +
                      actionType(i.action) +
                      i?.returnedItems?.map((i, index) => {
                        return i.name;
                      })
                    }
                  ></NormalName>
                  <NormalName
                    name={i.changedTime ? handleTime(i.changedTime) : null}
                  ></NormalName>
                  <NormalName name={i.user?.fullName}></NormalName>
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

export default Renting;
